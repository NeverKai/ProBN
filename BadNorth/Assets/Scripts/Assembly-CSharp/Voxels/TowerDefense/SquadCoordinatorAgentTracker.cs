using System;
using System.Collections.Generic;
using System.Reflection;

namespace Voxels.TowerDefense
{
	// Token: 0x020007E8 RID: 2024
	public abstract class SquadCoordinatorAgentTracker<T> : SquadCoordinator where T : AgentComponent
	{
		// Token: 0x06003484 RID: 13444 RVA: 0x000D73F8 File Offset: 0x000D57F8
		public override void Setup(Squad squad)
		{
			base.Setup(squad);
			List<Agent> agents = squad.agents;
			int i = 0;
			int count = agents.Count;
			while (i < count)
			{
				this.Add(agents[i]);
				i++;
			}
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000D7439 File Offset: 0x000D5839
		protected override void OnAgentCreated(Agent agent)
		{
			base.OnAgentCreated(agent);
			this.Add(agent);
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000D7449 File Offset: 0x000D5849
		protected override void OnAgentRemoved(Agent agent)
		{
			base.OnAgentRemoved(agent);
			this.Remove(agent);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000D7459 File Offset: 0x000D5859
		protected virtual void OnAgentComponentAdded(T agentComponent)
		{
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000D745B File Offset: 0x000D585B
		protected virtual void OnAgentComponentRemoved(T agentComponent)
		{
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000D7460 File Offset: 0x000D5860
		private void Add(Agent agent)
		{
			T t = agent.GetComponent<T>();
			if (!t && this.spawnAgentComponents)
			{
				t = agent.gameObject.AddComponent<T>();
				t.Setup(agent);
			}
			if (t && !this.agentComponents.Contains(t))
			{
				this.agentComponents.Add(t);
				this.OnAgentComponentAdded(t);
			}
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000D74E0 File Offset: 0x000D58E0
		private void Remove(Agent agent)
		{
			T component = agent.GetComponent<T>();
			if (component)
			{
				this.agentComponents.Remove(component);
				this.OnAgentComponentRemoved(component);
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000D7518 File Offset: 0x000D5918
		protected virtual void OnDestroy()
		{
			this.agentComponents.Clear();
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000D7525 File Offset: 0x000D5925
		public void Broadcast(Action task)
		{
			this.Broadcast(task.Method, null);
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000D7534 File Offset: 0x000D5934
		public void Broadcast<T1>(Action<T1> task, T1 parameter1)
		{
			this.Broadcast(task.Method, new object[]
			{
				parameter1
			});
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000D7551 File Offset: 0x000D5951
		public void Broadcast<T1, T2>(Action<T1, T2> task, T1 parameter1, T2 parameter2)
		{
			this.Broadcast(task.Method, new object[]
			{
				parameter1,
				parameter2
			});
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000D7577 File Offset: 0x000D5977
		public void Broadcast<TResult>(Func<TResult> task)
		{
			this.Broadcast(task.Method, new object[0]);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000D758B File Offset: 0x000D598B
		public void Broadcast<T1, TResult>(Func<T1, TResult> task, T1 parameter1)
		{
			this.Broadcast(task.Method, new object[]
			{
				parameter1
			});
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000D75A8 File Offset: 0x000D59A8
		public void Broadcast<T1, T2, TResult>(Func<T1, T2, TResult> task, T1 parameter1, T2 parameter2)
		{
			this.Broadcast(task.Method, new object[]
			{
				parameter1,
				parameter2
			});
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000D75D0 File Offset: 0x000D59D0
		private void Broadcast(MethodInfo methodInfo, params object[] parameters)
		{
			int i = 0;
			int count = this.agentComponents.Count;
			while (i < count)
			{
				methodInfo.Invoke(this.agentComponents[i], parameters);
				i++;
			}
		}

		// Token: 0x040023D6 RID: 9174
		protected bool spawnAgentComponents;

		// Token: 0x040023D7 RID: 9175
		public List<T> agentComponents = new List<T>();
	}
}
