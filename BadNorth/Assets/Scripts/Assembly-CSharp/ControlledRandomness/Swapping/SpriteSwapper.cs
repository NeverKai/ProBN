using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ControlledRandomness.Swapping
{
	// Token: 0x02000500 RID: 1280
	public class SpriteSwapper : MonoBehaviour
	{
		// Token: 0x060020B2 RID: 8370 RVA: 0x00058B22 File Offset: 0x00056F22
		public void Setup(Transform sourceContainer)
		{
			sourceContainer.GetComponentsInChildren<Swappable>(true, this.exampleSwappables);
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00058B31 File Offset: 0x00056F31
		public void Swap(PropertyBank propertyBank, Transform targetContainer, int seed)
		{
			this.RecursiveSwap(this.exampleSwappables, propertyBank, targetContainer, seed);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x00058B44 File Offset: 0x00056F44
		private void RecursiveSwap(IEnumerable<Swappable> exampleSwappables, PropertyBank propertyBank, Transform root, int seed)
		{
			for (int i = root.childCount - 1; i >= 0; i--)
			{
				Transform transform = root.transform.GetChild(i);
				Swappable swappable = transform.GetComponent<Swappable>();
				if (swappable)
				{
					this.viableReplacements.Clear();
					foreach (Swappable swappable4 in exampleSwappables)
					{
						if (swappable.CanBeReplacedBy(swappable4))
						{
							this.viableReplacements.Add(swappable4);
						}
					}
					if (this.viableReplacements.Count > 0)
					{
						this.viableReplacements[0].Seed(seed);
					}
					float value = UnityEngine.Random.value;
					Swappable swappable2 = propertyBank.Pick<Swappable>(this.viableReplacements, (Swappable s) => new Tag(swappable.tagKey + swappable.extraKey, s.tagValue), value);
					if (!swappable2 && swappable.isPlaceholder)
					{
						swappable2 = this.viableReplacements.FirstOrDefault<Swappable>();
					}
					if (swappable2)
					{
						Swappable swappable3 = UnityEngine.Object.Instantiate<Swappable>(swappable2);
						swappable3.name = swappable2.name;
						swappable.ReplaceWith(swappable3);
						transform = swappable3.transform;
					}
				}
				this.RecursiveSwap(exampleSwappables, propertyBank, transform, seed);
			}
		}

		// Token: 0x0400144D RID: 5197
		private List<Swappable> exampleSwappables = new List<Swappable>();

		// Token: 0x0400144E RID: 5198
		private List<Swappable> viableReplacements = new List<Swappable>();
	}
}
