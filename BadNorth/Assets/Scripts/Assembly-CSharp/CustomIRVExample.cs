using System;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000448 RID: 1096
[RequireComponent(typeof(InternetReachabilityVerifier))]
public class CustomIRVExample : MonoBehaviour
{
	// Token: 0x0600190F RID: 6415 RVA: 0x00041B19 File Offset: 0x0003FF19
	private void appendLog(string s, bool error = false)
	{
		this.log = this.log + s + "\n";
		if (error)
		{
			Debug.LogError(s, this);
		}
		else
		{
			Debug.Log(s, this);
		}
	}

	// Token: 0x06001910 RID: 6416 RVA: 0x00041B4C File Offset: 0x0003FF4C
	private bool verifyNetCheckData(UnityWebRequest wr, string customMethodExpectedData)
	{
		if (customMethodExpectedData == null || customMethodExpectedData.Length == 0)
		{
			this.appendLog("Custom verifyNetCheckData - Null or empty customMethodExpectedData!", false);
			return false;
		}
		string text = wr.downloadHandler.text;
		bool flag = text.Contains(customMethodExpectedData);
		this.appendLog(string.Concat(new object[]
		{
			"Custom verifyNetCheckData - result:",
			flag,
			", customMethodExpectedData:",
			customMethodExpectedData,
			", text:",
			text
		}), false);
		return flag;
	}

	// Token: 0x06001911 RID: 6417 RVA: 0x00041BC8 File Offset: 0x0003FFC8
	private void netStatusChanged(InternetReachabilityVerifier.Status newStatus)
	{
		this.appendLog("Net status changed: " + newStatus, false);
		if (newStatus == InternetReachabilityVerifier.Status.Error)
		{
			string lastError = this.irv.lastError;
			this.appendLog("Error: " + lastError, false);
			if (lastError.Contains("no crossdomain.xml"))
			{
				this.appendLog("See http://docs.unity3d.com/462/Documentation/Manual/SecuritySandbox.html - You should also check WWW Security Emulation Host URL of Unity Editor in Edit->Project Settings->Editor", false);
			}
		}
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x00041C30 File Offset: 0x00040030
	private void Start()
	{
		this.irv = base.GetComponent<InternetReachabilityVerifier>();
		this.irv.customMethodVerifierDelegate = new InternetReachabilityVerifier.CustomMethodVerifierDelegate(this.verifyNetCheckData);
		this.irv.statusChangedDelegate += this.netStatusChanged;
		this.appendLog("CustomIRVExample log:\n", false);
		this.appendLog("Selected method: " + this.irv.captivePortalDetectionMethod, false);
		this.appendLog("Custom Method URL: " + this.irv.customMethodURL, false);
		this.appendLog("Custom Method Expected Data: " + this.irv.customMethodExpectedData, false);
		if (this.irv.customMethodVerifierDelegate != null)
		{
			this.appendLog("Using custom method verifier delegate.", false);
		}
		if (this.irv.customMethodURL.Contains("strobotnik.com"))
		{
			this.appendLog("*** IMPORTANT WARNING: ***\nYou're using the default TEST value for Custom Method URL specified in example scene.\nTHAT SERVER HAS NO GUARANTEE OF BEING UP AND RUNNING.\nPlease use your own custom server and URL!\n*****\n", true);
		}
	}

	// Token: 0x06001913 RID: 6419 RVA: 0x00041D20 File Offset: 0x00040120
	private void OnGUI()
	{
		if (!this.onGUIscaled)
		{
			int num;
			if (Screen.width > Screen.height)
			{
				num = Mathf.Max(1, (int)((float)Screen.height / 480f));
			}
			else
			{
				num = Mathf.Max(1, (int)((float)Screen.height / 800f));
			}
			GUI.skin.label.fontSize = 14 * num;
			this.onGUIscaled = true;
		}
		GUI.color = new Color(0.9f, 0.95f, 1f);
		GUILayout.Label("Strobotnik InternetReachabilityVerifier for Unity", new GUILayoutOption[0]);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUI.color = new Color(0.7f, 0.8f, 0.9f);
		GUILayout.Label("Status: ", new GUILayoutOption[0]);
		GUI.color = Color.white;
		GUILayout.Label(string.Empty + this.irv.status, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, new GUILayoutOption[0]);
		GUILayout.Label(this.log, new GUILayoutOption[0]);
		GUILayout.EndScrollView();
	}

	// Token: 0x04000F6F RID: 3951
	private InternetReachabilityVerifier irv;

	// Token: 0x04000F70 RID: 3952
	private string log = string.Empty;

	// Token: 0x04000F71 RID: 3953
	private Vector2 scrollPos;

	// Token: 0x04000F72 RID: 3954
	private bool onGUIscaled;
}
