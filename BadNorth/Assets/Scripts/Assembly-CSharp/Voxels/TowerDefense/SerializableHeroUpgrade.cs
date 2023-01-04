using System;
using System.Runtime.Serialization;

namespace Voxels.TowerDefense
{
	// Token: 0x020005A2 RID: 1442
	[ObjectDumper.LeafAttribute]
	[Serializable]
	public class SerializableHeroUpgrade
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x00076DB5 File Offset: 0x000751B5
		public SerializableHeroUpgrade(HeroUpgradeDefinition definition, int level = 0)
		{
			this.definition = definition;
			this.level = level;
			this.name = string.Empty;
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x00076DE1 File Offset: 0x000751E1
		public string nameTerm
		{
			get
			{
				return this.definition.nameTerm;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x00076DEE File Offset: 0x000751EE
		public string baseDescriptionTerm
		{
			get
			{
				return this.definition.descriptionTerm;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x00076DFB File Offset: 0x000751FB
		public string descriptionTerm
		{
			get
			{
				return this.definition.GetLevelDescription(this.level);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x00076E0E File Offset: 0x0007520E
		public string prepurchaseTerm
		{
			get
			{
				return this.definition.levels[this.level].prepurchaseDescription;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x00076E2B File Offset: 0x0007522B
		public int cost
		{
			get
			{
				return this.definition.GetLevelCost(this.level);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002594 RID: 9620 RVA: 0x00076E3E File Offset: 0x0007523E
		public bool hasNextLevel
		{
			get
			{
				return this.definition.levels.Length > this.level + 1;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x00076E57 File Offset: 0x00075257
		public int nextLevelCost
		{
			get
			{
				return (!this.hasNextLevel) ? 0 : this.definition.GetLevelCost(this.level + 1);
			}
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x00076E7D File Offset: 0x0007527D
		public static implicit operator SerializableHeroUpgrade(HeroUpgradeDefinition definition)
		{
			return (!definition) ? null : new SerializableHeroUpgrade(definition, 0);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x00076E97 File Offset: 0x00075297
		public static implicit operator HeroUpgradeDefinition(SerializableHeroUpgrade definition)
		{
			return (definition == null) ? null : definition.definition;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00076EAB File Offset: 0x000752AB
		[OnSerializing]
		private void PreSave(StreamingContext context)
		{
			this.name = this.definition.name;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x00076EBE File Offset: 0x000752BE
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			this.definition = ResourceList<HeroUpgradeDefinition>.Get(this.name);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x00076ED1 File Offset: 0x000752D1
		public override string ToString()
		{
			return string.Format("{0} [{1}]", this.definition.dbgName, this.level);
		}

		// Token: 0x040017D4 RID: 6100
		private string name = string.Empty;

		// Token: 0x040017D5 RID: 6101
		[NonSerialized]
		public HeroUpgradeDefinition definition;

		// Token: 0x040017D6 RID: 6102
		public int level;
	}
}
