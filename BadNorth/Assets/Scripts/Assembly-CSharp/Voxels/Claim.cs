using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000669 RID: 1641
	[Serializable]
	public class Claim
	{
		// Token: 0x060029F8 RID: 10744 RVA: 0x000953A4 File Offset: 0x000937A4
		public Claim(Claim srcClaim)
		{
			for (int i = 0; i < this.keys.Length; i++)
			{
				this.keys[i] = srcClaim.keys[i];
			}
			for (int j = 0; j < this.cornersInside.Length; j++)
			{
				this.cornersInside[j] = srcClaim.cornersInside[j];
			}
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x00095454 File Offset: 0x00093854
		public Claim(Module.Cell cell, TransformSettings setting)
		{
			Matrix4x4 matrix = setting.matrix;
			this.pos = ExtraMath.RoundToInt(matrix.MultiplyPoint(cell.pos));
			List<string>[] array = new List<string>[6];
			for (int i = 0; i < 6; i++)
			{
				array[i] = new List<string>();
			}
			for (int j = 0; j < 6; j++)
			{
				this.edges[j] = new Claim.Edges();
			}
			for (int k = 0; k < 6; k++)
			{
				this.mode[k] = Claim.Mode.Standard;
			}
			bool flipped = setting.GetFlipped();
			for (int l = 0; l < cell.corners.Length; l++)
			{
				Vector3 a = matrix.MultiplyVector(Constants.corners[l]);
				for (int m = 0; m < 8; m++)
				{
					if (ExtraMath.CloseEnough(a, Constants.corners[m], 0.01f))
					{
						this.cornersInside[m] = cell.corners[l].inside;
					}
				}
			}
			for (int n = 0; n < 6; n++)
			{
				for (int num = 0; num < 6; num++)
				{
					Vector3 lhs = ExtraMath.Round(matrix.MultiplyVector(Constants.directions[num]));
					if (lhs == Constants.directions[n])
					{
						if (cell.internalSide[num])
						{
							this.mode[n] = Claim.Mode.Internal;
						}
						this.navigable[n] = cell.navigable[num];
						break;
					}
				}
			}
			this.anyNavigable = this.navigable.Contains(true);
			for (int num2 = 0; num2 < cell.edges.Count; num2++)
			{
				int num3 = (!flipped) ? 0 : 1;
				Vector3 vector = matrix.MultiplyVector(cell.edges[num2].GetVertex(num3));
				Vector3 vector2 = matrix.MultiplyVector(cell.edges[num2].GetVertex(1 - num3));
				Vector3 vector3 = matrix.MultiplyVector(cell.edges[num2].normal);
				Vector3 vector4 = ExtraMath.Round(matrix.MultiplyVector(cell.edges[num2].side));
				for (int num4 = 0; num4 < 6; num4++)
				{
					if (this.mode[num4] == Claim.Mode.Standard && vector4 == Constants.directions[num4])
					{
						vector -= Constants.directions[num4] * (int) 0.5f;
						vector2 -= Constants.directions[num4] * (int) 0.5f;
						string positionString = this.GetPositionString(vector);
						string positionString2 = this.GetPositionString(vector2);
						string text = string.Concat(new string[]
						{
							"{",
							(string.Compare(positionString, positionString2) != 1) ? (positionString2 + positionString) : (positionString + positionString2),
							this.GetNormalString(vector3, cell.edges[num2].normalPrecision),
							cell.edges[num2].extraString,
							"}"
						});
						this.edges[num4].edges.Add(new Edge(vector, vector2, vector3, vector4, cell.edges[num2].extraString, cell.edges[num2].normalPrecision, text));
						array[num4].Add(text);
						break;
					}
				}
			}
			for (int num5 = 0; num5 < 6; num5++)
			{
				if (this.edges[num5].edges.Count <= 0)
				{
					if (this.mode[num5] != Claim.Mode.Internal)
					{
						bool flag = true;
						bool flag2 = true;
						for (int num6 = 0; num6 < cell.corners.Length; num6++)
						{
							Vector3 vector5 = Constants.corners[num6];
							if (Vector3.Dot(vector5, Constants.directions[num5]) > 0f)
							{
								vector5 -= Constants.directions[num5] * (int) 0.5f;
								if (this.cornersInside[num6])
								{
									flag2 = false;
								}
								if (!this.cornersInside[num6])
								{
									flag = false;
								}
							}
						}
						if (flag)
						{
							this.mode[num5] = Claim.Mode.Inside;
						}
						else if (flag2)
						{
							this.mode[num5] = Claim.Mode.Outside;
						}
					}
				}
			}
			for (int num7 = 0; num7 < 6; num7++)
			{
				if (this.mode[num7] == Claim.Mode.Standard)
				{
					array[num7].Sort();
					this.keyStrings[num7] = string.Empty;
					string[] array2;
					int num8;
					(array2 = this.keyStrings)[num8 = num7] = array2[num8] + array[num7].Count;
					for (int num9 = 0; num9 < array[num7].Count; num9++)
					{
						int num10;
						(array2 = this.keyStrings)[num10 = num7] = array2[num10] + array[num7][num9];
					}
					for (int num11 = 0; num11 < cell.corners.Length; num11++)
					{
						Vector3 vector6 = Constants.corners[num11];
						if (Vector3.Dot(vector6, Constants.directions[num7]) > 0f)
						{
							vector6 -= Constants.directions[num7] * (int) 0.5f;
							int num12;
							(array2 = this.keyStrings)[num12 = num7] = array2[num12] + (vector6 * 2f).ToString("F0") + this.cornersInside[num11];
						}
					}
				}
				else
				{
					this.keyStrings[num7] = this.mode[num7].ToString();
				}
			}
			for (int num13 = 0; num13 < 6; num13++)
			{
				if (this.mode[num13] == Claim.Mode.Standard)
				{
					this.keys[num13] = this.keyStrings[num13].GetHashCode();
				}
				else
				{
					this.keys[num13] = (int)this.mode[num13];
				}
			}
			for (int num14 = 0; num14 < this.cornersInside.Length; num14++)
			{
				this.normal += ((!this.cornersInside[num14]) ? Constants.corners[num14] : (-Constants.corners[num14]));
			}
			this.normal.Normalize();
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x00095BFC File Offset: 0x00093FFC
		private string GetNormalString(Vector3 normal, float mul)
		{
			normal *= mul;
			return normal.ToString("F0");
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x00095C14 File Offset: 0x00094014
		private string GetPositionString(Vector3 position)
		{
			return (position * 100f).ToString("F0");
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x00095C3C File Offset: 0x0009403C
		public int GetSumKey()
		{
			string text = string.Empty;
			for (int i = 0; i < 6; i++)
			{
				text = text + this.keys[i].ToString() + "_";
			}
			text += this.pos.ToString();
			return text.GetHashCode();
		}

		// Token: 0x04001B4F RID: 6991
		public Vector3Int pos;

		// Token: 0x04001B50 RID: 6992
		public int[] keys = new int[6];

		// Token: 0x04001B51 RID: 6993
		public bool[] navigable = new bool[6];

		// Token: 0x04001B52 RID: 6994
		public bool anyNavigable;

		// Token: 0x04001B53 RID: 6995
		public string[] keyStrings = new string[6];

		// Token: 0x04001B54 RID: 6996
		public Claim.Edges[] edges = new Claim.Edges[6];

		// Token: 0x04001B55 RID: 6997
		public Vector3 normal;

		// Token: 0x04001B56 RID: 6998
		public Vector3 navigationNormal;

		// Token: 0x04001B57 RID: 6999
		public bool[] cornersInside = new bool[8];

		// Token: 0x04001B58 RID: 7000
		public Claim.Mode[] mode = new Claim.Mode[6];

		// Token: 0x0200066A RID: 1642
		[Serializable]
		public class Edges
		{
			// Token: 0x04001B59 RID: 7001
			public List<Edge> edges = new List<Edge>();
		}

		// Token: 0x0200066B RID: 1643
		public enum Mode
		{
			// Token: 0x04001B5B RID: 7003
			Outside,
			// Token: 0x04001B5C RID: 7004
			Inside,
			// Token: 0x04001B5D RID: 7005
			Internal,
			// Token: 0x04001B5E RID: 7006
			Standard
		}
	}
}
