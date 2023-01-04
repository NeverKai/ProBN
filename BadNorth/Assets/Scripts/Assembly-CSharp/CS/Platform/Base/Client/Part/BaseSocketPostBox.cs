using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using CS.Platform.Utils;
using CS.Platform.Utils.Data;

namespace CS.Platform.Base.Client.Part
{
	// Token: 0x02000031 RID: 49
	public abstract class BaseSocketPostBox<T> : BasePostBox<T> where T : struct
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000796B File Offset: 0x00005D6B
		public long HostPoint
		{
			get
			{
				return this._hostPoint.Address.Address;
			}
		}

		// Token: 0x060001A4 RID: 420
		public abstract void SetUpMessageClass(T newValue);

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000797D File Offset: 0x00005D7D
		public byte[] StartupConnectionMessage
		{
			get
			{
				if (BaseSocketPostBox<T>.STARTUP_MESSAGE == null)
				{
					BaseSocketPostBox<T>.STARTUP_MESSAGE = this.GenerateNewMessageArray(null, 0);
				}
				return BaseSocketPostBox<T>.STARTUP_MESSAGE;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000799C File Offset: 0x00005D9C
		public void Awake()
		{
			this._baseManager = base.gameObject.GetComponent<BasePlatformManager>();
			try
			{
				this._hostPoint = new IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], BaseSocketPostBox<T>.TARGETPORT);
				this._socketReliableListener = new TcpListener(this._hostPoint);
				this._socketReliableListener.Start();
				Debug.LogInfo("[BSPB] Sockets Setup", new object[0]);
			}
			catch (SocketException ex)
			{
				Debug.LogError("[BSPB] Socket Setup Failed: {0} | Link: {1}", new object[]
				{
					ex.Message,
					ex.HelpLink
				});
			}
			catch
			{
				Debug.LogError("[BSPB] Socket Setup Failed", new object[0]);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007A68 File Offset: 0x00005E68
		protected void OnDestroy()
		{
			base.StopMessageThread();
			for (int i = 0; i < this._connectionActive.Count; i++)
			{
				this._connectionActive[i].Shutdown();
			}
			this._connectionActive.Clear();
			for (int j = 0; j < this._connectionStore.Count; j++)
			{
				this._connectionStore[j].Shutdown();
			}
			this._connectionStore.Clear();
			this._socketReliableListener.Stop();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007AF8 File Offset: 0x00005EF8
		public bool ConnectionReady(T userInfo)
		{
			object obj = this.connectionStorageLocker;
			bool result;
			lock (obj)
			{
				BaseSocketPostBox<T>.StoredConnections storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userInfo));
				if (storedConnections != null)
				{
					if (storedConnections.DestroyFlag)
					{
						result = false;
					}
					else
					{
						result = true;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007B78 File Offset: 0x00005F78
		protected bool StartConnecting(T userInfo)
		{
			object obj = this.connectionStorageLocker;
			lock (obj)
			{
				BaseSocketPostBox<T>.StoredConnections storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userInfo));
				if (storedConnections != null)
				{
					storedConnections.DestroyFlag = false;
					return true;
				}
				storedConnections = this._connectionStore.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userInfo));
				if (storedConnections != null)
				{
					this._connectionStore.Remove(storedConnections);
					storedConnections.DestroyFlag = false;
					if (storedConnections.TryReconnecting(this.StartupConnectionMessage, null))
					{
						this._connectionActive.Add(storedConnections);
						return true;
					}
				}
				if (!this._connectionWaiting.ContainsKey(userInfo))
				{
					if (this.RequestSenderIP(userInfo))
					{
						this._connectionWaiting.Add(userInfo, new Queue<byte[]>());
						return true;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007C8C File Offset: 0x0000608C
		protected void Diconnect(T userInfo)
		{
			this._messageThread.Pause(true);
			BaseSocketPostBox<T>.StoredConnections storedConnections = null;
			object obj = this.connectionStorageLocker;
			lock (obj)
			{
				storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userInfo));
			}
			if (storedConnections != null)
			{
				object locker = storedConnections.locker;
				lock (locker)
				{
					storedConnections.DestroyFlag = true;
				}
			}
			if (this._connectionWaiting.ContainsKey(userInfo))
			{
				this._connectionWaiting.Remove(userInfo);
			}
			this._messageThread.PauseThread = false;
		}

		// Token: 0x060001AB RID: 427
		protected abstract bool RequestSenderIP(T senderID);

		// Token: 0x060001AC RID: 428 RVA: 0x00007D5C File Offset: 0x0000615C
		private byte[] GenerateNewMessageArray(byte[] message, int size)
		{
			if (this._messageBuilder != null)
			{
				this._messageBuilder.Data = message;
				return this._messageBuilder.CopyMessage();
			}
			Debug.LogError("[BSPB] No Message Builder Set!!!", new object[0]);
			return null;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007D94 File Offset: 0x00006194
		public override void SendNewMessage(T userID, byte[] message, int size, bool reliable)
		{
			BaseSocketPostBox<T>.StoredConnections storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userID));
			if (storedConnections != null)
			{
				if (reliable)
				{
					byte[] messageSending = this.GenerateNewMessageArray(message, size);
					try
					{
						int num = storedConnections.Send(messageSending);
					}
					catch (SocketException ex)
					{
						Debug.LogError("[BSPB] SendNewMessage: Failed | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
						{
							ex.SocketErrorCode,
							ex.ErrorCode,
							ex.HelpLink,
							ex.Message
						});
						try
						{
							if (!storedConnections.TryReconnecting(this.StartupConnectionMessage, null))
							{
								Debug.LogError("[BSPB] SendNewMessage: Failed Reconnecting", new object[0]);
							}
							else
							{
								Debug.LogInfo("[BSPB] SendNewMessage: Reconnected", new object[0]);
								if (reliable)
								{
									this.SendNewMessage(userID, message, size, reliable);
								}
							}
						}
						catch (SocketException ex2)
						{
							Debug.LogError("[BSPB] SendNewMessage: Reconnecting Failed | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
							{
								ex2.SocketErrorCode,
								ex2.ErrorCode,
								ex2.HelpLink,
								ex2.Message
							});
						}
						catch
						{
							throw;
						}
					}
					catch
					{
						Debug.LogError("[BSPB] SendNewMessage: Failed", new object[0]);
					}
				}
				else
				{
					Debug.LogError("[BSPB] _socketUnreliable: This is not set up", new object[0]);
				}
			}
			else
			{
				this.SendWithLockNewMessage(userID, message, size, reliable);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007F48 File Offset: 0x00006348
		private void SendWithLockNewMessage(T userID, byte[] message, int size, bool reliable)
		{
			object obj = this.connectionStorageLocker;
			lock (obj)
			{
				BaseSocketPostBox<T>.StoredConnections storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userID));
				if (storedConnections != null)
				{
					try
					{
						if (reliable)
						{
							byte[] messageSending = this.GenerateNewMessageArray(message, size);
							int num = storedConnections.Send(messageSending);
						}
						else
						{
							Debug.LogError("_socketUnreliable: This is not set up", new object[0]);
						}
					}
					catch (SocketException ex)
					{
						Debug.LogError("[BSPB] SendWithLockNewMessage: Failed | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
						{
							ex.SocketErrorCode,
							ex.ErrorCode,
							ex.HelpLink,
							ex.Message
						});
					}
					catch
					{
						Debug.LogError("[BSPB] SendWithLockNewMessage: Failed", new object[0]);
					}
				}
				else if (reliable && this._connectionWaiting.ContainsKey(userID))
				{
					byte[] item = this.GenerateNewMessageArray(message, size);
					this._connectionWaiting[userID].Enqueue(item);
				}
				else
				{
					bool flag = this.StartConnecting(userID);
					if (flag && reliable)
					{
						storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userID));
						if (storedConnections != null)
						{
							byte[] messageSending2 = this.GenerateNewMessageArray(message, size);
							int num2 = storedConnections.Send(messageSending2);
						}
						else
						{
							byte[] item2 = this.GenerateNewMessageArray(message, size);
							this._connectionWaiting[userID].Enqueue(item2);
						}
					}
				}
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008138 File Offset: 0x00006538
		public void AddUserIP(T userID, IPEndPoint endPoint, TcpClient socket = null)
		{
			object obj = this.connectionStorageLocker;
			lock (obj)
			{
				BaseSocketPostBox<T>.StoredConnections storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userID));
				if (storedConnections != null)
				{
					Debug.LogWarning("[BSPB] AddUserIP: Already Connected", new object[0]);
				}
				else
				{
					storedConnections = new BaseSocketPostBox<T>.StoredConnections(userID, endPoint, socket, this.GenerateNewMessageArray(null, 0));
					if (storedConnections.link.Connected)
					{
						this._connectionActive.Add(storedConnections);
						Debug.LogInfo("[BSPB] AddUserIP: Created connection", new object[0]);
					}
					else
					{
						Debug.LogError("[BSPB] AddUserIP: Create connection failed", new object[0]);
					}
				}
			}
			this.SendQueuedMessages(userID);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008214 File Offset: 0x00006614
		public void AddUserIP(T userID, long IP)
		{
			IPEndPoint endPoint = new IPEndPoint(IP, BaseSocketPostBox<T>.TARGETPORT);
			this.AddUserIP(userID, endPoint, null);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008238 File Offset: 0x00006638
		public void AddUserIP(T userID, IPAddress IP)
		{
			IPEndPoint endPoint = new IPEndPoint(IP, BaseSocketPostBox<T>.TARGETPORT);
			this.AddUserIP(userID, endPoint, null);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000825C File Offset: 0x0000665C
		public void SendQueuedMessages(T userID)
		{
			Debug.LogInfo("[BSPB] Sending Queued Messages", new object[0]);
			BaseSocketPostBox<T>.StoredConnections storedConnections = null;
			Queue<byte[]> queue = null;
			object obj = this.connectionStorageLocker;
			lock (obj)
			{
				storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(userID));
				if (this._connectionWaiting.ContainsKey(userID))
				{
					queue = this._connectionWaiting[userID];
				}
			}
			if (storedConnections != null && queue != null)
			{
				while (queue.Count > 0)
				{
					byte[] messageSending = queue.Dequeue();
					try
					{
						int num = storedConnections.Send(messageSending);
					}
					catch (SocketException ex)
					{
						Debug.LogError("[BSPB] AddUserIP Send: Failed | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
						{
							ex.SocketErrorCode,
							ex.ErrorCode,
							ex.HelpLink,
							ex.Message
						});
					}
					catch
					{
						Debug.LogError("[BSPB] AddUserIP Send: Failed", new object[0]);
					}
				}
				this._connectionWaiting.Remove(userID);
			}
		}

		// Token: 0x060001B3 RID: 435
		protected abstract bool ReadSenderID(ref T senderID, byte[] message, uint size, ref int messageStart);

		// Token: 0x060001B4 RID: 436 RVA: 0x000083B4 File Offset: 0x000067B4
		protected override bool GetMessage(ref T senderID, out DataReader message)
		{
			BaseSocketPostBox<T>.StoredConnections storedConnections = null;
			message = null;
			try
			{
				if (this._socketReliableListener.Pending())
				{
					TcpClient tcpClient = this._socketReliableListener.AcceptTcpClient();
					if (this.RecvMessage(tcpClient.GetStream()) != 0)
					{
						this.ReadMessage(ref senderID, tcpClient);
						if (this._messageReader.Data != null && this._messageReader.Data.Length != 0)
						{
							message = new DataReader(this._messageReader.Data, true);
						}
						return message != null;
					}
				}
			}
			catch (SocketException ex)
			{
				Debug.LogError("[BSPB] Listener Failed: Socket | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
				{
					ex.SocketErrorCode,
					ex.ErrorCode,
					ex.HelpLink,
					ex.Message
				});
			}
			catch
			{
				Debug.LogError("[BSPB] Listener Failed: General", new object[0]);
			}
			object obj = this.connectionStorageLocker;
			lock (obj)
			{
				if (this._currentlyReading < 0)
				{
					this._currentlyReading = 0;
				}
				if (this._connectionActive.Count <= this._currentlyReading)
				{
					this._currentlyReading = 0;
				}
				if (this._currentlyReading < this._connectionActive.Count)
				{
					storedConnections = this._connectionActive[this._currentlyReading];
				}
			}
			if (storedConnections != null)
			{
				this._currentlyReading++;
				object locker = storedConnections.locker;
				lock (locker)
				{
					try
					{
						if (storedConnections.DestroyFlag)
						{
							if (this.RemoveConnection(storedConnections))
							{
								this._currentlyReading--;
							}
						}
						else if (storedConnections.link != null && 2 <= storedConnections.link.Available && this.RecvMessage(storedConnections.stream) != 0)
						{
							this.ReadMessage(ref senderID, null);
							if (this._messageReader.Data != null && this._messageReader.Data.Length != 0)
							{
								message = new DataReader(this._messageReader.Data, true);
							}
						}
					}
					catch (SocketException ex2)
					{
						Debug.LogError("[BSPB] Connection Failed: Socket | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
						{
							ex2.SocketErrorCode,
							ex2.ErrorCode,
							ex2.HelpLink,
							ex2.Message
						});
						return false;
					}
					catch
					{
						Debug.LogError("[BSPB] Connection Failed: General", new object[0]);
						return false;
					}
				}
			}
			return message != null;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000086DC File Offset: 0x00006ADC
		private int RecvMessage(NetworkStream stream)
		{
			int result;
			try
			{
				if (!stream.CanRead)
				{
					throw null;
				}
				int i = 0;
				int num = 6;
				DataReader dataReader = new DataReader();
				while (i < num)
				{
					i += stream.Read(this.messageReadBuffer, i, num - i);
					if (i == 6 && num == 6)
					{
						dataReader = new DataReader(this.messageReadBuffer, false);
						num = dataReader.DataSize;
					}
				}
				this._messageReader.ApplyData(dataReader);
				result = num - 6;
			}
			catch (SocketException ex)
			{
				Debug.LogError("[BSPB] Received Read Failed: Socket | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
				{
					ex.SocketErrorCode,
					ex.ErrorCode,
					ex.HelpLink,
					ex.Message
				});
				result = 0;
			}
			catch
			{
				Debug.LogError("[BSPB] Received Read Failed: General | Permition: {0}", new object[]
				{
					stream.CanRead
				});
				result = 0;
			}
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000087E4 File Offset: 0x00006BE4
		private void ReadMessage(ref T senderID, TcpClient socket)
		{
			senderID = this._messageReader.GetSender();
			if (socket != null)
			{
				object obj = this.connectionStorageLocker;
				lock (obj)
				{
					T searcherVal = senderID;
					BaseSocketPostBox<T>.StoredConnections storedConnections = this._connectionActive.Find((BaseSocketPostBox<T>.StoredConnections x) => x.userInfo.Equals(searcherVal));
					if (storedConnections == null)
					{
						this.AddUserIP(senderID, socket.Client.RemoteEndPoint as IPEndPoint, socket);
					}
					else
					{
						storedConnections.link = socket;
					}
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000888C File Offset: 0x00006C8C
		private bool RemoveConnection(BaseSocketPostBox<T>.StoredConnections currentlyReading)
		{
			object obj = this.connectionStorageLocker;
			lock (obj)
			{
				object locker = currentlyReading.locker;
				lock (locker)
				{
					if (currentlyReading.DestroyFlag)
					{
						this._connectionActive.Remove(currentlyReading);
						if (!this._connectionStore.Contains(currentlyReading))
						{
							this._connectionStore.Add(currentlyReading);
						}
						Debug.LogInfo("[BSPB] Removed Connection: {0}", new object[]
						{
							currentlyReading.userInfo.ToString()
						});
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000094 RID: 148
		private BasePlatformManager _baseManager;

		// Token: 0x04000095 RID: 149
		private object connectionStorageLocker = new object();

		// Token: 0x04000096 RID: 150
		private TcpListener _socketReliableListener;

		// Token: 0x04000097 RID: 151
		private EndPoint _endPoint;

		// Token: 0x04000098 RID: 152
		private IPEndPoint _hostPoint;

		// Token: 0x04000099 RID: 153
		public static int TARGETPORT = 11000;

		// Token: 0x0400009A RID: 154
		private byte[] messageReadBuffer = new byte[1000000];

		// Token: 0x0400009B RID: 155
		protected BaseSocketPostBox<T>.SocketMessage _messageBuilder;

		// Token: 0x0400009C RID: 156
		protected BaseSocketPostBox<T>.SocketMessage _messageReader;

		// Token: 0x0400009D RID: 157
		private List<BaseSocketPostBox<T>.StoredConnections> _connectionActive = new List<BaseSocketPostBox<T>.StoredConnections>();

		// Token: 0x0400009E RID: 158
		private List<BaseSocketPostBox<T>.StoredConnections> _connectionStore = new List<BaseSocketPostBox<T>.StoredConnections>();

		// Token: 0x0400009F RID: 159
		private Dictionary<T, Queue<byte[]>> _connectionWaiting = new Dictionary<T, Queue<byte[]>>();

		// Token: 0x040000A0 RID: 160
		private int _currentlyReading;

		// Token: 0x040000A1 RID: 161
		private static byte[] STARTUP_MESSAGE;

		// Token: 0x02000032 RID: 50
		protected abstract class SocketMessage : PlatformMessageBase
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060001BB RID: 443 RVA: 0x00008B9F File Offset: 0x00006F9F
			// (set) Token: 0x060001BA RID: 442 RVA: 0x00008B83 File Offset: 0x00006F83
			public byte[] Data
			{
				get
				{
					return this._data;
				}
				set
				{
					if (this._data != value)
					{
						this._data = value;
						base.Dirty = true;
					}
				}
			}

			// Token: 0x060001BC RID: 444
			public abstract T GetSender();

			// Token: 0x060001BD RID: 445
			protected abstract void SerializeID(DataWriter writer);

			// Token: 0x060001BE RID: 446 RVA: 0x00008BA7 File Offset: 0x00006FA7
			protected override void Serialize(DataWriter writer)
			{
				base.Serialize(writer);
				this.SerializeID(writer);
				writer.WriteData(this._data);
			}

			// Token: 0x060001BF RID: 447
			protected abstract void DeserializeID(DataReader reader);

			// Token: 0x060001C0 RID: 448 RVA: 0x00008BC3 File Offset: 0x00006FC3
			protected override void Deserialize(DataReader reader)
			{
				base.Deserialize(reader);
				this.DeserializeID(reader);
				this._data = reader.ReadData();
			}

			// Token: 0x040000A2 RID: 162
			private byte[] _data;
		}

		// Token: 0x02000033 RID: 51
		private class StoredConnections
		{
			// Token: 0x060001C1 RID: 449 RVA: 0x00008BDF File Offset: 0x00006FDF
			public StoredConnections(T user, IPEndPoint target, TcpClient socket, byte[] startupMessage)
			{
				this.userInfo = user;
				this.endPoint = target;
				this.link = null;
				this.link = socket;
				this.StartConnection(startupMessage);
			}

			// Token: 0x060001C2 RID: 450 RVA: 0x00008C18 File Offset: 0x00007018
			private void StartConnection(byte[] startupMessage)
			{
				if (this.link == null)
				{
					try
					{
						this.link = new TcpClient();
						this.link.Connect(this.endPoint);
					}
					catch (SocketException ex)
					{
						Debug.LogError("[BSPB] StoredConnections Create: Failed | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
						{
							ex.SocketErrorCode,
							ex.ErrorCode,
							ex.HelpLink,
							ex.Message
						});
					}
				}
				this.link.NoDelay = true;
				this.stream = this.link.GetStream();
				if (!this.stream.CanWrite || !this.stream.CanRead)
				{
					Debug.LogError("[BSPB] StoredConnections: Can fail | Write: {0} | Read: {1}", new object[]
					{
						this.stream.CanWrite,
						this.stream.CanRead
					});
				}
				this.stream.Write(startupMessage, 0, startupMessage.Length);
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008D28 File Offset: 0x00007128
			// (set) Token: 0x060001C4 RID: 452 RVA: 0x00008D68 File Offset: 0x00007168
			public bool DestroyFlag
			{
				get
				{
					object obj = this.locker;
					bool destroyFlag;
					lock (obj)
					{
						destroyFlag = this._destroyFlag;
					}
					return destroyFlag;
				}
				set
				{
					object obj = this.locker;
					lock (obj)
					{
						this._destroyFlag = value;
					}
				}
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00008DA8 File Offset: 0x000071A8
			public void CheckConnection(byte[] checkMessage)
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.link != null)
					{
						this.Send(checkMessage);
					}
				}
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x00008DF4 File Offset: 0x000071F4
			public bool TryReconnecting(byte[] startupMessage, IPEndPoint newEndPoint)
			{
				object obj = this.locker;
				bool result;
				lock (obj)
				{
					if (newEndPoint != null)
					{
						this.endPoint = newEndPoint;
					}
					try
					{
						if (this.link != null)
						{
							this.CheckConnection(startupMessage);
							if (this.link.Connected)
							{
								return true;
							}
							this.link.Close();
							this.link = null;
							Debug.LogError("[BSPB] Try Reconnecting Failed | User: {0}", new object[]
							{
								this.userInfo
							});
						}
					}
					catch
					{
						this.link = null;
					}
					this.StartConnection(startupMessage);
					result = (this.link != null && this.link.Connected);
				}
				return result;
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x00008ED0 File Offset: 0x000072D0
			public int Send(byte[] messageSending)
			{
				if (messageSending == null)
				{
					return 0;
				}
				object obj = this.locker;
				int result;
				lock (obj)
				{
					if (this.stream != null)
					{
						try
						{
							if (!this.stream.CanWrite)
							{
								throw null;
							}
							this.stream.Write(messageSending, 0, messageSending.Length);
							return messageSending.Length;
						}
						catch (IOException ex)
						{
							Debug.LogError("[BSPB] Send: IO Failed | me: {0} | link: {1} | Inner: {2} | InnerLink: {3}", new object[]
							{
								ex.Message,
								ex.HelpLink,
								ex.InnerException.Message,
								ex.InnerException.HelpLink
							});
							return 0;
						}
						catch (ObjectDisposedException ex2)
						{
							Debug.LogError("[BSPB] Send: Disposed Failed | me: {0} | link: {1} | Inner: {2} | InnerLink: {3}", new object[]
							{
								ex2.Message,
								ex2.HelpLink,
								ex2.InnerException.Message,
								ex2.InnerException.HelpLink
							});
							return 0;
						}
						catch (SocketException ex3)
						{
							Debug.LogError("[BSPB] Send: Failed | se: {0} | er: {1} | hl: {2} | me: {3}", new object[]
							{
								ex3.SocketErrorCode,
								ex3.ErrorCode,
								ex3.HelpLink,
								ex3.Message
							});
							return 0;
						}
						catch
						{
							Debug.LogError("[BSPB] Send: Failed | Permition: {0}", new object[]
							{
								this.stream.CanWrite
							});
							return 0;
						}
					}
					Debug.LogError("[BSPB] Send: Stream not set", new object[0]);
					result = 0;
				}
				return result;
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x000090C8 File Offset: 0x000074C8
			public void Shutdown()
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.link != null)
					{
						this.link.Close();
						this.link = null;
						this.stream.Close();
						Debug.LogInfo("[BSPB] Closed connection: {0}", new object[]
						{
							this.userInfo.ToString()
						});
					}
				}
			}

			// Token: 0x040000A3 RID: 163
			public int dbug;

			// Token: 0x040000A4 RID: 164
			public T userInfo;

			// Token: 0x040000A5 RID: 165
			public IPEndPoint endPoint;

			// Token: 0x040000A6 RID: 166
			public TcpClient link;

			// Token: 0x040000A7 RID: 167
			public NetworkStream stream;

			// Token: 0x040000A8 RID: 168
			private bool _destroyFlag;

			// Token: 0x040000A9 RID: 169
			public object locker = new object();
		}
	}
}
