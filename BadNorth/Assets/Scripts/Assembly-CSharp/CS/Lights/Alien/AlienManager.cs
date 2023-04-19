using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using AlienFXManagedWrapper;
using CS.Platform;
using UnityEngine;

namespace CS.Lights.Alien
{
	// Token: 0x0200038C RID: 908
	public class AlienManager : LightEffectManager
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x0002AC64 File Offset: 0x00029064
		public static void SetupAlienFX()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			foreach (string text in commandLineArgs)
			{
				if (text.ToLower() == "noalienfx")
				{
					Debug.Log("Disabling AlienFX");
					return;
				}
			}
			try
			{
				// LFX_Result lfx_Result = AlienFXWrapperMap.LFX_Initialize();
				// Debug.Log("[AlienFX] init result: " + lfx_Result);
				// if (lfx_Result == LFX_Result.LFX_Success)
				// {
				// 	LightEffectManager._instance = BasePlatformManager.Instance.gameObject.AddComponent<AlienManager>();
				// }
			}
			catch
			{
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0002AD04 File Offset: 0x00029104
		private void OnDestroy()
		{
			this.runLights = false;
			// AlienFXWrapperMap.LFX_Release();
			LightEffectManager._instance = null;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0002AD19 File Offset: 0x00029119
		private void OnEnable()
		{
			base.StartCoroutine(this.UpdateLighting());
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0002AD28 File Offset: 0x00029128
		private void Awake()
		{
			this.FindDevices();
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0002AD30 File Offset: 0x00029130
		private void FindDevices()
		{
			uint num = 0U;
			// AlienFXWrapperMap.LFX_GetNumDevices(out num);
			// int devDescSize = 128;
			// StringBuilder devDesc = new StringBuilder();
			// this._activeDevices = new List<DeviceType>();
			// this._devices = new Dictionary<LFX_DeviceType, List<DeviceInfo>>();
			// for (uint num2 = 0U; num2 < num; num2 += 1U)
			// {
			// 	LFX_DeviceType lfx_DeviceType;
			// 	if (AlienFXWrapperMap.LFX_GetDeviceDescription(num2, devDesc, devDescSize, out lfx_DeviceType) == LFX_Result.LFX_Success)
			// 	{
			// 		DeviceInfo deviceInfo = new DeviceInfo(num2, lfx_DeviceType);
			// 		if (deviceInfo.LightTotal != 0U)
			// 		{
			// 			if (!this._devices.ContainsKey(lfx_DeviceType))
			// 			{
			// 				this._activeDevices.Add(Utility.AlienToDevice(lfx_DeviceType));
			// 				this._devices.Add(lfx_DeviceType, new List<DeviceInfo>());
			// 			}
			// 			this._devices[lfx_DeviceType].Add(deviceInfo);
			// 		}
			// 	}
			// }
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0002ADF4 File Offset: 0x000291F4
		private IEnumerator UpdateLighting()
		{
			while (this.runLights)
			{
				// AlienFXWrapperMap.LFX_Reset();
				// for (int i = 0; i < this._activeDevices.Count; i++)
				// {
				// 	this._lightEffectEvent.Reset();
				// 	this._lightEffectEvent.ActiveType = this._activeDevices[i];
				// 	LightEffectManager.ApplyEffect(this._lightEffectEvent);
				// 	if (!this._lightEffectEvent.ForceClear)
				// 	{
				// 		List<DeviceInfo> list = this._devices[Utility.DeviceToAlien(this._activeDevices[i])];
				// 		for (int j = 0; j < list.Count; j++)
				// 		{
				// 			list[j].Set(this._lightEffectEvent);
				// 		}
				// 	}
				// }
				// AlienFXWrapperMap.LFX_Update();
				yield return new WaitForSecondsRealtime(0.5f);
			}
			yield break;
		}

		// Token: 0x04000CDD RID: 3293
		protected SetLightingEvent _lightEffectEvent = new SetLightingEvent();

		// Token: 0x04000CDE RID: 3294
		private List<DeviceType> _activeDevices;

		// Token: 0x04000CDF RID: 3295
		// private Dictionary<LFX_DeviceType, List<DeviceInfo>> _devices;

		// Token: 0x04000CE0 RID: 3296
		private bool runLights = true;
	}
}
