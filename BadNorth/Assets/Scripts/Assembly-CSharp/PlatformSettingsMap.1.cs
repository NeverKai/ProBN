using System;
using UnityEngine;

[Serializable]
public class PlatformSettingsMap<M>
{
	[SerializeField]
	public EPlatform platform;
	[SerializeField]
	public M settings;
}
