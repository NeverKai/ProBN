using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200050F RID: 1295
public static class UnityComponentExtensions
{
	// Token: 0x0600215D RID: 8541 RVA: 0x0005BFC4 File Offset: 0x0005A3C4
	public static T CloneTo<T>(this T original, GameObject attachTo) where T : Component
	{
		T t = attachTo.AddComponent<T>();
		original.CloneTo(t);
		return t;
	}

	// Token: 0x0600215E RID: 8542 RVA: 0x0005BFE4 File Offset: 0x0005A3E4
	public static T CloneTo<T, O>(this O original, T target) where T : O where O : Component
	{
		string name = UnityComponentExtensions.GetName(target);
		Type typeFromHandle = typeof(O);
		BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		PropertyInfo[] properties = typeFromHandle.GetProperties(bindingAttr);
		int i = 0;
		int num = properties.Length;
		while (i < num)
		{
			PropertyInfo propertyInfo = properties[i];
			if (propertyInfo.CanRead && propertyInfo.CanWrite && !UnityComponentExtensions.IsObselete(propertyInfo))
			{
				object value = propertyInfo.GetValue(original, null);
				propertyInfo.SetValue(target, value, null);
			}
			i++;
		}
		FieldInfo[] fields = typeFromHandle.GetFields(bindingAttr);
		int j = 0;
		int num2 = fields.Length;
		while (j < num2)
		{
			FieldInfo fieldInfo = fields[j];
			object value2 = fieldInfo.GetValue(original);
			fieldInfo.SetValue(target, value2);
			j++;
		}
		UnityComponentExtensions.SetName(target, name);
		return target;
	}

	// Token: 0x0600215F RID: 8543 RVA: 0x0005C0D4 File Offset: 0x0005A4D4
	private static bool IsObselete(MemberInfo property)
	{
		object[] customAttributes = property.GetCustomAttributes(typeof(ObsoleteAttribute), true);
		return customAttributes.Length > 0;
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x0005C0F9 File Offset: 0x0005A4F9
	private static void SetName(Component comp, string name)
	{
		comp.name = name;
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x0005C102 File Offset: 0x0005A502
	private static string GetName(Component comp)
	{
		return comp.name;
	}
}
