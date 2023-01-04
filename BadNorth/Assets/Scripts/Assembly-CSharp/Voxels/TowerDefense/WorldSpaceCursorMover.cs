using System;
using System.Diagnostics;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200093C RID: 2364
	public class WorldSpaceCursorMover : MonoBehaviour
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x00122422 File Offset: 0x00120822
		private float radius
		{
			get
			{
				return this.sphereCollider.radius;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003FEB RID: 16363 RVA: 0x0012242F File Offset: 0x0012082F
		private float halfHeight
		{
			get
			{
				return this.sphereCollider.radius;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003FEC RID: 16364 RVA: 0x0012243C File Offset: 0x0012083C
		public Vector3 basePosition
		{
			get
			{
				return base.transform.position - Vector3.up * this.halfHeight;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003FED RID: 16365 RVA: 0x0012245E File Offset: 0x0012085E
		public Vector3 floorPosition
		{
			get
			{
				return base.transform.position - Vector3.up * 0.5f;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003FEE RID: 16366 RVA: 0x0012247F File Offset: 0x0012087F
		private float baseHoverHeight
		{
			get
			{
				return 0.5f - this.sphereCollider.radius;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x00122492 File Offset: 0x00120892
		private bool hasFloor
		{
			get
			{
				return this.floorNormal.y > 0f;
			}
		}

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06003FF0 RID: 16368 RVA: 0x001224A8 File Offset: 0x001208A8
		// (remove) Token: 0x06003FF1 RID: 16369 RVA: 0x001224E0 File Offset: 0x001208E0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onPostMove = delegate()
		{
		};

		// Token: 0x06003FF2 RID: 16370 RVA: 0x00122516 File Offset: 0x00120916
		public void Init(Island island)
		{
			this.island = island;
			this.sphereCollider = base.GetComponent<SphereCollider>();
			this.body = base.GetComponent<Rigidbody>();
			this.layerMask = this.body.GetCollidingLayers();
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x0012254D File Offset: 0x0012094D
		public void InvalidateInput(float duration)
		{
			this.inputAvailableTime = Time.unscaledTime + duration;
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x0012255C File Offset: 0x0012095C
		public void SetInput(Vector3 input)
		{
			this.input = input;
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x00122565 File Offset: 0x00120965
		public void TeleportTo(Vector3 floorPos)
		{
			base.transform.position = floorPos + Vector3.up * 0.5f;
			this.UpdateFloor();
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x00122590 File Offset: 0x00120990
		private void Update()
		{
			if (this.input == Vector3.zero)
			{
				this.inputAvailableTime = Time.unscaledTime;
			}
			else if (Time.unscaledTime < this.inputAvailableTime)
			{
				this.input = Vector3.zero;
			}
			Vector3 velocity = 8f * this.input * GameController.gamepadSensitivity;
			if (this.input == Vector3.zero)
			{
				this.blockage.Clear();
				return;
			}
			this.blockage.ClearWalls();
			this.Solve(velocity, Time.unscaledDeltaTime, 4);
			this.ProcessBlockage();
			this.IslandClamp();
			this.onPostMove();
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x0012264C File Offset: 0x00120A4C
		private void ProcessBlockage()
		{
			if (this.blockage.numWalls > 0)
			{
				UnityEngine.Debug.DrawRay(this.blockage.wall0.position, this.blockage.wall0.normal);
			}
			if (this.blockage.numWalls > 1)
			{
				UnityEngine.Debug.DrawRay(this.blockage.wall1.position, this.blockage.wall1.normal);
			}
			bool flag = this.blockage.wall0.pressing || this.blockage.wall1.pressing;
			if (!flag && this.blockage.numWalls > 1)
			{
				float num = ExtraMath.SignedAngle(this.blockage.wall0.normal, this.blockage.wall1.normal, Vector3.up);
				float num2 = Mathf.Sign(num);
				num *= num2;
				float num3 = num2 * ExtraMath.SignedAngle(this.blockage.wall0.normal, -this.input.normalized, Vector3.up);
				flag |= (num3 > 0f && num3 < num);
			}
			if (flag)
			{
				if (this.blockage.pressTime > 0.075f)
				{
					this.StepUp();
				}
				else
				{
					this.blockage.pressTime = this.blockage.pressTime + Time.unscaledDeltaTime;
				}
			}
			else
			{
				this.blockage.Clear();
			}
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x001227D0 File Offset: 0x00120BD0
		private void StepUp()
		{
			using ("WorldSpaceCursorMover.StepUp")
			{
				float y = base.transform.position.y;
				Vector3 vector = -(this.blockage.wall0.normal + this.blockage.wall1.normal).normalized;
				RaycastHit raycastHit;
				if (this.SweepTest(Vector3.up * 8f, out raycastHit))
				{
					base.transform.position = base.transform.position.SetY(raycastHit.point.y + 4f);
					if (this.SweepTest(Vector3.down * 4f, out raycastHit))
					{
						base.transform.position = base.transform.position.SetY(raycastHit.point.y + this.baseHoverHeight);
						base.transform.position = this.GetClosestGridCell(base.transform.position);
						this.UpdateFloor();
						this.InvalidateInput(0.2f);
					}
					else
					{
						UnityEngine.Debug.LogError("Handle Ceiling failed!");
					}
					this.blockage.Clear();
				}
				else
				{
					while (base.transform.position.y < 8f)
					{
						if (!this.SweepTest(vector))
						{
							base.transform.position += vector * (1f - this.radius);
							base.transform.position = this.GetClosestGridCell(base.transform.position);
							this.UpdateFloor();
							float num = base.transform.position.y - y;
							this.InvalidateInput((num <= 0f) ? 0.0667f : 0.11f);
							break;
						}
						base.transform.position += Vector3.up;
					}
					this.blockage.Clear();
				}
			}
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x00122A28 File Offset: 0x00120E28
		private void IslandClamp()
		{
			Vector3 vector = this.island.Target.size * 0.5f + Vector3.one * 0.75f;
			Vector3 position = base.transform.position;
			position.x = Mathf.Clamp(position.x, -vector.x, vector.x);
			position.z = Mathf.Clamp(position.z, -vector.z, vector.z);
			base.transform.position = position;
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x00122AC0 File Offset: 0x00120EC0
		private void Solve(Vector3 velocity, float timeRemain, int iterations)
		{
			if (timeRemain <= 0f || iterations <= 0)
			{
				return;
			}
			RaycastHit raycastHit;
			if (this.UpdateFloor(out raycastHit))
			{
				Vector3 vector = base.transform.position;
				Vector3 vector2 = velocity * timeRemain;
				vector2 = Vector3.ProjectOnPlane(vector2, this.floorNormal);
				RaycastHit raycastHit2;
				if (this.BlockageTest(vector2, out raycastHit2))
				{
					if (raycastHit2.distance > 0f)
					{
						vector += vector2.normalized * (raycastHit2.distance - 0.001f);
					}
					float num = 1f - raycastHit2.distance / vector2.magnitude;
					timeRemain *= num;
					float num2 = Mathf.Cos(0.5235988f);
					if (Vector3.Dot(this.input.normalized, -raycastHit2.normal) > num2)
					{
						this.blockage.AddWall(new WorldSpaceCursorMover.WallData(raycastHit2.point, raycastHit2.normal, true));
						this.blockage.pressTime = this.blockage.pressTime + timeRemain;
						return;
					}
					this.blockage.AddWall(new WorldSpaceCursorMover.WallData(raycastHit2.point, raycastHit2.normal, false));
					velocity = Vector3.ProjectOnPlane(velocity, raycastHit2.normal);
				}
				else
				{
					vector += vector2;
					timeRemain = 0f;
				}
				base.transform.position = vector;
				this.Solve(velocity, timeRemain, iterations - 1);
			}
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x00122C28 File Offset: 0x00121028
		private bool BlockageTest(Vector3 moveDelta, out RaycastHit moveHit)
		{
			moveHit = default(RaycastHit);
			float num = float.MaxValue;
			RaycastHit raycastHit;
			if (this.SweepTest(moveDelta, out raycastHit))
			{
				num = raycastHit.distance - this.ComputeWallPullBack(moveDelta, raycastHit.normal, raycastHit.distance);
				moveHit = raycastHit;
			}
			RaycastHit raycastHit2;
			if (this.LedgeTest(moveDelta, out raycastHit2))
			{
				float num2 = raycastHit2.distance - this.ComputeWallPullBack(moveDelta, raycastHit2.normal, raycastHit2.distance);
				if (num2 < num)
				{
					num = num2;
					moveHit = raycastHit2;
				}
			}
			moveHit.distance = num;
			UnityEngine.Debug.DrawRay(moveHit.point, moveHit.normal, Color.red);
			return num < float.MaxValue;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x00122CD8 File Offset: 0x001210D8
		private bool LedgeTest(Vector3 moveDelta, out RaycastHit moveHit)
		{
			moveHit = default(RaycastHit);
			Vector3 a = this.sphereCollider.GetWorldPosition() + moveDelta;
			float dist = 0.5f + this.radius;
			bool flag = false;
			bool flag2 = false;
			RaycastHit raycastHit = default(RaycastHit);
			RaycastHit raycastHit2 = default(RaycastHit);
			if (moveDelta.x > 0f)
			{
				flag = this.TestEdge(moveDelta, a + new Vector3(this.radius, 0f, 0f), dist, out raycastHit, this.layerMask);
			}
			else if (moveDelta.x < 0f)
			{
				flag = this.TestEdge(moveDelta, a + new Vector3(-this.radius, 0f, 0f), dist, out raycastHit, this.layerMask);
			}
			if (moveDelta.z > 0f)
			{
				flag2 = this.TestEdge(moveDelta, a + new Vector3(0f, 0f, this.radius), dist, out raycastHit2, this.layerMask);
			}
			else if (moveDelta.z < 0f)
			{
				flag2 = this.TestEdge(moveDelta, a + new Vector3(0f, 0f, -this.radius), dist, out raycastHit2, this.layerMask);
			}
			if (flag && flag2)
			{
				moveHit = ((raycastHit.distance >= raycastHit2.distance) ? raycastHit2 : raycastHit);
			}
			else if (flag)
			{
				moveHit = raycastHit;
			}
			else
			{
				if (!flag2)
				{
					return false;
				}
				moveHit = raycastHit2;
			}
			return true;
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x00122E7C File Offset: 0x0012127C
		private bool TestEdge(Vector3 moveVector, Vector3 rayPos, float dist, out RaycastHit hit, LayerMask mask)
		{
			UnityEngine.Debug.DrawRay(rayPos, rayPos - this.sphereCollider.GetWorldPosition());
			if (!Physics.Raycast(rayPos, Vector3.down, out hit, dist, this.layerMask) && Physics.SphereCast(rayPos, 0.2f, Vector3.down, out hit, (float)this.layerMask))
			{
				Vector3 zeroY = (hit.point - rayPos).GetZeroY();
				hit.normal = zeroY.normalized;
				Vector3 normalized = moveVector.GetZeroY().normalized;
				float num = Vector3.Dot(-normalized, hit.normal);
				if (num > 0f)
				{
					float num2 = zeroY.magnitude / num;
					hit.distance = Mathf.Max(moveVector.magnitude - num2, 0f);
					return true;
				}
			}
			hit = default(RaycastHit);
			return false;
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x00122F60 File Offset: 0x00121360
		private Vector3 GetClosestGridCell(Vector3 inPos)
		{
			Vector3 size = this.island.Target.size;
			Vector3 b = new Vector3(size.x * 0.5f % 1f, 0.5f, size.z * 0.5f % 1f);
			Vector3 vector = inPos + b;
			vector.x = Mathf.Round(vector.x);
			vector.y = Mathf.Round(vector.y);
			vector.z = Mathf.Round(vector.z);
			vector -= b;
			return vector;
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x00122FFC File Offset: 0x001213FC
		private bool UpdateFloor()
		{
			RaycastHit raycastHit = default(RaycastHit);
			return this.UpdateFloor(out raycastHit);
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x0012301C File Offset: 0x0012141C
		private bool UpdateFloor(out RaycastHit hit)
		{
			float d = 101f;
			Vector3 delta = Vector3.down * d;
			if (this.SweepTest(delta, out hit))
			{
				this.floorNormal = hit.normal;
				this.UpdateFloatingHeight(hit);
				return true;
			}
			this.floorNormal = Vector3.zero;
			return false;
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x00123070 File Offset: 0x00121470
		private void UpdateFloatingHeight(RaycastHit floorHit)
		{
			float magnitude = (base.transform.position - floorHit.point).GetZeroY().magnitude;
			float num = this.radius - Mathf.Sqrt(this.radius * this.radius - magnitude * magnitude);
			float d = floorHit.distance - num - this.baseHoverHeight;
			base.transform.position += Vector3.down * d;
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x001230F4 File Offset: 0x001214F4
		private bool SweepTest(Vector3 delta)
		{
			RaycastHit raycastHit;
			return this.SweepTest(delta, out raycastHit);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x0012310A File Offset: 0x0012150A
		private bool SweepTest(Vector3 delta, out RaycastHit hit)
		{
			return this.body.SweepTest(delta.normalized, out hit, delta.magnitude, QueryTriggerInteraction.Ignore);
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x00123128 File Offset: 0x00121528
		private float ComputeWallPullBack(Vector3 moveDelta, Vector3 hitNormal, float hitDistance)
		{
			float num = 0.001f / (moveDelta - Vector3.ProjectOnPlane(moveDelta, hitNormal)).magnitude;
			num = Mathf.Clamp(num, 0f, 1f);
			return hitDistance * num;
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x00123165 File Offset: 0x00121565
		private void OnDestroy()
		{
			this.onPostMove = null;
		}

		// Token: 0x04002CD5 RID: 11477
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("wsMover", EVerbosity.Quiet, 100);

		// Token: 0x04002CD6 RID: 11478
		private const float maxSpeed = 8f;

		// Token: 0x04002CD7 RID: 11479
		private const float floorTestDist = 0.5f;

		// Token: 0x04002CD8 RID: 11480
		private const int maxIterations = 4;

		// Token: 0x04002CD9 RID: 11481
		private const float centerHoverHeight = 0.5f;

		// Token: 0x04002CDA RID: 11482
		private const float barrierPressTime = 0.075f;

		// Token: 0x04002CDB RID: 11483
		private const float cliffJumpLockoutTime = 0.11f;

		// Token: 0x04002CDC RID: 11484
		private const float cliffDropLockoutTime = 0.0667f;

		// Token: 0x04002CDD RID: 11485
		private const float ceilingPassThroughLockoutTime = 0.2f;

		// Token: 0x04002CDE RID: 11486
		private WeakReference<Island> island = new WeakReference<Island>(null);

		// Token: 0x04002CDF RID: 11487
		private Rigidbody body;

		// Token: 0x04002CE0 RID: 11488
		private SphereCollider sphereCollider;

		// Token: 0x04002CE1 RID: 11489
		private LayerMask layerMask = 0;

		// Token: 0x04002CE2 RID: 11490
		private WorldSpaceCursorMover.Blockage blockage;

		// Token: 0x04002CE3 RID: 11491
		private Vector3 floorNormal = Vector3.zero;

		// Token: 0x04002CE4 RID: 11492
		private Vector3 input = Vector3.zero;

		// Token: 0x04002CE5 RID: 11493
		private float inputAvailableTime = float.MinValue;

		// Token: 0x0200093D RID: 2365
		private struct WallData
		{
			// Token: 0x06004007 RID: 16391 RVA: 0x00123170 File Offset: 0x00121570
			public WallData(Vector3 position, Vector3 normal, bool pressing)
			{
				this.position = position;
				this.normal = normal;
				this.pressing = pressing;
			}

			// Token: 0x04002CE8 RID: 11496
			public Vector3 position;

			// Token: 0x04002CE9 RID: 11497
			public Vector3 normal;

			// Token: 0x04002CEA RID: 11498
			public bool pressing;
		}

		// Token: 0x0200093E RID: 2366
		private struct Blockage
		{
			// Token: 0x06004008 RID: 16392 RVA: 0x00123187 File Offset: 0x00121587
			public void AddWall(WorldSpaceCursorMover.WallData wallData)
			{
				if (this.numWalls == 0)
				{
					this.wall0 = wallData;
				}
				else if (this.numWalls == 1)
				{
					this.wall1 = wallData;
				}
				this.numWalls++;
			}

			// Token: 0x06004009 RID: 16393 RVA: 0x001231C4 File Offset: 0x001215C4
			public void ClearWalls()
			{
				this.numWalls = 0;
				this.wall0 = default(WorldSpaceCursorMover.WallData);
				this.wall1 = default(WorldSpaceCursorMover.WallData);
			}

			// Token: 0x0600400A RID: 16394 RVA: 0x001231F6 File Offset: 0x001215F6
			public void Clear()
			{
				this = default(WorldSpaceCursorMover.Blockage);
			}

			// Token: 0x04002CEB RID: 11499
			public WorldSpaceCursorMover.WallData wall0;

			// Token: 0x04002CEC RID: 11500
			public WorldSpaceCursorMover.WallData wall1;

			// Token: 0x04002CED RID: 11501
			public int numWalls;

			// Token: 0x04002CEE RID: 11502
			public float pressTime;
		}
	}
}
