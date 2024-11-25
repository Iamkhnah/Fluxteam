using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace XenoUI
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public partial class ClientsWindow : Window
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000023 RID: 35 RVA: 0x00002478 File Offset: 0x00000678
		// (remove) Token: 0x06000024 RID: 36 RVA: 0x000024B0 File Offset: 0x000006B0
		public event Action<string, Brush> StatusUpdated;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000024E5 File Offset: 0x000006E5
		public bool IsInjected
		{
			get
			{
				return this._isInjected;
			}
		}

		// Token: 0x06000026 RID: 38
		[DllImport("FTdl.dll", CallingConvention = 2)]
		private static extern void Initialize();

		// Token: 0x06000027 RID: 39
		[DllImport("FTdl.dll", CallingConvention = 2)]
		private static extern IntPtr GetClients();

		// Token: 0x06000028 RID: 40
		[DllImport("FTdl.dll", CallingConvention = 2, CharSet = CharSet.Ansi)]
		private static extern void Execute(byte[] scriptSource, string[] clientUsers, int numUsers);

		// Token: 0x06000029 RID: 41
		[DllImport("FTdl.dll", CallingConvention = 2, CharSet = CharSet.Ansi)]
		private static extern IntPtr Compilable(byte[] scriptSource);

		// Token: 0x0600002A RID: 42
		[DllImport("FTdl.dll", CallingConvention = 2)]
		private static extern void Detach();

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024ED File Offset: 0x000006ED
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000024F5 File Offset: 0x000006F5
		public List<ClientsWindow.ClientInfo> ActiveClients { get; private set; } = new List<ClientsWindow.ClientInfo>();

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024FE File Offset: 0x000006FE
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002506 File Offset: 0x00000706
		public string SupportedVersion { get; private set; } = "";

		// Token: 0x0600002F RID: 47 RVA: 0x00002510 File Offset: 0x00000710
		public ClientsWindow()
		{
			this.OpenDiscordLink();
			this.FirstStartKeySystem();
			this.LoadSupportedVersion();
			this.InitializeComponent();
			base.MouseLeftButtonDown += delegate(object _, MouseButtonEventArgs _)
			{
				base.DragMove();
			};
			this._timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(100.0)
			};
			this._timer.Tick += this.UpdateClientList;
			this._processMonitorTimer = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(1000.0)
			};
			this._processMonitorTimer.Tick += this.MonitorRobloxProcess;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000025DC File Offset: 0x000007DC
		private Task LoadSupportedVersionAsync()
		{
			ClientsWindow.<LoadSupportedVersionAsync>d__25 <LoadSupportedVersionAsync>d__;
			<LoadSupportedVersionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadSupportedVersionAsync>d__.<>4__this = this;
			<LoadSupportedVersionAsync>d__.<>1__state = -1;
			<LoadSupportedVersionAsync>d__.<>t__builder.Start<ClientsWindow.<LoadSupportedVersionAsync>d__25>(ref <LoadSupportedVersionAsync>d__);
			return <LoadSupportedVersionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002620 File Offset: 0x00000820
		private Task FirstStartKeySystem()
		{
			ClientsWindow.<FirstStartKeySystem>d__26 <FirstStartKeySystem>d__;
			<FirstStartKeySystem>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FirstStartKeySystem>d__.<>1__state = -1;
			<FirstStartKeySystem>d__.<>t__builder.Start<ClientsWindow.<FirstStartKeySystem>d__26>(ref <FirstStartKeySystem>d__);
			return <FirstStartKeySystem>d__.<>t__builder.Task;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000265C File Offset: 0x0000085C
		public Task Attach()
		{
			ClientsWindow.<Attach>d__27 <Attach>d__;
			<Attach>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Attach>d__.<>4__this = this;
			<Attach>d__.<>1__state = -1;
			<Attach>d__.<>t__builder.Start<ClientsWindow.<Attach>d__27>(ref <Attach>d__);
			return <Attach>d__.<>t__builder.Task;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026A0 File Offset: 0x000008A0
		private void MonitorRobloxProcess(object sender, EventArgs e)
		{
			if (!this.IsRobloxRunning() && this._isInjected)
			{
				this._isInjected = false;
				try
				{
					ClientsWindow.Detach();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Failed to detach FluxTeam: " + ex.Message, "Detach Error", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this._timer.Stop();
				this._processMonitorTimer.Stop();
				Action<string, Brush> statusUpdated = this.StatusUpdated;
				if (statusUpdated == null)
				{
					return;
				}
				statusUpdated("Not Connected", Brushes.Red);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000272C File Offset: 0x0000092C
		private bool IsRobloxRunning()
		{
			Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
			if (processesByName.Length != 0)
			{
				this._robloxProcess = processesByName[0];
				return true;
			}
			this._robloxProcess = null;
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000275B File Offset: 0x0000095B
		private void UpdateStatus(string status)
		{
			MessageBox.Show(status);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002764 File Offset: 0x00000964
		private void DetachFromProcess()
		{
			try
			{
				ClientsWindow.Detach();
				this._isInjected = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to detach FluxTeam: " + ex.Message, "Detach Error", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027B0 File Offset: 0x000009B0
		private Task LoadSupportedVersion()
		{
			ClientsWindow.<LoadSupportedVersion>d__32 <LoadSupportedVersion>d__;
			<LoadSupportedVersion>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadSupportedVersion>d__.<>4__this = this;
			<LoadSupportedVersion>d__.<>1__state = -1;
			<LoadSupportedVersion>d__.<>t__builder.Start<ClientsWindow.<LoadSupportedVersion>d__32>(ref <LoadSupportedVersion>d__);
			return <LoadSupportedVersion>d__.<>t__builder.Task;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027F4 File Offset: 0x000009F4
		private void UpdateClientList(object sender, EventArgs e)
		{
			Dictionary<int, ClientsWindow.ClientInfo> newClients = this.GetClientsFromDll().ToDictionary((ClientsWindow.ClientInfo c) => c.id);
			(from cb in this.checkBoxContainer.Children.OfType<CheckBox>()
			where !newClients.ContainsKey(ClientsWindow.GetClientId(cb.Content.ToString()))
			select cb).ToList<CheckBox>().ForEach(delegate(CheckBox cb)
			{
				this.checkBoxContainer.Children.Remove(cb);
			});
			foreach (ClientsWindow.ClientInfo clientInfo in newClients.Values)
			{
				if (!this.IsClientListed(clientInfo) && !string.IsNullOrWhiteSpace(clientInfo.name))
				{
					this.AddClientCheckBox(clientInfo);
				}
			}
			this.ActiveClients = (from cb in this.checkBoxContainer.Children.OfType<CheckBox>()
			where cb.IsChecked.GetValueOrDefault()
			select new ClientsWindow.ClientInfo
			{
				name = ClientsWindow.GetClientName(cb.Content.ToString()),
				id = ClientsWindow.GetClientId(cb.Content.ToString())
			}).ToList<ClientsWindow.ClientInfo>();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000293C File Offset: 0x00000B3C
		private void OpenDiscordLink()
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = "https://discord.gg/fluxus",
					UseShellExecute = true
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to open Discord link: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000299C File Offset: 0x00000B9C
		public void ExecuteScript(string script)
		{
			string[] array = (from c in this.ActiveClients
			select c.name).ToArray<string>();
			ClientsWindow.Execute(Encoding.UTF8.GetBytes(script), array, array.Length);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029F0 File Offset: 0x00000BF0
		public string GetCompilableStatus(string script)
		{
			IntPtr ptr = ClientsWindow.Compilable(Encoding.ASCII.GetBytes(script));
			string result = Marshal.PtrToStringAnsi(ptr);
			Marshal.FreeCoTaskMem(ptr);
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A1C File Offset: 0x00000C1C
		private List<ClientsWindow.ClientInfo> GetClientsFromDll()
		{
			List<ClientsWindow.ClientInfo> list = new List<ClientsWindow.ClientInfo>();
			IntPtr intPtr = ClientsWindow.GetClients();
			for (;;)
			{
				ClientsWindow.ClientInfo clientInfo = Marshal.PtrToStructure<ClientsWindow.ClientInfo>(intPtr);
				if (clientInfo.name == null)
				{
					break;
				}
				list.Add(clientInfo);
				intPtr += (IntPtr)Marshal.SizeOf<ClientsWindow.ClientInfo>();
			}
			return list;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A57 File Offset: 0x00000C57
		private static int GetClientId(string content)
		{
			return int.Parse(content.Split(", PID: ", StringSplitOptions.None)[1]);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A6C File Offset: 0x00000C6C
		private static string GetClientName(string content)
		{
			return content.Split(", PID: ", StringSplitOptions.None)[0].Trim();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A84 File Offset: 0x00000C84
		private bool IsClientListed(ClientsWindow.ClientInfo client)
		{
			return this.checkBoxContainer.Children.OfType<CheckBox>().Any(delegate(CheckBox cb)
			{
				string a = cb.Content.ToString();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
				defaultInterpolatedStringHandler.AppendFormatted(client.name);
				defaultInterpolatedStringHandler.AppendLiteral(", PID: ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(client.id);
				return a == defaultInterpolatedStringHandler.ToStringAndClear();
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AC0 File Offset: 0x00000CC0
		private Task AddClientCheckBox(ClientsWindow.ClientInfo client)
		{
			ClientsWindow.<AddClientCheckBox>d__41 <AddClientCheckBox>d__;
			<AddClientCheckBox>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddClientCheckBox>d__.<>4__this = this;
			<AddClientCheckBox>d__.client = client;
			<AddClientCheckBox>d__.<>1__state = -1;
			<AddClientCheckBox>d__.<>t__builder.Start<ClientsWindow.<AddClientCheckBox>d__41>(ref <AddClientCheckBox>d__);
			return <AddClientCheckBox>d__.<>t__builder.Task;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B0B File Offset: 0x00000D0B
		private void buttonClose_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x04000016 RID: 22
		public string XenoVersion = "1.2";

		// Token: 0x04000017 RID: 23
		private bool _isInjected;

		// Token: 0x04000018 RID: 24
		private Process _robloxProcess;

		// Token: 0x04000019 RID: 25
		private readonly DispatcherTimer _timer;

		// Token: 0x0400001A RID: 26
		private readonly DispatcherTimer _processMonitorTimer;

		// Token: 0x02000010 RID: 16
		[Nullable(0)]
		public struct ClientInfo
		{
			// Token: 0x0400004A RID: 74
			public string version;

			// Token: 0x0400004B RID: 75
			public string name;

			// Token: 0x0400004C RID: 76
			public int id;
		}
	}
}
