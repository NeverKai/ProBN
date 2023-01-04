using System;

namespace I2.Loc.SimpleJSON
{
	// Token: 0x020003D8 RID: 984
	internal class JSONLazyCreator : JSONNode
	{
		// Token: 0x0600164A RID: 5706 RVA: 0x00034643 File Offset: 0x00032A43
		public JSONLazyCreator(JSONNode aNode)
		{
			this.m_Node = aNode;
			this.m_Key = null;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00034659 File Offset: 0x00032A59
		public JSONLazyCreator(JSONNode aNode, string aKey)
		{
			this.m_Node = aNode;
			this.m_Key = aKey;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0003466F File Offset: 0x00032A6F
		private void Set(JSONNode aVal)
		{
			if (this.m_Key == null)
			{
				this.m_Node.Add(aVal);
			}
			else
			{
				this.m_Node.Add(this.m_Key, aVal);
			}
			this.m_Node = null;
		}

		// Token: 0x1700013B RID: 315
		public override JSONNode this[int aIndex]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.Set(new JSONArray
				{
					value
				});
			}
		}

		// Token: 0x1700013C RID: 316
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				this.Set(new JSONClass
				{
					{
						aKey,
						value
					}
				});
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00034700 File Offset: 0x00032B00
		public override void Add(JSONNode aItem)
		{
			this.Set(new JSONArray
			{
				aItem
			});
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00034724 File Offset: 0x00032B24
		public override void Add(string aKey, JSONNode aItem)
		{
			this.Set(new JSONClass
			{
				{
					aKey,
					aItem
				}
			});
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00034746 File Offset: 0x00032B46
		public static bool operator ==(JSONLazyCreator a, object b)
		{
			return b == null || object.ReferenceEquals(a, b);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00034757 File Offset: 0x00032B57
		public static bool operator !=(JSONLazyCreator a, object b)
		{
			return !(a == b);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00034763 File Offset: 0x00032B63
		public override bool Equals(object obj)
		{
			return obj == null || object.ReferenceEquals(this, obj);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00034774 File Offset: 0x00032B74
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0003477C File Offset: 0x00032B7C
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00034783 File Offset: 0x00032B83
		public override string ToString(string aPrefix)
		{
			return string.Empty;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0003478C File Offset: 0x00032B8C
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x000347A8 File Offset: 0x00032BA8
		public override int AsInt
		{
			get
			{
				JSONData aVal = new JSONData(0);
				this.Set(aVal);
				return 0;
			}
			set
			{
				JSONData aVal = new JSONData(value);
				this.Set(aVal);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x000347C4 File Offset: 0x00032BC4
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x000347E8 File Offset: 0x00032BE8
		public override float AsFloat
		{
			get
			{
				JSONData aVal = new JSONData(0f);
				this.Set(aVal);
				return 0f;
			}
			set
			{
				JSONData aVal = new JSONData(value);
				this.Set(aVal);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x00034804 File Offset: 0x00032C04
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x00034830 File Offset: 0x00032C30
		public override double AsDouble
		{
			get
			{
				JSONData aVal = new JSONData(0.0);
				this.Set(aVal);
				return 0.0;
			}
			set
			{
				JSONData aVal = new JSONData(value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0003484C File Offset: 0x00032C4C
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x00034868 File Offset: 0x00032C68
		public override bool AsBool
		{
			get
			{
				JSONData aVal = new JSONData(false);
				this.Set(aVal);
				return false;
			}
			set
			{
				JSONData aVal = new JSONData(value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00034884 File Offset: 0x00032C84
		public override JSONArray AsArray
		{
			get
			{
				JSONArray jsonarray = new JSONArray();
				this.Set(jsonarray);
				return jsonarray;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x000348A0 File Offset: 0x00032CA0
		public override JSONClass AsObject
		{
			get
			{
				JSONClass jsonclass = new JSONClass();
				this.Set(jsonclass);
				return jsonclass;
			}
		}

		// Token: 0x04000DD6 RID: 3542
		private JSONNode m_Node;

		// Token: 0x04000DD7 RID: 3543
		private string m_Key;
	}
}
