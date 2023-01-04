using System;
using UnityEngine;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x02000892 RID: 2194
	public class FloatingIcon : SelfPoolingPrefab
	{
		// Token: 0x06003967 RID: 14695 RVA: 0x000FB6D0 File Offset: 0x000F9AD0
		public FloatingIcon Setup(Sprite sprite, Transform followTransform = null)
		{
			this.maskedSprite.sprite = sprite;
			this.followTransform = followTransform;
			if (followTransform)
			{
				base.transform.position = followTransform.position;
			}
			return this;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000FB702 File Offset: 0x000F9B02
		public void Close()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000FB710 File Offset: 0x000F9B10
		private void Awake()
		{
			this.canvas = base.GetComponentInChildren<Canvas>();
			this.rotationTrans = this.canvas.transform.parent;
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000FB734 File Offset: 0x000F9B34
		private void OnEnable()
		{
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000FB736 File Offset: 0x000F9B36
		protected override void OnDisable()
		{
			this.followTransform = null;
			this.maskedSprite.Clear();
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000FB74A File Offset: 0x000F9B4A
		private void Update()
		{
			this.maskedSprite.SetDirty();
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000FB757 File Offset: 0x000F9B57
		public void AnimNotifyFlash()
		{
			FabricWrapper.PostEvent(this.flashAudioId, this.maskedSprite.gameObject);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000FB770 File Offset: 0x000F9B70
		public void AnimNotifyDone()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000FB780 File Offset: 0x000F9B80
		private void LateUpdate()
		{
			if (this.followTransform)
			{
				base.transform.position = Vector3.Lerp(base.transform.position, this.followTransform.position, Time.unscaledDeltaTime * 20f);
			}
			this.rotationTrans.rotation = Quaternion.LookRotation(Singleton<LevelCamera>.instance.cameraRef.transform.forward, Vector3.up);
			this.maskedSprite.SetDirty();
		}

		// Token: 0x04002784 RID: 10116
		[SerializeField]
		private MaskedSprite maskedSprite;

		// Token: 0x04002785 RID: 10117
		public FabricEventReference flashAudioId = "Sfx/Ability/PopOut";

		// Token: 0x04002786 RID: 10118
		private Canvas canvas;

		// Token: 0x04002787 RID: 10119
		private Transform rotationTrans;

		// Token: 0x04002788 RID: 10120
		private Transform followTransform;
	}
}
