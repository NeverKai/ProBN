using System;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000925 RID: 2341
	public class AgentInfo : MonoBehaviour
	{
		// Token: 0x04002BFB RID: 11259
		[SerializeField]
		public HeadshotImage image;

		// Token: 0x04002BFC RID: 11260
		[SerializeField]
		public HeadshotImage unseenImage;

		// Token: 0x04002BFD RID: 11261
		[SerializeField]
		public Text text;

		// Token: 0x04002BFE RID: 11262
		public VikingReference vikingReference;
	}
}
