using System;
using System.IO;

namespace I2.Loc.SimpleJSON
{
	// Token: 0x020003D7 RID: 983
	public class JSONData : JSONNode
	{
		// Token: 0x06001640 RID: 5696 RVA: 0x000344AB File Offset: 0x000328AB
		public JSONData(string aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000344BA File Offset: 0x000328BA
		public JSONData(float aData)
		{
			this.AsFloat = aData;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000344C9 File Offset: 0x000328C9
		public JSONData(double aData)
		{
			this.AsDouble = aData;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000344D8 File Offset: 0x000328D8
		public JSONData(bool aData)
		{
			this.AsBool = aData;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000344E7 File Offset: 0x000328E7
		public JSONData(int aData)
		{
			this.AsInt = aData;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x000344F6 File Offset: 0x000328F6
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x000344FE File Offset: 0x000328FE
		public override string Value
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00034507 File Offset: 0x00032907
		public override string ToString()
		{
			return "\"" + JSONNode.Escape(this.m_Data) + "\"";
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00034523 File Offset: 0x00032923
		public override string ToString(string aPrefix)
		{
			return "\"" + JSONNode.Escape(this.m_Data) + "\"";
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00034540 File Offset: 0x00032940
		public override void Serialize(BinaryWriter aWriter)
		{
			JSONData jsondata = new JSONData(string.Empty);
			jsondata.AsInt = this.AsInt;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write(4);
				aWriter.Write(this.AsInt);
				return;
			}
			jsondata.AsFloat = this.AsFloat;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write(7);
				aWriter.Write(this.AsFloat);
				return;
			}
			jsondata.AsDouble = this.AsDouble;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write(5);
				aWriter.Write(this.AsDouble);
				return;
			}
			jsondata.AsBool = this.AsBool;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write(6);
				aWriter.Write(this.AsBool);
				return;
			}
			aWriter.Write(3);
			aWriter.Write(this.m_Data);
		}

		// Token: 0x04000DD5 RID: 3541
		private string m_Data;
	}
}
