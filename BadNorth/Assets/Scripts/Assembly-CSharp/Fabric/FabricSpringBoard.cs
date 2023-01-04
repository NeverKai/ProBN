using System;
using UnityEngine;

namespace Fabric
{
	// Token: 0x02000393 RID: 915
	[ExecuteInEditMode]
	public class FabricSpringBoard : MonoBehaviour
	{
		// Token: 0x060014E1 RID: 5345 RVA: 0x0002B560 File Offset: 0x00029960
		public FabricSpringBoard()
		{
			FabricSpringBoard._isPresent = true;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0002B56E File Offset: 0x0002996E
		private void OnEnable()
		{
			FabricSpringBoard._isPresent = true;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0002B576 File Offset: 0x00029976
		private void Awake()
		{
			this.Load();
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0002B580 File Offset: 0x00029980
		public void Load()
		{
			FabricManager fabricManagerInEditor = FabricSpringBoard.GetFabricManagerInEditor();
			if (!fabricManagerInEditor)
			{
				GameObject gameObject = Resources.Load(this._fabricManagerPrefabPath, typeof(GameObject)) as GameObject;
				if (gameObject)
				{
					UnityEngine.Object.Instantiate<GameObject>(gameObject);
				}
			}
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0002B5CC File Offset: 0x000299CC
		public static FabricManager GetFabricManagerInEditor()
		{
			FabricManager[] array = Resources.FindObjectsOfTypeAll(typeof(FabricManager)) as FabricManager[];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].gameObject != null && array[i].hideFlags != HideFlags.HideInHierarchy)
				{
					return array[i];
				}
			}
			return null;
		}

		// Token: 0x04000CFA RID: 3322
		public string _fabricManagerPrefabPath;

		// Token: 0x04000CFB RID: 3323
		public static bool _isPresent;
	}
}
