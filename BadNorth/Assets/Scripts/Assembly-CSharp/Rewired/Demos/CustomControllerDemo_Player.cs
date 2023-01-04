using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000478 RID: 1144
	[AddComponentMenu("")]
	[RequireComponent(typeof(CharacterController))]
	public class CustomControllerDemo_Player : MonoBehaviour
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00046E08 File Offset: 0x00045208
		private Player player
		{
			get
			{
				if (this._player == null)
				{
					this._player = ReInput.players.GetPlayer(this.playerId);
				}
				return this._player;
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00046E31 File Offset: 0x00045231
		private void Awake()
		{
			this.cc = base.GetComponent<CharacterController>();
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00046E40 File Offset: 0x00045240
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			Vector2 a = new Vector2(this.player.GetAxis("Move Horizontal"), this.player.GetAxis("Move Vertical"));
			this.cc.Move(a * this.speed * Time.deltaTime);
			if (this.player.GetButtonDown("Fire"))
			{
				Vector3 b = Vector3.Scale(new Vector3(1f, 0f, 0f), base.transform.right);
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position + b, Quaternion.identity);
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(this.bulletSpeed * base.transform.right.x, 0f, 0f);
			}
			if (this.player.GetButtonDown("Change Color"))
			{
				Renderer component = base.GetComponent<Renderer>();
				Material material = component.material;
				material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);
				component.material = material;
			}
		}

		// Token: 0x04001037 RID: 4151
		public int playerId;

		// Token: 0x04001038 RID: 4152
		public float speed = 1f;

		// Token: 0x04001039 RID: 4153
		public float bulletSpeed = 20f;

		// Token: 0x0400103A RID: 4154
		public GameObject bulletPrefab;

		// Token: 0x0400103B RID: 4155
		private Player _player;

		// Token: 0x0400103C RID: 4156
		private CharacterController cc;
	}
}
