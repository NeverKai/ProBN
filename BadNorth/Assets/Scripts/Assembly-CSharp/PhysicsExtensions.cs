using System;
using UnityEngine;

// Token: 0x0200050B RID: 1291
public static class PhysicsExtensions
{
	// Token: 0x06002130 RID: 8496 RVA: 0x0005B45F File Offset: 0x0005985F
	public static LayerMask GetCollidingLayers(this Component component)
	{
		return component.gameObject.GetCollidingLayers();
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x0005B46C File Offset: 0x0005986C
	public static LayerMask GetCollidingLayers(this GameObject obj)
	{
		int layer = obj.layer;
		int num = 0;
		for (int i = 0; i < 32; i++)
		{
			if (!string.IsNullOrEmpty(LayerMask.LayerToName(i)))
			{
				if (!Physics.GetIgnoreLayerCollision(layer, i))
				{
					num |= 1 << i;
				}
			}
		}
		return num;
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x0005B4C5 File Offset: 0x000598C5
	public static Vector3 GetWorldPosition(this SphereCollider collider)
	{
		return collider.transform.TransformPoint(collider.center);
	}
}
