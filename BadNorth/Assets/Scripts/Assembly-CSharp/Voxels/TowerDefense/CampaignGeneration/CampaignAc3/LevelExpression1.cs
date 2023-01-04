using System;
using InspectorExpressions;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x02000705 RID: 1797
	[Serializable]
	internal class LevelExpression1 : ExpressionSerialized<LevelExpression1>
	{
		// Token: 0x06002E80 RID: 11904 RVA: 0x000B566C File Offset: 0x000B3A6C
		[ExpressionEvaluator]
		public bool Evaluate(float height, float width, float index, float fraction, float count, float month, float noise, int enemies, int a)
		{
			return base.EvaluateBool(new double[]
			{
				(double)height,
				(double)width,
				(double)index,
				(double)fraction,
				(double)count,
				(double)month,
				(double)noise,
				(double)enemies,
				(double)a
			});
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000B56BC File Offset: 0x000B3ABC
		public bool Evaluate(LevelArcConsistency protoLevel, int hash, float a)
		{
			return base.EvaluateBool(new double[]
			{
				(double)protoLevel.height,
				(double)protoLevel.width,
				(double)protoLevel.index,
				(double)protoLevel.fraction,
				(double)protoLevel.count,
				(double)protoLevel.month,
				(double)protoLevel.GetNoise(hash),
				(double)protoLevel.enemyCount,
				(double)a
			});
		}
	}
}
