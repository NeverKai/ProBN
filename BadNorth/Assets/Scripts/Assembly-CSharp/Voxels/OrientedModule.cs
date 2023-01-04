using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels
{
	// Token: 0x0200062D RID: 1581
	[Serializable]
	public class OrientedModule
	{
		// Token: 0x0600288A RID: 10378 RVA: 0x000878B4 File Offset: 0x00085CB4
		public OrientedModule(Module module, TransformSettings settings, int index)
		{
			this.module = module;
			this.settings = settings;
			this.index = index;
			this.name = module.name + ",   " + settings.GetString();
			for (int i = 0; i < module.cells.Count; i++)
			{
				Claim item = new Claim(module.cells[i], settings);
				this.claims.Add(item);
			}
			if (this.claims.Count > 1)
			{
				for (int j = 0; j < this.claims.Count; j++)
				{
					Claim claim = this.claims[j];
					if (claim.navigable.Contains(true) && claim.mode.Contains(Claim.Mode.Internal))
					{
						for (int k = j + 1; k < this.claims.Count; k++)
						{
							Claim claim2 = this.claims[k];
							if (claim2.navigable.Contains(true) && claim2.mode.Contains(Claim.Mode.Internal))
							{
								for (int l = 0; l < 6; l++)
								{
									if (ExtraMath.CloseEnough(claim.pos + Constants.directions[l], claim2.pos, 0.01f))
									{
										claim.navigable[l] = true;
										claim2.navigable[Constants.opposites[l]] = true;
									}
								}
							}
						}
					}
				}
			}
			for (int m = 0; m < this.claims.Count; m++)
			{
				Claim claim3 = this.claims[m];
				for (int n = 0; n < 6; n++)
				{
					if (claim3.navigable[n])
					{
						claim3.navigationNormal += Constants.directions[n];
						if (claim3.mode[n] != Claim.Mode.Internal)
						{
							this.navigationNormal += Constants.directions[n];
						}
					}
				}
			}
			this.SetupCorners();
			this.claims.TrimExcess();
			this.corners.TrimExcess();
			List<string> list = new List<string>();
			for (int num = 0; num < this.claims.Count; num++)
			{
				list.Add(this.claims[num].GetSumKey().ToString());
			}
			list.Sort();
			string text = string.Empty;
			for (int num2 = 0; num2 < this.claims.Count; num2++)
			{
				text += list[num2];
			}
			this.key = text.GetHashCode();
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x00087BE2 File Offset: 0x00085FE2
		public static OrientedModule empty
		{
			get
			{
				return new OrientedModule(null, TransformSettings.identity, 0);
			}
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x00087BF0 File Offset: 0x00085FF0
		public GameObject GetGameObject()
		{
			return this.module.GetGameObject(this.settings);
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x00087C04 File Offset: 0x00086004
		public void SetupCorners()
		{
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < this.claims.Count; i++)
			{
				for (int j = 0; j < this.claims[i].cornersInside.Length; j++)
				{
					Vector3 vector = Constants.corners[j] + this.claims[i].pos;
					if (!list.Contains(vector))
					{
						this.corners.Add(new Corner(vector, this.claims[i].cornersInside[j]));
						list.Add(vector);
					}
				}
			}
			for (int k = 0; k < this.claims.Count; k++)
			{
				for (int l = 0; l < this.claims[k].cornersInside.Length; l++)
				{
					this.normal += ((!this.claims[k].cornersInside[l]) ? Constants.corners[l] : (-Constants.corners[l]));
					if (this.claims[k].cornersInside[l])
					{
						this.coverage += 1f;
					}
				}
			}
			this.coverage /= (float)(this.claims.Count * 8);
			this.normal /= (float)(this.claims.Count * 4);
		}

		// Token: 0x04001A12 RID: 6674
		[HideInInspector]
		public string name;

		// Token: 0x04001A13 RID: 6675
		public Module module;

		// Token: 0x04001A14 RID: 6676
		public TransformSettings settings;

		// Token: 0x04001A15 RID: 6677
		public int key;

		// Token: 0x04001A16 RID: 6678
		public int index;

		// Token: 0x04001A17 RID: 6679
		public Vector3 normal;

		// Token: 0x04001A18 RID: 6680
		public Vector3 navigationNormal;

		// Token: 0x04001A19 RID: 6681
		public float coverage;

		// Token: 0x04001A1A RID: 6682
		public List<Claim> claims = new List<Claim>();

		// Token: 0x04001A1B RID: 6683
		public List<Corner> corners = new List<Corner>();
	}
}
