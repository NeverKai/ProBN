using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000622 RID: 1570
	public class Marcher
	{
		// Token: 0x0600285B RID: 10331 RVA: 0x00085798 File Offset: 0x00083B98
		public Marcher(int radius)
		{
			int num = radius * 2 + 1;
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					for (int k = 0; k < num; k++)
					{
						Vector3 vector = new Vector3((float)i, (float)j, (float)k) - Vector3.one * (float)radius;
						float num2 = 1f - vector.magnitude / (float)radius;
						if (num2 > 0f)
						{
							Marcher.Node item = new Marcher.Node(vector);
							this.nodes.Add(item);
							if (vector == Vector3.zero)
							{
								this.center = item;
							}
						}
					}
				}
			}
			this.nodes.TrimExcess();
			this.Setup();
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00085870 File Offset: 0x00083C70
		public Marcher(Vector3 extents)
		{
			Vector3 vector = extents * 2f + Vector3.one;
			int num = 0;
			while ((float)num < vector.x)
			{
				int num2 = 0;
				while ((float)num2 < vector.y)
				{
					int num3 = 0;
					while ((float)num3 < vector.z)
					{
						Vector3 vector2 = new Vector3((float)num, (float)num2, (float)num3) - extents;
						Marcher.Node item = new Marcher.Node(vector2);
						this.nodes.Add(item);
						if (vector2 == Vector3.zero)
						{
							this.center = item;
						}
						num3++;
					}
					num2++;
				}
				num++;
			}
			this.nodes.TrimExcess();
			this.Setup();
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x00085944 File Offset: 0x00083D44
		private void Setup()
		{
			Dictionary<Vector3, Marcher.Node> dictionary = new Dictionary<Vector3, Marcher.Node>();
			for (int i = 0; i < this.nodes.Count; i++)
			{
				dictionary.Add(this.nodes[i].pos, this.nodes[i]);
			}
			for (int j = 0; j < this.nodes.Count; j++)
			{
				Marcher.Node node = this.nodes[j];
				if (node != null)
				{
					int[] array = new int[]
					{
						(int)node.pos.x,
						(int)node.pos.y,
						(int)node.pos.z
					};
					int[] array2 = new int[]
					{
						Mathf.Abs(array[0]),
						Mathf.Abs(array[1]),
						Mathf.Abs(array[2])
					};
					int num = Mathf.Max(array2);
					if (num != 0)
					{
						int num2 = (int)Mathf.Pow(2f, (float)num) % num;
						int[] array3 = new int[3];
						for (int k = 0; k < 3; k++)
						{
							if (array[k] != 0 && (array2[k] == num || array2[k] > num2))
							{
								array3[k] = (int)Mathf.Sign((float)array[k]);
							}
						}
						Vector3 b = new Vector3((float)(-(float)array3[0]), (float)(-(float)array3[1]), (float)(-(float)array3[2]));
						Vector3 key = node.pos + b;
						dictionary[key].nodes.Add(node);
					}
				}
			}
			this.center.alpha = 1f;
			this.center.PropagateAlpha();
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x00085AFC File Offset: 0x00083EFC
		public void RemoveBranch(Marcher.Node node)
		{
			for (int i = 0; i < node.nodes.Count; i++)
			{
				this.RemoveBranch(node.nodes[i]);
			}
			this.nodes.Remove(node);
		}

		// Token: 0x040019DC RID: 6620
		public List<Marcher.Node> nodes = new List<Marcher.Node>();

		// Token: 0x040019DD RID: 6621
		public Marcher.Node center;

		// Token: 0x02000623 RID: 1571
		public class Node
		{
			// Token: 0x0600285F RID: 10335 RVA: 0x00085B44 File Offset: 0x00083F44
			public Node(Vector3 pos)
			{
				this.pos = pos;
				this.normal = pos.normalized;
			}

			// Token: 0x06002860 RID: 10336 RVA: 0x00085B78 File Offset: 0x00083F78
			public void PropagateAlpha()
			{
				float num = this.alpha / (float)this.nodes.Count;
				for (int i = 0; i < this.nodes.Count; i++)
				{
					this.nodes[i].alpha = num;
					this.nodes[i].PropagateAlpha();
				}
			}

			// Token: 0x040019DE RID: 6622
			public float alpha;

			// Token: 0x040019DF RID: 6623
			public Vector4 color = Vector4.zero;

			// Token: 0x040019E0 RID: 6624
			public bool hasColor;

			// Token: 0x040019E1 RID: 6625
			public Vector3 pos;

			// Token: 0x040019E2 RID: 6626
			public Vector3 normal;

			// Token: 0x040019E3 RID: 6627
			public List<Marcher.Node> nodes = new List<Marcher.Node>();
		}
	}
}
