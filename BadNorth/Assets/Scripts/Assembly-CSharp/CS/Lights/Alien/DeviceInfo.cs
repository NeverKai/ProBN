using System;
using System.Collections.Generic;
//using AlienFXManagedWrapper;

namespace CS.Lights.Alien
{
	// Token: 0x0200038B RID: 907
	public class DeviceInfo
	{
		// Token: 0x060014B9 RID: 5305 RVA: 0x0002AA10 File Offset: 0x00028E10
		// public DeviceInfo(uint id, LFX_DeviceType type)
		// {
		// 	this.DeviceID = id;
		// 	this.DeviceType = type;
		// 	if (AlienFXWrapperMap.LFX_GetNumLights(this.DeviceID, out this.LightTotal) == LFX_Result.LFX_Success)
		// 	{
		// 		for (uint num = 0U; num < this.LightTotal; num += 1U)
		// 		{
		// 			LFX_Position key;
		// 			if (AlienFXWrapperMap.LFX_GetLightLocation(this.DeviceID, num, out key) == LFX_Result.LFX_Success)
		// 			{
		// 				if (!this.Lights.ContainsKey(key))
		// 				{
		// 					this.Lights.Add(key, new List<uint>());
		// 				}
		// 				this.Lights[key].Add(num);
		// 			}
		// 		}
		// 	}
		// }

		// Token: 0x060014BA RID: 5306 RVA: 0x0002AAB4 File Offset: 0x00028EB4
		// public void Set(SetLightingEvent effect)
		// {
		// 	uint num = 1U;
		// 	do
		// 	{
		// 		uint lightCol = Utility.ColourToUInt(effect.GetPoint(Utility.GetPoint((LFX_Position)num)));
		// 		AlienFXWrapperMap.LFX_Light((LFX_Position)num, lightCol);
		// 		num <<= 1;
		// 	}
		// 	while (num != 134217728U);
		// }

		// Token: 0x04000CD9 RID: 3289
		//public LFX_DeviceType DeviceType;

		// Token: 0x04000CDA RID: 3290
		public uint DeviceID;

		// Token: 0x04000CDB RID: 3291
		public uint LightTotal;

		// Token: 0x04000CDC RID: 3292
		//public Dictionary<LFX_Position, List<uint>> Lights = new Dictionary<LFX_Position, List<uint>>();
	}
}
