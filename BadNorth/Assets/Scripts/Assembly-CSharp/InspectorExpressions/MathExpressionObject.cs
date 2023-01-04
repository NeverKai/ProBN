using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace InspectorExpressions
{
	// Token: 0x02000446 RID: 1094
	public abstract class MathExpressionObject<T> : ExpressionObject where T : MathExpressionObject<T>
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x000412D8 File Offset: 0x0003F6D8
		static MathExpressionObject()
		{
			MethodInfo evaluatorMethodInfo = MathExpressionObject<T>.GetEvaluatorMethodInfo();
			if (evaluatorMethodInfo == null)
			{
				return;
			}
			foreach (ParameterInfo parameterInfo in evaluatorMethodInfo.GetParameters())
			{
				Type parameterType = parameterInfo.ParameterType;
				if (parameterType != typeof(int) && parameterType != typeof(double) && parameterType != typeof(float))
				{
					MathExpressionObject<T>.parameterWarnings.Add(parameterInfo.Name);
				}
				MathExpressionObject<T>.parameterNames.Add(parameterInfo.Name);
			}
			MathExpressionObject<T>.isValid = true;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00041386 File Offset: 0x0003F786
		protected MathExpressionObject()
		{
			CRTPChecker.Check<T>(this);
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x00041394 File Offset: 0x0003F794
		public override IList<string> ParameterNames
		{
			get
			{
				return MathExpressionObject<T>.parameterNames.AsReadOnly();
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x000413A0 File Offset: 0x0003F7A0
		public override IList<string> ParameterWarnings
		{
			get
			{
				return MathExpressionObject<T>.parameterWarnings.AsReadOnly();
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x000413AC File Offset: 0x0003F7AC
		public override bool IsValid
		{
			get
			{
				return MathExpressionObject<T>.isValid;
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000413B4 File Offset: 0x0003F7B4
		private static MethodInfo GetEvaluatorMethodInfo()
		{
			MethodInfo methodInfo = null;
			foreach (MethodInfo methodInfo2 in typeof(T).GetMethods())
			{
				if (methodInfo2.GetCustomAttributes(typeof(ExpressionEvaluatorAttribute), false).Length != 0)
				{
					if (methodInfo != null)
					{
						Debug.LogErrorFormat("Found more than one MathExpressionEvaluator method for type {0}", new object[]
						{
							typeof(T).FullName
						});
						return null;
					}
					methodInfo = methodInfo2;
				}
			}
			if (methodInfo == null)
			{
				Debug.LogErrorFormat("MathExpressionEvaluator method for type {0} not found", new object[]
				{
					typeof(T).FullName
				});
			}
			return methodInfo;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00041458 File Offset: 0x0003F858
		public override void ParseExpression(out string error)
		{
			try
			{
				if (this.expression == null)
				{
					throw new ArgumentNullException("Expression is not set");
				}
				this.e = InspectorExpressions.Expression.Parse(this.expression, this.Constatns, this.Functions);
				using (Dictionary<string, Parameter>.Enumerator enumerator = this.e.Parameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, Parameter> p = enumerator.Current;
						if (MathExpressionObject<T>.parameterNames.Find((string obj) => obj == p.Key) == null)
						{
							throw new ArgumentNullException(p.Key + " not found in Available parameters");
						}
					}
				}
				error = null;
			}
			catch (Exception ex)
			{
				error = ex.Message;
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00041548 File Offset: 0x0003F948
		protected double Evaluate(params double[] parameters)
		{
			if (this.e == null)
			{
				string text;
				this.ParseExpression(out text);
				if (text != null)
				{
					throw new ApplicationException("MathExpression evaluate error: " + text);
				}
			}
			if (parameters.Length != MathExpressionObject<T>.parameterNames.Count)
			{
				throw new ArgumentNullException(string.Format("Incorrect parameters count passed: {0} but need {1}", parameters.Length, MathExpressionObject<T>.parameterNames.Count));
			}
			int num = 0;
			foreach (double value in parameters)
			{
				Parameter parameter;
				if (this.e.Parameters.TryGetValue(MathExpressionObject<T>.parameterNames[num], out parameter))
				{
					parameter.Value = value;
				}
				num++;
			}
			return this.e.Value;
		}

		// Token: 0x04000F56 RID: 3926
		private static readonly List<string> parameterNames = new List<string>();

		// Token: 0x04000F57 RID: 3927
		private static readonly List<string> parameterWarnings = new List<string>();

		// Token: 0x04000F58 RID: 3928
		private static bool isValid;

		// Token: 0x04000F59 RID: 3929
		private Expression e;
	}
}
