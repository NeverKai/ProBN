using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x0200056E RID: 1390
	public class LoadScene : MonoBehaviour
	{
		// Token: 0x06002423 RID: 9251 RVA: 0x00071572 File Offset: 0x0006F972
		private void Start()
		{
			base.StartCoroutine(this.LoadSceneRoutine());
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00071584 File Offset: 0x0006F984
		private IEnumerator LoadSceneRoutine()
		{
			float time = Time.realtimeSinceStartup;
			AsyncOperation mainLoad = ExtraSceneManager.LoadSceneIfNotLoadedAsync("TowerDefense");
			AsyncOperation heroLoad = ExtraSceneManager.LoadSceneIfNotLoadedAsync("HeroGeneration");
			if (heroLoad != null)
			{
				heroLoad.allowSceneActivation = false;
			}
			if (mainLoad != null)
			{
				mainLoad.allowSceneActivation = false;
				while (mainLoad.progress < 0.9f)
				{
					yield return null;
				}
				mainLoad.allowSceneActivation = true;
				while (!Singleton<GameMaster>.instance)
				{
					yield return null;
				}
				if (heroLoad != null)
				{
					heroLoad.allowSceneActivation = true;
				}
				yield return null;
				CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
				canvasGroup.interactable = false;
				while (canvasGroup.alpha > 0f)
				{
					yield return null;
					canvasGroup.alpha -= Time.unscaledDeltaTime * 5f;
				}
			}
			if (heroLoad != null)
			{
				heroLoad.allowSceneActivation = true;
			}
			while (!HeroGeneratorUI.instance)
			{
				yield return null;
			}
			base.gameObject.SetActive(false);
			SceneManager.UnloadSceneAsync(base.gameObject.scene);
			yield break;
		}

		// Token: 0x040016C1 RID: 5825
		[SerializeField]
		private RectTransform tencentSafePlay;

		// Token: 0x040016C2 RID: 5826
		[SerializeField]
		private float tencentSafePlayDisplayTime = 7.5f;
	}
}
