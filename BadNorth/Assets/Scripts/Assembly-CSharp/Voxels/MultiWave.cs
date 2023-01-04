using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voxels.SetRules;

namespace Voxels
{
	// Token: 0x02000641 RID: 1601
	public class MultiWave
	{
		// Token: 0x060028C7 RID: 10439 RVA: 0x00089044 File Offset: 0x00087444
		private MultiWave()
		{
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000890DC File Offset: 0x000874DC
		public MultiWave(string name, Vector3Int size, int seed, int seed2, float minimumBeach, int coinTarget, List<string> tilesets)
		{
			this.name = name;
			this.seed = seed;
			this.seed2 = seed2;
			this.size = size;
			this.minimumBeach = minimumBeach;
			this.coinTarget = coinTarget;
			this.tilesetKeys = tilesets;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x000891A7 File Offset: 0x000875A7
		private PlacementManager placementManager
		{
			get
			{
				return Singleton<PlacementManager>.instance;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x000891AE File Offset: 0x000875AE
		public Vector3 moduleOffset
		{
			get
			{
				return new Vector3((float)(-(float)(this.size.x - 1)) / 2f, 0f, (float)(-(float)(this.size.z - 1)) / 2f);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x000891E4 File Offset: 0x000875E4
		public Matrix4x4 moduleMatrix
		{
			get
			{
				return this.moduleOffset.GetMoveMatrix();
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000891F1 File Offset: 0x000875F1
		public int remainingDominos
		{
			get
			{
				return this.allDominos.Count - this.removedDominos - this.placedDominos;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x0008920C File Offset: 0x0008760C
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x00089214 File Offset: 0x00087614
		public MultiWave.Phase phase { get; private set; }

		// Token: 0x060028CF RID: 10447 RVA: 0x00089220 File Offset: 0x00087620
		public void Clear()
		{
			for (int i = 0; i < this.slots.Length; i++)
			{
				this.slots[i].Clear();
				this.slots[i] = null;
			}
			for (int j = 0; j < this.cornerSlots.Length; j++)
			{
				this.cornerSlots[j].Clear();
				this.cornerSlots[j] = null;
			}
			this.openSlots.Clear();
			this.guessQueue.Clear();
			foreach (Domino domino in this.allDominos)
			{
				domino.Clear();
			}
			this.allDominos.Clear();
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000892FC File Offset: 0x000876FC
		public Slot GetSlot(Vector3Int pos)
		{
			return this.slots[ExtraMath.CoordinateToIndex(pos, this.size)];
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x00089311 File Offset: 0x00087711
		public CornerSlot GetCorner(Vector3 pos)
		{
			return this.cornerSlots[ExtraMath.CoordinateToIndex(pos, this.size + Vector3Int.one)];
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x00089338 File Offset: 0x00087738
		private void AddPlacement(Wrapper placementWrapper, Vector3Int offset, ref List<Domino> dominoList)
		{
			Placement placement = placementWrapper.placement;
			Vector3 point = placement.bounds.min + offset;
			Vector3 point2 = placement.bounds.max + offset;
			if (!this.bounds.Contains(point) || !this.bounds.Contains(point2))
			{
				return;
			}
			for (int i = 0; i < placementWrapper.allowPlacement.Count; i++)
			{
				if (!placementWrapper.allowPlacement[i].AllowPlacement(offset, placementWrapper.placement, this))
				{
					return;
				}
			}
			for (int j = 0; j < placement.claims.Count; j++)
			{
				Claim claim = placement.claims[j];
				Vector3Int a = claim.pos + offset;
				for (int k = 0; k < Constants.corners.Length; k++)
				{
					CornerSlot.Mode mode = this.GetCorner(a + Constants.cornersInt[k]).mode;
					if (mode != CornerSlot.Mode.any)
					{
						if (claim.cornersInside[k] != (mode == CornerSlot.Mode.inside))
						{
							return;
						}
					}
				}
				if (a.y == 0)
				{
					if (claim.edges[5].edges.Count > 0)
					{
						return;
					}
					for (int l = 0; l < 5; l++)
					{
						Vector3Int b = Constants.directions[l];
						Vector3Int v = a + b;
						if (!this.bounds.Contains(v))
						{
							if (!this.placementManager.lowEdge.Fits(claim.keys[l], l))
							{
								return;
							}
						}
					}
				}
				if (a.y > 0)
				{
					for (int m = 0; m < 6; m++)
					{
						Vector3Int v2 = a + Constants.directions[m];
						if (!this.bounds.Contains(v2))
						{
							if (claim.mode[m] != Claim.Mode.Outside)
							{
								return;
							}
						}
					}
				}
			}
			Domino domino = new Domino(placementWrapper, offset);
			for (int n = 0; n < placement.claims.Count; n++)
			{
				Claim claim2 = placement.claims[n];
				this.GetSlot(claim2.pos + offset).AddDomino(domino, claim2);
				for (int num = 0; num < Constants.corners.Length; num++)
				{
					CornerSlot corner = this.GetCorner(claim2.pos + offset + Constants.cornersInt[num]);
					corner.AddDomino(domino, num, claim2.cornersInside[num]);
				}
			}
			foreach (IOnDominoCreated onDominoCreated in placementWrapper.onDominoAdded)
			{
				onDominoCreated.OnDominoAdded(domino, this);
			}
			dominoList.Add(domino);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x00089698 File Offset: 0x00087A98
		public IEnumerable Setup()
		{
			this.phase = MultiWave.Phase.Setup;
			this.openBounds = default(Bounds);
			this.navigableBounds = default(Bounds);
			Vector3Int marcherSize = this.size - Vector3Int.one * 2;
			marcherSize.y = 0;
			this.marcher = new StraightMarcher(marcherSize);
			this.marcher.Bend(-Mathf.Tan(0.5235988f));
			this.slots = new Slot[this.size.x * this.size.y * this.size.z];
			this.bounds = default(Bounds);
			this.bounds.Encapsulate(this.size - Vector3.one);
			this.bounds.extents = this.bounds.extents + Vector3.one / 2f;
			this.cornerSlots = new CornerSlot[(this.size.x + 1) * (this.size.y + 1) * (this.size.z + 1)];
			this.resolvedSlotsPerHeight = new int[this.size.y];
			this.savedResolvedSlotsPerHeight = new int[this.size.y];
			this.goldPerHeight = new int[this.size.y];
			for (int i = 0; i < this.cornerSlots.Length; i++)
			{
				Vector3Int pos = ExtraMath.IndexToCoordinate(i, this.size + Vector3Int.one);
				CornerSlot cornerSlot = new CornerSlot(pos, this.marcher.rays.Count);
				this.cornerSlots[i] = cornerSlot;
				if (pos.y == 0)
				{
					cornerSlot.mode = CornerSlot.Mode.any;
				}
				else if (!this.bounds.Contains(pos - Vector3.one / 2f))
				{
					cornerSlot.mode = CornerSlot.Mode.outside;
				}
				else
				{
					cornerSlot.mode = CornerSlot.Mode.any;
				}
				yield return null;
			}
			for (int j = 0; j < this.slots.Length; j++)
			{
				Slot slot = new Slot(ExtraMath.IndexToCoordinate(j, this.size), this.placementManager.maxKeyCount);
				this.slots[j] = slot;
				yield return null;
			}
			ModuleSet[] allSets = this.placementManager.moduleSets;
			List<ModuleSet> excludedSets = (from x in allSets
			where !x.enabledByDefault && !this.tilesetKeys.Contains(x.cachedName)
			select x).ToList<ModuleSet>();
			float bestHouseSetScore = float.MinValue;
			foreach (ModuleSet moduleSet in allSets)
			{
				if (!excludedSets.Contains(moduleSet))
				{
					HouseSet houseSet = null;
					foreach (SetRule setRule in moduleSet.rules)
					{
						if (setRule is HouseSet)
						{
							houseSet = (setRule as HouseSet);
							break;
						}
					}
					if (houseSet)
					{
						float houseSetScore = houseSet.houseSetScore;
						if (houseSetScore >= bestHouseSetScore)
						{
							bestHouseSetScore = houseSetScore;
							this.bestHouseSet = moduleSet;
						}
					}
				}
			}
			List<Wrapper> wrappers = new List<Wrapper>((int)((float)this.placementManager.all.Count * 0.7f));
			for (int num3 = 0; num3 < this.placementManager.all.Count; num3++)
			{
				Placement placement = this.placementManager.all[num3];
				if (!excludedSets.Any((ModuleSet x) => x.ContainsModule(placement.firstModule)))
				{
					Wrapper item = new Wrapper(placement);
					wrappers.Add(item);
				}
			}
			List<Wrapper> setWrappers = new List<Wrapper>();
			for (int num4 = 0; num4 < this.placementManager.moduleSets.Length; num4++)
			{
				ModuleSet moduleSet2 = this.placementManager.moduleSets[num4];
				if (!excludedSets.Contains(moduleSet2))
				{
					for (int num5 = 0; num5 < wrappers.Count; num5++)
					{
						Wrapper wrapper2 = wrappers[num5];
						if (moduleSet2.ContainsModule(wrapper2.placement.firstModule))
						{
							setWrappers.Add(wrapper2);
						}
					}
					for (int num6 = 0; num6 < moduleSet2.rules.Length; num6++)
					{
						moduleSet2.rules[num6].GetRules(this, setWrappers);
					}
					setWrappers.Clear();
				}
			}
			List<Domino> allInitalDominos = new List<Domino>(this.slots.Length * wrappers.Count);
			this.allDominos = new List<Domino>(this.slots.Length * wrappers.Count);
			this.guessQueue = new List<Domino>(this.allDominos.Count);
			this.openSlots = new List<Slot>(this.slots.Length);
			for (int k = 0; k < wrappers.Count; k++)
			{
				Wrapper wrapper = wrappers[k];
				for (int l = 0; l < this.slots.Length; l++)
				{
					Slot slot2 = this.slots[l];
					this.AddPlacement(wrapper, slot2.pos, ref allInitalDominos);
					yield return null;
				}
			}
			this.phase = MultiWave.Phase.InitialContradictions;
			for (int m = 0; m < allInitalDominos.Count; m++)
			{
				Domino domino = allInitalDominos[m];
				if (!this.DominoFits(domino))
				{
					this.RemoveDomino(domino);
					yield return null;
				}
			}
			for (int n = 0; n < allInitalDominos.Count; n++)
			{
				Domino domino2 = allInitalDominos[n];
				if (domino2.state == Domino.State.idle)
				{
					this.allDominos.Add(domino2);
				}
				yield return null;
			}
			this.removedDominos = 0;
			this.placedDominos = 0;
			this.phase = MultiWave.Phase.PlacingObvious;
			yield return null;
			for (int i2 = 0; i2 < this.slots.Length; i2++)
			{
				Slot slot3 = this.slots[i2];
				if (slot3.dominos.Count == 1 && slot3.dominos[0].state != Domino.State.done)
				{
					this.Place(slot3.dominos[0]);
				}
				yield return null;
			}
			yield return null;
			this.phase = MultiWave.Phase.SavingState;
			this.SaveState();
			yield break;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000896BC File Offset: 0x00087ABC
		private void SaveState()
		{
			for (int i = 0; i < this.slots.Length; i++)
			{
				this.slots[i].SaveState();
			}
			for (int j = 0; j < this.allDominos.Count; j++)
			{
				this.allDominos[j].SaveState();
			}
			for (int k = 0; k < this.cornerSlots.Length; k++)
			{
				this.cornerSlots[k].SaveState();
			}
			for (int l = 0; l < this.size.y; l++)
			{
				this.savedResolvedSlotsPerHeight[l] = this.resolvedSlotsPerHeight[l];
			}
			this.removedDominosaved = this.removedDominos;
			this.placedDominosSaved = this.placedDominos;
			this.onSave(this);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x00089794 File Offset: 0x00087B94
		private bool ForceVisibility(CornerSlot cornerSlot)
		{
			for (int i = 0; i < this.marcher.rays.Count; i++)
			{
				if (!cornerSlot.occludedAngles[i])
				{
					for (int j = 1; j < this.marcher.rays[i].pos.Length; j++)
					{
						Vector3Int v = cornerSlot.pos - this.marcher.rays[i].pos[j];
						if (!this.bounds.Contains(v))
						{
							break;
						}
						CornerSlot corner = this.GetCorner(v);
						if (corner.state == CornerSlot.State.Unclear)
						{
							if (corner.state == CornerSlot.State.Inside)
							{
								break;
							}
							for (int k = 0; k < 8; k++)
							{
								List<Domino> list = corner.dominos[k, 1];
								int l = 0;
								while (l < list.Count)
								{
									Domino domino = list[l];
									if (domino.state == Domino.State.idle)
									{
										if (!this.RemoveDomino(domino))
										{
											return false;
										}
									}
									else
									{
										l++;
									}
								}
							}
							Color.white.a = (float)j / (float)this.marcher.rays[i].pos.Length;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0008990C File Offset: 0x00087D0C
		private bool Occlude(CornerSlot cornerSlot)
		{
			for (int i = 0; i < this.marcher.rays.Count; i++)
			{
				for (int j = 1; j < this.marcher.rays[i].pos.Length; j++)
				{
					Vector3Int vector3Int = cornerSlot.pos + this.marcher.rays[i].pos[j];
					if (!this.bounds.Contains(vector3Int))
					{
						break;
					}
					if (vector3Int.y < 1)
					{
						break;
					}
					CornerSlot corner = this.GetCorner(vector3Int);
					if (corner.Occlude(i) && corner.visiblity < this.visibilityThreshold && this.GetCorner(vector3Int + Vector3Int.down).state == CornerSlot.State.Inside)
					{
						if (corner.state == CornerSlot.State.Outside)
						{
							if (!this.ForceVisibility(corner))
							{
								return false;
							}
						}
						else if (corner.state == CornerSlot.State.Unclear)
						{
							for (int k = 0; k < 8; k++)
							{
								List<Domino> list = corner.dominos[k, 0];
								int l = 0;
								while (l < list.Count)
								{
									Domino domino = list[l];
									if (domino.state == Domino.State.idle)
									{
										if (!this.RemoveDomino(domino))
										{
											return false;
										}
									}
									else
									{
										l++;
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x00089AA0 File Offset: 0x00087EA0
		public bool RemoveDomino(Domino domino)
		{
			if (domino.state == Domino.State.idle)
			{
				Placement placement = domino.placement;
				Wrapper placementWrapper = domino.placementWrapper;
				domino.state = Domino.State.removing;
				this.removedDominos++;
				for (int i = 0; i < placement.claims.Count; i++)
				{
					Claim claim = placement.claims[i];
					Slot slot = this.GetSlot(domino.offset + claim.pos);
					if (slot.dominos.Remove(domino))
					{
						for (int j = 0; j < 6; j++)
						{
							int num = domino.GetClaim(slot.pos).keys[j];
							List<Domino> list = slot.keyCount[j, num];
							if (list.Remove(domino))
							{
								if (list.Count == 1)
								{
									Domino domino2 = list[0];
									Placement placement2 = domino2.placement;
									for (int k = 0; k < placementWrapper.onLast.Count; k++)
									{
										if (!placementWrapper.onLast[k].OnLast(domino2, this, j, claim, slot))
										{
											return false;
										}
									}
								}
								if (list.Count == 0)
								{
									Vector3Int vector3Int = slot.pos + Constants.directions[j];
									if (this.bounds.Contains(vector3Int))
									{
										Slot slot2 = this.GetSlot(vector3Int);
										List<Domino> list2 = slot2.keyCount[Constants.opposites[j], num];
										int l = 0;
										while (l < list2.Count)
										{
											Domino domino3 = list2[l];
											if (domino3.state == Domino.State.idle)
											{
												if (!this.RemoveDomino(domino3))
												{
													return false;
												}
											}
											else
											{
												l++;
											}
										}
									}
								}
							}
						}
						for (int m = 0; m < 8; m++)
						{
							CornerSlot corner = this.GetCorner(claim.pos + domino.offset + Constants.cornersInt[m]);
							int num2 = (!claim.cornersInside[m]) ? 0 : 1;
							List<Domino> list3 = corner.dominos[m, num2];
							if (list3.Remove(domino))
							{
								corner.stateCount[num2]--;
								if (list3.Count == 0)
								{
									for (int n = 0; n < 8; n++)
									{
										List<Domino> list4 = corner.dominos[n, num2];
										int num3 = 0;
										while (num3 < list4.Count)
										{
											Domino domino4 = list4[num3];
											if (domino4.state == Domino.State.idle)
											{
												if (!this.RemoveDomino(domino4))
												{
													return false;
												}
											}
											else
											{
												num3++;
											}
										}
									}
									if (corner.state == CornerSlot.State.Inside && !this.Occlude(corner))
									{
										return false;
									}
								}
							}
						}
						if (slot.dominos.Count == 1 && slot.dominos[0].state == Domino.State.idle)
						{
							if (!this.Place(slot.dominos[0]))
							{
								return false;
							}
						}
						else if (slot.dominos.Count == 0)
						{
							this.breakReason = "RemoveDomino()";
							this.broken = true;
							return false;
						}
					}
				}
				for (int num4 = 0; num4 < domino.placementWrapper.onRemoved.Count; num4++)
				{
					if (!domino.placementWrapper.onRemoved[num4].OnRemoved(domino, this))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x00089E6C File Offset: 0x0008826C
		public bool Place(Domino domino)
		{
			if (domino.state == Domino.State.idle)
			{
				domino.state = Domino.State.done;
				Placement placement = domino.placement;
				this.placedDominos++;
				this.beach += domino.placement.firstModule.beachLength;
				this.coinCount += domino.placement.firstModule.goldCount;
				this.goldPerHeight[domino.offset.y] += domino.placement.firstModule.goldCount;
				bool flag = false;
				if (domino.placement.navigable && this.navigating)
				{
					for (int i = 0; i < placement.claims.Count; i++)
					{
						Claim claim = placement.claims[i];
						if (claim.anyNavigable)
						{
							Slot slot = this.GetSlot(claim.pos + domino.offset);
							if (slot.navigability == Slot.Navigability.Open)
							{
								flag = true;
								break;
							}
							if (!flag)
							{
								for (int j = 0; j < 6; j++)
								{
									if (claim.navigable[j] && claim.mode[j] == Claim.Mode.Internal)
									{
										Slot slot2 = this.GetSlot(slot.pos + Constants.directions[j]);
										if (slot2.navigability != Slot.Navigability.Unclear)
										{
											flag = true;
											break;
										}
									}
								}
							}
							if (flag)
							{
								break;
							}
						}
					}
				}
				if (this.navigating && domino.placement.navigable)
				{
					this.navigableBounds.Encapsulate(domino.GetNavigableBounds());
					this.openBounds.Encapsulate(domino.GetNavigableBounds());
				}
				for (int k = 0; k < placement.claims.Count; k++)
				{
					Claim claim2 = placement.claims[k];
					Vector3Int pos = claim2.pos + domino.offset;
					Slot slot3 = this.GetSlot(pos);
					slot3.normal = claim2.normal;
					if (slot3.navigability == Slot.Navigability.Open && this.navigating)
					{
						slot3.navigability = Slot.Navigability.Navigable;
						this.openSlots.Remove(slot3);
						flag = true;
					}
					if (flag && claim2.anyNavigable && this.navigating)
					{
						slot3.navigability = Slot.Navigability.Navigable;
						for (int l = 0; l < 6; l++)
						{
							if (claim2.navigable[l] && claim2.mode[l] != Claim.Mode.Internal)
							{
								Slot slot4 = this.GetSlot(slot3.pos + Constants.directions[l]);
								if (!slot4.done && slot4.navigability == Slot.Navigability.Unclear)
								{
									slot4.navigability = Slot.Navigability.Open;
									this.openSlots.Add(slot4);
								}
							}
						}
					}
					if (slot3.domino != null)
					{
						this.breakReason = "Place() slot.domino != null";
						this.broken = true;
						return false;
					}
					slot3.domino = domino;
					this.resolvedSlotsPerHeight[pos.y]++;
					List<Domino> dominos = slot3.dominos;
					int m = 0;
					while (m < slot3.dominos.Count)
					{
						Domino domino2 = dominos[m];
						if (domino2 == domino || domino2.state == Domino.State.removing)
						{
							m++;
						}
						else
						{
							if (domino2.state == Domino.State.done)
							{
								this.breakReason = "otherDomino.state == Domino.State.done";
								this.broken = true;
								return false;
							}
							if (!this.RemoveDomino(domino2))
							{
								return false;
							}
						}
					}
				}
				if (!this.DominoFits(domino))
				{
					this.breakReason = "Place() !DominoFits(domino)";
					this.broken = true;
					return false;
				}
				for (int n = 0; n < domino.placementWrapper.onPlaced.Count; n++)
				{
					if (!domino.placementWrapper.onPlaced[n].OnPlaced(domino, this))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0008A2A4 File Offset: 0x000886A4
		private bool DominoFits(Domino domino)
		{
			Placement placement = domino.placement;
			for (int i = 0; i < placement.claims.Count; i++)
			{
				Claim claim = placement.claims[i];
				Vector3Int a = claim.pos + domino.offset;
				for (int j = 0; j < Constants.directions.Length; j++)
				{
					Vector3Int vector3Int = a + Constants.directions[j];
					if (this.bounds.Contains(vector3Int))
					{
						Slot slot = this.GetSlot(vector3Int);
						int num = claim.keys[j];
						if (slot.keyCount[Constants.opposites[j], num].Count == 0)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x0008A37C File Offset: 0x0008877C
		private void InitializeSeed()
		{
			System.Random random = new System.Random(this.seed + this.seed2);
			for (int i = 0; i < this.allDominos.Count; i++)
			{
				Domino domino = this.allDominos[i];
				domino.fraction = (float)random.Next() / 2.1474836E+09f;
				domino.score = domino.defaultScore * ((float)random.Next() / 2.1474836E+09f);
			}
			this.guessQueue.Clear();
			this.guessQueue.AddRange(this.allDominos);
			this.guessQueue.Sort();
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0008A41C File Offset: 0x0008881C
		private void Reset()
		{
			this.phase = MultiWave.Phase.Reset;
			this.broke++;
			for (int i = 0; i < this.slots.Length; i++)
			{
				this.slots[i].Reset();
			}
			for (int j = 0; j < this.allDominos.Count; j++)
			{
				this.allDominos[j].Reset();
			}
			for (int k = 0; k < this.cornerSlots.Length; k++)
			{
				this.cornerSlots[k].Reset();
			}
			this.removedDominos = this.removedDominosaved;
			this.placedDominos = this.placedDominosSaved;
			this.beach = 0f;
			this.coinCount = 0;
			this.onReset();
			this.openSlots.Clear();
			for (int l = 0; l < this.slots.Length; l++)
			{
				Slot slot = this.slots[l];
				if (slot.dominos.Count == 1 && slot.dominos[0].state != Domino.State.done)
				{
					this.Place(slot.dominos[0]);
				}
			}
			this.seed2++;
			this.broken = false;
			this.hasLevel = false;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0008A574 File Offset: 0x00088974
		private IEnumerable<bool> Resolve()
		{
			this.phase = MultiWave.Phase.Resolve;
			this.guessIndex = 0;
			this.openSlots.Clear();
			this.openBounds = default(Bounds);
			this.navigating = true;
			for (int k = 0; k < this.size.y; k++)
			{
				this.resolvedSlotsPerHeight[k] = this.savedResolvedSlotsPerHeight[k];
				this.goldPerHeight[k] = 0;
			}
			this.InitializeSeed();
			for (int i = 0; i < this.guessQueue.Count; i++)
			{
				Domino domino = this.guessQueue[i];
				if (domino.placement.house)
				{
					if (domino.state == Domino.State.idle)
					{
						if (domino.placement.sets.Contains(this.bestHouseSet))
						{
							for (int l = 0; l < domino.placement.claims.Count; l++)
							{
								Claim claim = domino.placement.claims[l];
								if (claim.anyNavigable)
								{
									Slot slot = this.GetSlot(domino.offset + claim.pos);
									this.openSlots.Add(slot);
									slot.navigability = Slot.Navigability.Open;
									if (this.openSlots.Count == 1)
									{
										this.openBounds = new Bounds(slot.pos, Vector3.zero);
										this.navigableBounds = new Bounds(slot.pos, Vector3.zero);
									}
								}
							}
							yield return this.Place(domino);
							break;
						}
					}
				}
			}
			float goldTargetPerSlot = (float)this.coinTarget / (float)this.slots.Length;
			float goldTargetPerHeight = (float)(this.coinTarget / this.size.y);
			while (this.openSlots.Count > 0)
			{
				Domino bestDomino = this.slots[0].dominos[0];
				float highestScore = float.MinValue;
				float resolvedSlots = 0f;
				for (int m = 0; m < this.resolvedSlotsPerHeight.Length; m++)
				{
					resolvedSlots += (float)this.resolvedSlotsPerHeight[m];
				}
				for (int n = 0; n < this.openSlots.Count; n++)
				{
					Slot slot2 = this.openSlots[n];
					int num = this.goldPerHeight[slot2.pos.y];
					float num2 = (float)this.resolvedSlotsPerHeight[slot2.pos.y];
					bool flag = this.coinCount < this.coinTarget && ((float)this.coinCount / resolvedSlots < goldTargetPerSlot * 1.2f || (float)num / num2 < goldTargetPerSlot * 1.5f);
					for (int num3 = 0; num3 < slot2.dominos.Count; num3++)
					{
						Domino domino4 = slot2.dominos[num3];
						if (domino4.state == Domino.State.idle)
						{
							float num4 = domino4.score;
							if (domino4.placement.house)
							{
								if (!flag)
								{
									goto IL_5E4;
								}
								num4 *= 10f;
							}
							if (this.openBounds.size != this.bounds.size && domino4.placement.navigable)
							{
								Bounds bounds = this.navigableBounds;
								Bounds bounds2 = this.openBounds;
								bounds.Encapsulate(domino4.GetNavigableBounds());
								bounds2.Encapsulate(domino4.GetOpenBounds());
								Vector3 vector = bounds.size - this.navigableBounds.size;
								if (vector.y != 0f)
								{
									num4 *= (float)this.size.y * 1.5f;
								}
								if (vector.x != 0f || vector.z != 0f)
								{
									num4 *= 2f;
								}
								vector = bounds2.size - this.openBounds.size;
								if (vector.y != 0f)
								{
									num4 *= (float)this.size.y * 1.5f;
								}
								if (vector.x != 0f || vector.z != 0f)
								{
									num4 *= 2f;
								}
							}
							if (num4 > highestScore)
							{
								bestDomino = domino4;
								highestScore = num4;
							}
						}
						IL_5E4:;
					}
				}
				yield return this.Place(bestDomino);
			}
			if (this.navigableBounds.min.y > 0f)
			{
				this.breakReason = "navigableBounds.min.y > 0";
				this.broken = true;
				yield return false;
			}
			if (this.navigableBounds.size.x + this.navigableBounds.size.z < (this.navigableBounds.size.x + this.navigableBounds.size.z) * 0.7f)
			{
				this.breakReason = "navigable bounds too small";
				this.broken = true;
				yield return false;
			}
			if (this.beach < this.minimumBeach)
			{
				this.breakReason = "beach < minimumBeach";
				this.broken = true;
				yield return false;
			}
			this.navigating = false;
			while (this.remainingDominos > 0)
			{
				Domino domino2 = this.guessQueue[this.guessIndex];
				this.guessIndex++;
				if (domino2.state == Domino.State.idle)
				{
					if (domino2.placement.forcedNavigability)
					{
						yield return this.RemoveDomino(domino2);
					}
					else
					{
						yield return this.Place(domino2);
					}
				}
			}
			for (int j = 0; j < this.guessQueue.Count; j++)
			{
				Domino domino3 = this.guessQueue[j];
				if (domino3.state == Domino.State.done && !this.DominoFits(domino3))
				{
					this.breakReason = "domino.state == Domino.State.done && !DominoFits(domino)";
					this.broken = true;
					yield return false;
				}
			}
			yield return true;
			this.hasLevel = true;
			this.savedWave = new SavedWave(this.size, this.slots.Length);
			for (int num5 = 0; num5 < this.guessQueue.Count; num5++)
			{
				Domino domino5 = this.guessQueue[num5];
				if (domino5.state == Domino.State.done)
				{
					this.savedWave.dominos.Add(new SavedWave.SavedModule
					{
						offset = domino5.offset,
						orientedModule = domino5.GetOrientedModule(),
						placement = domino5.placement
					});
				}
			}
			yield return true;
			yield break;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0008A598 File Offset: 0x00088998
		public IEnumerator<GenInfo> Generate()
		{
			if (this.hasLevel)
			{
				this.Reset();
				this.hasLevel = false;
			}
			yield return new GenInfo("Resolving", GenInfo.Mode.forceInterrupt);
			bool done = false;
			while (!done)
			{
				done = true;
				using (IEnumerator<bool> enumerator = this.Resolve().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current)
						{
							if (this.onBreak != null)
							{
								this.onBreak(this.breakReason);
							}
							this.Reset();
							yield return new GenInfo("Resetting", GenInfo.Mode.interruptable);
							done = false;
							break;
						}
					}
				}
			}
			yield return new GenInfo("Done", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x0008A5B4 File Offset: 0x000889B4
		public void DrawGizmos()
		{
			Gizmos.matrix = this.moduleMatrix;
			Color white = Color.white;
			if (this.openSlots != null && this.openSlots.Count > 0)
			{
				Gizmos.color = Color.green.SetA(0.2f);
				Gizmos.DrawWireCube(this.openBounds.center, this.openBounds.size + Vector3.one);
			}
			foreach (Slot slot in this.slots)
			{
				if (slot.navigability != Slot.Navigability.Unclear)
				{
					Gizmos.color = ((slot.navigability != Slot.Navigability.Open) ? Color.green : Color.red) * 2f;
					Gizmos.DrawSphere(slot.pos, 0.1f);
				}
			}
		}

		// Token: 0x04001A62 RID: 6754
		public string name;

		// Token: 0x04001A63 RID: 6755
		public Vector3Int size;

		// Token: 0x04001A64 RID: 6756
		public float minimumBeach;

		// Token: 0x04001A65 RID: 6757
		public float beach;

		// Token: 0x04001A66 RID: 6758
		public int seed;

		// Token: 0x04001A67 RID: 6759
		public int seed2;

		// Token: 0x04001A68 RID: 6760
		public Bounds bounds = default(Bounds);

		// Token: 0x04001A69 RID: 6761
		public bool hasLevel;

		// Token: 0x04001A6A RID: 6762
		public List<Domino> allDominos;

		// Token: 0x04001A6B RID: 6763
		public List<Domino> guessQueue;

		// Token: 0x04001A6C RID: 6764
		private bool broken;

		// Token: 0x04001A6D RID: 6765
		private string breakReason;

		// Token: 0x04001A6E RID: 6766
		public Action<string> onBreak = delegate(string A_0)
		{
		};

		// Token: 0x04001A6F RID: 6767
		private StraightMarcher marcher;

		// Token: 0x04001A70 RID: 6768
		public float visibilityThreshold = 0.4f;

		// Token: 0x04001A71 RID: 6769
		[Header("Counts")]
		public int broke;

		// Token: 0x04001A72 RID: 6770
		public Action onReset = delegate()
		{
		};

		// Token: 0x04001A73 RID: 6771
		public Action<MultiWave> onSave = delegate(MultiWave A_0)
		{
		};

		// Token: 0x04001A74 RID: 6772
		public int removedDominos;

		// Token: 0x04001A75 RID: 6773
		public int removedDominosaved;

		// Token: 0x04001A76 RID: 6774
		public int placedDominos;

		// Token: 0x04001A77 RID: 6775
		public int placedDominosSaved;

		// Token: 0x04001A78 RID: 6776
		[Header("Lists")]
		public int queueCount;

		// Token: 0x04001A79 RID: 6777
		public int startSum;

		// Token: 0x04001A7A RID: 6778
		public int currentSum;

		// Token: 0x04001A7B RID: 6779
		public Slot[] slots;

		// Token: 0x04001A7C RID: 6780
		public CornerSlot[] cornerSlots;

		// Token: 0x04001A7D RID: 6781
		public bool navigating;

		// Token: 0x04001A7E RID: 6782
		public List<Slot> openSlots;

		// Token: 0x04001A7F RID: 6783
		private int[] goldPerHeight;

		// Token: 0x04001A80 RID: 6784
		private int[] resolvedSlotsPerHeight;

		// Token: 0x04001A81 RID: 6785
		private int[] savedResolvedSlotsPerHeight;

		// Token: 0x04001A82 RID: 6786
		private Bounds openBounds;

		// Token: 0x04001A83 RID: 6787
		private Bounds navigableBounds;

		// Token: 0x04001A84 RID: 6788
		public SavedWave savedWave;

		// Token: 0x04001A85 RID: 6789
		private List<string> tilesetKeys;

		// Token: 0x04001A87 RID: 6791
		private int coinTarget;

		// Token: 0x04001A88 RID: 6792
		private int coinCount;

		// Token: 0x04001A89 RID: 6793
		public int guessIndex;

		// Token: 0x04001A8A RID: 6794
		public int guessQueueLength;

		// Token: 0x04001A8B RID: 6795
		private ModuleSet bestHouseSet;

		// Token: 0x02000642 RID: 1602
		public enum Phase
		{
			// Token: 0x04001A90 RID: 6800
			Setup,
			// Token: 0x04001A91 RID: 6801
			InitialContradictions,
			// Token: 0x04001A92 RID: 6802
			PlacingObvious,
			// Token: 0x04001A93 RID: 6803
			SavingState,
			// Token: 0x04001A94 RID: 6804
			Reset,
			// Token: 0x04001A95 RID: 6805
			Resolve
		}
	}
}
