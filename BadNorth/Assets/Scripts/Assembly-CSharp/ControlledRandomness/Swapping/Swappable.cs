using System;
using System.Collections.Generic;
using System.Linq;
using Spriteshop;
using UnityEngine;

namespace ControlledRandomness.Swapping
{
	// Token: 0x02000501 RID: 1281
	public class Swappable : SeededComponent, IGenOption
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x00058CF4 File Offset: 0x000570F4
		public PsdGroup psdGroup
		{
			get
			{
				if (!this._psdGroup)
				{
					this._psdGroup = base.GetComponent<PsdGroup>();
				}
				return this._psdGroup;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x00058D18 File Offset: 0x00057118
		private string[] splitName
		{
			get
			{
				if (this._splitName == null || this._splitName.Length == 0)
				{
					if (this.psdGroup && this.psdGroup.splitName.Length > 0)
					{
						this._splitName = this.psdGroup.splitName;
					}
					else
					{
						this._splitName = base.name.Split(Swappable.splitChar, StringSplitOptions.RemoveEmptyEntries);
					}
				}
				return this._splitName;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x00058D94 File Offset: 0x00057194
		// (set) Token: 0x060020B9 RID: 8377 RVA: 0x00058E0C File Offset: 0x0005720C
		public string extraKey
		{
			get
			{
				if (!this.hasExtraKey)
				{
					string text = this.splitName.FirstOrDefault((string x) => x[0] == '$');
					if (text != null)
					{
						this._extraKey = text.Remove(0, 1);
					}
					else
					{
						this._extraKey = string.Empty;
					}
					this.hasExtraKey = true;
				}
				return this._extraKey;
			}
			set
			{
				this._extraKey = value;
				this.hasExtraKey = true;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x00058E1C File Offset: 0x0005721C
		// (set) Token: 0x060020BB RID: 8379 RVA: 0x00058E44 File Offset: 0x00057244
		public string tagKey
		{
			get
			{
				if (!this.hasTagKey)
				{
					this._tagKey = this.splitName[0];
					this.hasTagKey = true;
				}
				return this._tagKey;
			}
			set
			{
				this._tagKey = value;
				this.hasTagKey = true;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x00058E54 File Offset: 0x00057254
		// (set) Token: 0x060020BD RID: 8381 RVA: 0x00058ECC File Offset: 0x000572CC
		public string swapKey
		{
			get
			{
				if (!this.hasSwapKey)
				{
					this._swapKey = this.tagKey;
					string text = this.splitName.FirstOrDefault((string x) => x.Length > 0 && x[0] == '.');
					if (text != null)
					{
						this._swapKey += text;
					}
					this.hasSwapKey = true;
				}
				return this._swapKey;
			}
			set
			{
				this._swapKey = value;
				this.hasSwapKey = true;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x00058EDC File Offset: 0x000572DC
		// (set) Token: 0x060020BF RID: 8383 RVA: 0x00059006 File Offset: 0x00057406
		public string tagValue
		{
			get
			{
				if (!this.hasTagValue)
				{
					this._tagValue = this.splitName.FirstOrDefault((string x) => x.Length > 0 && x[0] == '#');
					if (this._tagValue != null)
					{
						this._tagValue = this._tagValue.Remove(0, 1);
					}
					else
					{
						Transform parent = base.transform.parent;
						while (parent != null)
						{
							Swappable component = parent.GetComponent<Swappable>();
							if (component)
							{
								this._tagValue = component.tagValue;
								break;
							}
							this._tagValue = parent.name.Split(new char[]
							{
								' '
							}).FirstOrDefault((string x) => x.Length > 0 && x[0] == '#');
							if (this._tagValue != null)
							{
								this._tagValue = this._tagValue.Remove(0, 1);
								break;
							}
							parent = parent.parent;
						}
					}
					this.hasTagValue = true;
				}
				return this._tagValue;
			}
			set
			{
				this._tagValue = value;
				this.hasTagValue = true;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x00059018 File Offset: 0x00057418
		public PropertyBank propertyBank
		{
			get
			{
				if (!this.hasPropertyBank)
				{
					this._propertyBank = new PropertyBank();
					GenOption[] components = base.GetComponents<GenOption>();
					foreach (GenOption option in components)
					{
						this._propertyBank.Apply(option);
					}
					foreach (string text in this.splitName)
					{
						int num = text.IndexOf(':');
						if (num != -1)
						{
							string text2 = text.Substring(0, num);
							string text3 = text.Substring(num + 1, text.Length - num);
							int num2 = text3.IndexOf('-');
							if (num2 == -1)
							{
								this.tagValue = text3;
								this.tagKey = text2;
							}
							else
							{
								int num3 = int.Parse(text3.Substring(0, num2));
								int num4 = int.Parse(text3.Substring(num2 + 1, text3.Length - num2));
								this._propertyBank.AddRange(text2, (float)num3, (float)num4);
							}
						}
					}
					this.hasPropertyBank = true;
				}
				return this._propertyBank;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x00059144 File Offset: 0x00057544
		public bool isPlaceholder
		{
			get
			{
				if (!this.hasPlaceholder)
				{
					this._isPlaceholder = this.splitName.Any((string x) => x.ToLower() == "placeholder");
					this.hasPlaceholder = true;
				}
				return this._isPlaceholder;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x00059197 File Offset: 0x00057597
		IEnumerable<Range> IGenOption.ranges
		{
			get
			{
				return ((IGenOption)this.propertyBank).ranges;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x000591A4 File Offset: 0x000575A4
		IEnumerable<Tag> IGenOption.tags
		{
			get
			{
				return ((IGenOption)this.propertyBank).tags;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x000591B1 File Offset: 0x000575B1
		float IGenOption.probability
		{
			get
			{
				return ((IGenOption)this.propertyBank).probability;
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000591C0 File Offset: 0x000575C0
		public bool CanBeReplacedBy(Swappable other)
		{
			return !(other.swapKey != this.swapKey) && !other.isPlaceholder && (!this.fittedX || other.psdGroup.innerRect.width <= this.psdGroup.outerRect.width) && (!this.fittedY || other.psdGroup.innerRect.height <= this.psdGroup.outerRect.height) && (!this.sameSideX || Mathf.Sign(base.transform.localPosition.x) == Mathf.Sign(other.transform.localPosition.x)) && (!this.sameSideY || Mathf.Sign(base.transform.localPosition.y) == Mathf.Sign(other.transform.localPosition.y));
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000592DB File Offset: 0x000576DB
		public Swappable ReplaceWith(Swappable other)
		{
			other.ApplyReplaceSettings(other.GetReplaceSettings(this));
			base.transform.SetParent(null);
			UnityEngine.Object.Destroy(base.gameObject);
			return other;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00059304 File Offset: 0x00057704
		public static void Swap(Swappable swappable0, Swappable swappable1)
		{
			Swappable.TransformSettings replaceSettings = swappable0.GetReplaceSettings(swappable1);
			Swappable.TransformSettings replaceSettings2 = swappable1.GetReplaceSettings(swappable0);
			swappable0.ApplyReplaceSettings(replaceSettings);
			swappable1.ApplyReplaceSettings(replaceSettings2);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00059330 File Offset: 0x00057730
		private Swappable.TransformSettings GetReplaceSettings(Swappable other)
		{
			Swappable.TransformSettings result = default(Swappable.TransformSettings);
			result.parent = other.transform.parent;
			result.siblingIndex = other.transform.GetSiblingIndex();
			result.pos = other.transform.localPosition;
			result.scale = other.transform.localScale;
			result.rot = other.transform.localRotation;
			if (this.psdGroup && other.psdGroup)
			{
				if (other.normX)
				{
					result.scale.x = result.scale.x * (other.psdGroup.innerRect.width / this.psdGroup.innerRect.width);
				}
				if (other.normY)
				{
					result.scale.y = result.scale.y * (other.psdGroup.innerRect.height / this.psdGroup.innerRect.height);
				}
			}
			return result;
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00059440 File Offset: 0x00057840
		private void ApplyReplaceSettings(Swappable.TransformSettings ts)
		{
			base.transform.SetParent(ts.parent, false);
			base.transform.SetSiblingIndex(ts.siblingIndex);
			base.transform.localPosition = ts.pos;
			base.transform.localRotation = ts.rot;
			base.transform.localScale = ts.scale;
		}

		// Token: 0x0400144F RID: 5199
		[SerializeField]
		private bool normX;

		// Token: 0x04001450 RID: 5200
		[SerializeField]
		private bool normY;

		// Token: 0x04001451 RID: 5201
		[SerializeField]
		private bool fittedX;

		// Token: 0x04001452 RID: 5202
		[SerializeField]
		private bool fittedY;

		// Token: 0x04001453 RID: 5203
		[SerializeField]
		private bool sameSideX;

		// Token: 0x04001454 RID: 5204
		[SerializeField]
		private bool sameSideY;

		// Token: 0x04001455 RID: 5205
		private PsdGroup _psdGroup;

		// Token: 0x04001456 RID: 5206
		private static char[] splitChar = new char[]
		{
			' '
		};

		// Token: 0x04001457 RID: 5207
		private string[] _splitName;

		// Token: 0x04001458 RID: 5208
		private bool hasExtraKey;

		// Token: 0x04001459 RID: 5209
		private string _extraKey;

		// Token: 0x0400145A RID: 5210
		private bool hasTagKey;

		// Token: 0x0400145B RID: 5211
		private string _tagKey;

		// Token: 0x0400145C RID: 5212
		private bool hasSwapKey;

		// Token: 0x0400145D RID: 5213
		private string _swapKey;

		// Token: 0x0400145E RID: 5214
		private bool hasTagValue;

		// Token: 0x0400145F RID: 5215
		private string _tagValue;

		// Token: 0x04001460 RID: 5216
		private bool hasPropertyBank;

		// Token: 0x04001461 RID: 5217
		private PropertyBank _propertyBank;

		// Token: 0x04001462 RID: 5218
		private bool hasPlaceholder;

		// Token: 0x04001463 RID: 5219
		private bool _isPlaceholder;

		// Token: 0x02000502 RID: 1282
		private struct TransformSettings
		{
			// Token: 0x04001469 RID: 5225
			public Transform parent;

			// Token: 0x0400146A RID: 5226
			public int siblingIndex;

			// Token: 0x0400146B RID: 5227
			public Vector3 pos;

			// Token: 0x0400146C RID: 5228
			public Vector3 scale;

			// Token: 0x0400146D RID: 5229
			public Quaternion rot;
		}
	}
}
