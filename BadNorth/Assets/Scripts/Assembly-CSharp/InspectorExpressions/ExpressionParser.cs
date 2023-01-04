using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace InspectorExpressions
{
	// Token: 0x02000440 RID: 1088
	public class ExpressionParser
	{
		// Token: 0x060018C2 RID: 6338 RVA: 0x00040274 File Offset: 0x0003E674
		public ExpressionParser(Consts consts, Funcs funcs)
		{
			this.m_Consts = consts;
			this.m_Funcs = funcs;
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00040295 File Offset: 0x0003E695
		public void AddFunc(string aName, Func<double[], double> aMethod)
		{
			this.m_Funcs[aName] = aMethod;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000402A4 File Offset: 0x0003E6A4
		public void AddConst(string aName, Func<double> aMethod)
		{
			this.m_Consts[aName] = aMethod;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000402B3 File Offset: 0x0003E6B3
		public void RemoveFunc(string aName)
		{
			if (this.m_Funcs.ContainsKey(aName))
			{
				this.m_Funcs.Remove(aName);
			}
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000402D3 File Offset: 0x0003E6D3
		public void RemoveConst(string aName)
		{
			if (this.m_Consts.ContainsKey(aName))
			{
				this.m_Consts.Remove(aName);
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000402F4 File Offset: 0x0003E6F4
		private int FindClosingBracket(ref string aText, int aStart, char aOpen, char aClose)
		{
			int num = 0;
			for (int i = aStart; i < aText.Length; i++)
			{
				if (aText[i] == aOpen)
				{
					num++;
				}
				if (aText[i] == aClose)
				{
					num--;
				}
				if (num == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0004034C File Offset: 0x0003E74C
		private void SubstitudeBracket(ref string aExpression, int aIndex)
		{
			int num = this.FindClosingBracket(ref aExpression, aIndex, '(', ')');
			if (num > aIndex + 1)
			{
				string item = aExpression.Substring(aIndex + 1, num - aIndex - 1);
				this.m_BracketHeap.Add(item);
				string str = "¤" + (this.m_BracketHeap.Count - 1) + ";";
				aExpression = aExpression.Substring(0, aIndex) + str + aExpression.Substring(num + 1);
				return;
			}
			throw new ExpressionParser.ParseException("Bracket not closed!");
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000403D8 File Offset: 0x0003E7D8
		private string[] SplitStringInTwo(string toSplit, string splitter)
		{
			int num = toSplit.IndexOf(splitter);
			string text = toSplit.Substring(0, num);
			string text2 = toSplit.Substring(num + splitter.Length);
			return new string[]
			{
				text,
				text2
			};
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00040414 File Offset: 0x0003E814
		private string[] SplitStringInTwo(string toSplit, char splitter)
		{
			int num = toSplit.IndexOf(splitter);
			string text = toSplit.Substring(0, num);
			string text2 = toSplit.Substring(num + 1);
			return new string[]
			{
				text,
				text2
			};
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0004044C File Offset: 0x0003E84C
		private IValue Parse(string aExpression)
		{
			aExpression = aExpression.Trim();
			for (int i = aExpression.IndexOf('('); i >= 0; i = aExpression.IndexOf('('))
			{
				this.SubstitudeBracket(ref aExpression, i);
			}
			if (aExpression.Contains(','))
			{
				string[] array = aExpression.Split(new char[]
				{
					','
				});
				List<IValue> list = new List<IValue>(array.Length);
				for (int j = 0; j < array.Length; j++)
				{
					string text = array[j].Trim();
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(this.Parse(text));
					}
				}
				return new MultiParameterList(list.ToArray());
			}
			if (aExpression.Contains('?') && aExpression.Contains(':') && aExpression.IndexOf('?') < aExpression.IndexOf(':'))
			{
				string[] array2 = this.SplitStringInTwo(aExpression, '?');
				string[] array3 = this.SplitStringInTwo(array2[1], ':');
				return new OperationIf(this.Parse(array2[0]), this.Parse(array3[0]), this.Parse(array3[1]));
			}
			if (aExpression.Contains("&&"))
			{
				string[] array4 = aExpression.Split(new string[]
				{
					"&&"
				}, StringSplitOptions.None);
				List<IValue> list2 = new List<IValue>(array4.Length);
				for (int k = 0; k < array4.Length; k++)
				{
					string text2 = array4[k].Trim();
					if (!string.IsNullOrEmpty(text2))
					{
						list2.Add(this.Parse(text2));
					}
				}
				if (list2.Count == 1)
				{
					return list2[0];
				}
				return new OperationAnd(list2.ToArray());
			}
			else if (aExpression.Contains("||"))
			{
				string[] array5 = aExpression.Split(new string[]
				{
					"||"
				}, StringSplitOptions.None);
				List<IValue> list3 = new List<IValue>(array5.Length);
				for (int l = 0; l < array5.Length; l++)
				{
					string text3 = array5[l].Trim();
					if (!string.IsNullOrEmpty(text3))
					{
						list3.Add(this.Parse(text3));
					}
				}
				if (list3.Count == 1)
				{
					return list3[0];
				}
				return new OperationOr(list3.ToArray());
			}
			else
			{
				if (aExpression.Contains("=="))
				{
					string[] array6 = this.SplitStringInTwo(aExpression, "==");
					return new OperationEqual(this.Parse(array6[0]), this.Parse(array6[1]));
				}
				if (aExpression.Contains("!="))
				{
					string[] array7 = this.SplitStringInTwo(aExpression, "!=");
					return new OperationNotEqual(this.Parse(array7[0]), this.Parse(array7[1]));
				}
				if (aExpression.Contains(">="))
				{
					string[] array8 = this.SplitStringInTwo(aExpression, ">=");
					return new OperationGreaterOrEqual(this.Parse(array8[0]), this.Parse(array8[1]));
				}
				if (aExpression.Contains("<="))
				{
					string[] array9 = this.SplitStringInTwo(aExpression, "<=");
					return new OperationGreaterOrEqual(this.Parse(array9[1]), this.Parse(array9[0]));
				}
				if (aExpression.Contains('>'))
				{
					string[] array10 = this.SplitStringInTwo(aExpression, '>');
					return new OperationGreater(this.Parse(array10[0]), this.Parse(array10[1]));
				}
				if (aExpression.Contains('<'))
				{
					string[] array11 = this.SplitStringInTwo(aExpression, '<');
					return new OperationGreater(this.Parse(array11[1]), this.Parse(array11[0]));
				}
				if (aExpression.Contains('!'))
				{
					string[] array12 = aExpression.Split(new char[]
					{
						'!'
					});
					List<IValue> list4 = new List<IValue>(array12.Length);
					if (!string.IsNullOrEmpty(array12[0].Trim()))
					{
						list4.Add(this.Parse(array12[0]));
					}
					for (int m = 1; m < array12.Length; m++)
					{
						string text4 = array12[m].Trim();
						if (!string.IsNullOrEmpty(text4))
						{
							list4.Add(new OperationNot(this.Parse(text4)));
						}
					}
					if (list4.Count == 1)
					{
						return list4[0];
					}
				}
				else if (aExpression.Contains('+'))
				{
					string[] array13 = aExpression.Split(new char[]
					{
						'+'
					});
					List<IValue> list5 = new List<IValue>(array13.Length);
					for (int n = 0; n < array13.Length; n++)
					{
						string text5 = array13[n].Trim();
						if (!string.IsNullOrEmpty(text5))
						{
							list5.Add(this.Parse(text5));
						}
					}
					if (list5.Count == 1)
					{
						return list5[0];
					}
					return new OperationSum(list5.ToArray());
				}
				else if (aExpression.Contains('-'))
				{
					string[] array14 = aExpression.Split(new char[]
					{
						'-'
					});
					List<IValue> list6 = new List<IValue>(array14.Length);
					if (!string.IsNullOrEmpty(array14[0].Trim()))
					{
						list6.Add(this.Parse(array14[0]));
					}
					for (int num = 1; num < array14.Length; num++)
					{
						string text6 = array14[num].Trim();
						if (!string.IsNullOrEmpty(text6))
						{
							list6.Add(new OperationNegate(this.Parse(text6)));
						}
					}
					if (list6.Count == 1)
					{
						return list6[0];
					}
					return new OperationSum(list6.ToArray());
				}
				else if (aExpression.Contains('*'))
				{
					string[] array15 = aExpression.Split(new char[]
					{
						'*'
					});
					List<IValue> list7 = new List<IValue>(array15.Length);
					for (int num2 = 0; num2 < array15.Length; num2++)
					{
						list7.Add(this.Parse(array15[num2]));
					}
					if (list7.Count == 1)
					{
						return list7[0];
					}
					return new OperationProduct(list7.ToArray());
				}
				else
				{
					if (aExpression.Contains('/'))
					{
						string[] array16 = aExpression.Split(new char[]
						{
							'/'
						});
						List<IValue> list8 = new List<IValue>(array16.Length);
						if (!string.IsNullOrEmpty(array16[0].Trim()))
						{
							list8.Add(this.Parse(array16[0]));
						}
						for (int num3 = 1; num3 < array16.Length; num3++)
						{
							string text7 = array16[num3].Trim();
							if (!string.IsNullOrEmpty(text7))
							{
								list8.Add(new OperationReciprocal(this.Parse(text7)));
							}
						}
						return new OperationProduct(list8.ToArray());
					}
					if (aExpression.Contains('^'))
					{
						int num4 = aExpression.IndexOf('^');
						IValue aValue = this.Parse(aExpression.Substring(0, num4));
						IValue aPower = this.Parse(aExpression.Substring(num4 + 1));
						return new OperationPower(aValue, aPower);
					}
					if (aExpression.Contains('%'))
					{
						int num5 = aExpression.IndexOf('%');
						IValue value = this.Parse(aExpression.Substring(0, num5));
						IValue value2 = this.Parse(aExpression.Substring(num5 + 1));
						return new OperationModulo(value, value2);
					}
				}
				foreach (KeyValuePair<string, Func<double[], double>> keyValuePair in this.m_Funcs)
				{
					if (aExpression.StartsWith(keyValuePair.Key) && this.StartWithDelimiter(aExpression.Substring(keyValuePair.Key.Length)))
					{
						string aExpression2 = aExpression.Substring(keyValuePair.Key.Length);
						IValue value3 = this.Parse(aExpression2);
						MultiParameterList multiParameterList = value3 as MultiParameterList;
						IValue[] aValues;
						if (multiParameterList != null)
						{
							aValues = multiParameterList.Parameters;
						}
						else
						{
							aValues = new IValue[]
							{
								value3
							};
						}
						return new CustomFunction(keyValuePair.Key, keyValuePair.Value, aValues);
					}
				}
				using (Dictionary<string, Func<double>>.Enumerator enumerator2 = this.m_Consts.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<string, Func<double>> C = enumerator2.Current;
						if (aExpression.StartsWith(C.Key))
						{
							return new CustomFunction(C.Key, (double[] p) => C.Value(), null);
						}
					}
				}
				int num6 = aExpression.IndexOf('¤');
				int num7 = aExpression.IndexOf(';');
				if (num6 >= 0 && num7 >= 2)
				{
					string s = aExpression.Substring(num6 + 1, num7 - num6 - 1);
					int num8;
					if (int.TryParse(s, out num8) && num8 >= 0 && num8 < this.m_BracketHeap.Count)
					{
						return this.Parse(this.m_BracketHeap[num8]);
					}
					throw new ExpressionParser.ParseException("Can't parse substitude token");
				}
				else
				{
					if (aExpression == "true")
					{
						return new Number(1.0);
					}
					if (aExpression == "false")
					{
						return new Number(0.0);
					}
					double aValue2;
					if (double.TryParse(aExpression, NumberStyles.Any, CultureInfo.InvariantCulture, out aValue2))
					{
						return new Number(aValue2);
					}
					if (!this.ValidIdentifier(aExpression))
					{
						throw new ExpressionParser.ParseException("Reached unexpected end within the parsing tree");
					}
					if (this.Parameters.ContainsKey(aExpression))
					{
						return this.Parameters[aExpression];
					}
					Parameter parameter = new Parameter(aExpression);
					this.Parameters.Add(aExpression, parameter);
					return parameter;
				}
				IValue result;
				return result;
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00040E00 File Offset: 0x0003F200
		private bool StartWithDelimiter(string aExpression)
		{
			return string.IsNullOrEmpty(aExpression) || aExpression.Length < 1 || !"abcdefghijklmnopqrstuvwxyz§$_0123456789".Contains(char.ToLower(aExpression[0]));
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00040E38 File Offset: 0x0003F238
		private bool ValidIdentifier(string aExpression)
		{
			aExpression = aExpression.Trim();
			return !string.IsNullOrEmpty(aExpression) && aExpression.Length >= 1 && !aExpression.Contains(" ") && "abcdefghijklmnopqrstuvwxyz§$".Contains(char.ToLower(aExpression[0])) && !this.m_Consts.ContainsKey(aExpression) && !this.m_Funcs.ContainsKey(aExpression);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00040EC0 File Offset: 0x0003F2C0
		public Expression EvaluateExpression(string aExpression)
		{
			Expression expression = new Expression();
			this.Parameters = expression.Parameters;
			expression.ExpressionTree = this.Parse(aExpression);
			this.Parameters = null;
			this.m_BracketHeap.Clear();
			return expression;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00040EFF File Offset: 0x0003F2FF
		public double Evaluate(string aExpression)
		{
			return this.EvaluateExpression(aExpression).Value;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00040F0D File Offset: 0x0003F30D
		public static double Eval(string aExpression)
		{
			return new ExpressionParser(Utils.BaseConstants, Utils.BaseFunctions).Evaluate(aExpression);
		}

		// Token: 0x04000F47 RID: 3911
		private List<string> m_BracketHeap = new List<string>();

		// Token: 0x04000F48 RID: 3912
		private Consts m_Consts;

		// Token: 0x04000F49 RID: 3913
		private Funcs m_Funcs;

		// Token: 0x04000F4A RID: 3914
		private Dictionary<string, Parameter> Parameters;

		// Token: 0x04000F4B RID: 3915
		private Expression m_context;

		// Token: 0x02000441 RID: 1089
		public class ParseException : Exception
		{
			// Token: 0x060018D1 RID: 6353 RVA: 0x00040F24 File Offset: 0x0003F324
			public ParseException(string aMessage) : base(aMessage)
			{
			}
		}
	}
}
