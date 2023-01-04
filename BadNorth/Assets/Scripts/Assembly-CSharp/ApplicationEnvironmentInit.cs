using System;
using UnityEngine;
using UnityEngine.Assertions;

// Token: 0x020005D7 RID: 1495
public static class ApplicationEnvironmentInit
{
	// Token: 0x060026DF RID: 9951 RVA: 0x0007C594 File Offset: 0x0007A994
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		Screen.sleepTimeout = -1;
		Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
		Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
		Assert.raiseExceptions = true;
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		string text = "Command Line Arguments";
		foreach (string str in commandLineArgs)
		{
			text = text + " " + str;
		}
		Debug.Log(text);
	}
}
