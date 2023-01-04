using System;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000555 RID: 1365
	[ConsoleCommandClassCustomizer("UI")]
	public class IslandUIManager : MonoBehaviour, IGameSetup
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x0006DBE5 File Offset: 0x0006BFE5
		public IslandGameplayManager gameplayManager
		{
			get
			{
				return this._gameplayManager;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x0006DBED File Offset: 0x0006BFED
		public IslandUINotificationManager notificationManager
		{
			get
			{
				return this._notificationManager;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0006DBF5 File Offset: 0x0006BFF5
		public IslandViewfinder islandViewfinder
		{
			get
			{
				return this._islandViewfinder;
			}
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0006DC00 File Offset: 0x0006C000
		void IGameSetup.OnGameAwake()
		{
			IslandUIManager.instance = this;
			foreach (IslandUIManager.IAwake awake in base.transform.GetComponentsInChildren<IslandUIManager.IAwake>(true))
			{
				awake.OnAwake(this);
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0006DC40 File Offset: 0x0006C040
		[ConsoleCommand("")]
		private static void Show(bool show)
		{
			int targetDisplay = (!show) ? Display.displays.Length : 0;
			foreach (Canvas canvas in IslandUIManager.instance.transform.GetComponentsInChildren<Canvas>(true))
			{
				canvas.targetDisplay = targetDisplay;
				GraphicRaycaster component = canvas.GetComponent<GraphicRaycaster>();
				if (component)
				{
					component.enabled = show;
				}
			}
		}

		// Token: 0x0400161C RID: 5660
		private static IslandUIManager instance;

		// Token: 0x0400161D RID: 5661
		[SerializeField]
		private IslandGameplayManager _gameplayManager;

		// Token: 0x0400161E RID: 5662
		[SerializeField]
		private IslandUINotificationManager _notificationManager;

		// Token: 0x0400161F RID: 5663
		[SerializeField]
		private IslandViewfinder _islandViewfinder;

		// Token: 0x02000556 RID: 1366
		public interface IAwake
		{
			// Token: 0x06002379 RID: 9081
			void OnAwake(IslandUIManager manager);
		}
	}
}
