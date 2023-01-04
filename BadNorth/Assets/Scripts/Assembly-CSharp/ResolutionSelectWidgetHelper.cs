using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020008D1 RID: 2257
public class ResolutionSelectWidgetHelper : MonoBehaviour
{
	// Token: 0x1700085C RID: 2140
	// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x001092B7 File Offset: 0x001076B7
	// (set) Token: 0x06003BA2 RID: 15266 RVA: 0x001092BF File Offset: 0x001076BF
	public MultiSelectWidget widget { get; private set; }

	// Token: 0x06003BA3 RID: 15267 RVA: 0x001092C8 File Offset: 0x001076C8
	public void Setup(string labelTerm)
	{
		this.widget = base.GetComponent<MultiSelectWidget>();
		this.widget.Initialize(labelTerm, new Func<int>(this.GetResolutionIdx), new Func<int, bool>(this.SetResolutionIdx));
		this.UpdateList();
	}

	// Token: 0x06003BA4 RID: 15268 RVA: 0x00109304 File Offset: 0x00107704
	public void UpdateList()
	{
		this.map = new Dictionary<string, Resolution>();
		foreach (Resolution resolution in Screen.resolutions)
		{
			if (resolution.width >= 1000 && resolution.height >= 700)
			{
				string displayString = ResolutionSelectWidgetHelper.GetDisplayString(resolution);
				if (!this.map.ContainsKey(displayString))
				{
					this.map.Add(displayString, resolution);
					this.widget.SetValues(this.map.Keys.ToArray<string>(), false);
				}
			}
		}
	}

	// Token: 0x06003BA5 RID: 15269 RVA: 0x001093B1 File Offset: 0x001077B1
	private static string GetDisplayString(Resolution resolution)
	{
		return string.Format("{0} x {1}", resolution.width, resolution.height);
	}

	// Token: 0x06003BA6 RID: 15270 RVA: 0x001093D8 File Offset: 0x001077D8
	private int GetResolutionIdx()
	{
		int width = Screen.width;
		int height = Screen.height;
		int num = int.MaxValue;
		int result = 0;
		int num2 = 0;
		foreach (KeyValuePair<string, Resolution> keyValuePair in this.map)
		{
			Resolution value = keyValuePair.Value;
			if (value.width == width && value.height == height)
			{
				return num2;
			}
			if (Mathf.Abs(value.width - width) < Mathf.Abs(num - width))
			{
				num = value.width;
				result = num2;
			}
			num2++;
		}
		Debug.LogErrorFormat("current resolution {0}x{1} does not exist in the list", new object[]
		{
			Screen.width,
			Screen.height
		});
		return result;
	}

	// Token: 0x06003BA7 RID: 15271 RVA: 0x001094CC File Offset: 0x001078CC
	private bool SetResolutionIdx(int idx)
	{
		string text = this.widget.values[idx];
		Resolution resolution;
		if (this.map.TryGetValue(text, out resolution))
		{
			Debug.LogFormat("Setting resolution {0}", new object[]
			{
				resolution
			});
			Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
		}
		else
		{
			Debug.LogErrorFormat("Failed to set resolution #{0} ({1})", new object[]
			{
				idx,
				text
			});
		}
		return true;
	}

	// Token: 0x04002981 RID: 10625
	private Dictionary<string, Resolution> map;
}
