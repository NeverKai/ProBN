using System;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Voxels.TowerDefense
{
	// Token: 0x020006BE RID: 1726
	[ConsoleCommandClassCustomizer("CinematicCamera")]
	public class CinematicCamera : MonoBehaviour, ILevelCamera
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000A66BE File Offset: 0x000A4ABE
		public static bool isActive
		{
			get
			{
				return CinematicCamera._isActive;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000A66C5 File Offset: 0x000A4AC5
		public Camera cameraRef
		{
			get
			{
				return this.cameraReference;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000A66CD File Offset: 0x000A4ACD
		// (set) Token: 0x06002CAD RID: 11437 RVA: 0x000A66E0 File Offset: 0x000A4AE0
		public float orthoHeight
		{
			get
			{
				return this.cameraRef.orthographicSize * 2f;
			}
			set
			{
				this.cameraRef.orthographicSize = value * 0.5f;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000A66F4 File Offset: 0x000A4AF4
		// (set) Token: 0x06002CAF RID: 11439 RVA: 0x000A671C File Offset: 0x000A4B1C
		public float yaw
		{
			get
			{
				return this.yawTransform.rotation.eulerAngles.y;
			}
			set
			{
				this.yawTransform.rotation = Quaternion.Euler(0f, value, 0f);
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000A6739 File Offset: 0x000A4B39
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x000A6746 File Offset: 0x000A4B46
		public Vector3 position
		{
			get
			{
				return this.panTransform.position;
			}
			set
			{
				this.panTransform.position = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000A6754 File Offset: 0x000A4B54
		public bool lockPanY
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000A6757 File Offset: 0x000A4B57
		public Rect screenRect
		{
			get
			{
				return new Rect(Vector2.zero, Vector2.one);
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000A6768 File Offset: 0x000A4B68
		public Rect defaultScreenRect
		{
			get
			{
				return new Rect(Vector2.zero, Vector2.one);
			}
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000A6779 File Offset: 0x000A4B79
		private void LateUpdate()
		{
			Singleton<CameraShaker>.instance.ApplyTo(this.shakeTransform);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000A678C File Offset: 0x000A4B8C
		[ConsoleCommand("")]
		private static void Activate()
		{
			CinematicCamera cinematicCamera = CinematicCamera.FindCamera();
			cinematicCamera.gameObject.SetActive(true);
			if (Display.displays.Length > 1)
			{
				Display.displays[1].Activate();
			}
			else
			{
				cinematicCamera.editorRender.SetActive(true);
			}
			ClaimableAudioListener.AddClaim(cinematicCamera, true);
			AudioMixerSnapshot audioMixerSnapshot = cinematicCamera.vikingMixer.FindSnapshot("NoUI");
			audioMixerSnapshot.TransitionTo(0f);
			CinematicCamera._isActive = true;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000A6800 File Offset: 0x000A4C00
		private static CinematicCamera FindCamera()
		{
			int sceneCount = SceneManager.sceneCount;
			for (int i = 0; i < sceneCount; i++)
			{
				GameObject[] rootGameObjects = SceneManager.GetSceneAt(i).GetRootGameObjects();
				foreach (GameObject gameObject in rootGameObjects)
				{
					CinematicCamera componentInChildren = gameObject.GetComponentInChildren<CinematicCamera>(true);
					if (componentInChildren)
					{
						return componentInChildren;
					}
				}
			}
			return null;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000A6874 File Offset: 0x000A4C74
		private static void FlipScreens()
		{
			CinematicCamera.instance.cameraRef.targetDisplay = 0;
			Singleton<LevelCamera>.instance.cameraRef.targetDisplay = 1;
			int i = 0;
			int sceneCount = SceneManager.sceneCount;
			while (i < sceneCount)
			{
				foreach (GameObject gameObject in SceneManager.GetSceneAt(i).GetRootGameObjects())
				{
					foreach (Canvas canvas in gameObject.transform.GetComponentsInChildren<Canvas>(true))
					{
						canvas.targetDisplay = ((canvas.targetDisplay != 0) ? 0 : 1);
					}
				}
				i++;
			}
		}

		// Token: 0x04001D53 RID: 7507
		private static CinematicCamera instance;

		// Token: 0x04001D54 RID: 7508
		[SerializeField]
		private Camera cameraReference;

		// Token: 0x04001D55 RID: 7509
		[SerializeField]
		private Transform panTransform;

		// Token: 0x04001D56 RID: 7510
		[SerializeField]
		private Transform yawTransform;

		// Token: 0x04001D57 RID: 7511
		[SerializeField]
		private Transform shakeTransform;

		// Token: 0x04001D58 RID: 7512
		[SerializeField]
		private GameObject editorRender;

		// Token: 0x04001D59 RID: 7513
		[SerializeField]
		private AudioMixer vikingMixer;

		// Token: 0x04001D5A RID: 7514
		private static bool _isActive;
	}
}
