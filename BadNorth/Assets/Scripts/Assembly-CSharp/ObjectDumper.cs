using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

// Token: 0x020005EC RID: 1516
public class ObjectDumper
{
	// Token: 0x06002738 RID: 10040 RVA: 0x0007EA02 File Offset: 0x0007CE02
	public string Dump(object obj)
	{
		return this.Dump(obj, null, -1);
	}

	// Token: 0x06002739 RID: 10041 RVA: 0x0007EA10 File Offset: 0x0007CE10
	private string Dump(object obj, FieldInfo fieldInfo, int lvl)
	{
		if (lvl++ > 10)
		{
			return string.Empty;
		}
		if (obj == null)
		{
			return "<null>";
		}
		string text = string.Empty;
		Type type = obj.GetType();
		object[] array = (fieldInfo == null) ? null : fieldInfo.GetCustomAttributes(typeof(ObjectDumper.HideValuesAttribute), true);
		if (array != null && array.Length > 0)
		{
			return "<values hidden>";
		}
		ObjectDumper.FormatAttribute formatAttribute = null;
		object[] array2 = (fieldInfo == null) ? null : fieldInfo.GetCustomAttributes(typeof(ObjectDumper.FormatAttribute), true);
		if (array2 != null && array2.Length > 0)
		{
			formatAttribute = (array2[0] as ObjectDumper.FormatAttribute);
		}
		if (formatAttribute != null)
		{
			return string.Format(formatAttribute.format, obj);
		}
		if (ObjectDumper.IsSimple(type, lvl))
		{
			return obj.ToString();
		}
		if (type.GetInterfaces().Contains(typeof(IEnumerable)))
		{
			Type type2 = null;
			object[] array3 = (fieldInfo == null) ? null : fieldInfo.GetCustomAttributes(typeof(ObjectDumper.EnumIndexedCollectionAttribute), true);
			if (array3 != null && array3.Length > 0)
			{
				Type enumType = ((ObjectDumper.EnumIndexedCollectionAttribute)array3[0]).enumType;
				type2 = ((!enumType.IsEnum) ? null : enumType);
			}
			int num = 0;
			this.indent = ++this.indent;
			IEnumerator enumerator = ((IEnumerable)obj).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Current;
					string text2 = null;
					if (type2 != null)
					{
						text2 = Enum.GetName(type2, num);
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = num.ToString();
					}
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						"\n",
						this.indent,
						text2,
						" : ",
						this.Dump(obj2, fieldInfo, lvl)
					});
					num++;
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			this.indent = --this.indent;
			if (num == 0)
			{
				text = text + type.Name + " [empty]";
			}
		}
		else
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.indent = ++this.indent;
			foreach (FieldInfo fieldInfo2 in fields)
			{
				string autoPropertyName = ObjectDumper.GetAutoPropertyName(fieldInfo2.Name);
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					"\n",
					this.indent,
					autoPropertyName,
					" : ",
					this.Dump(fieldInfo2.GetValue(obj), fieldInfo2, lvl)
				});
			}
			this.indent = --this.indent;
		}
		return text;
	}

	// Token: 0x0600273A RID: 10042 RVA: 0x0007ED10 File Offset: 0x0007D110
	private static string GetAutoPropertyName(string fieldName)
	{
		Match match = Regex.Match(fieldName, "<(.+?)>k__BackingField");
		if (!match.Success)
		{
			return fieldName;
		}
		fieldName = match.Groups[1].Value;
		string[] array = fieldName.Split(new char[]
		{
			'.'
		});
		if (array.Length > 0)
		{
			fieldName = array.Last<string>();
		}
		return fieldName;
	}

	// Token: 0x0600273B RID: 10043 RVA: 0x0007ED70 File Offset: 0x0007D170
	private static bool IsSimple(Type type, int lvl)
	{
		return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string)) || type.Equals(typeof(decimal)) || (lvl > 0 && type.GetCustomAttributes(typeof(ObjectDumper.LeafAttribute), true).Count<object>() > 0);
	}

	// Token: 0x04001920 RID: 6432
	private ObjectDumper.Indent indent = new ObjectDumper.Indent();

	// Token: 0x04001921 RID: 6433
	private const BindingFlags bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

	// Token: 0x020005ED RID: 1517
	[AttributeUsage(AttributeTargets.Field)]
	public class FormatAttribute : Attribute
	{
		// Token: 0x0600273C RID: 10044 RVA: 0x0007EDE3 File Offset: 0x0007D1E3
		public FormatAttribute(string format)
		{
			this.format = format;
		}

		// Token: 0x04001922 RID: 6434
		public readonly string format;
	}

	// Token: 0x020005EE RID: 1518
	[AttributeUsage(AttributeTargets.Field)]
	public class HideValuesAttribute : Attribute
	{
	}

	// Token: 0x020005EF RID: 1519
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class LeafAttribute : Attribute
	{
	}

	// Token: 0x020005F0 RID: 1520
	[AttributeUsage(AttributeTargets.Field)]
	public class EnumIndexedCollectionAttribute : Attribute
	{
		// Token: 0x0600273F RID: 10047 RVA: 0x0007EE02 File Offset: 0x0007D202
		private EnumIndexedCollectionAttribute()
		{
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0007EE0A File Offset: 0x0007D20A
		public EnumIndexedCollectionAttribute(Type enumType)
		{
			this.enumType = enumType;
		}

		// Token: 0x04001923 RID: 6435
		public readonly Type enumType;
	}

	// Token: 0x020005F1 RID: 1521
	private class Indent
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x0007EE2C File Offset: 0x0007D22C
		public static implicit operator string(ObjectDumper.Indent i)
		{
			return i._str;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0007EE34 File Offset: 0x0007D234
		public static ObjectDumper.Indent operator ++(ObjectDumper.Indent i)
		{
			i._level++;
			i.UpdateStr();
			return i;
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0007EE4B File Offset: 0x0007D24B
		public static ObjectDumper.Indent operator --(ObjectDumper.Indent i)
		{
			i._level--;
			i.UpdateStr();
			return i;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0007EE64 File Offset: 0x0007D264
		private void UpdateStr()
		{
			this._str = string.Empty;
			for (int i = 0; i < this._level; i++)
			{
				this._str += "    ";
			}
		}

		// Token: 0x04001924 RID: 6436
		private int _level;

		// Token: 0x04001925 RID: 6437
		private string _str = string.Empty;
	}
}
