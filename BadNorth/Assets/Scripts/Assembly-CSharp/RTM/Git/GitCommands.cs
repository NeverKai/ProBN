using System;
using System.Diagnostics;
using ReflexCLI.Attributes;
using UnityEngine;

namespace RTM.Git
{
	// Token: 0x020004B4 RID: 1204
	[ConsoleCommandClassCustomizer("Git")]
	public static class GitCommands
	{
		// Token: 0x06001E7F RID: 7807 RVA: 0x000515D0 File Offset: 0x0004F9D0
		public static string Run(string args)
		{
			string result;
			try
			{
				Process process = new Process
				{
					StartInfo = 
					{
						WorkingDirectory = GitCommands.WorkingDir,
						FileName = "git",
						Arguments = args,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						CreateNoWindow = true
					}
				};
				string text = string.Empty;
				process.Start();
				while (!process.StandardOutput.EndOfStream)
				{
					text = text + process.StandardOutput.ReadLine() + Environment.NewLine;
				}
				text = text.TrimEnd(Environment.NewLine.ToCharArray());
				result = text;
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarningFormat("Attempting to call git command failed\n{0}", new object[]
				{
					ex.Message
				});
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000516C0 File Offset: 0x0004FAC0
		[ConsoleCommand("")]
		public static string GetHeadRevisionShortHash()
		{
			return GitCommands.Run("rev-parse --short HEAD");
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x000516CC File Offset: 0x0004FACC
		[ConsoleCommand("")]
		public static string GetHeadRevisionDate()
		{
			return GitCommands.Run("show -s --format=%ci");
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000516D8 File Offset: 0x0004FAD8
		[ConsoleCommand("")]
		public static string GetCurrentBranchName()
		{
			return GitCommands.Run("rev-parse --abbrev-ref HEAD");
		}

		// Token: 0x040012F9 RID: 4857
		private static string WorkingDir = Application.dataPath + "/..";
	}
}
