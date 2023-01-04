using System;
using System.Collections.Generic;

namespace InspectorExpressions
{
	// Token: 0x0200043A RID: 1082
	public class Expression : IValue
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0003FFF1 File Offset: 0x0003E3F1
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x0003FFF9 File Offset: 0x0003E3F9
		public IValue ExpressionTree { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x00040002 File Offset: 0x0003E402
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0004000A File Offset: 0x0003E40A
		public IValue ExpressionTreeBool { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x00040013 File Offset: 0x0003E413
		public double Value
		{
			get
			{
				return this.ExpressionTree.Value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x00040020 File Offset: 0x0003E420
		public double[] MultiValue
		{
			get
			{
				MultiParameterList multiParameterList = this.ExpressionTree as MultiParameterList;
				if (multiParameterList != null)
				{
					double[] array = new double[multiParameterList.Parameters.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = multiParameterList.Parameters[i].Value;
					}
					return array;
				}
				return null;
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00040074 File Offset: 0x0003E474
		public override string ToString()
		{
			return this.ExpressionTree.ToString();
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00040084 File Offset: 0x0003E484
		public ExpressionDelegate ToDelegate(params string[] aParamOrder)
		{
			List<Parameter> list = new List<Parameter>(aParamOrder.Length);
			for (int i = 0; i < aParamOrder.Length; i++)
			{
				if (this.Parameters.ContainsKey(aParamOrder[i]))
				{
					list.Add(this.Parameters[aParamOrder[i]]);
				}
				else
				{
					list.Add(null);
				}
			}
			Parameter[] parameters2 = list.ToArray();
			return (double[] p) => this.Invoke(p, parameters2);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00040108 File Offset: 0x0003E508
		public MultiResultDelegate ToMultiResultDelegate(params string[] aParamOrder)
		{
			List<Parameter> list = new List<Parameter>(aParamOrder.Length);
			for (int i = 0; i < aParamOrder.Length; i++)
			{
				if (this.Parameters.ContainsKey(aParamOrder[i]))
				{
					list.Add(this.Parameters[aParamOrder[i]]);
				}
				else
				{
					list.Add(null);
				}
			}
			Parameter[] parameters2 = list.ToArray();
			return (double[] p) => this.InvokeMultiResult(p, parameters2);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0004018C File Offset: 0x0003E58C
		private double Invoke(double[] aParams, Parameter[] aParamList)
		{
			int num = Math.Min(aParamList.Length, aParams.Length);
			for (int i = 0; i < num; i++)
			{
				if (aParamList[i] != null)
				{
					aParamList[i].Value = aParams[i];
				}
			}
			return this.Value;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000401D0 File Offset: 0x0003E5D0
		private double[] InvokeMultiResult(double[] aParams, Parameter[] aParamList)
		{
			int num = Math.Min(aParamList.Length, aParams.Length);
			for (int i = 0; i < num; i++)
			{
				if (aParamList[i] != null)
				{
					aParamList[i].Value = aParams[i];
				}
			}
			return this.MultiValue;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00040214 File Offset: 0x0003E614
		public static Expression Parse(string aExpression, Consts consts, Funcs funcs)
		{
			return new ExpressionParser(consts, funcs).EvaluateExpression(aExpression);
		}

		// Token: 0x04000F44 RID: 3908
		public Dictionary<string, Parameter> Parameters = new Dictionary<string, Parameter>();

		// Token: 0x0200043B RID: 1083
		public class ParameterException : Exception
		{
			// Token: 0x060018B7 RID: 6327 RVA: 0x00040223 File Offset: 0x0003E623
			public ParameterException(string aMessage) : base(aMessage)
			{
			}
		}
	}
}
