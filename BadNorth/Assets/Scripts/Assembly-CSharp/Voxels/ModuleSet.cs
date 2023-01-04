using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.SetRules;

namespace Voxels
{
	// Token: 0x02000630 RID: 1584
	public class ModuleSet : ModuleProcessor
	{
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x00088159 File Offset: 0x00086559
		public bool enabledByDefault
		{
			get
			{
				return this.defaultMode == ModuleSet.Mode.Enabled;
			}
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x00088164 File Offset: 0x00086564
		private void OnValidate()
		{
			base.transform.position = ExtraMath.Round(base.transform.position);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x00088184 File Offset: 0x00086584
		public override void PreProcessModules(Module[] modules)
		{
			base.PreProcessModules(modules);
			this.cachedName = base.name;
			this.rules = base.GetComponents<SetRule>();
			foreach (Module module in modules)
			{
				if (!module.isNull)
				{
					bool flag = false;
					Matrix4x4 matrix4x = base.transform.worldToLocalMatrix * module.transform.localToWorldMatrix;
					for (int j = 0; j < module.cells.Count; j++)
					{
						Vector3 point = matrix4x.MultiplyPoint(module.cells[j].pos);
						for (int k = 0; k < this.bounds.Count; k++)
						{
							if (this.bounds[k].Contains(point))
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
					if (flag)
					{
						this.modules.Add(module);
						module.sets.Add(this);
						module.MarkDirty();
						for (int l = 0; l < this.rules.Length; l++)
						{
							this.rules[l].OnPreProcess(module);
						}
					}
				}
			}
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000882D0 File Offset: 0x000866D0
		public bool ContainsModule(Module module)
		{
			return !module.isNull && module.sets.Contains(this);
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000882EC File Offset: 0x000866EC
		[ContextMenu("Center Pivot")]
		private void CenterPivot()
		{
			Bounds bounds = this.bounds[0];
			for (int i = 1; i < this.bounds.Count; i++)
			{
				bounds.Encapsulate(this.bounds[i]);
			}
			Vector3 b = ExtraMath.Round(bounds.center);
			base.transform.position += b;
			for (int j = 0; j < this.bounds.Count; j++)
			{
				this.bounds[j] = new Bounds(this.bounds[j].center - b, this.bounds[j].size);
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000883B8 File Offset: 0x000867B8
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = this.color * new Color(1f, 1f, 1f, 0.2f);
			for (int i = 0; i < this.bounds.Count; i++)
			{
				Gizmos.DrawWireCube(this.bounds[i].center, this.bounds[i].extents * 2f - Vector3.one * 0.04f);
			}
			if (this.defaultMode == ModuleSet.Mode.Disabled)
			{
				for (int j = 0; j < this.bounds.Count; j++)
				{
					Bounds bounds = this.bounds[j];
					Vector3 min = bounds.min;
					Vector3 to = bounds.max.SetY(min.y);
					Gizmos.DrawLine(min, to);
					min.x = bounds.max.x;
					to.x = bounds.min.x;
					Gizmos.DrawLine(min, to);
				}
			}
		}

		// Token: 0x04001A1C RID: 6684
		[Header("Editor")]
		public Color color = Color.white;

		// Token: 0x04001A1D RID: 6685
		public ModuleSet.Mode defaultMode;

		// Token: 0x04001A1E RID: 6686
		[HideInInspector]
		[SerializeField]
		public string cachedName;

		// Token: 0x04001A1F RID: 6687
		[Space]
		public List<Bounds> bounds = new List<Bounds>
		{
			new Bounds(Vector3.zero, Vector3.one * 2f)
		};

		// Token: 0x04001A20 RID: 6688
		[Header("Runtime")]
		public List<Module> modules = new List<Module>();

		// Token: 0x04001A21 RID: 6689
		public SetRule[] rules;

		// Token: 0x02000631 RID: 1585
		public enum Mode
		{
			// Token: 0x04001A23 RID: 6691
			Enabled,
			// Token: 0x04001A24 RID: 6692
			Disabled
		}
	}
}
