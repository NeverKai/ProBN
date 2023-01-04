using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace I2.Loc.SimpleJSON
{
	// Token: 0x020003D4 RID: 980
	public class JSONNode
	{
		// Token: 0x060015F3 RID: 5619 RVA: 0x00032A49 File Offset: 0x00030E49
		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x17000126 RID: 294
		public virtual JSONNode this[int aIndex]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000127 RID: 295
		public virtual JSONNode this[string aKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00032A55 File Offset: 0x00030E55
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x00032A5C File Offset: 0x00030E5C
		public virtual string Value
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00032A5E File Offset: 0x00030E5E
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00032A61 File Offset: 0x00030E61
		public virtual void Add(JSONNode aItem)
		{
			this.Add(string.Empty, aItem);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00032A6F File Offset: 0x00030E6F
		public virtual JSONNode Remove(string aKey)
		{
			return null;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00032A72 File Offset: 0x00030E72
		public virtual JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00032A75 File Offset: 0x00030E75
		public virtual JSONNode Remove(JSONNode aNode)
		{
			return aNode;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00032A78 File Offset: 0x00030E78
		public virtual IEnumerable<JSONNode> Childs
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00032A94 File Offset: 0x00030E94
		public IEnumerable<JSONNode> DeepChilds
		{
			get
			{
				foreach (JSONNode C in this.Childs)
				{
					foreach (JSONNode D in C.DeepChilds)
					{
						yield return D;
					}
				}
				yield break;
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00032AB7 File Offset: 0x00030EB7
		public override string ToString()
		{
			return "JSONNode";
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00032ABE File Offset: 0x00030EBE
		public virtual string ToString(string aPrefix)
		{
			return "JSONNode";
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00032AC8 File Offset: 0x00030EC8
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x00032AEC File Offset: 0x00030EEC
		public virtual int AsInt
		{
			get
			{
				int result = 0;
				if (int.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00032B04 File Offset: 0x00030F04
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x00032B30 File Offset: 0x00030F30
		public virtual float AsFloat
		{
			get
			{
				float result = 0f;
				if (float.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0f;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00032B48 File Offset: 0x00030F48
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x00032B7C File Offset: 0x00030F7C
		public virtual double AsDouble
		{
			get
			{
				double result = 0.0;
				if (double.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0.0;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00032B94 File Offset: 0x00030F94
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x00032BC5 File Offset: 0x00030FC5
		public virtual bool AsBool
		{
			get
			{
				bool result = false;
				if (bool.TryParse(this.Value, out result))
				{
					return result;
				}
				return !string.IsNullOrEmpty(this.Value);
			}
			set
			{
				this.Value = ((!value) ? "false" : "true");
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00032BE2 File Offset: 0x00030FE2
		public virtual JSONArray AsArray
		{
			get
			{
				return this as JSONArray;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00032BEA File Offset: 0x00030FEA
		public virtual JSONClass AsObject
		{
			get
			{
				return this as JSONClass;
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00032BF2 File Offset: 0x00030FF2
		public static implicit operator JSONNode(string s)
		{
			return new JSONData(s);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00032BFA File Offset: 0x00030FFA
		public static implicit operator string(JSONNode d)
		{
			return (!(d == null)) ? d.Value : null;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00032C14 File Offset: 0x00031014
		public static bool operator ==(JSONNode a, object b)
		{
			return (b == null && a is JSONLazyCreator) || object.ReferenceEquals(a, b);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00032C30 File Offset: 0x00031030
		public static bool operator !=(JSONNode a, object b)
		{
			return !(a == b);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00032C3C File Offset: 0x0003103C
		public override bool Equals(object obj)
		{
			return object.ReferenceEquals(this, obj);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00032C45 File Offset: 0x00031045
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00032C50 File Offset: 0x00031050
		internal static string Escape(string aText)
		{
			string text = string.Empty;
			foreach (char c in aText)
			{
				switch (c)
				{
				case '\b':
					text += "\\b";
					break;
				case '\t':
					text += "\\t";
					break;
				case '\n':
					text += "\\n";
					break;
				default:
					if (c != '"')
					{
						if (c != '\\')
						{
							text += c;
						}
						else
						{
							text += "\\\\";
						}
					}
					else
					{
						text += "\\\"";
					}
					break;
				case '\f':
					text += "\\f";
					break;
				case '\r':
					text += "\\r";
					break;
				}
			}
			return text;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00032D44 File Offset: 0x00031144
		public static JSONNode Parse(string aJSON)
		{
			Stack<JSONNode> stack = new Stack<JSONNode>();
			JSONNode jsonnode = null;
			int i = 0;
			string text = string.Empty;
			string text2 = string.Empty;
			bool flag = false;
			while (i < aJSON.Length)
			{
				char c = aJSON[i];
				switch (c)
				{
				case '\t':
					goto IL_333;
				case '\n':
				case '\r':
					break;
				default:
					switch (c)
					{
					case '[':
						if (flag)
						{
							text += aJSON[i];
							goto IL_45C;
						}
						stack.Push(new JSONArray());
						if (jsonnode != null)
						{
							text2 = text2.Trim();
							if (jsonnode is JSONArray)
							{
								jsonnode.Add(stack.Peek());
							}
							else if (text2 != string.Empty)
							{
								jsonnode.Add(text2, stack.Peek());
							}
						}
						text2 = string.Empty;
						text = string.Empty;
						jsonnode = stack.Peek();
						goto IL_45C;
					case '\\':
						i++;
						if (flag)
						{
							char c2 = aJSON[i];
							switch (c2)
							{
							case 'r':
								text += '\r';
								break;
							default:
								if (c2 != 'b')
								{
									if (c2 != 'f')
									{
										if (c2 != 'n')
										{
											text += c2;
										}
										else
										{
											text += '\n';
										}
									}
									else
									{
										text += '\f';
									}
								}
								else
								{
									text += '\b';
								}
								break;
							case 't':
								text += '\t';
								break;
							case 'u':
							{
								string s = aJSON.Substring(i + 1, 4);
								text += (char)int.Parse(s, NumberStyles.AllowHexSpecifier);
								i += 4;
								break;
							}
							}
						}
						goto IL_45C;
					case ']':
						break;
					default:
						switch (c)
						{
						case ' ':
							goto IL_333;
						default:
							switch (c)
							{
							case '{':
								if (flag)
								{
									text += aJSON[i];
									goto IL_45C;
								}
								stack.Push(new JSONClass());
								if (jsonnode != null)
								{
									text2 = text2.Trim();
									if (jsonnode is JSONArray)
									{
										jsonnode.Add(stack.Peek());
									}
									else if (text2 != string.Empty)
									{
										jsonnode.Add(text2, stack.Peek());
									}
								}
								text2 = string.Empty;
								text = string.Empty;
								jsonnode = stack.Peek();
								goto IL_45C;
							default:
								if (c != ',')
								{
									if (c != ':')
									{
										text += aJSON[i];
										goto IL_45C;
									}
									if (flag)
									{
										text += aJSON[i];
										goto IL_45C;
									}
									text2 = text;
									text = string.Empty;
									goto IL_45C;
								}
								else
								{
									if (flag)
									{
										text += aJSON[i];
										goto IL_45C;
									}
									if (text != string.Empty)
									{
										if (jsonnode is JSONArray)
										{
											jsonnode.Add(text);
										}
										else if (text2 != string.Empty)
										{
											jsonnode.Add(text2, text);
										}
									}
									text2 = string.Empty;
									text = string.Empty;
									goto IL_45C;
								}
								break;
							case '}':
								break;
							}
							break;
						case '"':
							flag ^= true;
							goto IL_45C;
						}
						break;
					}
					if (flag)
					{
						text += aJSON[i];
					}
					else
					{
						if (stack.Count == 0)
						{
							throw new Exception("JSON Parse: Too many closing brackets");
						}
						stack.Pop();
						if (text != string.Empty)
						{
							text2 = text2.Trim();
							if (jsonnode is JSONArray)
							{
								jsonnode.Add(text);
							}
							else if (text2 != string.Empty)
							{
								jsonnode.Add(text2, text);
							}
						}
						text2 = string.Empty;
						text = string.Empty;
						if (stack.Count > 0)
						{
							jsonnode = stack.Peek();
						}
					}
					break;
				}
				IL_45C:
				i++;
				continue;
				IL_333:
				if (flag)
				{
					text += aJSON[i];
				}
				goto IL_45C;
			}
			if (flag)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			return jsonnode;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000331D0 File Offset: 0x000315D0
		public virtual void Serialize(BinaryWriter aWriter)
		{
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000331D4 File Offset: 0x000315D4
		public void SaveToStream(Stream aData)
		{
			BinaryWriter aWriter = new BinaryWriter(aData);
			this.Serialize(aWriter);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x000331EF File Offset: 0x000315EF
		public void SaveToCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000331FB File Offset: 0x000315FB
		public void SaveToCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00033207 File Offset: 0x00031607
		public string SaveToCompressedBase64()
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00033214 File Offset: 0x00031614
		public void SaveToFile(string aFileName)
		{
			Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
			using (FileStream fileStream = File.OpenWrite(aFileName))
			{
				this.SaveToStream(fileStream);
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00033268 File Offset: 0x00031668
		public string SaveToBase64()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.SaveToStream(memoryStream);
				memoryStream.Position = 0L;
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000332BC File Offset: 0x000316BC
		public static JSONNode Deserialize(BinaryReader aReader)
		{
			JSONBinaryTag jsonbinaryTag = (JSONBinaryTag)aReader.ReadByte();
			switch (jsonbinaryTag)
			{
			case JSONBinaryTag.Array:
			{
				int num = aReader.ReadInt32();
				JSONArray jsonarray = new JSONArray();
				for (int i = 0; i < num; i++)
				{
					jsonarray.Add(JSONNode.Deserialize(aReader));
				}
				return jsonarray;
			}
			case JSONBinaryTag.Class:
			{
				int num2 = aReader.ReadInt32();
				JSONClass jsonclass = new JSONClass();
				for (int j = 0; j < num2; j++)
				{
					string aKey = aReader.ReadString();
					JSONNode aItem = JSONNode.Deserialize(aReader);
					jsonclass.Add(aKey, aItem);
				}
				return jsonclass;
			}
			case JSONBinaryTag.Value:
				return new JSONData(aReader.ReadString());
			case JSONBinaryTag.IntValue:
				return new JSONData(aReader.ReadInt32());
			case JSONBinaryTag.DoubleValue:
				return new JSONData(aReader.ReadDouble());
			case JSONBinaryTag.BoolValue:
				return new JSONData(aReader.ReadBoolean());
			case JSONBinaryTag.FloatValue:
				return new JSONData(aReader.ReadSingle());
			default:
				throw new Exception("Error deserializing JSON. Unknown tag: " + jsonbinaryTag);
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000333BB File Offset: 0x000317BB
		public static JSONNode LoadFromCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000333C7 File Offset: 0x000317C7
		public static JSONNode LoadFromCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000333D3 File Offset: 0x000317D3
		public static JSONNode LoadFromCompressedBase64(string aBase64)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x000333E0 File Offset: 0x000317E0
		public static JSONNode LoadFromStream(Stream aData)
		{
			JSONNode result;
			using (BinaryReader binaryReader = new BinaryReader(aData))
			{
				result = JSONNode.Deserialize(binaryReader);
			}
			return result;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00033420 File Offset: 0x00031820
		public static JSONNode LoadFromFile(string aFileName)
		{
			JSONNode result;
			using (FileStream fileStream = File.OpenRead(aFileName))
			{
				result = JSONNode.LoadFromStream(fileStream);
			}
			return result;
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00033460 File Offset: 0x00031860
		public static JSONNode LoadFromBase64(string aBase64)
		{
			byte[] buffer = Convert.FromBase64String(aBase64);
			return JSONNode.LoadFromStream(new MemoryStream(buffer)
			{
				Position = 0L
			});
		}
	}
}
