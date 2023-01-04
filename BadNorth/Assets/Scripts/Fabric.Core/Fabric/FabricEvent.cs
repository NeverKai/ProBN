using System;
using UnityEngine.Playables;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class FabricEvent : PlayableAsset
	{
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return default(Playable);
		}

		public string _enterEventName;
		public EventAction _enterEventAction;
		public string _exitEventName;
		public EventAction _exitEventAction;
		public bool _stopEventOnExit;
	}
}
