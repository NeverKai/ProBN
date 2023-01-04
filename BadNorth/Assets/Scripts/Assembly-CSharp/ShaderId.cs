using System;
using UnityEngine;

// Token: 0x020005F9 RID: 1529
[Serializable]
public struct ShaderId
{
	// Token: 0x0600276D RID: 10093 RVA: 0x0007FA5B File Offset: 0x0007DE5B
	public ShaderId(string name)
	{
		this._name = name;
		this._id = 0;
		this.hasId = false;
	}

	// Token: 0x1700054C RID: 1356
	// (get) Token: 0x0600276E RID: 10094 RVA: 0x0007FA72 File Offset: 0x0007DE72
	// (set) Token: 0x0600276F RID: 10095 RVA: 0x0007FA7A File Offset: 0x0007DE7A
	public string name
	{
		get
		{
			return this._name;
		}
		set
		{
			this._name = value;
			this.hasId = false;
		}
	}

	// Token: 0x06002770 RID: 10096 RVA: 0x0007FA8A File Offset: 0x0007DE8A
	public static implicit operator ShaderId(string input)
	{
		return new ShaderId(input);
	}

	// Token: 0x06002771 RID: 10097 RVA: 0x0007FA92 File Offset: 0x0007DE92
	public static implicit operator int(ShaderId input)
	{
		return input.id;
	}

	// Token: 0x1700054D RID: 1357
	// (get) Token: 0x06002772 RID: 10098 RVA: 0x0007FA9B File Offset: 0x0007DE9B
	public int id
	{
		get
		{
			if (!this.hasId)
			{
				this._id = Shader.PropertyToID(this.name);
				this.hasId = true;
			}
			return this._id;
		}
	}

	// Token: 0x04001942 RID: 6466
	[SerializeField]
	private string _name;

	// Token: 0x04001943 RID: 6467
	private int _id;

	// Token: 0x04001944 RID: 6468
	private bool hasId;

	// Token: 0x04001945 RID: 6469
	public static ShaderId mainTexId = "_MainTex";

	// Token: 0x04001946 RID: 6470
	public static ShaderId colorId = "_Color";
}
