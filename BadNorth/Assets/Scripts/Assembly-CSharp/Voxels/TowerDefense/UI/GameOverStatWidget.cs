using System;
using I2.Loc;
using RTM.Pools;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008B1 RID: 2225
	public class GameOverStatWidget : MonoBehaviour, IPoolable
	{
		// Token: 0x06003A60 RID: 14944 RVA: 0x00102B87 File Offset: 0x00100F87
		public void Setup(GameOverReason reason, string labelLocTerm, int value)
		{
			this.locLabel.Term = labelLocTerm;
			this.valueText.text = IntStringCache.clean.Get(value);
			this.reason = reason;
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x00102BB2 File Offset: 0x00100FB2
		public void Setup(GameOverReason reason, string labelLocTerm, float value, string format = "0.00")
		{
			this.locLabel.Term = labelLocTerm;
			this.valueText.text = value.ToString(format);
			this.reason = reason;
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x00102BDB File Offset: 0x00100FDB
		public void Setup(GameOverReason reason, string labelLocTerm, string value)
		{
			this.locLabel.Term = labelLocTerm;
			this.valueText.text = value;
			this.reason = reason;
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x00102BFC File Offset: 0x00100FFC
		public void SetupLocalized(GameOverReason reason, string labelLocTerm, string valueTerm)
		{
			this.locLabel.Term = labelLocTerm;
			this.valueLoc.Term = valueTerm;
			this.valueLoc.enabled = true;
			this.reason = reason;
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x00102C2C File Offset: 0x0010102C
		public void SetVisible(bool visible)
		{
			foreach (Text text in this.texts)
			{
				text.color = ((this.reason != GameOverReason.Won) ? this.lostColor : this.wonColor);
			}
			this.visibility.SetVisible(visible, false);
			FabricWrapper.PostEvent(this.openAudioId);
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x00102C94 File Offset: 0x00101094
		void IPoolable.OnRemoved()
		{
			base.transform.SetAsLastSibling();
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x00102CAD File Offset: 0x001010AD
		void IPoolable.OnReturned()
		{
			base.gameObject.SetActive(false);
			this.valueLoc.Term = string.Empty;
			this.valueLoc.enabled = false;
			this.visibility.SetVisible(false, true);
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x00102CE4 File Offset: 0x001010E4
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.visibility = base.GetComponent<IUIVisibility>();
			this.valueLoc = this.valueText.GetComponent<Localize>();
			this.texts = base.GetComponentsInChildren<Text>();
		}

		// Token: 0x04002872 RID: 10354
		[SerializeField]
		private Localize locLabel;

		// Token: 0x04002873 RID: 10355
		[SerializeField]
		private Text valueText;

		// Token: 0x04002874 RID: 10356
		private Localize valueLoc;

		// Token: 0x04002875 RID: 10357
		[SerializeField]
		private Color wonColor;

		// Token: 0x04002876 RID: 10358
		[SerializeField]
		private Color lostColor;

		// Token: 0x04002877 RID: 10359
		private IUIVisibility visibility;

		// Token: 0x04002878 RID: 10360
		private Text[] texts;

		// Token: 0x04002879 RID: 10361
		private FabricEventReference openAudioId = "UI/InGame/NotificationOn";

		// Token: 0x0400287A RID: 10362
		private GameOverReason reason;
	}
}
