using System;
using System.Diagnostics;
using Fabric;
using UnityEngine;

// Token: 0x02000397 RID: 919
public class FabricWrapper : MonoBehaviour
{
	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0002B87D File Offset: 0x00029C7D
	private static EventManager em
	{
		get
		{
			return EventManager.Instance;
		}
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x0002B884 File Offset: 0x00029C84
	public static bool PostEvent(string eventName)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName);
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x0002B8A6 File Offset: 0x00029CA6
	public static bool PostEvent(string eventName, GameObject parentGameObject)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, parentGameObject);
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x0002B8C9 File Offset: 0x00029CC9
	public static bool PostEvent(string eventName, EventAction eventAction)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, eventAction);
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x0002B8EC File Offset: 0x00029CEC
	public static bool PostEvent(string eventName, GameObject parentGameObject, InitialiseParameters initialiseParameters)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, parentGameObject, initialiseParameters);
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x0002B910 File Offset: 0x00029D10
	public static bool PostEvent(string eventName, EventAction eventAction, object parameter)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, eventAction, parameter);
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x0002B934 File Offset: 0x00029D34
	public static bool PostEvent(string eventName, EventAction eventAction, GameObject parentGameObject)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, eventAction, parentGameObject);
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x0002B958 File Offset: 0x00029D58
	public static bool PostEvent(string eventName, EventAction eventAction, object parameter, GameObject parentGameObject)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, eventAction, parameter, parentGameObject);
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x0002B97D File Offset: 0x00029D7D
	public static bool PostEvent(string eventName, EventAction eventAction, object parameter, GameObject parentGameObject, InitialiseParameters initialiseParameters)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, eventAction, parameter, parentGameObject, initialiseParameters);
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x0002B9A4 File Offset: 0x00029DA4
	public static bool PostEvent(string eventName, EventAction eventAction, object parameter, GameObject parentGameObject, InitialiseParameters initialiseParameters, bool addToQueue)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, eventAction, parameter, parentGameObject, initialiseParameters, addToQueue);
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x0002B9CD File Offset: 0x00029DCD
	public static bool PostEvent(string eventName, EventAction eventAction, object parameter, GameObject parentGameObject, InitialiseParameters initialiseParameters, bool addToQueue, OnEventNotify onEventNotify)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventName, eventAction, parameter, parentGameObject, initialiseParameters, addToQueue, onEventNotify);
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x0002B9F8 File Offset: 0x00029DF8
	public static bool PostEvent(int eventId)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventId);
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x0002BA1A File Offset: 0x00029E1A
	public static bool PostEvent(int eventId, GameObject parentGameObject)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventId, parentGameObject);
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x0002BA3D File Offset: 0x00029E3D
	public static bool PostEvent(int eventId, EventAction eventAction)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventId, eventAction);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x0002BA60 File Offset: 0x00029E60
	public static bool PostEvent(int eventId, EventAction eventAction, GameObject parentGameObject)
	{
		return FabricWrapper.em && FabricWrapper.em.PostEvent(eventId, eventAction, parentGameObject);
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x0002BA84 File Offset: 0x00029E84
	public static bool PostEvent(FabricEventReference eventId)
	{
		return FabricWrapper.PostEvent(eventId.id);
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x0002BA91 File Offset: 0x00029E91
	public static bool PostEvent(FabricEventReference eventId, GameObject parentGameObject)
	{
		return FabricWrapper.PostEvent(eventId.id, parentGameObject);
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x0002BA9F File Offset: 0x00029E9F
	public static bool PostEvent(FabricEventReference eventId, EventAction eventAction)
	{
		return FabricWrapper.PostEvent(eventId.id, eventAction);
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x0002BAAD File Offset: 0x00029EAD
	public static bool PostEvent(FabricEventReference eventId, EventAction eventAction, GameObject parentGameObject)
	{
		return FabricWrapper.PostEvent(eventId.id, eventAction, parentGameObject);
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x0002BABC File Offset: 0x00029EBC
	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void Validate(string eventName)
	{
		if (!EventManager.Instance)
		{
			return;
		}
	}
}
