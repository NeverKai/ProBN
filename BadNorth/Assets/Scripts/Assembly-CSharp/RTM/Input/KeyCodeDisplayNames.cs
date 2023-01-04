using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTM.Input
{
	// Token: 0x020004B7 RID: 1207
	internal static class KeyCodeDisplayNames
	{
		// Token: 0x06001EA4 RID: 7844 RVA: 0x00051D68 File Offset: 0x00050168
		public static string Get(KeyCode code)
		{
			string text = null;
			if (!KeyCodeDisplayNames.names.TryGetValue(code, out text))
			{
				text = code.ToString();
				KeyCodeDisplayNames.names.Add(code, text);
			}
			return text;
		}

		// Token: 0x04001304 RID: 4868
		private static Dictionary<KeyCode, string> names = new Dictionary<KeyCode, string>
		{
			{
				KeyCode.None,
				string.Empty
			},
			{
				KeyCode.Backspace,
				"BkSpace"
			},
			{
				KeyCode.Clear,
				"Clr"
			},
			{
				KeyCode.Return,
				"Rtn"
			},
			{
				KeyCode.Escape,
				"Esc"
			},
			{
				KeyCode.Exclaim,
				"Excl"
			},
			{
				KeyCode.DoubleQuote,
				"\""
			},
			{
				KeyCode.Hash,
				"#"
			},
			{
				KeyCode.Dollar,
				"$"
			},
			{
				KeyCode.Ampersand,
				"£"
			},
			{
				KeyCode.Quote,
				"'"
			},
			{
				KeyCode.LeftParen,
				"("
			},
			{
				KeyCode.RightParen,
				")"
			},
			{
				KeyCode.Asterisk,
				"*"
			},
			{
				KeyCode.Plus,
				"+"
			},
			{
				KeyCode.Comma,
				","
			},
			{
				KeyCode.Minus,
				"-"
			},
			{
				KeyCode.Period,
				"."
			},
			{
				KeyCode.Slash,
				"/"
			},
			{
				KeyCode.Alpha0,
				"0"
			},
			{
				KeyCode.Alpha1,
				"1"
			},
			{
				KeyCode.Alpha2,
				"2"
			},
			{
				KeyCode.Alpha3,
				"3"
			},
			{
				KeyCode.Alpha4,
				"4"
			},
			{
				KeyCode.Alpha5,
				"5"
			},
			{
				KeyCode.Alpha6,
				"6"
			},
			{
				KeyCode.Alpha7,
				"7"
			},
			{
				KeyCode.Alpha8,
				"8"
			},
			{
				KeyCode.Alpha9,
				"9"
			},
			{
				KeyCode.Colon,
				":"
			},
			{
				KeyCode.Semicolon,
				";"
			},
			{
				KeyCode.Less,
				"<"
			},
			{
				KeyCode.Equals,
				"="
			},
			{
				KeyCode.Greater,
				">"
			},
			{
				KeyCode.Question,
				"?"
			},
			{
				KeyCode.At,
				"@"
			},
			{
				KeyCode.LeftBracket,
				"["
			},
			{
				KeyCode.Backslash,
				"\\"
			},
			{
				KeyCode.RightBracket,
				"]"
			},
			{
				KeyCode.Caret,
				"^"
			},
			{
				KeyCode.Underscore,
				"_"
			},
			{
				KeyCode.BackQuote,
				"`"
			},
			{
				KeyCode.Delete,
				"Del"
			},
			{
				KeyCode.Keypad0,
				"Num 0"
			},
			{
				KeyCode.Keypad1,
				"Num 1"
			},
			{
				KeyCode.Keypad2,
				"Num 2"
			},
			{
				KeyCode.Keypad3,
				"Num 3"
			},
			{
				KeyCode.Keypad4,
				"Num 4"
			},
			{
				KeyCode.Keypad5,
				"Num 5"
			},
			{
				KeyCode.Keypad6,
				"Num 6"
			},
			{
				KeyCode.Keypad7,
				"Num 7"
			},
			{
				KeyCode.Keypad8,
				"Num 8"
			},
			{
				KeyCode.Keypad9,
				"Num 9"
			},
			{
				KeyCode.KeypadPeriod,
				"Num ."
			},
			{
				KeyCode.KeypadDivide,
				"Num /"
			},
			{
				KeyCode.KeypadMultiply,
				"Num *"
			},
			{
				KeyCode.KeypadMinus,
				"Num -"
			},
			{
				KeyCode.KeypadPlus,
				"Num +"
			},
			{
				KeyCode.KeypadEnter,
				"Enter"
			},
			{
				KeyCode.KeypadEquals,
				"Num ="
			},
			{
				KeyCode.UpArrow,
				"↑"
			},
			{
				KeyCode.DownArrow,
				"↓"
			},
			{
				KeyCode.RightArrow,
				"→"
			},
			{
				KeyCode.LeftArrow,
				"←"
			},
			{
				KeyCode.Insert,
				"Ins"
			},
			{
				KeyCode.PageUp,
				"PgUp"
			},
			{
				KeyCode.PageDown,
				"PgDn"
			},
			{
				KeyCode.RightShift,
				"RShift"
			},
			{
				KeyCode.LeftShift,
				"LShift"
			},
			{
				KeyCode.RightControl,
				"RCtrl"
			},
			{
				KeyCode.LeftControl,
				"LCtrl"
			},
			{
				KeyCode.RightAlt,
				"RAlt"
			},
			{
				KeyCode.LeftAlt,
				"LAlt"
			},
			{
				KeyCode.RightCommand,
				"RCmd"
			},
			{
				KeyCode.LeftCommand,
				"LCmd"
			},
			{
				KeyCode.LeftWindows,
				"LWindows"
			},
			{
				KeyCode.RightWindows,
				"RWindows"
			},
			{
				KeyCode.Mouse0,
				"Mouse 0"
			},
			{
				KeyCode.Mouse1,
				"Mouse 1"
			},
			{
				KeyCode.Mouse2,
				"Mouse 2"
			},
			{
				KeyCode.Mouse3,
				"Mouse 3"
			},
			{
				KeyCode.Mouse4,
				"Mouse 4"
			},
			{
				KeyCode.Mouse5,
				"Mouse 5"
			}
		};
	}
}
