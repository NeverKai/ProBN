using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x02000872 RID: 2162
	public class Thunder : ChildComponent<WorldWeather>
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x000F4CC0 File Offset: 0x000F30C0
		private float thunderIntensity
		{
			get
			{
				return base.manager.weatherSystem.current.thunder;
			}
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000F4CE5 File Offset: 0x000F30E5
		private void Update()
		{
			if (this.thunderIntensity > 0f && this.enumerator != null)
			{
				this.enumerator.MoveNext();
			}
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000F4D10 File Offset: 0x000F3110
		private void OnDisable()
		{
			for (int i = 0; i < this.soundObjects.Count; i++)
			{
				this.soundObjects[i].SetActive(false);
			}
			Shader.SetGlobalVector(this.flashColorId, this.color * 0f);
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000F4D70 File Offset: 0x000F3170
		private void Awake()
		{
			this.enumerator = this.ThunderEnumerator();
			this.enumerator.MoveNext();
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000F4D8C File Offset: 0x000F318C
		private Vector3 GetRandomFlashPos()
		{
			Vector3 vector;
			vector.y = 0f;
			Vector2 vector2 = UnityEngine.Random.insideUnitCircle * 20f;
			vector.x = vector2.x;
			vector.z = vector2.y;
			return vector.SetY(Mathf.Abs(vector.y * 0.5f));
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000F4DEC File Offset: 0x000F31EC
		private IEnumerator ThunderEnumerator()
		{
			Vector2 flashPos2D = UnityEngine.Random.insideUnitCircle.GetXZtoXYZ() * this.radius;
			Shader.SetGlobalVector(this.flashColorId, this.color * 0f);
			yield return null;
			for (;;)
			{
				float timer = UnityEngine.Random.Range(this.intervals.x, this.intervals.y);
				while (timer > 0f)
				{
					timer -= Time.deltaTime * this.thunderIntensity;
					yield return null;
				}
				float flashTimer = 0f;
				flashPos2D += UnityEngine.Random.insideUnitCircle * this.radius * 0.2f;
				flashPos2D = flashPos2D.normalized * Mathf.Clamp(flashPos2D.magnitude, this.radius * 0.2f, this.radius);
				Vector3 flashPos = flashPos2D.GetXZtoXYZ().SetY(4f);
				Vector3 flashDir = flashPos;
				flashDir.Normalize();
				float flashIntensity = this.thunderIntensity;
				AnimationCurve flashCurve = this.flashCurves[UnityEngine.Random.Range(0, this.flashCurves.Length - 1)];
				Keyframe[] keys = flashCurve.keys;
				float flashLength = keys[keys.Length - 1].time;
				Shader.SetGlobalVector(this.flashDirId, flashDir);
				base.StartCoroutine(this.PlayFlashSound(flashPos, flashPos.magnitude / 8f));
				while (flashTimer < flashLength)
				{
					float localIntensity = Mathf.Clamp01(flashCurve.Evaluate(flashTimer));
					localIntensity *= flashIntensity;
					localIntensity *= this.intensity;
					Shader.SetGlobalVector(this.flashColorId, this.color * localIntensity);
					yield return null;
					flashTimer += Time.deltaTime;
				}
				Shader.SetGlobalVector(this.flashColorId, this.color * 0f);
			}
			yield break;
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000F4E08 File Offset: 0x000F3208
		private IEnumerator PlayFlashSound(Vector3 pos, float delay)
		{
			GameObject go = null;
			for (int i = 0; i < this.soundObjects.Count; i++)
			{
				go = this.soundObjects[i];
				if (!go.activeSelf)
				{
					break;
				}
			}
			if (!go)
			{
				go = base.gameObject.AddEmptyChild("SoundObject");
				this.soundObjects.Add(go);
			}
			go.SetActive(true);
			go.transform.position = pos;
			yield return new WaitForSeconds(delay);
			FabricWrapper.PostEvent("Sfx/Thunder", go);
			go.SetActive(false);
			yield return null;
			yield break;
		}

		// Token: 0x0400268A RID: 9866
		[SerializeField]
		private Color color = Color.white;

		// Token: 0x0400268B RID: 9867
		[SerializeField]
		private float intensity = 10f;

		// Token: 0x0400268C RID: 9868
		[SerializeField]
		private Vector2 intervals = new Vector2(1f, 10f);

		// Token: 0x0400268D RID: 9869
		private ShaderId flashDirId = "_FlashDir";

		// Token: 0x0400268E RID: 9870
		private ShaderId flashColorId = "_FlashColor";

		// Token: 0x0400268F RID: 9871
		[SerializeField]
		private AnimationCurve[] flashCurves;

		// Token: 0x04002690 RID: 9872
		private List<GameObject> soundObjects = new List<GameObject>();

		// Token: 0x04002691 RID: 9873
		private float radius = 50f;

		// Token: 0x04002692 RID: 9874
		private IEnumerator enumerator;
	}
}
