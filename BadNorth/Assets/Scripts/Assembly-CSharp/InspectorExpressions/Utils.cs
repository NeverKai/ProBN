using System;
using System.Linq;

namespace InspectorExpressions
{
	// Token: 0x02000447 RID: 1095
	public static class Utils
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00041630 File Offset: 0x0003FA30
		public static Funcs BaseFunctions
		{
			get
			{
				Funcs funcs = new Funcs();
				funcs.Add("f_sqrt", (double[] p) => Math.Sqrt(p.FirstOrDefault<double>()));
				funcs.Add("f_abs", (double[] p) => Math.Abs(p.FirstOrDefault<double>()));
				funcs.Add("f_ln", (double[] p) => Math.Log(p.FirstOrDefault<double>()));
				funcs.Add("f_floor", (double[] p) => Math.Floor(p.FirstOrDefault<double>()));
				funcs.Add("f_ceiling", (double[] p) => Math.Ceiling(p.FirstOrDefault<double>()));
				funcs.Add("f_round", (double[] p) => Math.Round(p.FirstOrDefault<double>()));
				funcs.Add("f_sin", (double[] p) => Math.Sin(p.FirstOrDefault<double>()));
				funcs.Add("f_cos", (double[] p) => Math.Cos(p.FirstOrDefault<double>()));
				funcs.Add("f_tan", (double[] p) => Math.Tan(p.FirstOrDefault<double>()));
				funcs.Add("f_asin", (double[] p) => Math.Asin(p.FirstOrDefault<double>()));
				funcs.Add("f_acos", (double[] p) => Math.Acos(p.FirstOrDefault<double>()));
				funcs.Add("f_atan", (double[] p) => Math.Atan(p.FirstOrDefault<double>()));
				funcs.Add("f_atan2", (double[] p) => Math.Atan2(p.FirstOrDefault<double>(), p.ElementAtOrDefault(1)));
				funcs.Add("f_min", (double[] p) => Math.Min(p.FirstOrDefault<double>(), p.ElementAtOrDefault(1)));
				funcs.Add("f_max", (double[] p) => Math.Max(p.FirstOrDefault<double>(), p.ElementAtOrDefault(1)));
				funcs.Add("f_clamp", (double[] p) => Math.Min(Math.Max(p.FirstOrDefault<double>(), p.ElementAtOrDefault(1)), p.ElementAtOrDefault(2)));
				funcs.Add("f_clamp01", (double[] p) => Math.Min(Math.Max(p.FirstOrDefault<double>(), 0.0), 1.0));
				Random rnd = new Random();
				funcs.Add("f_rnd", delegate(double[] p)
				{
					if (p.Length == 2)
					{
						return p[0] + rnd.NextDouble() * (p[1] - p[0]);
					}
					if (p.Length == 1)
					{
						return rnd.NextDouble() * p[0];
					}
					return rnd.NextDouble();
				});
				return funcs;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x00041914 File Offset: 0x0003FD14
		public static Consts BaseConstants
		{
			get
			{
				Consts consts = new Consts();
				consts.Add("k_pi", () => 3.141592653589793);
				consts.Add("k_e", () => 2.718281828459045);
				return consts;
			}
		}

		// Token: 0x04000F5A RID: 3930
		public const char kDiceNotationOperator = '@';

		// Token: 0x04000F5B RID: 3931
		public const char kZeroBiasNotationOperator = '#';
	}
}
