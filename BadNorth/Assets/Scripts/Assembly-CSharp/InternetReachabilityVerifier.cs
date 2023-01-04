using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x0200044B RID: 1099
public class InternetReachabilityVerifier : MonoBehaviour
{
	// Token: 0x14000056 RID: 86
	// (add) Token: 0x06001920 RID: 6432 RVA: 0x000425AC File Offset: 0x000409AC
	// (remove) Token: 0x06001921 RID: 6433 RVA: 0x000425E4 File Offset: 0x000409E4
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event InternetReachabilityVerifier.StatusChangedDelegate statusChangedDelegate;

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x06001922 RID: 6434 RVA: 0x0004261A File Offset: 0x00040A1A
	// (set) Token: 0x06001923 RID: 6435 RVA: 0x00042624 File Offset: 0x00040A24
	public InternetReachabilityVerifier.Status status
	{
		get
		{
			return this._status;
		}
		set
		{
			InternetReachabilityVerifier.Status status = this._status;
			this._status = value;
			if (status == InternetReachabilityVerifier.Status.NetVerified && this._status != InternetReachabilityVerifier.Status.NetVerified)
			{
				this.noInternetStartTime = Time.realtimeSinceStartup;
			}
			if (this.statusChangedDelegate != null)
			{
				this.statusChangedDelegate(value);
			}
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06001924 RID: 6436 RVA: 0x00042674 File Offset: 0x00040A74
	// (set) Token: 0x06001925 RID: 6437 RVA: 0x0004267C File Offset: 0x00040A7C
	public string lastError
	{
		get
		{
			return this._lastError;
		}
		set
		{
			this._lastError = value;
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06001926 RID: 6438 RVA: 0x00042685 File Offset: 0x00040A85
	public static InternetReachabilityVerifier Instance
	{
		get
		{
			return InternetReachabilityVerifier._instance;
		}
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x0004268C File Offset: 0x00040A8C
	public float getTimeWithoutInternetConnection()
	{
		if (this.status == InternetReachabilityVerifier.Status.NetVerified)
		{
			return 0f;
		}
		return Time.realtimeSinceStartup - this.noInternetStartTime;
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x000426AC File Offset: 0x00040AAC
	public IEnumerator waitForNetVerifiedStatus()
	{
		if (this.status != InternetReachabilityVerifier.Status.NetVerified)
		{
			this.forceReverification();
		}
		while (this.status != InternetReachabilityVerifier.Status.NetVerified)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x000426C7 File Offset: 0x00040AC7
	public void setNetActivityTimes(float defaultCheckPeriodSeconds, float errorRetryDelaySeconds, float mismatchRetryDelaySeconds)
	{
		this.defaultCheckPeriod = defaultCheckPeriodSeconds;
		this.errorRetryDelay = errorRetryDelaySeconds;
		this.mismatchRetryDelay = mismatchRetryDelaySeconds;
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x000426DE File Offset: 0x00040ADE
	public void forceReverification()
	{
		this.status = InternetReachabilityVerifier.Status.PendingVerification;
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x000426E8 File Offset: 0x00040AE8
	private string getCaptivePortalDetectionURL(InternetReachabilityVerifier.CaptivePortalDetectionMethod cpdm)
	{
		string text = string.Empty;
		bool flag = false;
		if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.Custom)
		{
			text = this.customMethodURL;
			if (this.customMethodWithCacheBuster)
			{
				flag = true;
			}
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.Google204)
		{
			text = "http://clients3.google.com/generate_204";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftNCSI)
		{
			text = "http://www.msftncsi.com/ncsi.txt";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.GoogleBlank)
		{
			text = "http://www.google.com/blank.html";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.Apple)
		{
			text = "http://www.apple.com/library/test/success.html";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.Ubuntu)
		{
			text = "http://start.ubuntu.com/connectivity-check";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.Apple2)
		{
			if (this.apple2MethodURL.Length == 0)
			{
				this.apple2MethodURL = "http://captive.apple.com/";
				char[] array = new char[17];
				for (int i = 0; i < 17; i++)
				{
					array[i] = (char)(97 + UnityEngine.Random.Range(0, 26));
				}
				array[8] = '/';
				this.apple2MethodURL += new string(array);
			}
			text = this.apple2MethodURL;
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.AppleHTTPS)
		{
			text = "https://www.apple.com/library/test/success.html";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.Google204HTTPS)
		{
			text = "https://clients3.google.com/generate_204";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.UbuntuHTTPS)
		{
			text = "https://start.ubuntu.com/connectivity-check";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftConnectTest)
		{
			text = "http://www.msftconnecttest.com/connecttest.txt";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftNCSI_IPV6)
		{
			text = "http://ipv6.msftncsi.com/ncsi.txt";
		}
		else if (cpdm == InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftConnectTest_IPV6)
		{
			text = "http://ipv6.msftconnecttest.com/connecttest.txt";
		}
		if (flag || this.alwaysUseCacheBuster)
		{
			text = text + "?z=" + (UnityEngine.Random.Range(0, int.MaxValue) ^ 322402983);
		}
		return text;
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x0004288B File Offset: 0x00040C8B
	private long iwrGet_bytesDownloaded(UnityWebRequest iwr)
	{
		return (long)iwr.downloadedBytes;
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x00042893 File Offset: 0x00040C93
	private string iwrGet_text(UnityWebRequest iwr)
	{
		if (iwr == null || iwr.isNetworkError || iwr.downloadHandler == null)
		{
			return string.Empty;
		}
		return iwr.downloadHandler.text;
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x000428C2 File Offset: 0x00040CC2
	private byte[] iwrGet_bytes(UnityWebRequest iwr)
	{
		if (iwr.isNetworkError || iwr.downloadHandler == null)
		{
			return new byte[0];
		}
		return iwr.downloadHandler.data;
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x000428EC File Offset: 0x00040CEC
	private Dictionary<string, string> iwrGet_responseHeaders(UnityWebRequest iwr)
	{
		return iwr.GetResponseHeaders();
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x000428F4 File Offset: 0x00040CF4
	private string iwrGet_responseHeader(UnityWebRequest iwr, string key)
	{
		return iwr.GetResponseHeader(key);
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x000428FD File Offset: 0x00040CFD
	private bool iwrGet_isError(UnityWebRequest iwr)
	{
		return iwr.isNetworkError || iwr.responseCode >= 400L;
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x00042920 File Offset: 0x00040D20
	private string iwrGet_errorString(UnityWebRequest iwr)
	{
		if (iwr.isNetworkError)
		{
			return iwr.error;
		}
		if (iwr.responseCode >= 400L)
		{
			return iwr.responseCode.ToString();
		}
		return null;
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x00042968 File Offset: 0x00040D68
	private bool checkCaptivePortalDetectionResult(InternetReachabilityVerifier.CaptivePortalDetectionMethod cpdm, UnityWebRequest iwr)
	{
		if (iwr == null)
		{
			return false;
		}
		if (iwr.error != null && iwr.error.Length > 0)
		{
			return false;
		}
		switch (cpdm)
		{
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.Google204:
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.Google204HTTPS:
			if (iwr.responseCode == 204L)
			{
				return true;
			}
			break;
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.GoogleBlank:
			if (this.iwrGet_bytesDownloaded(iwr) == 0L)
			{
				return true;
			}
			break;
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftNCSI:
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftNCSI_IPV6:
			if (this.iwrGet_text(iwr).StartsWith("Microsoft NCSI"))
			{
				return true;
			}
			break;
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.Apple:
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.Apple2:
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.AppleHTTPS:
		{
			string text = this.iwrGet_text(iwr).ToLower();
			int num = text.IndexOf("<body>success</body>");
			int num2 = text.IndexOf("<title>success</title>");
			if ((num >= 0 && num < 500) || (num2 >= 0 && num2 < 500))
			{
				return true;
			}
			break;
		}
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.Ubuntu:
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.UbuntuHTTPS:
			if (this.iwrGet_text(iwr).IndexOf("Lorem ipsum dolor sit amet") == 109)
			{
				return true;
			}
			break;
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.Custom:
		{
			if (this.customMethodVerifierDelegate != null)
			{
				return this.customMethodVerifierDelegate(iwr, this.customMethodExpectedData);
			}
			string text2 = this.iwrGet_text(iwr);
			byte[] array = this.iwrGet_bytes(iwr);
			if ((this.customMethodExpectedData.Length > 0 && text2 != null && text2.StartsWith(this.customMethodExpectedData)) || (this.customMethodExpectedData.Length == 0 && (array == null || array.Length == 0)))
			{
				return true;
			}
			break;
		}
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftConnectTest:
		case InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftConnectTest_IPV6:
			if (this.iwrGet_text(iwr).StartsWith("Microsoft Connect Test"))
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x00042B34 File Offset: 0x00040F34
	private bool internal_yieldWait(float seconds)
	{
		if (this._yieldWaitStart == 0f)
		{
			this._yieldWaitStart = Time.realtimeSinceStartup;
		}
		bool flag = Time.realtimeSinceStartup - this._yieldWaitStart < seconds;
		if (!flag)
		{
			this._yieldWaitStart = 0f;
		}
		return flag;
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x00042B80 File Offset: 0x00040F80
	private IEnumerator netActivity()
	{
		this.netActivityRunning = true;
		NetworkReachability prevUnityReachability = Application.internetReachability;
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			this.status = InternetReachabilityVerifier.Status.PendingVerification;
		}
		else
		{
			this.status = InternetReachabilityVerifier.Status.Offline;
		}
		this.noInternetStartTime = Time.realtimeSinceStartup;
		while (this.netActivityRunning)
		{
			if (this.status == InternetReachabilityVerifier.Status.Error)
			{
				while (this.internal_yieldWait(this.errorRetryDelay) && this.status != InternetReachabilityVerifier.Status.PendingVerification)
				{
					yield return null;
				}
				this.status = InternetReachabilityVerifier.Status.PendingVerification;
			}
			else if (this.status == InternetReachabilityVerifier.Status.Mismatch)
			{
				while (this.internal_yieldWait(this.mismatchRetryDelay) && this.status != InternetReachabilityVerifier.Status.PendingVerification)
				{
					yield return null;
				}
				this.status = InternetReachabilityVerifier.Status.PendingVerification;
			}
			NetworkReachability unityReachability = Application.internetReachability;
			if (prevUnityReachability != unityReachability)
			{
				if (unityReachability != NetworkReachability.NotReachable)
				{
					this.status = InternetReachabilityVerifier.Status.PendingVerification;
				}
				else if (unityReachability == NetworkReachability.NotReachable)
				{
					this.status = InternetReachabilityVerifier.Status.Offline;
				}
				prevUnityReachability = Application.internetReachability;
			}
			if (this.status == InternetReachabilityVerifier.Status.PendingVerification)
			{
				this.verifyCaptivePortalDetectionMethod();
				InternetReachabilityVerifier.CaptivePortalDetectionMethod cpdm = this.captivePortalDetectionMethod;
				string url = this.getCaptivePortalDetectionURL(cpdm);
				UnityWebRequest iwr = UnityWebRequest.Get(url);
				yield return iwr.SendWebRequest();
				if (this.iwrGet_isError(iwr))
				{
					this.lastError = this.iwrGet_errorString(iwr);
					this.status = InternetReachabilityVerifier.Status.Error;
					continue;
				}
				bool flag = this.checkCaptivePortalDetectionResult(cpdm, iwr);
				if (!flag)
				{
					this.status = InternetReachabilityVerifier.Status.Mismatch;
					continue;
				}
				this.status = InternetReachabilityVerifier.Status.NetVerified;
			}
			while (this.internal_yieldWait(this.defaultCheckPeriod) && this.status != InternetReachabilityVerifier.Status.PendingVerification)
			{
				yield return null;
			}
		}
		this.netActivityRunning = false;
		this.status = InternetReachabilityVerifier.Status.PendingVerification;
		yield break;
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x00042B9B File Offset: 0x00040F9B
	private void Awake()
	{
		if (InternetReachabilityVerifier._instance)
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
			return;
		}
		InternetReachabilityVerifier._instance = this;
		if (this.dontDestroyOnLoad)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x00042BD4 File Offset: 0x00040FD4
	private void verifyCaptivePortalDetectionMethod()
	{
		if (this.captivePortalDetectionMethod == InternetReachabilityVerifier.CaptivePortalDetectionMethod.DefaultByPlatform)
		{
			this.captivePortalDetectionMethod = InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftNCSI;
			if (this.captivePortalDetectionMethod == InternetReachabilityVerifier.CaptivePortalDetectionMethod.DefaultByPlatform)
			{
				this.captivePortalDetectionMethod = InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftNCSI;
			}
		}
		if (this.captivePortalDetectionMethod == InternetReachabilityVerifier.CaptivePortalDetectionMethod.Google204 && Array.IndexOf<RuntimePlatform>(InternetReachabilityVerifier.methodGoogle204Supported, Application.platform) < 0)
		{
			this.captivePortalDetectionMethod = InternetReachabilityVerifier.CaptivePortalDetectionMethod.GoogleBlank;
		}
		if (this.captivePortalDetectionMethod == InternetReachabilityVerifier.CaptivePortalDetectionMethod.Custom && this.customMethodURL.Length == 0)
		{
			UnityEngine.Debug.LogError("IRV - Custom method is selected but URL is empty, cannot start! (disabling component)", this);
			base.enabled = false;
			if (this.netActivityRunning)
			{
				this.Stop();
			}
			return;
		}
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x00042C6D File Offset: 0x0004106D
	private void Start()
	{
		this.verifyCaptivePortalDetectionMethod();
		if (!this.netActivityRunning)
		{
			base.StartCoroutine("netActivity");
		}
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x00042C8C File Offset: 0x0004108C
	private void OnDisable()
	{
		this.Stop();
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x00042C94 File Offset: 0x00041094
	private void OnEnable()
	{
		this.Start();
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x00042C9C File Offset: 0x0004109C
	public void Stop()
	{
		base.StopCoroutine("netActivity");
		this.netActivityRunning = false;
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x00042CB0 File Offset: 0x000410B0
	// Note: this type is marked as 'beforefieldinit'.
	static InternetReachabilityVerifier()
	{
		RuntimePlatform[] array = new RuntimePlatform[6];
		RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.$field-A521C39CEF3283C4ED1AFD0A913B091FAFDDFD6D).FieldHandle);
		InternetReachabilityVerifier.methodGoogle204Supported = array;
	}

	// Token: 0x04000F7D RID: 3965
	private const int version = 1200;

	// Token: 0x04000F7E RID: 3966
	public InternetReachabilityVerifier.CaptivePortalDetectionMethod captivePortalDetectionMethod;

	// Token: 0x04000F7F RID: 3967
	[Tooltip("Self-hosted URL for using CaptivePortalDetectionMethod.Custom. For example: https://example.com/IRV.txt")]
	public string customMethodURL = string.Empty;

	// Token: 0x04000F80 RID: 3968
	[Tooltip("Data expected from the custom self-hosted URL. By default the data returned by the custom url is expected to start with contents of this string. Alternatively you can set the customMethodVerifierDelegate (see example code), in which case this string will be passed to the delegate.")]
	public string customMethodExpectedData = "OK";

	// Token: 0x04000F81 RID: 3969
	[Tooltip("Makes the IRV object not be destroyed automatically when loading a new scene.")]
	public bool dontDestroyOnLoad = true;

	// Token: 0x04000F82 RID: 3970
	[Tooltip("When enabled, custom method URL is appended with a query string containing a random number.\nExample of what such a query string may look like: ?z=13371337")]
	public bool customMethodWithCacheBuster = true;

	// Token: 0x04000F83 RID: 3971
	[Tooltip("Default time in seconds to wait until trying to verify network connectivity again.\nSuggested minimum: 1 second.")]
	public float defaultCheckPeriod = 4f;

	// Token: 0x04000F84 RID: 3972
	[Tooltip("Time in seconds to wait before retrying, after last verification attempt resulted in an error.\nSuggested minimum: 3 seconds.")]
	public float errorRetryDelay = 15f;

	// Token: 0x04000F85 RID: 3973
	[Tooltip("Time in seconds to wait after detecting a captive portal (WiFi login screen).\nSuggested minimum: 2 seconds.")]
	public float mismatchRetryDelay = 7f;

	// Token: 0x04000F86 RID: 3974
	[HideInInspector]
	public bool alwaysUseCacheBuster;

	// Token: 0x04000F88 RID: 3976
	public InternetReachabilityVerifier.CustomMethodVerifierDelegate customMethodVerifierDelegate;

	// Token: 0x04000F89 RID: 3977
	private float noInternetStartTime;

	// Token: 0x04000F8A RID: 3978
	private InternetReachabilityVerifier.Status _status;

	// Token: 0x04000F8B RID: 3979
	private string _lastError = string.Empty;

	// Token: 0x04000F8C RID: 3980
	private static InternetReachabilityVerifier _instance = null;

	// Token: 0x04000F8D RID: 3981
	private static RuntimePlatform[] methodGoogle204Supported;

	// Token: 0x04000F8E RID: 3982
	private const InternetReachabilityVerifier.CaptivePortalDetectionMethod fallbackMethodIfNoDefaultByPlatform = InternetReachabilityVerifier.CaptivePortalDetectionMethod.MicrosoftNCSI;

	// Token: 0x04000F8F RID: 3983
	private bool netActivityRunning;

	// Token: 0x04000F90 RID: 3984
	private string apple2MethodURL = string.Empty;

	// Token: 0x04000F91 RID: 3985
	private float _yieldWaitStart;

	// Token: 0x0200044C RID: 1100
	public enum CaptivePortalDetectionMethod
	{
		// Token: 0x04000F93 RID: 3987
		DefaultByPlatform,
		// Token: 0x04000F94 RID: 3988
		Google204,
		// Token: 0x04000F95 RID: 3989
		GoogleBlank,
		// Token: 0x04000F96 RID: 3990
		MicrosoftNCSI,
		// Token: 0x04000F97 RID: 3991
		Apple,
		// Token: 0x04000F98 RID: 3992
		Ubuntu,
		// Token: 0x04000F99 RID: 3993
		Custom,
		// Token: 0x04000F9A RID: 3994
		Apple2,
		// Token: 0x04000F9B RID: 3995
		AppleHTTPS,
		// Token: 0x04000F9C RID: 3996
		Google204HTTPS,
		// Token: 0x04000F9D RID: 3997
		UbuntuHTTPS,
		// Token: 0x04000F9E RID: 3998
		MicrosoftConnectTest,
		// Token: 0x04000F9F RID: 3999
		MicrosoftNCSI_IPV6,
		// Token: 0x04000FA0 RID: 4000
		MicrosoftConnectTest_IPV6
	}

	// Token: 0x0200044D RID: 1101
	public enum Status
	{
		// Token: 0x04000FA2 RID: 4002
		Offline,
		// Token: 0x04000FA3 RID: 4003
		PendingVerification,
		// Token: 0x04000FA4 RID: 4004
		Error,
		// Token: 0x04000FA5 RID: 4005
		Mismatch,
		// Token: 0x04000FA6 RID: 4006
		NetVerified
	}

	// Token: 0x0200044E RID: 1102
	// (Invoke) Token: 0x0600193E RID: 6462
	public delegate void StatusChangedDelegate(InternetReachabilityVerifier.Status newStatus);

	// Token: 0x0200044F RID: 1103
	// (Invoke) Token: 0x06001942 RID: 6466
	public delegate bool CustomMethodVerifierDelegate(UnityWebRequest wr, string customMethodExpectedData);
}
