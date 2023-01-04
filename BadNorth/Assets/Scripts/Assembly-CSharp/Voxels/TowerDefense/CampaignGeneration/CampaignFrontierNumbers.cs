using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006D5 RID: 1749
	public class CampaignFrontierNumbers : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x06002D4D RID: 11597 RVA: 0x000AA5F0 File Offset: 0x000A89F0
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			LineFrontier lineFrontier = campaign.GetComponentInChildren<LineFrontier>(true);
			LineFrontier.Point[,] points = lineFrontier.points;
			float defaultA = this.bundleText.color.a;
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < 2; i++)
			{
				bool bottom = i == 0;
				int j = 0;
				while (j < campaign.frontierDepth)
				{
					List<LineFrontier.Anchor> anchors = lineFrontier.anchorLists[j + 4];
					LineFrontier.Anchor anchor = (!bottom) ? anchors[3] : anchors[anchors.Count - 4];
					int depth0 = Mathf.Min(anchor.vert0.frontierDepth, anchor.vert1.frontierDepth);
					int depth1 = Mathf.Max(anchor.vert0.frontierDepth, anchor.vert1.frontierDepth) - 1;
					j = depth1 + 1;
					int y = Mathf.RoundToInt(anchor.t + 40f);
					y += ((!bottom) ? -2 : 2);
					if (!hashSet.Contains(depth0))
					{
						int num = depth0 + 4;
						LineFrontier.Point point = points[num, y];
						Text text = UnityEngine.Object.Instantiate<Text>(this.bundleText, this.bundleText.transform.parent);
						text.text = (depth0 + 1).ToString();
						text.alignment = TextAnchor.MiddleRight;
						text.transform.localPosition = point.pos + 0.6f * Vector2.Scale(text.transform.localScale, Vector2.Scale(text.rectTransform.sizeDelta, point.tangent));
						lineFrontier.mainLineAnim.Subscribe(delegate(float x)
						{
							text.gameObject.SetActive((float)depth0 - 0.1f > x);
						});
						lineFrontier.nextLineAnim.Subscribe(delegate(float x)
						{
							text.color = text.color.SetA(Mathf.Lerp(defaultA, 0.85f, lineFrontier.GetNextLerp((float)depth0, x)));
						});
						hashSet.Add(depth0);
					}
					if (!hashSet.Contains(depth1))
					{
						int num2 = depth1 + 4;
						LineFrontier.Point point2 = points[num2, y];
						Text text = UnityEngine.Object.Instantiate<Text>(this.bundleText, this.bundleText.transform.parent);
						text.text = (depth1 + 1).ToString();
						text.alignment = TextAnchor.MiddleLeft;
						text.transform.localPosition = point2.pos - 0.6f * Vector2.Scale(text.transform.localScale, Vector2.Scale(text.rectTransform.sizeDelta, point2.tangent));
						lineFrontier.mainLineAnim.Subscribe(delegate(float x)
						{
							text.gameObject.SetActive((float)depth1 + 0.1f > x + 1f);
						});
						lineFrontier.nextLineAnim.Subscribe(delegate(float x)
						{
							text.color = text.color.SetA(Mathf.Lerp(defaultA, 0.85f, lineFrontier.GetNextLerp((float)depth1, x)));
						});
						hashSet.Add(depth1);
					}
					yield return new GenInfo("CampaignFrontierNumbers", GenInfo.Mode.interruptable);
				}
			}
			this.bundleText.gameObject.SetActive(false);
			yield break;
		}

		// Token: 0x04001DF0 RID: 7664
		[SerializeField]
		private int distanceFromEdge = -4;

		// Token: 0x04001DF1 RID: 7665
		[SerializeField]
		private Text bundleText;
	}
}
