using System;
using UnityEngine;

// Token: 0x0200060D RID: 1549
[Serializable]
public class Fake3dTex
{
	// Token: 0x060027E2 RID: 10210 RVA: 0x0008192C File Offset: 0x0007FD2C
	public Fake3dTex(Vector3 size, Texture2D texture)
	{
		this.sizeX = (int)size.x;
		this.sizeY = (int)size.y;
		this.sizeZ = (int)size.z;
		this.bounds = default(Bounds);
		this.bounds.Encapsulate(size - Vector3.one);
		this.texSizeX = Mathf.NextPowerOfTwo(this.sizeX * this.sizeY);
		this.texSizeY = Mathf.NextPowerOfTwo(this.sizeZ);
		this.texture = texture;
		this.pixels = texture.GetPixels32();
		texture.filterMode = FilterMode.Point;
	}

	// Token: 0x060027E3 RID: 10211 RVA: 0x000819D4 File Offset: 0x0007FDD4
	public Fake3dTex(Vector3 size, Color color, bool linear, TexturePool texturePool)
	{
		this.sizeX = (int)size.x;
		this.sizeY = (int)size.y;
		this.sizeZ = (int)size.z;
		this.bounds = default(Bounds);
		this.bounds.Encapsulate(size - Vector3.one);
		this.texSizeX = Mathf.NextPowerOfTwo(this.sizeX * this.sizeY);
		this.texSizeY = Mathf.NextPowerOfTwo(this.sizeZ);
		this.texturePool = texturePool;
		this.texture = texturePool.GetTexture(this.texSizeX, this.texSizeY, false);
		this.pixels = this.texture.GetPixels32();
		this.SetAllPixels(color);
	}

	// Token: 0x17000557 RID: 1367
	// (get) Token: 0x060027E4 RID: 10212 RVA: 0x00081A99 File Offset: 0x0007FE99
	public Vector3Int size
	{
		get
		{
			return new Vector3Int(this.sizeX, this.sizeY, this.sizeZ);
		}
	}

	// Token: 0x060027E5 RID: 10213 RVA: 0x00081AB2 File Offset: 0x0007FEB2
	public void ApplyPixels()
	{
		this.texture.SetPixels32(this.pixels);
		this.texture.Apply();
	}

	// Token: 0x060027E6 RID: 10214 RVA: 0x00081AD0 File Offset: 0x0007FED0
	private void SetComponentLinear(Vector3 coordinate, int index, float value, float interpolator = 1f)
	{
		Vector3Int vector3Int = ExtraMath.FloorToInt(coordinate);
		Vector3 vector = coordinate - vector3Int;
		Vector3 v = Vector3.one - vector;
		for (int i = 0; i < Fake3dTex.interpolators.Length; i++)
		{
			Vector3Int vector3Int2 = vector3Int + Fake3dTex.interpolators[i];
			if (this.bounds.Contains(vector3Int2))
			{
				Vector3 vector2 = ExtraMath.Lerp(v, vector, Fake3dTex.interpolators[i]);
				this.LerpComponent(vector3Int2, index, value, vector2.x * vector2.y * vector2.z * interpolator);
			}
		}
		this.texture.Apply();
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x00081B94 File Offset: 0x0007FF94
	private void SetPixelLinear(Vector3 coordinate, Color color)
	{
		Vector3Int vector3Int = ExtraMath.FloorToInt(coordinate);
		Vector3 vector = coordinate - vector3Int;
		Vector3 v = Vector3.one - vector;
		float a = color.a;
		color.a = 1f;
		for (int i = 0; i < Fake3dTex.interpolators.Length; i++)
		{
			Vector3Int vector3Int2 = vector3Int + Fake3dTex.interpolators[i];
			if (this.bounds.Contains(vector3Int2))
			{
				Vector3 vector2 = ExtraMath.Lerp(v, vector, Fake3dTex.interpolators[i]);
				this.LerpPixel(vector3Int2, color, vector2.x * vector2.y * vector2.z * a);
			}
		}
		this.texture.Apply();
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x00081C70 File Offset: 0x00080070
	public void AddPixelLinear(Vector3 coordinate, Color color)
	{
		Vector3Int vector3Int = ExtraMath.FloorToInt(coordinate);
		Vector3 vector = coordinate - vector3Int;
		Vector3 v = Vector3.one - vector;
		for (int i = 0; i < Fake3dTex.interpolators.Length; i++)
		{
			Vector3Int vector3Int2 = vector3Int + Fake3dTex.interpolators[i];
			if (this.bounds.Contains(vector3Int2))
			{
				Vector3 vector2 = ExtraMath.Lerp(v, vector, Fake3dTex.interpolators[i]);
				float b = vector2.x * vector2.y * vector2.z;
				this.AddPixel(vector3Int2, color * b);
			}
		}
		this.texture.Apply();
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x00081D38 File Offset: 0x00080138
	private void LerpPixel(Vector3Int coordinate, Color color, float interpolator)
	{
		int index = this.GetIndex(coordinate);
		Color color2 = this.pixels[index];
		color2 = Color.Lerp(color2, color, interpolator);
		this.pixels[index] = color2;
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x00081D84 File Offset: 0x00080184
	private void LerpComponent(Vector3Int coordinate, int index, float value, float interpolator)
	{
		int index2 = this.GetIndex(coordinate);
		Color color = this.pixels[index2];
		color = color.SetComponent(index, Mathf.Lerp(color.GetComponent(index), value, interpolator));
		this.pixels[index2] = color;
	}

	// Token: 0x060027EB RID: 10219 RVA: 0x00081DE0 File Offset: 0x000801E0
	public void SetPixel(Vector3Int coordinate, Color color)
	{
		int index = this.GetIndex(coordinate);
		this.pixels[index] = color;
	}

	// Token: 0x060027EC RID: 10220 RVA: 0x00081E0C File Offset: 0x0008020C
	public void AddPixel(Vector3Int coordinate, Color color)
	{
		int index = this.GetIndex(coordinate);
		Color32[] array = this.pixels;
		int num = index;
		array[num] += color;
	}

	// Token: 0x060027ED RID: 10221 RVA: 0x00081E48 File Offset: 0x00080248
	public int GetIndex(Vector3Int coordinate)
	{
		return coordinate.x + coordinate.y * this.sizeX + coordinate.z * this.texture.width;
	}

	// Token: 0x060027EE RID: 10222 RVA: 0x00081E74 File Offset: 0x00080274
	public void SetAllPixels(Color color)
	{
		Color32 color2 = color;
		for (int i = 0; i < this.pixels.Length; i++)
		{
			this.pixels[i] = color2;
		}
		this.texture.SetPixels32(this.pixels);
		this.texture.Apply();
	}

	// Token: 0x060027EF RID: 10223 RVA: 0x00081ED0 File Offset: 0x000802D0
	public void SetShaderVariables(string textureName, string volumeSize = "", string textureSize = "")
	{
		if (textureName != string.Empty)
		{
			Shader.SetGlobalTexture(textureName, this.texture);
		}
		if (volumeSize != string.Empty)
		{
			Shader.SetGlobalVector(volumeSize, this.size);
		}
		if (textureSize != string.Empty)
		{
			Shader.SetGlobalVector(textureSize, new Vector2((float)this.texSizeX, (float)this.texSizeY));
		}
	}

	// Token: 0x060027F0 RID: 10224 RVA: 0x00081F4D File Offset: 0x0008034D
	public void Apply()
	{
		this.texture.Apply();
	}

	// Token: 0x060027F1 RID: 10225 RVA: 0x00081F5A File Offset: 0x0008035A
	public void Destroy()
	{
		if (this.texture && this.texturePool)
		{
			this.texturePool.ReturnTexture(ref this.texture);
		}
	}

	// Token: 0x04001989 RID: 6537
	public Texture2D texture;

	// Token: 0x0400198A RID: 6538
	private Bounds bounds;

	// Token: 0x0400198B RID: 6539
	public readonly Color32[] pixels;

	// Token: 0x0400198C RID: 6540
	private readonly int sizeX;

	// Token: 0x0400198D RID: 6541
	private readonly int sizeY;

	// Token: 0x0400198E RID: 6542
	private readonly int sizeZ;

	// Token: 0x0400198F RID: 6543
	private readonly int texSizeX;

	// Token: 0x04001990 RID: 6544
	private readonly int texSizeY;

	// Token: 0x04001991 RID: 6545
	private TexturePool texturePool;

	// Token: 0x04001992 RID: 6546
	public static Vector3Int[] interpolators = new Vector3Int[]
	{
		new Vector3Int(0, 0, 0),
		new Vector3Int(0, 0, 1),
		new Vector3Int(0, 1, 0),
		new Vector3Int(0, 1, 1),
		new Vector3Int(1, 0, 0),
		new Vector3Int(1, 0, 1),
		new Vector3Int(1, 1, 0),
		new Vector3Int(1, 1, 1)
	};
}
