using System;
using System.Linq;
using Spriteshop;
using UnityEngine;

namespace ControlledRandomness
{
	// Token: 0x020004FB RID: 1275
	public class RandomTransform : SeededComponent, IOnGenerate, IOnReset
	{
		// Token: 0x0600209B RID: 8347 RVA: 0x000582E8 File Offset: 0x000566E8
		void IOnGenerate.OnGenerate(PropertyBank propertyBank, int seed)
		{
			base.Seed(seed);
			Vector3 vector = this.defaultScale = base.transform.localScale;
			Vector3 localPosition = this.defaultPosition = base.transform.localPosition;
			Quaternion quaternion = this.defaultRotation = base.transform.localRotation;
			PsdGroup component = base.GetComponent<PsdGroup>();
			string[] source;
			if (component)
			{
				source = component.splitName;
			}
			else
			{
				source = base.name.Split(new char[]
				{
					' '
				});
			}
			string text = source.FirstOrDefault((string x) => x[0] == '#');
			if (text == null)
			{
				text = base.name;
			}
			this.Fit(ref vector);
			if (this.flipX)
			{
				if (propertyBank.PickBool(text + "FlipX", () => UnityEngine.Random.value < 0.5f))
				{
					vector.x = -vector.x;
				}
			}
			if (this.flipY)
			{
				if (propertyBank.PickBool(text + "FlipY", () => UnityEngine.Random.value < 0.5f))
				{
					vector.y = -vector.y;
				}
			}
			if (this.randomOffset.x != 0f)
			{
				localPosition.x += Mathf.Sign(localPosition.x) * propertyBank.PickFloat(text + "OffsetX", () => this.GetRandom(this.randomOffset.x));
			}
			if (this.randomOffset.y != 0f)
			{
				localPosition.y += propertyBank.PickFloat(text + "OffsetY", () => this.GetRandom(this.randomOffset.y));
			}
			if (this.randomScale != 0f)
			{
				RandomTransform.ScaleAxis scaleAxis = this.scaleAxis;
				if (scaleAxis != RandomTransform.ScaleAxis.Both)
				{
					if (scaleAxis != RandomTransform.ScaleAxis.X)
					{
						if (scaleAxis == RandomTransform.ScaleAxis.Y)
						{
							vector.y *= propertyBank.PickFloat(text + "ScaleY", () => Mathf.Pow(2f, this.GetRandom(this.randomScale)));
						}
					}
					else
					{
						vector.x *= propertyBank.PickFloat(text + "ScaleX", () => Mathf.Pow(2f, this.GetRandom(this.randomScale)));
					}
				}
				else
				{
					vector *= propertyBank.PickFloat(text + "Scale", () => Mathf.Pow(2f, this.GetRandom(this.randomScale)));
				}
			}
			if (this.randomRotation != 0f)
			{
				quaternion *= Quaternion.Euler(0f, 0f, Mathf.Sign(localPosition.x) * propertyBank.PickFloat(text + "Rotation", () => this.GetRandom(this.randomRotation)));
			}
			base.transform.localScale = vector;
			base.transform.localPosition = localPosition;
			base.transform.localRotation = quaternion;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0005861A File Offset: 0x00056A1A
		private float GetRandom(float scale)
		{
			return UnityEngine.Random.value * UnityEngine.Random.value * Mathf.Sign(UnityEngine.Random.value - 0.5f) * scale;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0005863C File Offset: 0x00056A3C
		private void Fit(ref Vector3 scale)
		{
			float z = scale.z;
			Vector3 vector = ExtraMath.Abs(base.transform.lossyScale);
			switch (this.normalizationMode)
			{
			case RandomTransform.NormalizationMode.None:
				return;
			case RandomTransform.NormalizationMode.FitX:
				vector /= vector.x;
				break;
			case RandomTransform.NormalizationMode.FitY:
				vector /= vector.y;
				break;
			case RandomTransform.NormalizationMode.FitSmallest:
				vector /= Mathf.Min(vector.x, vector.y);
				break;
			case RandomTransform.NormalizationMode.Average:
				vector /= (vector.x + vector.y) / 2f;
				break;
			}
			scale = ExtraMath.Divide(scale, vector);
			scale.z = z;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x00058707 File Offset: 0x00056B07
		void IOnReset.OnReset()
		{
			base.transform.localPosition = this.defaultPosition;
			base.transform.localScale = this.defaultScale;
			base.transform.localRotation = this.defaultRotation;
		}

		// Token: 0x0400142D RID: 5165
		[SerializeField]
		private bool flipX;

		// Token: 0x0400142E RID: 5166
		[SerializeField]
		private bool flipY;

		// Token: 0x0400142F RID: 5167
		[SerializeField]
		private Vector2 randomOffset;

		// Token: 0x04001430 RID: 5168
		[SerializeField]
		private float randomRotation;

		// Token: 0x04001431 RID: 5169
		[SerializeField]
		private float randomScale;

		// Token: 0x04001432 RID: 5170
		[SerializeField]
		private RandomTransform.NormalizationMode normalizationMode;

		// Token: 0x04001433 RID: 5171
		[SerializeField]
		private RandomTransform.ScaleAxis scaleAxis;

		// Token: 0x04001434 RID: 5172
		private Vector3 defaultPosition;

		// Token: 0x04001435 RID: 5173
		private Vector3 defaultScale;

		// Token: 0x04001436 RID: 5174
		private Quaternion defaultRotation;

		// Token: 0x020004FC RID: 1276
		private enum NormalizationMode
		{
			// Token: 0x0400143B RID: 5179
			None,
			// Token: 0x0400143C RID: 5180
			FitX,
			// Token: 0x0400143D RID: 5181
			FitY,
			// Token: 0x0400143E RID: 5182
			FitSmallest,
			// Token: 0x0400143F RID: 5183
			Average
		}

		// Token: 0x020004FD RID: 1277
		private enum ScaleAxis
		{
			// Token: 0x04001441 RID: 5185
			Both,
			// Token: 0x04001442 RID: 5186
			X,
			// Token: 0x04001443 RID: 5187
			Y
		}
	}
}
