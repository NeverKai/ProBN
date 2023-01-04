using System;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000449 RID: 1097
public class IRVExample : MonoBehaviour
{
	// Token: 0x06001915 RID: 6421 RVA: 0x00041E6A File Offset: 0x0004026A
	private void appendLog(string s)
	{
		this.log = this.log + s + "\n";
		Debug.Log(s, this);
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x00041E8C File Offset: 0x0004028C
	private void netStatusChanged(InternetReachabilityVerifier.Status newStatus)
	{
		this.appendLog("Net status changed: " + newStatus);
		if (newStatus == InternetReachabilityVerifier.Status.Error)
		{
			string lastError = this.internetReachabilityVerifier.lastError;
			this.appendLog("Error: " + lastError);
			if (lastError.Contains("no crossdomain.xml"))
			{
				this.appendLog("See http://docs.unity3d.com/462/Documentation/Manual/SecuritySandbox.html - You should also check WWW Security Emulation Host URL of Unity Editor in Edit->Project Settings->Editor");
			}
		}
	}

	// Token: 0x06001917 RID: 6423 RVA: 0x00041EF0 File Offset: 0x000402F0
	private void Start()
	{
		if (this.internetReachabilityVerifier == null)
		{
			this.internetReachabilityVerifier = (InternetReachabilityVerifier)UnityEngine.Object.FindObjectOfType(typeof(InternetReachabilityVerifier));
			if (this.internetReachabilityVerifier == null)
			{
				Debug.LogError("No Internet Reachability Verifier set up for the IRVExample and none can be found in the scene!", this);
				return;
			}
		}
		this.internetReachabilityVerifier.statusChangedDelegate += this.netStatusChanged;
		this.appendLog("IRVExample log:\n");
		this.appendLog("(Initially selected method: " + this.internetReachabilityVerifier.captivePortalDetectionMethod + ")");
		if (this.internetReachabilityVerifier.captivePortalDetectionMethod == InternetReachabilityVerifier.CaptivePortalDetectionMethod.DefaultByPlatform)
		{
			this.logChosenDefaultByPlatformMethodPending = true;
		}
		this.selectedMethod = (int)this.internetReachabilityVerifier.captivePortalDetectionMethod;
		int num = 14;
		this.methodNames = new string[num];
		for (int i = 0; i < num; i++)
		{
			string[] array = this.methodNames;
			int num2 = i;
			InternetReachabilityVerifier.CaptivePortalDetectionMethod captivePortalDetectionMethod = (InternetReachabilityVerifier.CaptivePortalDetectionMethod)i;
			array[num2] = captivePortalDetectionMethod.ToString();
		}
	}

	// Token: 0x06001918 RID: 6424 RVA: 0x00041FEC File Offset: 0x000403EC
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
			GUI.skin.button.fontSize = 15 * num;
			GUI.skin.textField.fontSize = 13 * num;
			this.onGUIscaled = true;
		}
		if (this.logChosenDefaultByPlatformMethodPending && this.internetReachabilityVerifier.captivePortalDetectionMethod != InternetReachabilityVerifier.CaptivePortalDetectionMethod.DefaultByPlatform)
		{
			this.appendLog("DefaultByPlatform selected, actual method: " + this.internetReachabilityVerifier.captivePortalDetectionMethod);
			this.logChosenDefaultByPlatformMethodPending = false;
		}
		GUI.color = new Color(0.9f, 0.95f, 1f);
		GUILayout.Label("Strobotnik InternetReachabilityVerifier for Unity", new GUILayoutOption[0]);
		GUILayout.Label("Selected method: (changes to actual method as needed)", new GUILayoutOption[0]);
		this.selectedMethod = (int)this.internetReachabilityVerifier.captivePortalDetectionMethod;
		int num2 = GUILayout.SelectionGrid(this.selectedMethod, this.methodNames, 3, new GUILayoutOption[0]);
		if (this.selectedMethod != num2)
		{
			this.selectedMethod = num2;
			this.internetReachabilityVerifier.captivePortalDetectionMethod = (InternetReachabilityVerifier.CaptivePortalDetectionMethod)this.selectedMethod;
			if (this.selectedMethod == 0)
			{
				this.logChosenDefaultByPlatformMethodPending = true;
			}
			else if (this.selectedMethod == 6)
			{
				this.appendLog("Using custom method " + ((!this.internetReachabilityVerifier.customMethodWithCacheBuster) ? "without cache buster, url:\n" : "with cache buster, base url:\n") + this.internetReachabilityVerifier.customMethodURL);
			}
		}
		if (GUILayout.Button("Force reverification", new GUILayoutOption[0]))
		{
			this.internetReachabilityVerifier.forceReverification();
		}
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUI.color = new Color(0.7f, 0.8f, 0.9f);
		GUILayout.Label("Status: ", new GUILayoutOption[0]);
		GUI.color = Color.white;
		GUILayout.Label(string.Empty + this.internetReachabilityVerifier.status, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		GUI.color = new Color(0.7f, 0.8f, 0.9f);
		GUILayout.Label("Test WWW access:", new GUILayoutOption[0]);
		bool flag = this.internetReachabilityVerifier.status == InternetReachabilityVerifier.Status.NetVerified;
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (!flag || (this.testRequest != null && !this.testRequest.isDone))
		{
			GUI.enabled = false;
		}
		if (GUILayout.Button("Fetch", new GUILayoutOption[0]))
		{
			this.testRequest = UnityWebRequest.Get(this.url);
			this.testRequest.SendWebRequest();
		}
		if (this.testRequest != null && !this.testRequest.isDone)
		{
			GUI.enabled = false;
		}
		else
		{
			GUI.enabled = true;
		}
		this.url = GUILayout.TextField(this.url, new GUILayoutOption[0]);
		GUI.enabled = true;
		GUILayout.EndHorizontal();
		string text = string.Empty;
		if (this.testRequest != null)
		{
			bool isNetworkError = this.testRequest.isNetworkError;
			float downloadProgress = this.testRequest.downloadProgress;
			if (isNetworkError)
			{
				text = "error:" + this.testRequest.error;
			}
			else if (this.testRequest.isDone)
			{
				text = "done";
			}
			else
			{
				text = "progress:" + (int)(downloadProgress * 100f) + "%";
			}
		}
		GUILayout.Label(text, new GUILayoutOption[0]);
		GUI.color = Color.white;
		this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, new GUILayoutOption[0]);
		GUILayout.Label(this.log, new GUILayoutOption[0]);
		GUILayout.EndScrollView();
	}

	// Token: 0x04000F73 RID: 3955
	public InternetReachabilityVerifier internetReachabilityVerifier;

	// Token: 0x04000F74 RID: 3956
	private string log = string.Empty;

	// Token: 0x04000F75 RID: 3957
	private bool logChosenDefaultByPlatformMethodPending;

	// Token: 0x04000F76 RID: 3958
	private string url = "https://www.google.com";

	// Token: 0x04000F77 RID: 3959
	private UnityWebRequest testRequest;

	// Token: 0x04000F78 RID: 3960
	private string[] methodNames;

	// Token: 0x04000F79 RID: 3961
	private int selectedMethod;

	// Token: 0x04000F7A RID: 3962
	private Vector2 scrollPos;

	// Token: 0x04000F7B RID: 3963
	private bool onGUIscaled;
}
