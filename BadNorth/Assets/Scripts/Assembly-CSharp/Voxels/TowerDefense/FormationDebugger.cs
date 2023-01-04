using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007AA RID: 1962
	public class FormationDebugger : MonoBehaviour
	{
		// Token: 0x060032CF RID: 13007 RVA: 0x000D857C File Offset: 0x000D697C
		private void OnDrawGizmos()
		{
			float num = 0.08f;
			Vector3 right = Vector3.right;
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					for (int k = 1; k <= 24; k++)
					{
						SquadFormation squadFormation = new SquadFormation(k, new Bounds(Vector3.zero, (i != 0) ? new Vector3(1f, 1f, 0.8f) : new Vector3(0.8f, 1f, 1f)), right);
						Vector3 vector = new Vector3((float)k, 0f, (float)(j + i * 4));
						Gizmos.matrix = base.transform.localToWorldMatrix * Matrix4x4.Translate(vector);
						Gizmos.color = Color.yellow;
						for (int l = 0; l < k - 1; l++)
						{
							Vector3 from = squadFormation.Get(l) * num * 2f;
							Vector3 to = squadFormation.Get(l + 1) * num * 2f;
							Gizmos.DrawLine(from, to);
						}
						Gizmos.color = Color.yellow;
						for (int m = 0; m < k; m++)
						{
							Vector3 vector2 = squadFormation.Get(m);
							vector2 *= num * 2f;
							ExtraGizmos.DrawCircle(vector2, num, 8);
							Gizmos.color = Color.white;
						}
						Gizmos.color = Color.white.SetA(0.2f);
						Vector3 vector3 = new Vector3((float)squadFormation.sizeX, 0f, (float)squadFormation.sizeY) * num * 2f;
						Gizmos.DrawWireCube(Vector3.zero, vector3);
						Gizmos.DrawRay(Vector3.Scale(right, vector3 / 2f), right * 0.2f);
					}
					right = new Vector3(right.z, 0f, -right.x);
				}
			}
		}
	}
}
