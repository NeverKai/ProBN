using System;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using UnityEngine;

// Token: 0x02000026 RID: 38
[RequireComponent(typeof(AudioListener))]
public class ClaimableAudioListener : MonoBehaviour
{
	// Token: 0x060000B1 RID: 177 RVA: 0x0000556A File Offset: 0x0000396A
	public static void AddClaim(Component claimant, bool priority = false)
	{
		if (claimant && !ClaimableAudioListener.claimants.Contains(claimant))
		{
			ClaimableAudioListener.claimants.Add(claimant);
		}
		if (priority)
		{
			ClaimableAudioListener.priorityClaim = claimant;
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x0000559E File Offset: 0x0000399E
	public static void RemoveClaim(Component claimant)
	{
		ClaimableAudioListener.claimants.Remove(claimant);
		if (ClaimableAudioListener.priorityClaim == claimant)
		{
			ClaimableAudioListener.priorityClaim = null;
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x000055C2 File Offset: 0x000039C2
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x000055D0 File Offset: 0x000039D0
	private void LateUpdate()
	{
		ClaimableAudioListener.claimants.RemoveAll(ClaimableAudioListener.nullRemover);
		Component component = (!ClaimableAudioListener.priorityClaim) ? ClaimableAudioListener.claimants.Last<Component>() : ClaimableAudioListener.priorityClaim;
		if (component)
		{
			Transform transform = component.transform;
			base.transform.position = transform.position;
			base.transform.rotation = transform.rotation;
		}
		for (int i = ClaimableAudioListener.claimants.Count - 1; i > 0; i--)
		{
		}
	}

	// Token: 0x0400004C RID: 76
	private DebugChannelGroup dbgGroup = new DebugChannelGroup("AudioListener", EVerbosity.Quiet, 0);

	// Token: 0x0400004D RID: 77
	private static Component priorityClaim = null;

	// Token: 0x0400004E RID: 78
	private static List<Component> claimants = new List<Component>();

	// Token: 0x0400004F RID: 79
	private static Predicate<Component> nullRemover = (Component x) => !x;
}
