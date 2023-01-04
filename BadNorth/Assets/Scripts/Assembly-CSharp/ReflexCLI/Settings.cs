using System;
using UnityEngine;

namespace ReflexCLI
{
	// Token: 0x02000458 RID: 1112
	public class Settings : ScriptableObject
	{
		// Token: 0x04000FB3 RID: 4019
		[Header("General")]
		[SerializeField]
		[Tooltip("Global enable control for the console (cannot be changed at runtime)")]
		private bool _EnableConsole = true;

		// Token: 0x04000FB4 RID: 4020
		[Header("Control Keys")]
		[SerializeField]
		[Tooltip("KeyCode for opening the console")]
		private KeyCode _OpenConsoleKey = KeyCode.Tab;

		// Token: 0x04000FB5 RID: 4021
		[SerializeField]
		[Tooltip("KeyCode for closing the console")]
		private KeyCode _CloseConsoleKey = KeyCode.Escape;

		// Token: 0x04000FB6 RID: 4022
		[Header("Control Buttons")]
		[SerializeField]
		[Tooltip("Button for opening the console")]
		private int _OpenConsoleButton = 8;

		// Token: 0x04000FB7 RID: 4023
		[SerializeField]
		[Tooltip("Button for closing the console")]
		private int _CloseConsoleButton = 9;

		// Token: 0x04000FB8 RID: 4024
		[Header("Logging")]
		[SerializeField]
		[Tooltip("Displays standard Unity Debug.Log() Messages in the console")]
		private bool _DisplayUnityLog;

		// Token: 0x04000FB9 RID: 4025
		[Header("Command Format")]
		[SerializeField]
		[Tooltip("Define the default naming for Commands")]
		private ENamingConvention _DefaultNamingConvention = ENamingConvention.NamespaceAndClassPrefix;

		// Token: 0x04000FBA RID: 4026
		[SerializeField]
		[Tooltip("Ignore case for autocomplete suggestions")]
		private bool _SuggestionsCaseInsensitive = true;

		// Token: 0x04000FBB RID: 4027
		[SerializeField]
		[Tooltip("Autocomplete using hierarchies (i.e. group by namespace/classname etc.)")]
		private bool _HierarchicalAutocomplete = true;

		// Token: 0x04000FBC RID: 4028
		[Header("Console Behaviour")]
		[SerializeField]
		[Tooltip("Should console search for inactive GameObjects / MonoBehaviours when processing command parameters?")]
		private bool _IncludeInactiveObjects = true;

		// Token: 0x04000FBD RID: 4029
		[Header("Console Visuals")]
		[SerializeField]
		[Tooltip("Set Font Overrides")]
		private Font _ConsoleFont;
	}
}
