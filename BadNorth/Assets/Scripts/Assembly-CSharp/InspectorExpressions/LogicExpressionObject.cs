using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace InspectorExpressions
{
	// Token: 0x02000445 RID: 1093
	public abstract class LogicExpressionObject<T> : ExpressionObject where T : LogicExpressionObject<T>
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x00040F80 File Offset: 0x0003F380
		static LogicExpressionObject()
		{
			MethodInfo evaluatorMethodInfo = LogicExpressionObject<T>.GetEvaluatorMethodInfo();
			if (evaluatorMethodInfo == null)
			{
				return;
			}
			foreach (ParameterInfo parameterInfo in evaluatorMethodInfo.GetParameters())
			{
				Type parameterType = parameterInfo.ParameterType;
				if (parameterType != typeof(int) && parameterType != typeof(double) && parameterType != typeof(float))
				{
					LogicExpressionObject<T>.parameterWarnings.Add(parameterInfo.Name);
				}
				LogicExpressionObject<T>.parameterNames.Add(parameterInfo.Name);
			}
			LogicExpressionObject<T>.isValid = true;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0004102E File Offset: 0x0003F42E
		protected LogicExpressionObject()
		{
			CRTPChecker.Check<T>(this);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0004103C File Offset: 0x0003F43C
		public override IList<string> ParameterNames
		{
			get
			{
				return LogicExpressionObject<T>.parameterNames.AsReadOnly();
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00041048 File Offset: 0x0003F448
		public override IList<string> ParameterWarnings
		{
			get
			{
				return LogicExpressionObject<T>.parameterWarnings.AsReadOnly();
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x00041054 File Offset: 0x0003F454
		public override bool IsValid
		{
			get
			{
				return LogicExpressionObject<T>.isValid;
			}
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x0004105C File Offset: 0x0003F45C
		private static MethodInfo GetEvaluatorMethodInfo()
		{
			MethodInfo methodInfo = null;
			foreach (MethodInfo methodInfo2 in typeof(T).GetMethods())
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

		// Token: 0x060018EF RID: 6383 RVA: 0x00041100 File Offset: 0x0003F500
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
						if (LogicExpressionObject<T>.parameterNames.Find((string obj) => obj == p.Key) == null)
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

		// Token: 0x060018F0 RID: 6384 RVA: 0x000411F0 File Offset: 0x0003F5F0
		protected double Evaluate(params double[] parameters)
		{
			if (this.e == null)
			{
				string text;
				this.ParseExpression(out text);
				if (text != null)
				{
					throw new ApplicationException("LogicExpression evaluate error: " + text);
				}
			}
			if (parameters.Length != LogicExpressionObject<T>.parameterNames.Count)
			{
				throw new ArgumentNullException(string.Format("Incorrect parameters count passed: {0} but need {1}", parameters.Length, LogicExpressionObject<T>.parameterNames.Count));
			}
			int num = 0;
			foreach (double value in parameters)
			{
				Parameter parameter;
				if (this.e.Parameters.TryGetValue(LogicExpressionObject<T>.parameterNames[num], out parameter))
				{
					parameter.Value = value;
				}
				num++;
			}
			return this.e.Value;
		}

		// Token: 0x04000F52 RID: 3922
		private static readonly List<string> parameterNames = new List<string>();

		// Token: 0x04000F53 RID: 3923
		private static readonly List<string> parameterWarnings = new List<string>();

		// Token: 0x04000F54 RID: 3924
		private static bool isValid;

		// Token: 0x04000F55 RID: 3925
		private Expression e;
	}
}
