using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000720 RID: 1824
	public class ProtoCampaign
	{
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x000BD440 File Offset: 0x000BB840
		public float xWeight
		{
			get
			{
				return (this.xTotal != 0f) ? (this.xTotal / this.xDenom) : 0f;
			}
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000BD46C File Offset: 0x000BB86C
		public bool ChoseNode(int index)
		{
			Node value = this.nodes[index];
			if (value.state != Node.State.Idle)
			{
				return false;
			}
			value.state = Node.State.Chosen;
			float num = value.outerRadius * value.outerRadius * 3.14f;
			this.xTotal += value.pos.x * num;
			this.xDenom += num;
			this.chosenCount++;
			this.nodes[index] = value;
			for (int i = (int)value.connectionIndex0; i < (int)value.connectionIndex1; i++)
			{
				Node value2 = this.nodes[this.connections[i].y];
				value2.chosenNeigbours += 1;
				this.nodes[this.connections[i].y] = value2;
			}
			return true;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000BD568 File Offset: 0x000BB968
		public bool RandomWalk(int index, int maxCount)
		{
			Node node = this.nodes[index];
			if (node.state != Node.State.Chosen)
			{
				return false;
			}
			float num = Mathf.Sign(-node.pos.x);
			int num2 = maxCount - this.chosenCount;
			int num3 = 0;
			int num4 = 0;
			List<int> list = ListPool<int>.GetList(num2);
			int num5 = 0;
			int num6 = 0;
			while (num5 <= num6 && num3 < num2)
			{
				int connectionCount = node.connectionCount;
				int num7 = (connectionCount % 1 != 0) ? 2 : 1;
				bool flag = UnityEngine.Random.value > 0.5f;
				int num8 = UnityEngine.Random.Range(0, connectionCount);
				for (int i = 0; i < connectionCount; i++)
				{
					int num9 = (!flag) ? (connectionCount - i + num8 - 1) : (i + num8);
					num9 *= num7;
					num9 %= connectionCount;
					int y = this.connections[(int)node.connectionIndex0 + num9].y;
					Node node2 = this.nodes[y];
					if (node2.state != Node.State.Forbidden)
					{
						if ((node2.pos.x - node.pos.x) * num >= 0f)
						{
							if (node2.state == Node.State.Chosen)
							{
								num4 = list.Count;
							}
							else
							{
								num3++;
							}
							list.Add(y);
							Debug.DrawLine(node.pos, node2.pos, Color.red);
							node = node2;
							num6++;
							break;
						}
					}
				}
				num5++;
			}
			if (num4 > 0)
			{
				list.RemoveRange(num4, list.Count - num4);
				foreach (int index2 in list)
				{
					this.ChoseNode(index2);
				}
				list.ReturnToListPool<int>();
				return true;
			}
			list.ReturnToListPool<int>();
			return false;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000BD794 File Offset: 0x000BBB94
		public bool CreateOffshoot(int index)
		{
			Node value = this.nodes[index];
			if (value.state != Node.State.Idle || value.type != Node.Type.Normal)
			{
				return false;
			}
			int num = 0;
			for (int i = (int)value.connectionIndex0; i < (int)value.connectionIndex1; i++)
			{
				if (this.nodes[this.connections[i].y].state == Node.State.Chosen && ++num > 1)
				{
					break;
				}
			}
			if (num != 1)
			{
				return false;
			}
			value.state = Node.State.Chosen;
			value.type = Node.Type.Offshoot;
			this.nodes[index] = value;
			for (int j = (int)value.connectionIndex0; j < (int)value.connectionIndex1; j++)
			{
				Node value2 = this.nodes[this.connections[j].y];
				value2.type = Node.Type.Buffer;
				if (value2.state != Node.State.Chosen)
				{
					value2.state = Node.State.Forbidden;
				}
				this.nodes[this.connections[j].y] = value2;
			}
			return true;
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000BD8D4 File Offset: 0x000BBCD4
		public void CalculateIndexRanges()
		{
			this.connections.Sort(new Comparison<Vector2Int>(this.ConnectionSorter));
			Node value = this.nodes[0];
			value.connectionIndex0 = 0;
			int i = 0;
			int num = 0;
			while (i < this.connections.Count)
			{
				Vector2Int vector2Int = this.connections[i];
				if (num != vector2Int.x)
				{
					value.connectionIndex1 = (short)i;
					this.nodes[num] = value;
					num = vector2Int.x;
					value = this.nodes[num];
					value.connectionIndex0 = (short)i;
				}
				i++;
			}
			value.connectionIndex1 = (short)this.connections.Count;
			this.nodes[this.nodes.Count - 1] = value;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000BD9A8 File Offset: 0x000BBDA8
		public void RemoveUnusedNodes()
		{
			for (int i = this.nodes.Count - 1; i >= 0; i--)
			{
				if (this.nodes[i].state != Node.State.Chosen)
				{
					for (int j = this.connections.Count - 1; j >= 0; j--)
					{
						Vector2Int value = this.connections[j];
						if (value.x == i || value.y == i)
						{
							this.connections.RemoveAt(j);
						}
						else
						{
							if (value.x > i)
							{
								value.x--;
							}
							if (value.y > i)
							{
								value.y--;
							}
							this.connections[j] = value;
						}
					}
					this.nodes.RemoveAt(i);
				}
			}
			this.CalculateIndexRanges();
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000BDA9C File Offset: 0x000BBE9C
		public void RemoveUnusedConnections()
		{
			for (int i = this.connections.Count - 1; i >= 0; i--)
			{
				if (this.nodes[this.connections[i].x].state != Node.State.Chosen || this.nodes[this.connections[i].y].state != Node.State.Chosen)
				{
					this.connections.RemoveAt(i);
				}
			}
			this.CalculateIndexRanges();
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000BDB34 File Offset: 0x000BBF34
		public void OrderConnections()
		{
			int i = 0;
			int count = this.connections.Count;
			while (i < count)
			{
				this.connections.Add(new Vector2Int(this.connections[i].y, this.connections[i].x));
				i++;
			}
			this.CalculateIndexRanges();
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000BDBA0 File Offset: 0x000BBFA0
		private int ConnectionSorter(Vector2Int a, Vector2Int b)
		{
			if (a.x != b.x)
			{
				return a.x.CompareTo(b.x);
			}
			return ExtraMath.Atan2(this.nodes[a.y].pos - this.nodes[a.x].pos).CompareTo(ExtraMath.Atan2(this.nodes[b.y].pos - this.nodes[b.x].pos));
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000BDC60 File Offset: 0x000BC060
		public void PropagateSteps()
		{
			for (int i = 0; i < this.nodes.Count; i++)
			{
				Node value = this.nodes[i];
				value.stepsFromStart = ((i != 0) ? byte.MaxValue : 0);
				value.stepsFromEnd = ((i != this.nodes.Count - 1) ? byte.MaxValue : 0);
				this.nodes[i] = value;
			}
			List<int> list = ListPool<int>.GetList(this.nodes.Count);
			list.Add(0);
			for (int j = 0; j < list.Count; j++)
			{
				int index = list[j];
				Node node = this.nodes[index];
				int num = (int)(node.stepsFromStart + 1);
				for (int k = (int)node.connectionIndex0; k < (int)node.connectionIndex1; k++)
				{
					int y = this.connections[k].y;
					Node value2 = this.nodes[y];
					if ((int)value2.stepsFromStart > num && value2.state == Node.State.Chosen)
					{
						value2.stepsFromStart = (byte)num;
						this.nodes[y] = value2;
						list.Add(y);
					}
				}
			}
			list.Clear();
			list.Add(this.nodes.Count - 1);
			for (int l = 0; l < list.Count; l++)
			{
				int index2 = list[l];
				Node node2 = this.nodes[index2];
				int num2 = (int)(node2.stepsFromEnd + 1);
				for (int m = (int)node2.connectionIndex0; m < (int)node2.connectionIndex1; m++)
				{
					int y2 = this.connections[m].y;
					Node value3 = this.nodes[y2];
					if ((int)value3.stepsFromEnd > num2 && value3.state == Node.State.Chosen)
					{
						value3.stepsFromEnd = (byte)num2;
						this.nodes[y2] = value3;
						list.Add(y2);
					}
				}
			}
			list.ReturnToListPool<int>();
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000BDE9C File Offset: 0x000BC29C
		private void PropagateRewardSteps(int index, List<int> distances)
		{
			distances[index] = 0;
			List<int> list = ListPool<int>.GetList(this.nodes.Count);
			list.Add(index);
			for (int i = 0; i < list.Count; i++)
			{
				int index2 = list[i];
				Node node = this.nodes[index2];
				int num = distances[index2] + 1;
				for (int j = (int)node.connectionIndex0; j < (int)node.connectionIndex1; j++)
				{
					int y = this.connections[j].y;
					if (num < distances[y])
					{
						distances[y] = num;
						list.Add(y);
					}
				}
			}
			list.ReturnToListPool<int>();
		}

		// Token: 0x04001F93 RID: 8083
		public List<Node> nodes;

		// Token: 0x04001F94 RID: 8084
		public List<Vector2Int> connections;

		// Token: 0x04001F95 RID: 8085
		public int chosenCount;

		// Token: 0x04001F96 RID: 8086
		private float xTotal;

		// Token: 0x04001F97 RID: 8087
		private float xDenom;
	}
}
