using System;
using UnityEngine;
using Voxels.TowerDefense.UI;

// Token: 0x02000567 RID: 1383
public class LoadingSpinner : MonoBehaviour
{
	// Token: 0x060023FC RID: 9212 RVA: 0x00070B00 File Offset: 0x0006EF00
	private void Awake()
	{
		this.dots = base.GetComponentsInChildren<DistanceFieldSettings>(true);
		this.fraction0 = float.MaxValue;
		this.fraction1 = float.MinValue;
		this.scale0 = float.MaxValue;
		this.scale1 = float.MinValue;
		for (int i = 0; i < this.dots.Length; i++)
		{
			this.fraction0 = Mathf.Min(this.fraction0, this.dots[i].fraction);
			this.fraction1 = Mathf.Max(this.fraction1, this.dots[i].fraction);
			this.scale0 = Mathf.Min(this.scale0, this.dots[i].transform.localScale.z);
			this.scale1 = Mathf.Max(this.scale1, this.dots[i].transform.localScale.z);
		}
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x00070BF4 File Offset: 0x0006EFF4
	private void Update()
	{
		float num = 4f;
		float num2 = Mathf.Min(Time.unscaledDeltaTime, 0.1f) * num;
		this.time += num2;
		for (int i = 0; i < this.dots.Length; i++)
		{
			DistanceFieldSettings distanceFieldSettings = this.dots[i];
			float num3 = ((float)this.dots.Length + this.time - (float)i) % (float)this.dots.Length;
			float t = this.curve.Evaluate(num3);
			distanceFieldSettings.transform.localScale = new Vector3(1f, 1f, Mathf.Lerp(this.scale0, this.scale1, t));
			distanceFieldSettings.fraction = Mathf.Lerp(this.fraction0, this.fraction1, t);
		}
		if (!string.IsNullOrEmpty(this.tickAudio.name) && Mathf.RoundToInt(this.time) != Mathf.RoundToInt(this.time - num2))
		{
			FabricWrapper.PostEvent(this.tickAudio);
		}
	}

	// Token: 0x060023FE RID: 9214 RVA: 0x00070CFB File Offset: 0x0006F0FB
	private float GetPhase(float ratio, float time)
	{
		return (time * 5f - 6.2831855f * (ratio / 1.5f)) % 6.2831855f;
	}

	// Token: 0x0400169B RID: 5787
	private DistanceFieldSettings[] dots;

	// Token: 0x0400169C RID: 5788
	[SerializeField]
	private FabricEventReference tickAudio = "UI/Menu/GenerateTick";

	// Token: 0x0400169D RID: 5789
	[SerializeField]
	private AnimationCurve curve;

	// Token: 0x0400169E RID: 5790
	private float fraction0;

	// Token: 0x0400169F RID: 5791
	private float fraction1;

	// Token: 0x040016A0 RID: 5792
	private float scale0;

	// Token: 0x040016A1 RID: 5793
	private float scale1;

	// Token: 0x040016A2 RID: 5794
	private float time;
}
