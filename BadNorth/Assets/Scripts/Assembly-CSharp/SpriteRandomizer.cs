using System;
using UnityEngine;

// Token: 0x020005D0 RID: 1488
public class SpriteRandomizer : MonoBehaviour
{
	// Token: 0x060026BF RID: 9919 RVA: 0x0007B6F8 File Offset: 0x00079AF8
	private void Awake()
	{
		base.transform.localScale = Vector3.one * UnityEngine.Random.Range(this.minScale, this.maxScale);
		base.GetComponent<SpriteRenderer>().sprite = this.sprites[UnityEngine.Random.Range(0, this.sprites.Length)];
	}

	// Token: 0x040018B7 RID: 6327
	public Sprite[] sprites;

	// Token: 0x040018B8 RID: 6328
	public float minScale = 0.9f;

	// Token: 0x040018B9 RID: 6329
	public float maxScale = 1.1f;
}
