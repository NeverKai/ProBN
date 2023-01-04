using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000717 RID: 1815
	public class LinkSpawner : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x06002F18 RID: 12056 RVA: 0x000BB528 File Offset: 0x000B9928
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("LinkSpawner", GenInfo.Mode.interruptable);
			int linkCount = 0;
			foreach (LevelNode levelNode in campaign.levels)
			{
				foreach (LevelNode levelNode2 in levelNode.connectedLevels)
				{
					if (levelNode2.index > levelNode.index)
					{
						linkCount++;
					}
				}
			}
			this.links = new List<LinkSpawner.Link>(linkCount);
			for (int i = 0; i < campaign.levels.Count; i++)
			{
				LevelNode level2 = campaign.levels[i];
				foreach (LevelNode level in level2.connectedLevels)
				{
					if (level.index > level2.index)
					{
						LinkSpawner.Link sr = new LinkSpawner.Link(level2, level);
						this.links.Add(sr);
						yield return info;
					}
				}
			}
			this.arrowGraphic.tris.Capacity = linkCount * 4 * 3;
			this.linkGraphic.tris.Capacity = linkCount * 2 * 3;
			for (int k = 0; k < this.links.Count; k++)
			{
				int num = k * 6;
				this.arrowGraphic.tris.Add(num);
				this.arrowGraphic.tris.Add(num + 3);
				this.arrowGraphic.tris.Add(num + 4);
				this.arrowGraphic.tris.Add(num);
				this.arrowGraphic.tris.Add(num + 4);
				this.arrowGraphic.tris.Add(num + 1);
				this.arrowGraphic.tris.Add(num + 1);
				this.arrowGraphic.tris.Add(num + 4);
				this.arrowGraphic.tris.Add(num + 5);
				this.arrowGraphic.tris.Add(num + 1);
				this.arrowGraphic.tris.Add(num + 5);
				this.arrowGraphic.tris.Add(num + 2);
			}
			this.arrowGraphic.verts.Capacity = this.links.Count * 6;
			this.linkGraphic.verts.Capacity = this.links.Count * 4;
			yield return info;
			for (int l = 0; l < this.links.Count * 4; l++)
			{
				this.linkGraphic.verts.Add(default(UIVertex));
			}
			for (int m = 0; m < this.links.Count * 6; m++)
			{
				this.arrowGraphic.verts.Add(default(UIVertex));
			}
			this.Update();
			campaign.GetComponentInChildren<LineFrontier>(true).mainLineAnim.Subscribe(new Action<float>(this.UpdateLinks));
			Action<int> addLink = delegate(int index)
			{
				int num2 = index * 4;
				this.linkGraphic.tris.Add(num2);
				this.linkGraphic.tris.Add(num2 + 1);
				this.linkGraphic.tris.Add(num2 + 2);
				this.linkGraphic.tris.Add(num2 + 2);
				this.linkGraphic.tris.Add(num2 + 1);
				this.linkGraphic.tris.Add(num2 + 3);
				this.linkGraphic.SetVerticesDirty();
			};
			for (int j = 0; j < this.links.Count; j++)
			{
				LinkSpawner.Link link = this.links[j];
				LevelNode level0 = link.level0;
				LevelNode level1 = link.level1;
				int index = j;
				if (level0.levelVisuals.cloudState.active || level1.levelVisuals.cloudState.active)
				{
					addLink(index);
				}
				else
				{
					AnimatedState cloudState = level0.levelVisuals.cloudState;
					cloudState.OnActivate = (Action)Delegate.Combine(cloudState.OnActivate, new Action(delegate()
					{
						if (!level1.levelVisuals.cloudState.active)
						{
							addLink(index);
						}
					}));
					AnimatedState cloudState2 = level1.levelVisuals.cloudState;
					cloudState2.OnActivate = (Action)Delegate.Combine(cloudState2.OnActivate, new Action(delegate()
					{
						if (!level0.levelVisuals.cloudState.active)
						{
							addLink(index);
						}
					}));
				}
				yield return info;
			}
			this.arrowGraphic.SetAllDirty();
			this.linkGraphic.SetAllDirty();
			base.enabled = true;
			yield return info;
			yield break;
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000BB54C File Offset: 0x000B994C
		private void OnValidate()
		{
			if (this.links != null)
			{
				foreach (LinkSpawner.Link link in this.links)
				{
					link.enabled = true;
				}
				this.UpdateLinks(base.campaign.GetComponentInChildren<LineFrontier>(true).mainLineAnim.current);
				this.Update();
			}
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000BB5D8 File Offset: 0x000B99D8
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000BB5E4 File Offset: 0x000B99E4
		private void UpdateLinks(float depth)
		{
			Rect uvrect = SpriteBounds.GetUVRect(this.linkSprite);
			this.linkGraphic.texture = this.linkSprite.texture;
			Vector2 uv = new Vector2(0f, this.linkPixelWidth - 1f);
			Vector2 uv2 = new Vector2(-1f, 0f);
			for (int i = 0; i < this.links.Count; i++)
			{
				LinkSpawner.Link link = this.links[i];
				int num = i * 4;
				LevelNode level = link.level0;
				LevelNode level2 = link.level1;
				float num2 = Mathf.Clamp01((float)Mathf.Min(level.frontierDepth, level2.frontierDepth) - depth);
				if (num2 > 0f)
				{
					Color c = Color.white.SetA(num2);
					Vector2 pos = link.pos0;
					Vector2 pos2 = link.pos1;
					Vector2 b = ExtraMath.Rotate2D90(link.dir) * (this.linkWidth / 2f) * num2;
					this.linkGraphic.verts[num] = new UIVertex
					{
						position = (pos + b).SetZ(1f),
						uv0 = new Vector2(uvrect.xMin, uvrect.yMin),
						color = c,
						uv1 = uv,
						uv2 = uv2
					};
					this.linkGraphic.verts[num + 1] = new UIVertex
					{
						position = (pos - b).SetZ(1f),
						uv0 = new Vector2(uvrect.xMin, uvrect.yMax),
						color = c,
						uv1 = uv,
						uv2 = uv2
					};
					this.linkGraphic.verts[num + 2] = new UIVertex
					{
						position = (pos2 + b).SetZ(1f),
						uv0 = new Vector2(uvrect.xMax, uvrect.yMin),
						color = c,
						uv1 = uv,
						uv2 = uv2
					};
					this.linkGraphic.verts[num + 3] = new UIVertex
					{
						position = (pos2 - b).SetZ(1f),
						uv0 = new Vector2(uvrect.xMax, uvrect.yMax),
						color = c,
						uv1 = uv,
						uv2 = uv2
					};
				}
				else
				{
					this.linkGraphic.verts[num] = default(UIVertex);
					this.linkGraphic.verts[num + 1] = default(UIVertex);
					this.linkGraphic.verts[num + 2] = default(UIVertex);
					this.linkGraphic.verts[num + 3] = default(UIVertex);
				}
			}
			this.linkGraphic.SetVerticesDirty();
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000BB930 File Offset: 0x000B9D30
		private void Update()
		{
			Rect uvrect = SpriteBounds.GetUVRect(this.arrowSprite);
			this.arrowGraphic.texture = this.arrowSprite.texture;
			bool flag = false;
			for (int i = 0; i < this.links.Count; i++)
			{
				LinkSpawner.Link link = this.links[i];
				if (link.enabled)
				{
					flag = true;
					if ((link.uCurrent - link.uTarget).sqrMagnitude < 1E-05f)
					{
						link.enabled = false;
						link.uCurrent = link.uTarget;
						link.fCurrent = link.fTarget;
					}
					else
					{
						float num = link.speed * Time.deltaTime;
						link.uCurrent = Vector2.MoveTowards(Vector2.Lerp(link.uCurrent, link.uTarget, 4f * num), link.uTarget, 0.3f * num);
						link.fCurrent = Vector2.MoveTowards(Vector2.Lerp(link.fCurrent, link.fTarget, 4f * num), link.fTarget, 0.3f * num);
					}
					int num2 = i * 6;
					Vector2 b = link.uCurrent;
					float num3 = Mathf.Max(b.x, b.y);
					Vector2 pos = link.pos0;
					Vector2 pos2 = link.pos1;
					Vector2 dir = link.dir;
					Vector2 b2 = ExtraMath.Rotate2D90(dir) * this.arrowWidth / 2f;
					if (num3 > 0f)
					{
						b = Vector2.one - b;
						b.x = Mathf.Lerp(uvrect.xMin, uvrect.xMax, b.x);
						b.y = Mathf.Lerp(uvrect.xMin, uvrect.xMax, b.y);
						this.arrowGraphic.verts[num2] = new UIVertex
						{
							position = pos + b2,
							uv0 = new Vector2(b.x, uvrect.yMax),
							color = Color.white
						};
						this.arrowGraphic.verts[num2 + 1] = new UIVertex
						{
							position = pos,
							uv0 = new Vector2(b.x, uvrect.yMin),
							color = Color.white
						};
						this.arrowGraphic.verts[num2 + 2] = new UIVertex
						{
							position = pos - b2,
							uv0 = new Vector2(b.x, uvrect.yMax),
							color = Color.white
						};
						this.arrowGraphic.verts[num2 + 3] = new UIVertex
						{
							position = pos2 + b2,
							uv0 = new Vector2(b.y, uvrect.yMax),
							color = Color.white
						};
						this.arrowGraphic.verts[num2 + 4] = new UIVertex
						{
							position = pos2,
							uv0 = new Vector2(b.y, uvrect.yMin),
							color = Color.white
						};
						this.arrowGraphic.verts[num2 + 5] = new UIVertex
						{
							position = pos2 - b2,
							uv0 = new Vector2(b.y, uvrect.yMax),
							color = Color.white
						};
					}
					else
					{
						this.arrowGraphic.verts[num2] = default(UIVertex);
						this.arrowGraphic.verts[num2 + 1] = default(UIVertex);
						this.arrowGraphic.verts[num2 + 2] = default(UIVertex);
						this.arrowGraphic.verts[num2 + 3] = default(UIVertex);
						this.arrowGraphic.verts[num2 + 4] = default(UIVertex);
						this.arrowGraphic.verts[num2 + 5] = default(UIVertex);
					}
				}
			}
			if (flag)
			{
				this.arrowGraphic.SetAllDirty();
			}
		}

		// Token: 0x04001F49 RID: 8009
		[SerializeField]
		private float linkWidth = 1f;

		// Token: 0x04001F4A RID: 8010
		[SerializeField]
		private float arrowWidth = 2f;

		// Token: 0x04001F4B RID: 8011
		[SerializeField]
		private float linkPixelWidth = 1f;

		// Token: 0x04001F4C RID: 8012
		[SerializeField]
		private float arrowFraction0 = 0.3f;

		// Token: 0x04001F4D RID: 8013
		[SerializeField]
		private float arrowFraction1 = 0.6f;

		// Token: 0x04001F4E RID: 8014
		[SerializeField]
		private Sprite arrowSprite;

		// Token: 0x04001F4F RID: 8015
		[SerializeField]
		private Sprite linkSprite;

		// Token: 0x04001F50 RID: 8016
		[SerializeField]
		private AgentStateRoot root;

		// Token: 0x04001F51 RID: 8017
		[SerializeField]
		private UIVertexListGraphic arrowGraphic;

		// Token: 0x04001F52 RID: 8018
		[SerializeField]
		private UIVertexListGraphic linkGraphic;

		// Token: 0x04001F53 RID: 8019
		private List<LinkSpawner.Link> links;

		// Token: 0x02000718 RID: 1816
		public class Link : IComparable<LinkSpawner.Link>
		{
			// Token: 0x06002F1D RID: 12061 RVA: 0x000BBDF4 File Offset: 0x000BA1F4
			public Link(LevelNode level0, LevelNode level1)
			{
				this.level0 = level0;
				this.level1 = level1;
				this.pos0 = level0.pos;
				this.pos1 = level1.pos;
				this.dir = (this.pos1 - this.pos0).normalized;
				this.pos0 += level0.innerRadius * 0.3f * this.dir;
				this.pos1 -= level1.innerRadius * 0.3f * this.dir;
				this.name = level0.name + " - " + level1.name;
				level0.links[level0.levelState.connections.IndexOf(level1.index)] = this;
				level1.links[level1.levelState.connections.IndexOf(level0.index)] = this;
				this.uTarget = this.GetNewUTarget();
				this.uCurrent = this.uTarget;
				this.SetFTarget();
				this.fCurrent = this.uCurrent;
				this.enabled = true;
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x06002F1E RID: 12062 RVA: 0x000BBF37 File Offset: 0x000BA337
			public bool bothPlayed
			{
				get
				{
					return this.level0.levelState.playCount > 0 && this.level1.levelState.playCount > 0;
				}
			}

			// Token: 0x06002F1F RID: 12063 RVA: 0x000BBF68 File Offset: 0x000BA368
			private Vector2 GetNewUTarget()
			{
				bool flag = this.level0.levelState.playCount > 0;
				bool flag2 = this.level1.levelState.playCount > 0;
				bool flag3 = this.level0.IsBehindFrontier();
				bool flag4 = this.level1.IsBehindFrontier();
				bool flag5 = this.level0.levelState.unlocked && !flag3;
				bool flag6 = this.level1.levelState.unlocked && !flag4;
				if (flag && flag2)
				{
					return Vector2.one;
				}
				if (flag && flag6)
				{
					float num = 0f;
					if (flag3)
					{
						num += 0.5f;
					}
					if (this.level0.IsBehindNextFrontier())
					{
						num += 0.5f;
					}
					return Vector2.Lerp(new Vector2(1f, 0f), Vector2.one * num, 0.2f);
				}
				if (flag2 && flag5)
				{
					float num2 = 0f;
					if (flag4)
					{
						num2 += 0.5f;
					}
					if (this.level1.IsBehindNextFrontier())
					{
						num2 += 0.5f;
					}
					return Vector2.Lerp(new Vector2(0f, 1f), Vector2.one * num2, 0.2f);
				}
				return Vector2.zero;
			}

			// Token: 0x06002F20 RID: 12064 RVA: 0x000BC0D0 File Offset: 0x000BA4D0
			private void SetFTarget()
			{
				this.fTarget.x = (float)((!this.level0.IsBehindFrontier()) ? 1 : 0);
				this.fTarget.y = (float)((!this.level1.IsBehindFrontier()) ? 1 : 0);
			}

			// Token: 0x06002F21 RID: 12065 RVA: 0x000BC124 File Offset: 0x000BA524
			public void Update(float speed = 1f)
			{
				Vector2 newUTarget = this.GetNewUTarget();
				this.SetFTarget();
				this.speed = speed;
				bool flag = this.uTarget != newUTarget;
				this.uTarget = newUTarget;
				this.enabled = (this.uTarget != this.uCurrent);
			}

			// Token: 0x06002F22 RID: 12066 RVA: 0x000BC170 File Offset: 0x000BA570
			public LevelNode GetOtherLevel(LevelNode level)
			{
				return (!(level == this.level0)) ? this.level0 : this.level1;
			}

			// Token: 0x06002F23 RID: 12067 RVA: 0x000BC194 File Offset: 0x000BA594
			private void SubscribeToState(AnimatedState state)
			{
				if (state.anim.state.active)
				{
					this.stateCount++;
				}
				AgentState state2 = state.anim.state;
				state2.OnChange = (Action<bool>)Delegate.Combine(state2.OnChange, new Action<bool>(this.StateChange));
			}

			// Token: 0x06002F24 RID: 12068 RVA: 0x000BC1F0 File Offset: 0x000BA5F0
			private void StateChange(bool change)
			{
				this.stateCount += ((!change) ? -1 : 1);
			}

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000BC20C File Offset: 0x000BA60C
			private float sort
			{
				get
				{
					return this.level0.pos.x + this.level1.pos.x;
				}
			}

			// Token: 0x06002F26 RID: 12070 RVA: 0x000BC240 File Offset: 0x000BA640
			int IComparable<LinkSpawner.Link>.CompareTo(LinkSpawner.Link other)
			{
				return this.sort.CompareTo(other.sort);
			}

			// Token: 0x04001F54 RID: 8020
			public readonly LevelNode level0;

			// Token: 0x04001F55 RID: 8021
			public readonly LevelNode level1;

			// Token: 0x04001F56 RID: 8022
			public readonly Vector2 pos0;

			// Token: 0x04001F57 RID: 8023
			public readonly Vector2 pos1;

			// Token: 0x04001F58 RID: 8024
			public readonly Vector2 dir;

			// Token: 0x04001F59 RID: 8025
			public readonly string name;

			// Token: 0x04001F5A RID: 8026
			public Vector2 uCurrent;

			// Token: 0x04001F5B RID: 8027
			public Vector2 uTarget;

			// Token: 0x04001F5C RID: 8028
			public Vector2 fCurrent;

			// Token: 0x04001F5D RID: 8029
			public Vector2 fTarget;

			// Token: 0x04001F5E RID: 8030
			public bool enabled = true;

			// Token: 0x04001F5F RID: 8031
			public float speed = 1f;

			// Token: 0x04001F60 RID: 8032
			public int stateCount;
		}
	}
}
