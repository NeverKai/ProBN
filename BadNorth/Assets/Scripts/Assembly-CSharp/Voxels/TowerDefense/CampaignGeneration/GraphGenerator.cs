using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006EA RID: 1770
	public class GraphGenerator : CampaignComponent, Campaign.ICampaignGenerator, Campaign.ICampaignCreator
	{
		// Token: 0x06002DA7 RID: 11687 RVA: 0x000AE290 File Offset: 0x000AC690
		IEnumerator Campaign.ICampaignCreator.OnCampaigCreation(Campaign campaign, ProtoCampaign graph)
		{
			IEnumerator enumerator = this.GenerateGraph(graph, campaign).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object v = enumerator.Current;
					yield return null;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			yield break;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000AE2BC File Offset: 0x000AC6BC
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			Bounds bounds = default(Bounds);
			foreach (LevelState levelState in campaign.campaignSave.levelStates)
			{
				Bounds bounds2 = new Bounds(levelState.pos, Vector2.one * levelState.innerRadius * 2f);
				bounds.Encapsulate(bounds2);
				if (levelState.metaReward)
				{
					HeroDefinition heroDefinition = campaign.campaignSave.TryGetHeroDefinition(levelState.heroId);
					HeroUpgradeDefinition heroUpgradeDefinition;
					if (heroDefinition && heroDefinition.traitUpgrade != null)
					{
						heroUpgradeDefinition = heroDefinition.traitUpgrade.definition;
					}
					else
					{
						heroUpgradeDefinition = levelState.item;
					}
					if (!heroUpgradeDefinition || Profile.userSave.inventory.IsStartingItem(heroUpgradeDefinition))
					{
						levelState.metaReward = false;
						if (heroDefinition && !heroDefinition.recruited)
						{
							heroDefinition.propertyBank.SetBool("MetaTrait", false);
						}
					}
				}
			}
			campaign.bounds = bounds;
			campaign.rect = new Rect(bounds.min, bounds.size);
			yield return new GenInfo("GraphGenerator", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000AE2D8 File Offset: 0x000AC6D8
		private void AddReward(ProtoCampaign graph, int index, ref GraphGenerator.RewardStruct reward)
		{
			Node value = graph.nodes[index];
			value.reward = reward.type;
			graph.nodes[index] = value;
			for (int i = (int)value.connectionIndex0; i < (int)value.connectionIndex1; i++)
			{
				Node value2 = graph.nodes[graph.connections[i].y];
				value2.rewardNeighbours += 1;
				graph.nodes[graph.connections[i].y] = value2;
			}
			reward.credit--;
			reward.steps[index] = 0;
			List<int> list = ListPool<int>.GetList(graph.nodes.Count);
			list.Add(index);
			for (int j = 0; j < list.Count; j++)
			{
				int index2 = list[j];
				Node node = graph.nodes[index2];
				int num = reward.steps[index2] + 1;
				for (int k = (int)node.connectionIndex0; k < (int)node.connectionIndex1; k++)
				{
					int y = graph.connections[k].y;
					if (num < reward.steps[y])
					{
						reward.steps[y] = num;
						list.Add(y);
					}
				}
			}
			list.ReturnToListPool<int>();
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000AE464 File Offset: 0x000AC864
		private IEnumerator TestEnumerator()
		{
			this.graphs = new List<ProtoCampaign>();
			for (;;)
			{
				ProtoCampaign graph = new ProtoCampaign();
				this.graphs.Add(graph);
				if (this.graphs.Count > 32)
				{
					this.graphs.RemoveAt(0);
				}
				IEnumerator enumerator = this.GenerateGraph(graph, null).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object v = enumerator.Current;
						yield return null;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			yield break;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000AE480 File Offset: 0x000AC880
		private IEnumerable GenerateGraph(ProtoCampaign graph, Campaign campaign)
		{
			CampaignDifficultySettings difficultySettings = campaign.GetDifficultySettings();
			int levelCount = difficultySettings.levelCount;
			int spawnCount = levelCount * 2;
			List<Node> nodes = graph.nodes = new List<Node>(spawnCount);
			List<Vector2Int> connections = graph.connections = new List<Vector2Int>(nodes.Count * 4);
			float mapHalfHeight = this.mapHeight / 2f;
			float averageWidth = this.islandSizeField.GetAverageWidth();
			float averageInnerRadius = averageWidth * 0.8f;
			float averageOuterRadius = averageInnerRadius + 3f;
			float averageArea = averageOuterRadius * averageOuterRadius * 3.5f;
			float mapLength = averageArea * (float)levelCount * 1.5f / (this.mapHeight + 6f);
			float mapHalfLength = mapLength / 2f;
			float x = 0f;
			float y = 0f;
			float avWidth = 0f;
			float totalDenom = 0f;
			float fraction = 0f;
			float sign = 1f;
			while (x < mapLength)
			{
				Node node = new Node
				{
					state = Node.State.Idle
				};
				Vector2Int dimensions = this.islandSizeField.GetRandomDimensions(fraction);
				node.width = (byte)dimensions.x;
				node.height = (byte)dimensions.y;
				y += node.outerRadius;
				if (y > this.mapHeight)
				{
					y -= this.mapHeight;
					x += avWidth / totalDenom;
					fraction = x / mapLength;
					sign = -sign;
					avWidth = 0f;
					totalDenom = 0f;
				}
				y += node.outerRadius;
				node.pos.x = x - mapHalfLength;
				node.pos.y = (y - mapHalfHeight) * sign;
				node.pos += UnityEngine.Random.insideUnitCircle * node.innerRadius * 0.5f;
				node.pos.y = Mathf.Clamp(node.pos.y, node.innerRadius - mapHalfHeight, mapHalfHeight - node.innerRadius);
				node.pos.x = Mathf.Clamp(node.pos.x, Mathf.Abs(node.pos.y * 0.6f) - mapHalfLength, mapHalfLength - Mathf.Abs(node.pos.y * 0.6f));
				float denom = node.outerRadius;
				avWidth += node.outerRadius * 2f * denom * 0.95f;
				totalDenom += denom;
				nodes.Add(node);
				yield return null;
			}
			yield return null;
			for (int num = 0; num < 2; num++)
			{
				int index3 = num * (nodes.Count - 1);
				Node value = nodes[index3];
				value.pos = new Vector2((float)(num * 2 - 1) * mapHalfLength, 0f);
				nodes[index3] = value;
			}
			for (float i = 0f; i < 100f; i += 1f)
			{
				for (int num2 = 0; num2 < nodes.Count - 1; num2++)
				{
					Node value2 = nodes[num2];
					for (int num3 = num2 + 1; num3 < nodes.Count; num3++)
					{
						Node value3 = nodes[num3];
						Vector2 a = value2.pos - value3.pos;
						float num4 = value2.outerRadius + value3.outerRadius;
						float magnitude = a.magnitude;
						float num5 = num4 - magnitude;
						if (num5 > 0f)
						{
							num5 *= 0.2f;
							Vector2 a2 = a / magnitude;
							Vector2 b = a2 * num5;
							value2.move += b;
							value3.move -= b;
							nodes[num3] = value3;
						}
					}
					nodes[num2] = value2;
				}
				yield return null;
				float maxMove = 0f;
				for (int num6 = 1; num6 < nodes.Count - 1; num6++)
				{
					Node value4 = nodes[num6];
					Vector2 vector = value4.pos + value4.move;
					vector.y = Mathf.Clamp(vector.y, value4.innerRadius - mapHalfHeight * 0.95f, mapHalfHeight * 0.95f - value4.innerRadius);
					vector.x = Mathf.Clamp(vector.x, Mathf.Abs(vector.y * 0.6f) - mapHalfLength, mapHalfLength - Mathf.Abs(vector.y * 0.6f));
					maxMove = Mathf.Max(maxMove, (value4.pos - vector).sqrMagnitude);
					value4.pos = vector;
					value4.move = Vector2.zero;
					nodes[num6] = value4;
				}
				if (maxMove < 0.001f)
				{
					break;
				}
				yield return null;
			}
			for (int j = 0; j < nodes.Count; j++)
			{
				Node n0 = nodes[j];
				for (int num7 = j + 1; num7 < nodes.Count; num7++)
				{
					Node node9 = nodes[num7];
					float num8 = (n0.outerRadius + node9.outerRadius) * 2f;
					float sqrMagnitude = (n0.pos - node9.pos).sqrMagnitude;
					if (sqrMagnitude <= num8 * num8)
					{
						Vector2Int item = new Vector2Int(j, num7);
						float num9 = Mathf.Sqrt(sqrMagnitude);
						bool flag = true;
						int num10 = Mathf.Max(j - 20, 0);
						while (num10 < Mathf.Min(j + 20, nodes.Count) && flag)
						{
							if (num10 != item.x && num10 != item.y)
							{
								Node node10 = nodes[num10];
								float num11 = Mathf.Max(node10.outerRadius, num9 * 0.4f);
								if (ExtraMath.DistanceTolineSegment(n0.pos, node9.pos, node10.pos) < num11)
								{
									flag = false;
								}
							}
							num10++;
						}
						if (flag)
						{
							for (int num12 = Mathf.Max(connections.Count - 20, 0); num12 < connections.Count; num12++)
							{
								Vector2Int vector2Int = connections[num12];
								if (item.x != vector2Int.x && item.x != vector2Int.y && item.y != vector2Int.x && item.y != vector2Int.y)
								{
									Node node11 = nodes[vector2Int.x];
									Node node12 = nodes[vector2Int.y];
									if (ExtraMath.LineIntersection(n0.pos, node9.pos, node11.pos, node12.pos))
									{
										flag = (sqrMagnitude < (node11.pos - node12.pos).sqrMagnitude);
										if (flag)
										{
											connections.RemoveAt(num12);
										}
										break;
									}
								}
							}
							if (flag)
							{
								connections.Add(item);
							}
						}
					}
				}
				yield return null;
			}
			for (float k = 0f; k < 20f; k += 1f)
			{
				for (int num13 = 0; num13 < connections.Count; num13++)
				{
					Vector2Int vector2Int2 = connections[num13];
					Node value5 = nodes[vector2Int2.x];
					Node value6 = nodes[vector2Int2.y];
					Vector2 a3 = value5.pos - value6.pos;
					float num14 = value5.outerRadius + value6.outerRadius;
					float magnitude2 = a3.magnitude;
					float num15 = num14 - magnitude2;
					if (num15 > 0f)
					{
						num15 *= 0.3f;
					}
					else
					{
						num15 *= 0.05f;
					}
					Vector2 b2 = a3 / magnitude2 * num15;
					value5.move += b2;
					value6.move -= b2;
					nodes[vector2Int2.x] = value5;
					nodes[vector2Int2.y] = value6;
				}
				yield return null;
				for (int num16 = 1; num16 < nodes.Count - 1; num16++)
				{
					Node value7 = nodes[num16];
					Vector2 pos = value7.pos + value7.move;
					pos.y = Mathf.Clamp(pos.y, value7.innerRadius - mapHalfHeight, mapHalfHeight - value7.innerRadius);
					value7.pos = pos;
					value7.move = Vector2.zero;
					nodes[num16] = value7;
				}
				yield return null;
			}
			Node startNode = new Node
			{
				width = 6,
				height = 2,
				state = Node.State.Chosen
			};
			startNode.pos.x = nodes[0].pos.x - startNode.outerRadius - nodes[0].outerRadius * 1.4f - 8f;
			nodes.Insert(0, startNode);
			for (int num17 = 0; num17 < connections.Count; num17++)
			{
				List<Vector2Int> list;
				int index4;
				(list = connections)[index4 = num17] = list[index4] + Vector2Int.one;
			}
			connections.Add(new Vector2Int(0, 1));
			graph.chosenCount++;
			yield return null;
			Node endNode = new Node
			{
				width = 11,
				height = 3,
				state = Node.State.Chosen
			};
			endNode.pos.x = nodes[nodes.Count - 1].pos.x + endNode.outerRadius * 1.4f + nodes[nodes.Count - 1].outerRadius + 20f;
			nodes.Add(endNode);
			yield return null;
			connections.Add(new Vector2Int(nodes.Count - 2, nodes.Count - 1));
			graph.chosenCount++;
			yield return null;
			graph.OrderConnections();
			yield return null;
			bool hasOffshoot = false;
			int maxWalkCount = levelCount - 8;
			while (!graph.RandomWalk(0, maxWalkCount))
			{
				yield return null;
			}
			int l = 0;
			while (l < nodes.Count && !graph.RandomWalk(nodes.Count - 1, maxWalkCount))
			{
				yield return null;
				l++;
			}
			int m = 0;
			while (m < nodes.Count && !graph.RandomWalk(0, maxWalkCount))
			{
				yield return null;
				m++;
			}
			int i2 = 0;
			int start = nodes.Count / 2;
			int dist = nodes.Count / 4;
			while (i2 < dist && !hasOffshoot)
			{
				if (graph.CreateOffshoot(start + i2) || graph.CreateOffshoot(start - i2))
				{
					hasOffshoot = true;
				}
				else
				{
					yield return null;
				}
				i2++;
			}
			int attempts = 0;
			int count = 0;
			while (attempts < nodes.Count && graph.chosenCount < maxWalkCount - 4)
			{
				int index = UnityEngine.Random.Range(0, nodes.Count - 1);
				if (graph.RandomWalk(index, maxWalkCount))
				{
					count++;
				}
				yield return null;
				attempts++;
			}
			int centerIndex;
			for (centerIndex = 0; centerIndex < nodes.Count; centerIndex++)
			{
				if (nodes[centerIndex].pos.x > 0f)
				{
					break;
				}
			}
			int i3 = UnityEngine.Random.Range(0, nodes.Count);
			int attempts2 = 0;
			while (graph.chosenCount < levelCount && attempts2 < levelCount)
			{
				yield return null;
				attempts2++;
				int index2 = (graph.xWeight <= nodes[centerIndex].pos.x) ? (i3 % (nodes.Count - centerIndex) + centerIndex) : (i3 % centerIndex);
				Node node2 = nodes[index2];
				if (node2.state == Node.State.Idle)
				{
					if (node2.chosenNeigbours >= 2)
					{
						if (node2.connectionCount <= 4 || node2.idleNeighbours != 0)
						{
							if (graph.ChoseNode(index2))
							{
								i3 += nodes.Count / 10;
								attempts2 = 0;
							}
						}
					}
				}
				i3++;
			}
			int num18 = 0;
			while (num18 < nodes.Count && graph.chosenCount < levelCount)
			{
				graph.ChoseNode((num18 + nodes.Count / 2) % nodes.Count);
				num18++;
			}
			graph.RemoveUnusedNodes();
			yield return null;
			graph.PropagateSteps();
			yield return null;
			int i4 = 0;
			int max = 0;
			while (i4 < nodes.Count - 1)
			{
				Node node3 = nodes[i4];
				Node node4 = nodes[i4 + 1];
				if (Node.Compare(node3, node4) > 0)
				{
					for (int num19 = 0; num19 < connections.Count; num19++)
					{
						Vector2Int value8 = connections[num19];
						if (value8.x == i4)
						{
							value8.x = i4 + 1;
						}
						else if (value8.x == i4 + 1)
						{
							value8.x = i4;
						}
						if (value8.y == i4)
						{
							value8.y = i4 + 1;
						}
						else if (value8.y == i4 + 1)
						{
							value8.y = i4;
						}
						connections[num19] = value8;
					}
					nodes[i4] = node4;
					nodes[i4 + 1] = node3;
					if (i4 > 0)
					{
						i4--;
					}
					else
					{
						i4 = ++max;
					}
				}
				else
				{
					i4 = ++max;
				}
				yield return null;
			}
			int offshootIndex = 0;
			int shortestPath = (int)nodes[0].stepsFromEnd;
			if (hasOffshoot)
			{
				while (offshootIndex < nodes.Count)
				{
					if (nodes[offshootIndex].type == Node.Type.Offshoot)
					{
						break;
					}
					offshootIndex++;
				}
			}
			for (int i5 = 0; i5 < nodes.Count; i5++)
			{
				Node node5 = nodes[i5];
				if (node5.connectionCount != 1)
				{
					if (node5.connectionCount < 6)
					{
						bool valid = false;
						if (node5.totalSteps == shortestPath)
						{
							valid = true;
							int num20 = 0;
							while (num20 < nodes.Count && valid)
							{
								if (num20 != i5)
								{
									if (nodes[num20].stepsFromStart == node5.stepsFromStart)
									{
										valid = false;
									}
								}
								num20++;
							}
							if (valid)
							{
								node5.chokepointLevel = ((node5.connectionCount != 2) ? 3 : 4);
							}
						}
						if (!valid)
						{
							valid = true;
							int num21 = (int)node5.connectionIndex0;
							while (num21 < (int)node5.connectionIndex1 && valid)
							{
								Node node13 = nodes[connections[num21].y];
								if (node5.stepsFromStart == node13.stepsFromStart || node5.stepsFromEnd == node13.stepsFromEnd)
								{
									valid = false;
								}
								num21++;
							}
							if (valid)
							{
								node5.chokepointLevel = 2;
							}
						}
						if (!valid)
						{
							int num22 = 0;
							int num23 = 0;
							int y2 = connections[(int)(node5.connectionIndex1 - 1)].y;
							Node node14 = nodes[y2];
							for (int num24 = (int)node5.connectionIndex0; num24 < (int)node5.connectionIndex1; num24++)
							{
								int y3 = connections[num24].y;
								Node node15 = nodes[y3];
								if (node14.state == Node.State.Chosen)
								{
									num22++;
								}
								if (node14.state == Node.State.Chosen && node15.state == Node.State.Chosen)
								{
									for (int num25 = (int)node14.connectionIndex0; num25 < (int)node14.connectionIndex1; num25++)
									{
										if (connections[num25].y == y3)
										{
											num23++;
											break;
										}
									}
								}
								node14 = node15;
							}
							valid = (num23 < num22 - 1);
							if (valid)
							{
								node5.chokepointLevel = 1;
							}
						}
						if (valid)
						{
							if (node5.type == Node.Type.Normal)
							{
								node5.type = Node.Type.Chokepoint;
							}
							nodes[i5] = node5;
						}
						yield return null;
					}
				}
			}
			List<HeroUpgradeDefinition> itemDefs = (from def in ResourceList<HeroUpgradeDefinition>.list
			where def.upgradeType == this.itemType || def.upgradeType == this.consumableType
			select def).ToList<HeroUpgradeDefinition>();
			foreach (HeroDefinition heroDefinition in campaign.campaignSave.heroes)
			{
				foreach (SerializableHeroUpgrade serializableHeroUpgrade in heroDefinition.upgrades)
				{
					itemDefs.Remove(serializableHeroUpgrade.definition);
				}
			}
			int checkpointCount = difficultySettings.checkpointCount;
			int checkpointStart = Mathf.FloorToInt((float)nodes.Count * difficultySettings.checkpointRange.x);
			int checkpointEnd = Mathf.CeilToInt((float)nodes.Count * (1f - difficultySettings.checkpointRange.y));
			GraphGenerator.RewardStruct checkpointRewards = new GraphGenerator.RewardStruct(RewardType.Checkpoint, checkpointCount, checkpointStart, checkpointEnd, nodes.Count);
			GraphGenerator.RewardStruct itemRewards = new GraphGenerator.RewardStruct(RewardType.Item, itemDefs.Count, 2, 2, nodes.Count);
			GraphGenerator.RewardStruct heroRewards = new GraphGenerator.RewardStruct(RewardType.Hero, 8, 4, 6, nodes.Count);
			GraphGenerator.RewardStruct eldoradoRewards = new GraphGenerator.RewardStruct(RewardType.Eldorado, this.eldoradoCount, shortestPath / 6, 3, nodes.Count);
			if (hasOffshoot)
			{
				this.AddReward(graph, offshootIndex, ref itemRewards);
			}
			for (int i6 = nodes.Count - 3; i6 >= 4; i6--)
			{
				Node node6 = nodes[i6];
				if (node6.chokepointLevel >= 2 && node6.type != Node.Type.Buffer)
				{
					for (int num26 = i6 - 1; num26 >= 3; num26--)
					{
						Node node16 = nodes[num26];
						if (node16.chokepointLevel >= 2 && node6.type != Node.Type.Buffer)
						{
							if (node6.stepsFromStart > node16.stepsFromStart - 2 && node6.stepsFromStart < node16.stepsFromStart + 2)
							{
								bool flag2 = false;
								int num27 = (int)node6.connectionIndex0;
								while (num27 < (int)node6.connectionIndex1 && !flag2)
								{
									if (connections[num27].y == num26)
									{
										flag2 = true;
									}
									num27++;
								}
								if (!flag2)
								{
									this.AddReward(graph, i6, ref itemRewards);
									this.AddReward(graph, num26, ref heroRewards);
									i6 = num26 - 10;
									break;
								}
							}
						}
					}
					yield return null;
				}
			}
			this.AddReward(graph, 3, ref heroRewards);
			for (int r = 0; r < 4; r++)
			{
				GraphGenerator.RewardStruct rewards = default(GraphGenerator.RewardStruct);
				switch (r)
				{
				case 0:
					rewards = checkpointRewards;
					break;
				case 1:
					rewards = eldoradoRewards;
					break;
				case 2:
					rewards = heroRewards;
					break;
				case 3:
					rewards = itemRewards;
					break;
				}
				for (int i7 = rewards.credit; i7 > 0; i7--)
				{
					int bestIndex = -1;
					float bestScore = float.MinValue;
					for (int num28 = rewards.margin0; num28 < nodes.Count - rewards.margin1 - 1; num28++)
					{
						Node node17 = nodes[num28];
						if (node17.type != Node.Type.Buffer)
						{
							if (node17.reward == RewardType.None)
							{
								float num29 = 2f / (float)rewards.steps[num28];
								float num30 = 0f;
								num30 -= num29;
								num30 += (float)(node17.totalSteps - shortestPath) * 0.1f;
								num30 -= (float)node17.chokepointLevel * 0.1f;
								num30 -= (float)node17.rewardNeighbours;
								switch (rewards.type)
								{
								case RewardType.Hero:
									num30 += (float)node17.height * 0.1f;
									break;
								case RewardType.Eldorado:
									num30 += (float)node17.width * 0.1f - (float)node17.height * 0.1f;
									break;
								case RewardType.Checkpoint:
									num30 += (float)(node17.chokepointLevel * node17.chokepointLevel) * 0.2f;
									num30 -= num29 * 5f;
									break;
								}
								if (num30 > bestScore)
								{
									bestIndex = num28;
									bestScore = num30;
								}
							}
						}
					}
					this.AddReward(graph, bestIndex, ref rewards);
					yield return null;
				}
			}
			for (int num31 = eldoradoRewards.margin0; num31 < nodes.Count - eldoradoRewards.margin1; num31++)
			{
				Node value9 = nodes[num31];
				if (value9.reward == RewardType.Eldorado)
				{
					value9.height = 1;
					nodes[num31] = value9;
				}
			}
			checkpointRewards.steps.ReturnToListPool<int>();
			itemRewards.steps.ReturnToListPool<int>();
			heroRewards.steps.ReturnToListPool<int>();
			eldoradoRewards.steps.ReturnToListPool<int>();
			for (int num32 = 0; num32 < nodes.Count; num32++)
			{
				Node value10 = nodes[num32];
				float num33 = 0f;
				num33 += UnityEngine.Random.value * 0.01f;
				num33 += (float)(value10.chokepointLevel * value10.chokepointLevel) * 0.2f;
				num33 -= (float)(value10.totalSteps - shortestPath) * 0.1f;
				num33 += (float)value10.width * 0.2f;
				switch (value10.reward)
				{
				case RewardType.Item:
					num33 += difficultySettings.itemDifficultyModifier;
					break;
				case RewardType.Hero:
					num33 += difficultySettings.heroDifficultyModifier;
					break;
				case RewardType.Eldorado:
					num33 += difficultySettings.eldoradoDifficultyModifier;
					break;
				case RewardType.Checkpoint:
					num33 += difficultySettings.checkpointDifficultyModifier;
					break;
				}
				if (value10.type == Node.Type.Offshoot)
				{
					num33 += difficultySettings.offshootDifficultyModifier;
				}
				value10.rawDifficulty = num33;
				nodes[num32] = value10;
			}
			for (int num34 = 0; num34 < nodes.Count; num34++)
			{
				Node value11 = nodes[num34];
				int num35 = 0;
				int num36 = 0;
				for (int num37 = (int)value11.connectionIndex0; num37 < (int)value11.connectionIndex1; num37++)
				{
					if (value11.rawDifficulty > nodes[connections[num37].y].rawDifficulty)
					{
						num35++;
					}
					else
					{
						num36++;
					}
				}
				for (int num38 = Mathf.Max(0, num34 - 2); num38 < Mathf.Min(num34 + 3, nodes.Count); num38++)
				{
					if (num38 != num34)
					{
						if (value11.rawDifficulty > nodes[num38].rawDifficulty)
						{
							num35++;
						}
						else
						{
							num36++;
						}
					}
				}
				value11.difficulty = (float)num35 / (float)(num35 + num36);
				nodes[num34] = value11;
			}
			int frontierDepth = 0;
			int frontierTarget = difficultySettings.frontierTarget;
			int i8 = 0;
			int last = 0;
			while (i8 < nodes.Count)
			{
				Node node7 = nodes[i8];
				if (((int)node7.stepsFromStart != last && node7.totalSteps == shortestPath) || frontierDepth < i8 * (frontierTarget - 3) / nodes.Count)
				{
					frontierDepth++;
					last = (int)node7.stepsFromStart;
				}
				node7.frontierDepth = (byte)frontierDepth;
				nodes[i8] = node7;
				yield return null;
				i8++;
			}
			int t = 4;
			while (t >= 2 && frontierDepth < frontierTarget)
			{
				int i9 = 0;
				int d = 0;
				int last2 = 0;
				while (i9 < nodes.Count * 3 / 4 && frontierDepth < frontierTarget)
				{
					if ((int)nodes[i9].frontierDepth != d)
					{
						int count2 = i9 - last2;
						if (count2 >= t)
						{
							for (int num39 = (last2 + i9) / 2; num39 < nodes.Count; num39++)
							{
								Node value12 = nodes[num39];
								value12.frontierDepth += 1;
								nodes[num39] = value12;
							}
							frontierDepth++;
							yield return null;
						}
						last2 = i9;
						d = (int)nodes[i9].frontierDepth;
					}
					i9++;
				}
				t--;
			}
			if (hasOffshoot)
			{
				Node value13 = nodes[offshootIndex];
				value13.frontierDepth = nodes[connections[(int)value13.connectionIndex0].y].frontierDepth;
				nodes[offshootIndex] = value13;
			}
			if (!campaign)
			{
				yield break;
			}
			campaign.campaignSave.levelStates = new List<LevelState>(nodes.Count);
			HeroUpgradeDefinition metaItem = null;
			List<HeroUpgradeDefinition> currentStartingItems = Profile.userSave.inventory.startingItems;
			int numStartingItems = currentStartingItems.Count;
			List<HeroUpgradeDefinition> list2 = (from u in itemDefs
			where !currentStartingItems.Contains(u)
			select u).ToList<HeroUpgradeDefinition>();
			if (list2.Count > 0)
			{
				list2.ShuffleWeighted(new Dictionary<HeroUpgradeDefinition, float>(itemDefs.Count), (HeroUpgradeDefinition u) => (u.unlockValue != HeroUpgradeDefinition.UnlockValue.Normal) ? ((numStartingItems <= 1) ? 0f : 0.5f) : 1f);
				metaItem = list2[0];
				itemDefs.Remove(metaItem);
			}
			SmartShuffler<HeroUpgradeDefinition> itemShuffle = new SmartShuffler<HeroUpgradeDefinition>(itemDefs);
			int heroIndex = 2;
			int metaItemIndex = (!hasOffshoot) ? this.FindRewardIndex(nodes, nodes.Count / 2, RewardType.Item) : offshootIndex;
			int metaTraitIndex0 = this.FindRewardIndex(nodes, nodes.Count / 6, RewardType.Hero);
			int metaTraitIndex = this.FindRewardIndex(nodes, nodes.Count * 3 / 4, RewardType.Hero);
			for (int i10 = 0; i10 < nodes.Count; i10++)
			{
				Node node8 = nodes[i10];
				LevelState levelState = new LevelState();
				levelState.seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
				levelState.width = node8.width;
				levelState.height = node8.height;
				levelState.frontierDepth = node8.frontierDepth;
				levelState.stepsFromStart = node8.stepsFromStart;
				levelState.stepsFromEnd = node8.stepsFromEnd;
				levelState.pos = node8.pos;
				levelState.index = (byte)i10;
				levelState.unlocked = (i10 == 0);
				for (int num40 = (int)node8.connectionIndex0; num40 < (int)node8.connectionIndex1; num40++)
				{
					levelState.connections.Add(connections[num40].y);
				}
				campaign.campaignSave.levelStates.Add(levelState);
				switch (node8.reward)
				{
				case RewardType.Item:
				{
					bool flag3 = i10 == metaItemIndex;
					if (flag3 && metaItem)
					{
						levelState.item = metaItem;
						levelState.metaReward = true;
					}
					else
					{
						levelState.item = itemShuffle.Get();
					}
					break;
				}
				case RewardType.Hero:
				{
					LevelState levelState2 = levelState;
					int heroId;
					heroIndex = (heroId = heroIndex) + 1;
					levelState2.heroId = heroId;
					bool flag4 = i10 == metaTraitIndex0 || i10 == metaTraitIndex;
					if (flag4)
					{
						levelState.metaReward = true;
					}
					break;
				}
				case RewardType.Checkpoint:
					levelState.checkpointState = LevelState.CheckpointState.Available;
					campaign.campaignSave.hasAnyCheckpoints = true;
					break;
				}
				yield return null;
			}
			int totalGold = difficultySettings.totalGold;
			int num41 = totalGold;
			int num42 = 7;
			float num43 = (float)totalGold / (float)(levelCount - this.eldoradoCount * num42);
			for (int num44 = 0; num44 < nodes.Count; num44++)
			{
				LevelState levelState3 = campaign.campaignSave.levelStates[num44];
				Node node18 = nodes[num44];
				float num45 = num43;
				num45 += (float)node18.width * 0.3f - (float)node18.height * 0.2f;
				num45 += node18.difficulty * 1f;
				if (nodes[num44].reward == RewardType.Eldorado)
				{
					num45 += (float)num42;
				}
				levelState3.coinTarget = (byte)((int)num45);
				num41 -= (int)num45;
			}
			int num46 = 0;
			while (num41 > 0)
			{
				LevelState levelState4 = campaign.campaignSave.levelStates[num46 % campaign.campaignSave.levelStates.Count];
				LevelState levelState5 = levelState4;
				levelState5.coinTarget += 1;
				num41--;
				num46++;
			}
			int num47 = 0;
			while (num41 < 0)
			{
				LevelState levelState6 = campaign.campaignSave.levelStates[num47 % campaign.campaignSave.levelStates.Count];
				if (levelState6.coinTarget > 2)
				{
					LevelState levelState7 = levelState6;
					levelState7.coinTarget -= 1;
					num41++;
				}
				num47++;
			}
			yield break;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000AE4B4 File Offset: 0x000AC8B4
		private int FindRewardIndex(List<Node> nodes, int startIdx, RewardType type)
		{
			int count = nodes.Count;
			int num = startIdx;
			int num2 = num + 1;
			while (num >= 0 || num2 < count)
			{
				if (nodes.IsValidIndex(num) && nodes[num].reward == type)
				{
					return num;
				}
				if (nodes.IsValidIndex(num2) && nodes[num2].reward == type)
				{
					return num2;
				}
				num--;
				num++;
				num2++;
			}
			return -1;
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000AE538 File Offset: 0x000AC938
		private void OnDrawGizmos()
		{
			if (this.graphs == null)
			{
				return;
			}
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = Color.white.SetA(0.2f);
			float num = this.mapHeight / 2f;
			Gizmos.color = Color.white.SetA(0.5f);
			for (int i = 0; i < this.graphs.Count; i++)
			{
				ProtoCampaign protoCampaign = this.graphs[i];
				Gizmos.matrix = Matrix4x4.Translate(new Vector3(0f, this.mapHeight * 1.5f * (float)i, 0f));
				if (protoCampaign.nodes != null)
				{
					foreach (Node node in protoCampaign.nodes)
					{
						Gizmos.color = node.GetColor(this.gizmoType);
						ExtraGizmos.DrawCircleZ(node.pos, node.innerRadius * 0.2f, 8);
						Gizmos.color *= new Color(1f, 1f, 1f, 0.4f);
						ExtraGizmos.DrawCircleZ(node.pos, node.innerRadius, 8);
						Gizmos.color *= new Color(1f, 1f, 1f, 0.6f);
						ExtraGizmos.DrawCircleZ(node.pos, node.outerRadius, 8);
					}
					Gizmos.color = Color.white;
					if (protoCampaign.connections == null || protoCampaign.connections.Count == 0)
					{
						for (int j = 0; j < protoCampaign.nodes.Count - 1; j++)
						{
							Gizmos.DrawLine(protoCampaign.nodes[j].pos, protoCampaign.nodes[j + 1].pos);
						}
					}
				}
				if (protoCampaign.connections != null)
				{
					for (int k = 0; k < protoCampaign.connections.Count; k++)
					{
						Vector2Int vector2Int = protoCampaign.connections[k];
						Node node2 = protoCampaign.nodes[vector2Int.x];
						Node node3 = protoCampaign.nodes[vector2Int.y];
						Node.State state = (Node.State)Mathf.Min((int)node2.state, (int)node3.state);
						if (state != Node.State.Forbidden)
						{
							if (state != Node.State.Idle)
							{
								if (state == Node.State.Chosen)
								{
									Gizmos.color = (node2.GetColor(this.gizmoType) + node3.GetColor(this.gizmoType)) / 2f;
								}
							}
							else
							{
								Gizmos.color = Color.white.SetA(0.5f);
							}
							Gizmos.DrawLine(node2.pos, node3.pos);
							if (vector2Int.y > vector2Int.x)
							{
								int num2 = Mathf.Abs((int)(node3.frontierDepth - node2.frontierDepth));
								if (num2 > 0)
								{
									Vector2 a = (node2.pos + node3.pos) / 2f;
									Vector2 normalized = (node2.pos - node3.pos).normalized;
									Vector2 b = ExtraMath.Rotate2D90(normalized) * 2f;
									for (int l = 0; l < num2; l++)
									{
										float d = ((float)l + 0.5f) / (float)num2 - 0.5f;
										Vector2 a2 = a + normalized * d * 2f;
										Gizmos.DrawLine(a2 + b, a2 - b);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04001E48 RID: 7752
		[SerializeField]
		private float mapHeight = 70f;

		// Token: 0x04001E49 RID: 7753
		[SerializeField]
		private int itemCount = 8;

		// Token: 0x04001E4A RID: 7754
		[SerializeField]
		private int eldoradoCount = 3;

		// Token: 0x04001E4B RID: 7755
		[SerializeField]
		private HeroUpgradeType itemType;

		// Token: 0x04001E4C RID: 7756
		[SerializeField]
		private HeroUpgradeType consumableType;

		// Token: 0x04001E4D RID: 7757
		[SerializeField]
		private IslandSizeField islandSizeField = new IslandSizeField(6, 11, 1, 4);

		// Token: 0x04001E4E RID: 7758
		private List<ProtoCampaign> graphs;

		// Token: 0x04001E4F RID: 7759
		[SerializeField]
		private Node.ColorType gizmoType;

		// Token: 0x020006EB RID: 1771
		private struct RewardStruct
		{
			// Token: 0x06002DAE RID: 11694 RVA: 0x000AE960 File Offset: 0x000ACD60
			public RewardStruct(RewardType type, int credit, int margin0, int margin1, int capacity)
			{
				this.type = type;
				this.credit = credit;
				this.margin0 = margin0;
				this.margin1 = margin1;
				this.steps = ListPool<int>.GetList(capacity);
				for (int i = 0; i < capacity; i++)
				{
					this.steps.Add(capacity);
				}
			}

			// Token: 0x04001E50 RID: 7760
			public RewardType type;

			// Token: 0x04001E51 RID: 7761
			public int credit;

			// Token: 0x04001E52 RID: 7762
			public int margin0;

			// Token: 0x04001E53 RID: 7763
			public int margin1;

			// Token: 0x04001E54 RID: 7764
			public List<int> steps;
		}
	}
}
