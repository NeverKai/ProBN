using System;
using InspectorExpressions;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x02000703 RID: 1795
	[Serializable]
	internal class LevelExpression : ExpressionSerialized<LevelExpression>
	{
		// Token: 0x06002E7A RID: 11898 RVA: 0x000B54D4 File Offset: 0x000B38D4
		[ExpressionEvaluator]
		public float Evaluate(float height, float width, float index, float fraction, float count, float month, float noise, float enemies, float coinTarget)
		{
			return base.EvaluateFloat(new double[]
			{
				(double)height,
				(double)width,
				(double)index,
				(double)fraction,
				(double)count,
				(double)month,
				(double)noise,
				(double)enemies,
				(double)coinTarget
			});
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000B5524 File Offset: 0x000B3924
		public float Evaluate(LevelArcConsistency protoLevel, int hash)
		{
			return base.EvaluateFloat(new double[]
			{
				(double)protoLevel.height,
				(double)protoLevel.width,
				(double)protoLevel.index,
				(double)protoLevel.fraction,
				(double)protoLevel.count,
				(double)protoLevel.month,
				(double)protoLevel.GetNoise(hash),
				(double)protoLevel.enemyCount,
				(double)protoLevel.levelState.coinTarget
			});
		}
	}
}
