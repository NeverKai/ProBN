using System;
using System.Collections.Generic;

namespace ReflexCLI.Parameters
{
	// Token: 0x02000455 RID: 1109
	public class ParameterProcessor
	{
		// Token: 0x0600194F RID: 6479 RVA: 0x000431AA File Offset: 0x000415AA
		public virtual List<Suggestion> GetSuggestions(Type type, string subStr, object[] attributes, int maxResults)
		{
			return null;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000431AD File Offset: 0x000415AD
		public virtual object ConvertString(Type type, string inString)
		{
			return null;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000431B0 File Offset: 0x000415B0
		protected List<Suggestion> GetMatchingSuggestions(List<Suggestion> allSuggestions, string subStr, int maxResults)
		{
			return null;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x000431C0 File Offset: 0x000415C0
		protected List<Suggestion> GetMatchingSuggestions<T>(IEnumerable<T> allSuggestions, string subStr, int maxResults)
		{
			return null;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x000431D0 File Offset: 0x000415D0
		protected static bool StringStartsWith(string inString, string subStr)
		{
			return false;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000431D3 File Offset: 0x000415D3
		protected static bool StringEquals(string inString, string candidate)
		{
			return false;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000431D6 File Offset: 0x000415D6
		protected static object GetMatchingNamedValue(Type type, string name)
		{
			return NamedParameterRegistry.GetMatchingNamedValue(type, name);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000431DF File Offset: 0x000415DF
		protected static List<Suggestion> GetNamedValueSuggestions(Type type, string subStr)
		{
			return NamedParameterRegistry.GetNamedValueSuggestions(type, subStr);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000431E8 File Offset: 0x000415E8
		protected static bool AddHierarchically(List<Suggestion> suggestions, string name, string subStr, char separator)
		{
			return false;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000431EB File Offset: 0x000415EB
		public static implicit operator bool(ParameterProcessor p)
		{
			return p != null;
		}
	}
}
