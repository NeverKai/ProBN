using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace InspectorExpressions
{
	// Token: 0x02000443 RID: 1091
	public abstract class ExpressionSerialized<T> : ExpressionSerialized where T : class, new()
	{
		// Token: 0x060018DB RID: 6363 RVA: 0x0000382C File Offset: 0x00001C2C
		static ExpressionSerialized()
		{
			MethodInfo evaluatorMethodInfo = ExpressionSerialized<T>.GetEvaluatorMethodInfo();
			if (evaluatorMethodInfo == null)
			{
				return;
			}
			foreach (ParameterInfo parameterInfo in evaluatorMethodInfo.GetParameters())
			{
				Type parameterType = parameterInfo.ParameterType;
				if (parameterType != typeof(int) && parameterType != typeof(double) && parameterType != typeof(float))
				{
					ExpressionSerialized<T>.parameterWarnings.Add(parameterInfo.Name);
				}
				ExpressionSerialized<T>.parameterNames.Add(parameterInfo.Name);
			}
			ExpressionSerialized<T>.isValid = true;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000038DA File Offset: 0x00001CDA
		protected ExpressionSerialized()
		{
			CRTPChecker.Check<T>(this);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x000038E8 File Offset: 0x00001CE8
		public override IList<string> ParameterNames
		{
			get
			{
				return ExpressionSerialized<T>.parameterNames;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x000038EF File Offset: 0x00001CEF
		public override IList<string> ParameterWarnings
		{
			get
			{
				return ExpressionSerialized<T>.parameterWarnings;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x000038F6 File Offset: 0x00001CF6
		public override bool IsValid
		{
			get
			{
				return ExpressionSerialized<T>.isValid;
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00003900 File Offset: 0x00001D00
		private static MethodInfo GetEvaluatorMethodInfo()
		{
			MethodInfo methodInfo = null;
			foreach (MethodInfo methodInfo2 in typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo2.GetCustomAttributes(typeof(ExpressionEvaluatorAttribute), false).Length != 0)
				{
					if (methodInfo != null)
					{
						Debug.LogErrorFormat("Found more than one LogicExpressionEvaluator method for type {0}", new object[]
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
				Debug.LogErrorFormat("LogicExpressionEvaluator method for type {0} not found", new object[]
				{
					typeof(T).FullName
				});
			}
			return methodInfo;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000039A8 File Offset: 0x00001DA8
		public override string TryParseExpression()
		{
			string empty = string.Empty;
			this.ParseExpression(out empty);
			return empty;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000039C4 File Offset: 0x00001DC4
		public override void ParseExpression(out string error)
		{
			try
			{
				if (this.expression == null || this.expression == string.Empty)
				{
					throw new ArgumentNullException("Expression is not set");
				}
				this.e = InspectorExpressions.Expression.Parse(this.expression, this.Constants, this.Functions);
				this.lastExpression = this.expression;
				using (Dictionary<string, Parameter>.Enumerator enumerator = this.e.Parameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, Parameter> p = enumerator.Current;
						if (ExpressionSerialized<T>.parameterNames.Find((string obj) => obj == p.Key) == null)
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

		// Token: 0x060018E3 RID: 6371 RVA: 0x00003AD8 File Offset: 0x00001ED8
		private void PrepareEvaluation(params double[] parameters)
		{
			if (this.e == null || this.expression != this.lastExpression)
			{
				string text;
				this.ParseExpression(out text);
				if (text != null)
				{
					throw new ApplicationException("LogicExpression evaluate error: " + text);
				}
			}
			if (parameters.Length != ExpressionSerialized<T>.parameterNames.Count)
			{
				throw new ArgumentNullException(string.Format("Incorrect parameters count passed: {0} but need {1}", parameters.Length, ExpressionSerialized<T>.parameterNames.Count));
			}
			int num = 0;
			foreach (double value in parameters)
			{
				Parameter parameter;
				if (this.e.Parameters.TryGetValue(ExpressionSerialized<T>.parameterNames[num], out parameter))
				{
					parameter.Value = value;
				}
				num++;
			}
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00003BAD File Offset: 0x00001FAD
		protected float EvaluateFloat(params double[] parameters)
		{
			this.PrepareEvaluation(parameters);
			return (float)this.e.Value;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00003BC2 File Offset: 0x00001FC2
		protected double EvaluateDouble(params double[] parameters)
		{
			this.PrepareEvaluation(parameters);
			return this.e.Value;
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00003BD6 File Offset: 0x00001FD6
		protected bool EvaluateBool(params double[] parameters)
		{
			this.PrepareEvaluation(parameters);
			return this.e.Value == 1.0;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00003BF8 File Offset: 0x00001FF8
		private void Validate()
		{
			string empty = string.Empty;
			this.ParseExpression(out empty);
			if (empty != null && empty != string.Empty)
			{
				Debug.LogError(empty);
			}
		}

		// Token: 0x04000F4D RID: 3917
		private static readonly List<string> parameterNames = new List<string>();

		// Token: 0x04000F4E RID: 3918
		private static readonly List<string> parameterWarnings = new List<string>();

		// Token: 0x04000F4F RID: 3919
		private static bool isValid;

		// Token: 0x04000F50 RID: 3920
		private Expression e;

		// Token: 0x04000F51 RID: 3921
		private string lastExpression;
	}
}
