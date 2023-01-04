using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace ControlledRandomness
{
	// Token: 0x020004F9 RID: 1273
	[Serializable]
	public class PropertyBank : IGenOption
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x000576CF File Offset: 0x00055ACF
		IEnumerable<Range> IGenOption.ranges
		{
			get
			{
				return this.ranges;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x000576D7 File Offset: 0x00055AD7
		IEnumerable<Tag> IGenOption.tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x000576DF File Offset: 0x00055ADF
		float IGenOption.probability
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000576E8 File Offset: 0x00055AE8
		public bool Allowed(Tag tag)
		{
			return this.tags.All((Tag t) => t.Compatible(tag));
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x00057719 File Offset: 0x00055B19
		public bool Allowed(IGenOption option)
		{
			return !option.tags.Any((Tag a) => this.tags.Any((Tag b) => !a.Compatible(b))) && !option.ranges.Any((Range a) => this.ranges.Any((Range b) => !a.Compatible(b)));
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00057758 File Offset: 0x00055B58
		public bool TryGetValue(string key, ref string value)
		{
			value = this.tags.FirstOrDefault((Tag x) => x.key == key).value;
			return !(value == null);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000577A4 File Offset: 0x00055BA4
		public string GetValue(string key)
		{
			return this.tags.First((Tag x) => x.key == key).value;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000577E0 File Offset: 0x00055BE0
		public void AddTag(Tag tag)
		{
			if (!this.tags.Any((Tag x) => x.key == tag.key))
			{
				this.tags.Add(tag);
			}
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00057827 File Offset: 0x00055C27
		public void AddTag(string key, string value)
		{
			this.AddTag(new Tag(key, value));
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00057838 File Offset: 0x00055C38
		public void AddRange(Range range)
		{
			for (int i = 0; i < this.ranges.Count; i++)
			{
				Range value = this.ranges[i];
				if (range.key == value.key)
				{
					value.min = Mathf.Max(value.min, range.min);
					value.max = Mathf.Min(value.max, range.max);
					this.ranges[i] = value;
					return;
				}
			}
			this.ranges.Add(range);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000578D4 File Offset: 0x00055CD4
		public void AddRange(string key, float min, float max)
		{
			this.AddRange(new Range(key, min, max));
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000578E4 File Offset: 0x00055CE4
		public void AddRange(string key, float value)
		{
			this.AddRange(new Range(key, value, value));
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000578F4 File Offset: 0x00055CF4
		private static string CountKey(Tag tag)
		{
			return tag.key + tag.value;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0005790C File Offset: 0x00055D0C
		public IGenOption Apply(IGenOption option)
		{
			foreach (Tag tag in option.tags)
			{
				this.AddTag(tag);
			}
			foreach (Range range in option.ranges)
			{
				this.AddRange(range);
			}
			this.probability *= option.probability;
			return option;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000579C4 File Offset: 0x00055DC4
		public float PickFloat(string key, Func<float> function)
		{
			key += "Float";
			if (this.tags.Any((Tag t) => t.key == key))
			{
				return float.Parse(this.tags.First((Tag t) => t.key == key).value, CultureInfo.InvariantCulture);
			}
			float result = function();
			this.tags.Add(new Tag(key, result.ToString(CultureInfo.InvariantCulture)));
			return result;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00057A64 File Offset: 0x00055E64
		public string PickString(string key, Func<string> function)
		{
			if (this.tags.Any((Tag t) => t.key == key))
			{
				return this.tags.First((Tag t) => t.key == key).value;
			}
			string text = function();
			this.tags.Add(new Tag(key, text));
			return text;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00057ADC File Offset: 0x00055EDC
		public bool PickBool(string key, Func<bool> function)
		{
			key += "Bool";
			if (this.tags.Any((Tag t) => t.key == key))
			{
				return this.tags.First((Tag t) => t.key == key).value == "true";
			}
			bool flag = function();
			this.tags.Add(new Tag(key, (!flag) ? "false" : "true"));
			return flag;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00057B85 File Offset: 0x00055F85
		public Color SetColor(string key, Color color)
		{
			key += "Color";
			this.tags.Add(new Tag(key, '#' + ColorUtility.ToHtmlStringRGBA(color)));
			return color;
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00057BB8 File Offset: 0x00055FB8
		public void SetBool(string key, bool value)
		{
			key += "Bool";
			string value2 = (!value) ? "false" : "true";
			for (int i = 0; i < this.tags.Count; i++)
			{
				if (this.tags[i].key == key)
				{
					this.tags[i] = new Tag(key, value2);
					return;
				}
			}
			this.tags.Add(new Tag(key, value2));
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00057C4C File Offset: 0x0005604C
		public Color PickColor(string key, Func<Color> function)
		{
			key += "Color";
			if (this.tags.Any((Tag t) => t.key == key))
			{
				Color result;
				ColorUtility.TryParseHtmlString(this.tags.First((Tag t) => t.key == key).value, out result);
				return result;
			}
			Color color = function();
			this.tags.Add(new Tag(key, '#' + ColorUtility.ToHtmlStringRGBA(color)));
			return color;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00057CF4 File Offset: 0x000560F4
		public bool TryGetColor(string key, ref Color color)
		{
			key += "Color";
			return this.tags.Any((Tag t) => t.key == key) && ColorUtility.TryParseHtmlString(this.tags.First((Tag t) => t.key == key).value, out color);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00057D68 File Offset: 0x00056168
		public T Pick<T>(IEnumerable<T> options, float randomValue) where T : IGenOption
		{
			float num = 0f;
			foreach (T t in options)
			{
				if (this.Allowed(t))
				{
					num += Mathf.Max(0f, t.probability);
				}
			}
			num *= randomValue;
			IGenOption genOption = null;
			T result = default(T);
			foreach (T t2 in options)
			{
				if (this.Allowed(t2))
				{
					num -= Mathf.Max(0f, t2.probability);
					if (num <= 0f)
					{
						genOption = t2;
						result = t2;
						break;
					}
				}
			}
			if (genOption != null)
			{
				this.Apply(genOption);
			}
			return result;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00057E94 File Offset: 0x00056294
		public T Pick<T>(IEnumerable<T> options, Func<T, Tag> additionalTags, float randomValue) where T : IGenOption
		{
			float num = 0f;
			foreach (T t in options)
			{
				if (this.Allowed(t) && this.Allowed(additionalTags(t)))
				{
					num += Mathf.Max(0f, t.probability);
				}
			}
			num *= randomValue;
			IGenOption genOption = null;
			T t2 = default(T);
			foreach (T t3 in options)
			{
				if (this.Allowed(t3) && this.Allowed(additionalTags(t3)))
				{
					num -= Mathf.Max(0f, t3.probability);
					if (num <= 0f)
					{
						genOption = t3;
						t2 = t3;
						break;
					}
				}
			}
			if (genOption != null)
			{
				this.Apply(genOption);
				this.AddTag(additionalTags(t2));
			}
			return t2;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00057FF4 File Offset: 0x000563F4
		public void Clear()
		{
			this.tags.Clear();
			this.ranges.Clear();
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x0005800C File Offset: 0x0005640C
		public string GetPrint()
		{
			List<string> list = new List<string>();
			list.AddRange(from x in this.tags
			select x.ToString());
			list.AddRange(from x in this.ranges
			select x.ToString());
			return string.Join("\n", list.ToArray());
		}

		// Token: 0x04001426 RID: 5158
		[SerializeField]
		private List<Tag> tags = new List<Tag>();

		// Token: 0x04001427 RID: 5159
		[SerializeField]
		private List<Range> ranges = new List<Range>();

		// Token: 0x04001428 RID: 5160
		[SerializeField]
		private float probability = 1f;

		// Token: 0x020004FA RID: 1274
		[Serializable]
		public class Item
		{
			// Token: 0x06002099 RID: 8345 RVA: 0x0005810F File Offset: 0x0005650F
			public Item(string key, string value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x0400142B RID: 5163
			[HideInInspector]
			public string key;

			// Token: 0x0400142C RID: 5164
			public string value;
		}
	}
}
