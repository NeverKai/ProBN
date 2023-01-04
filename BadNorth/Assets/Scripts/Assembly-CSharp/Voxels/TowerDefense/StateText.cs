using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x02000800 RID: 2048
	public class StateText : MonoBehaviour, IAllStateChange
	{
		// Token: 0x060035A3 RID: 13731 RVA: 0x000E6414 File Offset: 0x000E4814
		public void OnAllStateChange(Stack stack)
		{
			string text = string.Empty;
			for (int i = 0; i < Singleton<Stack>.instance.states.Length; i++)
			{
				State state = Singleton<Stack>.instance.states[i];
				if (state.active)
				{
					if (i > 0)
					{
						text += "\n";
					}
					Transform parent = state.transform.parent;
					for (int j = 0; j < 100; j++)
					{
						text += "-";
						parent = parent.parent;
						if (parent == null)
						{
							break;
						}
					}
					text += Singleton<Stack>.instance.states[i].name;
				}
			}
			this.stateText.text = text;
		}

		// Token: 0x04002465 RID: 9317
		public Text stateText;
	}
}
