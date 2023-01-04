using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voxels.TowerDefense.WorldEnvironment;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x02000758 RID: 1880
	public class ForestDef : MonoBehaviour
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000CA5C8 File Offset: 0x000C89C8
		public TreePool treePool
		{
			get
			{
				if (this._treePool == null)
				{
					this._treePool = new TreePool(base.transform);
				}
				return this._treePool;
			}
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000CA5EC File Offset: 0x000C89EC
		public bool GetAllowed(Year year)
		{
			int month = (int)year.month;
			if (this.minMonth < this.maxMonth)
			{
				return month >= this.minMonth && month <= this.maxMonth;
			}
			return month >= this.minMonth || month <= this.maxMonth;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000CA649 File Offset: 0x000C8A49
		private float GetY(Vector3 wPos, Vector3 size)
		{
			return wPos.y / (size.y - 2f);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000CA660 File Offset: 0x000C8A60
		private float GetRadial(Vector3 wPos, Vector3 refSize)
		{
			Vector2 vector = new Vector2(wPos.x / (refSize.x - 2f), wPos.z / (refSize.z - 2f));
			vector *= 2f;
			vector = Vector2.one - ExtraMath.Abs(vector);
			return 1f - vector.y * vector.x;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000CA6D0 File Offset: 0x000C8AD0
		public float GetBaseRadius()
		{
			return UnityEngine.Random.Range(this.randomRadius.x, this.randomRadius.y);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000CA6F0 File Offset: 0x000C8AF0
		public float GetRadius(NavPos navPos, float noise)
		{
			float result = 1f;
			foreach (ForestDef.Modifier modifier in this.radiusModifiers)
			{
				modifier.ModifyValue(ref result, navPos, noise);
			}
			return result;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000CA730 File Offset: 0x000C8B30
		public float GetDominance(NavPos navPos, float noise)
		{
			float result = this.dominance;
			foreach (ForestDef.Modifier modifier in this.dominanceModifiers)
			{
				modifier.ModifyValue(ref result, navPos, noise);
			}
			return result;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000CA770 File Offset: 0x000C8B70
		public float GetDensity(NavPos navPos, float noise)
		{
			float a = this.baseDensity;
			foreach (ForestDef.Modifier modifier in this.densityModifiers)
			{
				modifier.ModifyValue(ref a, navPos, noise);
			}
			return Mathf.Max(a, 0f);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000CA7B8 File Offset: 0x000C8BB8
		public void RunDynamics(List<Shoot> shoots)
		{
			for (int i = 0; i < shoots.Count; i++)
			{
				Shoot shoot = shoots[i];
				if (this.toWall != 0f)
				{
					shoot.move -= shoot.navPos.GetWallVector().normalized * this.toWall;
				}
				if (this.toCliff != 0f)
				{
					shoot.move -= shoot.navPos.GetCliffVector().normalized * this.toCliff;
				}
				if (this.upHill != 0f)
				{
					shoot.move -= shoot.navPos.GetNormal().GetZeroY().normalized * this.upHill;
				}
				shoot.ComittMove();
				float num = shoot.radius - shoot.navPos.GetWallDistance() - 0.1f;
				if (num > 0f)
				{
					shoot.move += shoot.navPos.GetWallVector().normalized * num;
				}
				shoot.ComittMove();
			}
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000CA908 File Offset: 0x000C8D08
		public static IEnumerator PlantForest(IEnumerable<Vert> verts, Transform container, Island island, IEnumerable<ForestDef> forestDefs)
		{
			ForestDef.Forest[] forests = (from x in forestDefs
			where x
			select new ForestDef.Forest(x, verts)).ToArray<ForestDef.Forest>();
			if (forests.Count<ForestDef.Forest>() == 0)
			{
				yield break;
			}
			List<Shoot> shoots = ListPool<Shoot>.GetList(1024);
			foreach (Vert vert in verts)
			{
				float num = 0f;
				float num2 = 0f;
				float a = 0f;
				NavPos navPos = new NavPos(vert);
				foreach (ForestDef.Forest forest2 in forests)
				{
					float density = forest2.GetDensity(navPos);
					float num3 = forest2.GetDominance(navPos);
					num = Mathf.Max(num, density);
					num2 += num3 * density;
					a = Mathf.Max(a, num3 * density);
				}
				if (num2 > 0f)
				{
					float cellArea = vert.cellArea;
					foreach (ForestDef.Forest forest3 in forests)
					{
						float num4 = forest3.GetDensity(navPos);
						float num5 = forest3.GetDominance(navPos);
						num4 = num5 * num4 / num2 * num;
						forest3.SpawnShoots(vert, num4 * cellArea);
					}
				}
			}
			foreach (ForestDef.Forest forest4 in forests)
			{
				shoots.AddRange(forest4.shoots);
			}
			for (int i = 0; i < 4; i++)
			{
				using (new ScopedProfiler("Push apart", null))
				{
					for (int m = 0; m < shoots.Count; m++)
					{
						Shoot shoot = shoots[m];
						for (int n = m + 1; n < shoots.Count; n++)
						{
							Shoot shoot2 = shoots[n];
							Shoot.PushApart(shoot, shoot2);
						}
					}
				}
				yield return null;
				using (new ScopedProfiler("Push away from cliffs and walls", null))
				{
					foreach (ForestDef.Forest forest5 in forests)
					{
						forest5.ShootDynamics();
					}
				}
				yield return null;
			}
			for (int num7 = 0; num7 < shoots.Count; num7++)
			{
				Shoot shoot3 = shoots[num7];
				shoot3.fraction = Mathf.PerlinNoise((float)num7 / 3.1415927f, 0f);
				shoots[num7] = shoot3;
			}
			foreach (ForestDef.Forest forest in forests)
			{
				IEnumerator e = forest.CreateTrees(container);
				while (e.MoveNext())
				{
					object obj = e.Current;
					yield return obj;
				}
			}
			shoots.ReturnToListPool<Shoot>();
			yield break;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000CA934 File Offset: 0x000C8D34
		private IEnumerator CreateTrees(List<Shoot> shoots, Transform container)
		{
			for (int i = 0; i < shoots.Count; i++)
			{
				Shoot shoot = shoots[i];
				if (shoot.radius > 0f)
				{
					Vector3 pos = shoot.navPos.pos;
					Vector3 normal = shoot.navPos.tri.up;
					if (normal.y >= this.minY)
					{
						Tree srcTree = this.treePool.GetTree(shoots[i]);
						srcTree.PlantTree(shoots[i], container, pos, normal);
						yield return null;
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x040020D1 RID: 8401
		private const int maxCount = 1024;

		// Token: 0x040020D2 RID: 8402
		private const int iterations = 4;

		// Token: 0x040020D3 RID: 8403
		[SerializeField]
		private int minMonth;

		// Token: 0x040020D4 RID: 8404
		[SerializeField]
		private int maxMonth = 12;

		// Token: 0x040020D5 RID: 8405
		[Header("Density")]
		public float baseDensity = 1f;

		// Token: 0x040020D6 RID: 8406
		public bool onPaths = true;

		// Token: 0x040020D7 RID: 8407
		[SerializeField]
		private ForestDef.Modifier[] densityModifiers = new ForestDef.Modifier[0];

		// Token: 0x040020D8 RID: 8408
		[Header("Radius")]
		[SerializeField]
		private Vector2 randomRadius = new Vector2(0.6f, 1.4f);

		// Token: 0x040020D9 RID: 8409
		[SerializeField]
		private ForestDef.Modifier[] radiusModifiers = new ForestDef.Modifier[0];

		// Token: 0x040020DA RID: 8410
		[Header("Dominance")]
		[SerializeField]
		private float dominance = 1f;

		// Token: 0x040020DB RID: 8411
		[SerializeField]
		private ForestDef.Modifier[] dominanceModifiers = new ForestDef.Modifier[0];

		// Token: 0x040020DC RID: 8412
		[Header("Slope")]
		[Space]
		public float minY = 0.8f;

		// Token: 0x040020DD RID: 8413
		[Header("Dynamics")]
		public float toWall;

		// Token: 0x040020DE RID: 8414
		public float toCliff;

		// Token: 0x040020DF RID: 8415
		public float upHill;

		// Token: 0x040020E0 RID: 8416
		private TreePool _treePool;

		// Token: 0x02000759 RID: 1881
		[Serializable]
		private class Modifier
		{
			// Token: 0x06003121 RID: 12577 RVA: 0x000CA970 File Offset: 0x000C8D70
			public void ModifyValue(ref float value, NavPos navPos, float noise)
			{
				float num = 0f;
				switch (this.parameter)
				{
				case ForestDef.Modifier.Parameter.Cliff:
					num = navPos.GetCliffyness();
					break;
				case ForestDef.Modifier.Parameter.Wall:
					num = navPos.GetWalliness();
					break;
				case ForestDef.Modifier.Parameter.WallDistance:
					num = navPos.GetWallDistance();
					break;
				case ForestDef.Modifier.Parameter.CliffDistance:
					num = navPos.GetCliffDistance();
					break;
				case ForestDef.Modifier.Parameter.Coverage:
					num = 1f - navPos.island.voxelSpace.GetNormalLinear(navPos.pos).w;
					break;
				case ForestDef.Modifier.Parameter.Grass:
					num = navPos.GetGrass();
					break;
				case ForestDef.Modifier.Parameter.Height:
					num = navPos.pos.y;
					break;
				case ForestDef.Modifier.Parameter.RelativeHeight:
					num = navPos.pos.y / navPos.navigationMesh.bounds.size.y;
					break;
				case ForestDef.Modifier.Parameter.PerlinNoise:
					num = noise;
					break;
				case ForestDef.Modifier.Parameter.HouseDistance:
					num = navPos.island.village.houses.Min((House x) => x.distanceField.SampleDistance(navPos));
					break;
				case ForestDef.Modifier.Parameter.Door:
					for (int i = 0; i < navPos.island.village.houses.Length; i++)
					{
						House house = navPos.island.village.houses[i];
						for (int j = 0; j < 3; j++)
						{
							if (navPos.tri.verts[j] == house.doorVert)
							{
								num = navPos.bary.GetComponent(j);
								i = navPos.island.village.houses.Length;
								break;
							}
						}
					}
					break;
				}
				num = ExtraMath.RemapValue(num, this.inMin, this.inMax, this.outMin, this.outMax);
				ForestDef.Modifier.Mode mode = this.mode;
				if (mode != ForestDef.Modifier.Mode.Add)
				{
					if (mode == ForestDef.Modifier.Mode.Multiply)
					{
						value *= num;
					}
				}
				else
				{
					value += num;
				}
			}

			// Token: 0x040020E1 RID: 8417
			[SerializeField]
			private ForestDef.Modifier.Parameter parameter;

			// Token: 0x040020E2 RID: 8418
			[SerializeField]
			private ForestDef.Modifier.Mode mode;

			// Token: 0x040020E3 RID: 8419
			[SerializeField]
			private float inMin;

			// Token: 0x040020E4 RID: 8420
			[SerializeField]
			private float inMax = 1f;

			// Token: 0x040020E5 RID: 8421
			[SerializeField]
			private float outMin;

			// Token: 0x040020E6 RID: 8422
			[SerializeField]
			private float outMax;

			// Token: 0x0200075A RID: 1882
			private enum Parameter
			{
				// Token: 0x040020E8 RID: 8424
				Cliff,
				// Token: 0x040020E9 RID: 8425
				Wall,
				// Token: 0x040020EA RID: 8426
				WallDistance,
				// Token: 0x040020EB RID: 8427
				CliffDistance,
				// Token: 0x040020EC RID: 8428
				Coverage,
				// Token: 0x040020ED RID: 8429
				Grass,
				// Token: 0x040020EE RID: 8430
				Height,
				// Token: 0x040020EF RID: 8431
				RelativeHeight,
				// Token: 0x040020F0 RID: 8432
				PerlinNoise,
				// Token: 0x040020F1 RID: 8433
				HouseDistance,
				// Token: 0x040020F2 RID: 8434
				Door
			}

			// Token: 0x0200075B RID: 1883
			private enum Mode
			{
				// Token: 0x040020F4 RID: 8436
				Add,
				// Token: 0x040020F5 RID: 8437
				Multiply
			}
		}

		// Token: 0x0200075C RID: 1884
		public class Forest
		{
			// Token: 0x06003122 RID: 12578 RVA: 0x000CAC00 File Offset: 0x000C9000
			public Forest(ForestDef forestDef, IEnumerable<Vert> refVerts)
			{
				this.forestDef = forestDef;
				this.noiseOffset = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value) * 1000f;
				this.inMin = float.MaxValue;
				this.inMax = float.MinValue;
				foreach (Vert vert in refVerts)
				{
					NavPos navPos = vert.navPos;
					float radius = forestDef.GetRadius(navPos, this.GetNoise(navPos));
					this.inMin = Mathf.Min(this.inMin, radius);
					this.inMax = Mathf.Max(this.inMax, radius);
				}
				this.inMin *= Mathf.Min(forestDef.randomRadius.x, forestDef.randomRadius.y);
				this.inMax *= Mathf.Max(forestDef.randomRadius.x, forestDef.randomRadius.y);
				this.outMin = forestDef.treePool.minRadius;
				this.outMax = forestDef.treePool.maxRadius;
			}

			// Token: 0x06003123 RID: 12579 RVA: 0x000CAD4C File Offset: 0x000C914C
			public float GetNoise(NavPos navPos)
			{
				Vector3 vector = navPos.pos / 2.55676f + this.noiseOffset;
				return Mathf.PerlinNoise(vector.x, vector.z);
			}

			// Token: 0x06003124 RID: 12580 RVA: 0x000CAD8B File Offset: 0x000C918B
			public float GetDensity(NavPos navPos)
			{
				return this.forestDef.GetDensity(navPos, this.GetNoise(navPos));
			}

			// Token: 0x06003125 RID: 12581 RVA: 0x000CADA0 File Offset: 0x000C91A0
			public float GetDominance(NavPos navPos)
			{
				return this.forestDef.GetDominance(navPos, this.GetNoise(navPos));
			}

			// Token: 0x06003126 RID: 12582 RVA: 0x000CADB8 File Offset: 0x000C91B8
			public float GetRadius(NavPos navPos)
			{
				float radius = this.forestDef.GetRadius(navPos, this.GetNoise(navPos));
				return ExtraMath.RemapValue(radius, this.inMin, this.inMax, this.outMin, this.outMax);
			}

			// Token: 0x06003127 RID: 12583 RVA: 0x000CADF8 File Offset: 0x000C91F8
			public void SpawnShoots(Vert vert, float newDebt)
			{
				this.debt += newDebt;
				while (this.debt > 0f)
				{
					NavPos cellPos = vert.GetCellPos(UnityEngine.Random.value, UnityEngine.Random.value);
					Shoot shoot = new Shoot(cellPos, this);
					float radius = shoot.radius;
					this.debt -= radius * radius * 3.5f;
					if (cellPos.tri.borderCount < 3 || this.forestDef.onPaths)
					{
						this.shoots.Add(shoot);
					}
				}
			}

			// Token: 0x06003128 RID: 12584 RVA: 0x000CAE8C File Offset: 0x000C928C
			public IEnumerator CreateTrees(Transform container)
			{
				IEnumerator e = this.forestDef.CreateTrees(this.shoots, container);
				while (e.MoveNext())
				{
					object obj = e.Current;
					yield return obj;
				}
				yield break;
			}

			// Token: 0x06003129 RID: 12585 RVA: 0x000CAEAE File Offset: 0x000C92AE
			public void ShootDynamics()
			{
				this.forestDef.RunDynamics(this.shoots);
			}

			// Token: 0x040020F6 RID: 8438
			public ForestDef forestDef;

			// Token: 0x040020F7 RID: 8439
			private Vector3 noiseOffset;

			// Token: 0x040020F8 RID: 8440
			private float debt;

			// Token: 0x040020F9 RID: 8441
			public List<Shoot> shoots = new List<Shoot>();

			// Token: 0x040020FA RID: 8442
			public float inMin;

			// Token: 0x040020FB RID: 8443
			public float inMax;

			// Token: 0x040020FC RID: 8444
			public float outMin;

			// Token: 0x040020FD RID: 8445
			public float outMax;
		}
	}
}
