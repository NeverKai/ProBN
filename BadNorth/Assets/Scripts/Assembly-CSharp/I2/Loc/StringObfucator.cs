using System;
using System.Text;

namespace I2.Loc
{
	// Token: 0x0200041E RID: 1054
	public class StringObfucator
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x0003F1E8 File Offset: 0x0003D5E8
		public static string Encode(string NormalString)
		{
			string result;
			try
			{
				string regularString = StringObfucator.XoREncode(NormalString);
				result = StringObfucator.ToBase64(regularString);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0003F224 File Offset: 0x0003D624
		public static string Decode(string ObfucatedString)
		{
			string result;
			try
			{
				string normalString = StringObfucator.FromBase64(ObfucatedString);
				result = StringObfucator.XoREncode(normalString);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0003F260 File Offset: 0x0003D660
		private static string ToBase64(string regularString)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(regularString);
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0003F280 File Offset: 0x0003D680
		private static string FromBase64(string base64string)
		{
			byte[] array = Convert.FromBase64String(base64string);
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0003F2A4 File Offset: 0x0003D6A4
		private static string XoREncode(string NormalString)
		{
			string result;
			try
			{
				char[] stringObfuscatorPassword = StringObfucator.StringObfuscatorPassword;
				char[] array = NormalString.ToCharArray();
				int num = stringObfuscatorPassword.Length;
				int i = 0;
				int num2 = array.Length;
				while (i < num2)
				{
					array[i] = (array[i] ^ stringObfuscatorPassword[i % num] ^ (char)((byte)((i % 2 != 0) ? (-i * 51) : (i * 23))));
					i++;
				}
				result = new string(array);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000F1F RID: 3871
		public static char[] StringObfuscatorPassword = "ÝúbUu¸CÁÂ§*4PÚ©-á©¾@T6Dl±ÒWâuzÅm4GÐóØ$=Íg,¥Që®iKEßr¡×60Ít4öÃ~^«y:Èd1<QÛÝúbUu¸CÁÂ§*4PÚ©-á©¾@T6Dl±ÒWâuzÅm4GÐóØ$=Íg,¥Që®iKEßr¡×60Ít4öÃ~^«y:Èd".ToCharArray();
	}
}
