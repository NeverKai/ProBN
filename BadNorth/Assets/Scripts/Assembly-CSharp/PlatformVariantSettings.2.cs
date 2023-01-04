using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlatformVariantSettings<T, M>
{
	[SerializeField]
	private T defaultValues;
	[SerializeField]
	private List<M> platformVariants;
}
