using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace I2.Loc.SimpleJSON
{
	// Token: 0x020003D5 RID: 981
	public class JSONArray : JSONNode, IEnumerable
	{
		// Token: 0x17000132 RID: 306
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					return new JSONLazyCreator(this);
				}
				return this.m_List[aIndex];
			}
			set
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					this.m_List.Add(value);
				}
				else
				{
					this.m_List[aIndex] = value;
				}
			}
		}

		// Token: 0x17000133 RID: 307
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.m_List.Add(value);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x000337AE File Offset: 0x00031BAE
		public override int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000337BB File Offset: 0x00031BBB
		public override void Add(string aKey, JSONNode aItem)
		{
			this.m_List.Add(aItem);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000337CC File Offset: 0x00031BCC
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_List.Count)
			{
				return null;
			}
			JSONNode result = this.m_List[aIndex];
			this.m_List.RemoveAt(aIndex);
			return result;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0003380D File Offset: 0x00031C0D
		public override JSONNode Remove(JSONNode aNode)
		{
			this.m_List.Remove(aNode);
			return aNode;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x00033820 File Offset: 0x00031C20
		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				foreach (JSONNode N in this.m_List)
				{
					yield return N;
				}
				yield break;
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00033844 File Offset: 0x00031C44
		public IEnumerator GetEnumerator()
		{
			foreach (JSONNode N in this.m_List)
			{
				yield return N;
			}
			yield break;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00033860 File Offset: 0x00031C60
		public override string ToString()
		{
			string text = "[ ";
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (text.Length > 2)
				{
					text += ", ";
				}
				text += jsonnode.ToString();
			}
			text += " ]";
			return text;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000338F0 File Offset: 0x00031CF0
		public override string ToString(string aPrefix)
		{
			string text = "[ ";
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (text.Length > 3)
				{
					text += ", ";
				}
				text = text + "\n" + aPrefix + "   ";
				text += jsonnode.ToString(aPrefix + "   ");
			}
			text = text + "\n" + aPrefix + "]";
			return text;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000339A0 File Offset: 0x00031DA0
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(1);
			aWriter.Write(this.m_List.Count);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				this.m_List[i].Serialize(aWriter);
			}
		}

		// Token: 0x04000DD3 RID: 3539
		private List<JSONNode> m_List = new List<JSONNode>();
	}
}
