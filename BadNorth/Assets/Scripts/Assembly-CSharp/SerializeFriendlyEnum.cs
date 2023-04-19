
using System;
using System.Reflection;
using System.Runtime.Serialization;

// Token: 0x020005A3 RID: 1443
[ObjectDumper.LeafAttribute]
[Serializable]
public class SerializeFriendlyEnum<TEnum> : IComparable<TEnum> where TEnum : struct, IComparable, IFormattable, IConvertible
{
	// Token: 0x0600259B RID: 9627 RVA: 0x00076EF3 File Offset: 0x000752F3
	private SerializeFriendlyEnum(TEnum value)
	{
		this._value = value;
		this._valueString = string.Empty;
	}

	// Token: 0x170004DB RID: 1243
	// (get) Token: 0x0600259C RID: 9628 RVA: 0x00076F0D File Offset: 0x0007530D
	// (set) Token: 0x0600259D RID: 9629 RVA: 0x00076F15 File Offset: 0x00075315
	public TEnum value
	{
		get
		{
			return this._value;
		}
		set
		{
			this._value = value;
		}
	}

	// Token: 0x0600259E RID: 9630 RVA: 0x00076F1E File Offset: 0x0007531E
	public static implicit operator SerializeFriendlyEnum<TEnum>(TEnum value)
	{
		return new SerializeFriendlyEnum<TEnum>(value);
	}

	// Token: 0x0600259F RID: 9631 RVA: 0x00076F26 File Offset: 0x00075326
	public static implicit operator TEnum(SerializeFriendlyEnum<TEnum> value)
	{
		return value._value;
	}

	// Token: 0x060025A0 RID: 9632 RVA: 0x00076F2E File Offset: 0x0007532E
	[OnSerializing]
	private void OnSerilaizing(StreamingContext context)
	{
		this._valueString = this._value.ToString();
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x00076F48 File Offset: 0x00075348
	[OnDeserialized]
	private void OnDeserialized(StreamingContext context)
	{
		try
		{
			this._value = (TEnum)((object)Enum.Parse(typeof(TEnum), this._valueString, true));
		}
		catch
		{
			Type typeFromHandle = typeof(TEnum);
			if (!Enum.IsDefined(typeFromHandle, this._value))
			{
				BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;
				FieldInfo[] fields = typeFromHandle.GetFields(bindingAttr);
				foreach (FieldInfo fieldInfo in fields)
				{
					if (fieldInfo.FieldType == typeFromHandle)
					{
						this._value = (TEnum)((object)fieldInfo.GetValue(this));
						break;
					}
				}
			}
		}
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x00077004 File Offset: 0x00075404
	public override string ToString()
	{
		return string.Format("{0} ('{1}')", this._value, this._valueString);
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x00077024 File Offset: 0x00075424
	public int CompareTo(TEnum other)
	{
		TEnum value = this.value;
		return value.CompareTo(other);
	}

	// Token: 0x040017D7 RID: 6103
	private TEnum _value;

	// Token: 0x040017D8 RID: 6104
	private string _valueString;
}
