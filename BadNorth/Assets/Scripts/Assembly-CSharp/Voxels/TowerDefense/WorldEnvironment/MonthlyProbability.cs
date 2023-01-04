using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.WorldEnvironment
{
	// Token: 0x02000871 RID: 2161
	[Serializable]
	internal class MonthlyProbability : IEnumerable<float>, IEnumerable
	{
		// Token: 0x060038A2 RID: 14498 RVA: 0x000F4B5E File Offset: 0x000F2F5E
		private MonthlyProbability()
		{
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000F4B73 File Offset: 0x000F2F73
		public MonthlyProbability(float[] probabilities)
		{
			this.probabilities = probabilities;
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000F4B90 File Offset: 0x000F2F90
		public MonthlyProbability(Vector4 seasons)
		{
			this.probabilities = new float[12];
			for (int i = 0; i < 12; i++)
			{
				this.probabilities[i] = seasons.GetComponent(MonthlyProbability.MonthToSeason(i));
			}
		}

		// Token: 0x1700081D RID: 2077
		public float this[int month]
		{
			get
			{
				return this.probabilities[month];
			}
			set
			{
				this.probabilities[month] = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		public float this[Month month]
		{
			get
			{
				return this.probabilities[(int)month];
			}
			set
			{
				this.probabilities[(int)month] = value;
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000F4C0E File Offset: 0x000F300E
		public static implicit operator MonthlyProbability(float[] probabilities)
		{
			return new MonthlyProbability(probabilities);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000F4C16 File Offset: 0x000F3016
		public static implicit operator MonthlyProbability(Vector4 seasons)
		{
			return new MonthlyProbability(seasons);
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000F4C1E File Offset: 0x000F301E
		public IEnumerator<float> GetEnumerator()
		{
			return ((IEnumerable<float>)this.probabilities).GetEnumerator();
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000F4C2B File Offset: 0x000F302B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<float>)this.probabilities).GetEnumerator();
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000F4C38 File Offset: 0x000F3038
		public static int MonthToSeason(int month)
		{
			return Mathf.FloorToInt((float)(month + 1) / 3f) % 4;
		}

		// Token: 0x04002689 RID: 9865
		[SerializeField]
		private float[] probabilities = new float[12];
	}
}
