using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Web.WebView2.Wpf;
using ScriptBloxSearch;

namespace XenoUI
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public partial class MainWindow : Window
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002E00 File Offset: 0x00001000
		public MainWindow()
		{
			this.InitializeComponent();
			this._scriptsWindow = new ScriptsWindow(this);
			this.InitializeProcessMonitoring();
			this.InitializeWebView2();
			this._timer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(3.0)
			};
			this._timer.Tick += this.SaveChanges;
			this._timer.Start();
			this.LoadSettings();
			this._clientsWindow.StatusUpdated += this.OnStatusUpdated;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E9C File Offset: 0x0000109C
		private void LoadSettings()
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.txt");
			if (File.Exists(path))
			{
				foreach (string text in File.ReadAllLines(path))
				{
					if (text.StartsWith("Topmost="))
					{
						text.Substring("Topmost=".Length).Trim().Equals("on", StringComparison.OrdinalIgnoreCase);
						return;
					}
				}
				return;
			}
			File.WriteAllText(path, "Topmost=on");
			base.Topmost = true;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F24 File Offset: 0x00001124
		private void OnStatusUpdated(string statusText, Brush statusColor)
		{
			base.Dispatcher.Invoke(delegate()
			{
				this.StatusText.Text = statusText;
				this.StatusCircle.Fill = statusColor;
			});
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F63 File Offset: 0x00001163
		private void OpenScriptHubButton_Click(object sender, RoutedEventArgs e)
		{
			new ScriptHub().Show();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F70 File Offset: 0x00001170
		private void InitializeProcessMonitoring()
		{
			this._processCheckTimer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(2.0)
			};
			this._processCheckTimer.Tick += this.CheckRobloxProcess;
			this._processCheckTimer.Start();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002FBE File Offset: 0x000011BE
		private void CheckRobloxProcess(object sender, EventArgs e)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002FC0 File Offset: 0x000011C0
		private Task SetScriptContent(string content)
		{
			MainWindow.<SetScriptContent>d__14 <SetScriptContent>d__;
			<SetScriptContent>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SetScriptContent>d__.<>4__this = this;
			<SetScriptContent>d__.content = content;
			<SetScriptContent>d__.<>1__state = -1;
			<SetScriptContent>d__.<>t__builder.Start<MainWindow.<SetScriptContent>d__14>(ref <SetScriptContent>d__);
			return <SetScriptContent>d__.<>t__builder.Task;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000300B File Offset: 0x0000120B
		private static string EscapeForScript(string content)
		{
			return content.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000304C File Offset: 0x0000124C
		private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<buttonOpenFile_Click>d__16 <buttonOpenFile_Click>d__;
			<buttonOpenFile_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<buttonOpenFile_Click>d__.<>4__this = this;
			<buttonOpenFile_Click>d__.<>1__state = -1;
			<buttonOpenFile_Click>d__.<>t__builder.Start<MainWindow.<buttonOpenFile_Click>d__16>(ref <buttonOpenFile_Click>d__);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003084 File Offset: 0x00001284
		private void CopyScriptToClipboard(string scriptName)
		{
			string path = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts"), scriptName + ".lua");
			if (File.Exists(path))
			{
				Clipboard.SetText(File.ReadAllText(path));
				MessageBox.Show("Script copied to clipboard.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			MessageBox.Show("Script not found at the expected location.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030F0 File Offset: 0x000012F0
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003104 File Offset: 0x00001304
		private void SaveSettings()
		{
			string str = base.Topmost ? "on" : "off";
			File.WriteAllText("settings.txt", "TopMost=" + str);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000313C File Offset: 0x0000133C
		private void SaveChanges(object sender, EventArgs e)
		{
			MainWindow.<SaveChanges>d__20 <SaveChanges>d__;
			<SaveChanges>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SaveChanges>d__.<>4__this = this;
			<SaveChanges>d__.<>1__state = -1;
			<SaveChanges>d__.<>t__builder.Start<MainWindow.<SaveChanges>d__20>(ref <SaveChanges>d__);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003174 File Offset: 0x00001374
		private void buttonSaveFile_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<buttonSaveFile_Click>d__21 <buttonSaveFile_Click>d__;
			<buttonSaveFile_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<buttonSaveFile_Click>d__.<>4__this = this;
			<buttonSaveFile_Click>d__.<>1__state = -1;
			<buttonSaveFile_Click>d__.<>t__builder.Start<MainWindow.<buttonSaveFile_Click>d__21>(ref <buttonSaveFile_Click>d__);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031AC File Offset: 0x000013AC
		private void buttonClear_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<buttonClear_Click>d__22 <buttonClear_Click>d__;
			<buttonClear_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<buttonClear_Click>d__.<>4__this = this;
			<buttonClear_Click>d__.<>1__state = -1;
			<buttonClear_Click>d__.<>t__builder.Start<MainWindow.<buttonClear_Click>d__22>(ref <buttonClear_Click>d__);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031E4 File Offset: 0x000013E4
		private void InitializeWebView2()
		{
			MainWindow.<InitializeWebView2>d__23 <InitializeWebView2>d__;
			<InitializeWebView2>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<InitializeWebView2>d__.<>4__this = this;
			<InitializeWebView2>d__.<>1__state = -1;
			<InitializeWebView2>d__.<>t__builder.Start<MainWindow.<InitializeWebView2>d__23>(ref <InitializeWebView2>d__);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000321C File Offset: 0x0000141C
		private Task LoadWebView()
		{
			MainWindow.<LoadWebView>d__24 <LoadWebView>d__;
			<LoadWebView>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadWebView>d__.<>4__this = this;
			<LoadWebView>d__.<>1__state = -1;
			<LoadWebView>d__.<>t__builder.Start<MainWindow.<LoadWebView>d__24>(ref <LoadWebView>d__);
			return <LoadWebView>d__.<>t__builder.Task;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003260 File Offset: 0x00001460
		private Task<string> GetScriptContent()
		{
			MainWindow.<GetScriptContent>d__25 <GetScriptContent>d__;
			<GetScriptContent>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<GetScriptContent>d__.<>4__this = this;
			<GetScriptContent>d__.<>1__state = -1;
			<GetScriptContent>d__.<>t__builder.Start<MainWindow.<GetScriptContent>d__25>(ref <GetScriptContent>d__);
			return <GetScriptContent>d__.<>t__builder.Task;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000032A4 File Offset: 0x000014A4
		private void Attach_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<Attach_Click>d__26 <Attach_Click>d__;
			<Attach_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Attach_Click>d__.<>4__this = this;
			<Attach_Click>d__.<>1__state = -1;
			<Attach_Click>d__.<>t__builder.Start<MainWindow.<Attach_Click>d__26>(ref <Attach_Click>d__);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032DB File Offset: 0x000014DB
		private static void ShowError(string message)
		{
			MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032EC File Offset: 0x000014EC
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032F8 File Offset: 0x000014F8
		private void Close_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<Close_Click>d__29 <Close_Click>d__;
			<Close_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Close_Click>d__.<>4__this = this;
			<Close_Click>d__.<>1__state = -1;
			<Close_Click>d__.<>t__builder.Start<MainWindow.<Close_Click>d__29>(ref <Close_Click>d__);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000332F File Offset: 0x0000152F
		private void ToggleTopMost(bool isChecked)
		{
			base.Topmost = isChecked;
			this.SaveSettings();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000333E File Offset: 0x0000153E
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			this.SaveSettings("on");
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003352 File Offset: 0x00001552
		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			base.Topmost = false;
			this.SaveSettings("off");
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003366 File Offset: 0x00001566
		private void SaveSettings(string topmostValue)
		{
			File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.txt"), "Topmost=" + topmostValue);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000338C File Offset: 0x0000158C
		private void Kill_Roblox_Click(object sender, RoutedEventArgs e)
		{
			Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
			if (processesByName.Length != 0)
			{
				Process[] array = processesByName;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Kill();
				}
				MessageBox.Show("Roblox terminated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			MessageBox.Show("RobloxPlayerBeta process not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033EA File Offset: 0x000015EA
		private void clients(object sender, RoutedEventArgs e)
		{
			new ClientsWindow().Show();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000033F8 File Offset: 0x000015F8
		private Task SaveScriptContent()
		{
			MainWindow.<SaveScriptContent>d__36 <SaveScriptContent>d__;
			<SaveScriptContent>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveScriptContent>d__.<>4__this = this;
			<SaveScriptContent>d__.<>1__state = -1;
			<SaveScriptContent>d__.<>t__builder.Start<MainWindow.<SaveScriptContent>d__36>(ref <SaveScriptContent>d__);
			return <SaveScriptContent>d__.<>t__builder.Task;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000343B File Offset: 0x0000163B
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003444 File Offset: 0x00001644
		public void ExecuteScript(string scriptContent)
		{
			if (!this._clientsWindow.ActiveClients.Any<ClientsWindow.ClientInfo>())
			{
				MessageBox.Show("No clients are currently selected. Please select at least one client before attempting to execute the script.", "No Client Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}
			string compilableStatus = this._clientsWindow.GetCompilableStatus(scriptContent);
			if (compilableStatus != "success")
			{
				MessageBox.Show(compilableStatus, "Compiler Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}
			this._clientsWindow.ExecuteScript(scriptContent);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034B0 File Offset: 0x000016B0
		private void buttonExecute_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<buttonExecute_Click>d__39 <buttonExecute_Click>d__;
			<buttonExecute_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<buttonExecute_Click>d__.<>4__this = this;
			<buttonExecute_Click>d__.<>1__state = -1;
			<buttonExecute_Click>d__.<>t__builder.Start<MainWindow.<buttonExecute_Click>d__39>(ref <buttonExecute_Click>d__);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034E7 File Offset: 0x000016E7
		private void buttonShowMultinstance_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.ToggleWindowVisibility(this._clientsWindow);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000034F4 File Offset: 0x000016F4
		private void buttonShowScripts_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.ToggleWindowVisibility(this._scriptsWindow);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003501 File Offset: 0x00001701
		private static void ToggleWindowVisibility(Window window)
		{
			if (window.IsVisible)
			{
				window.Hide();
				return;
			}
			window.Show();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003518 File Offset: 0x00001718
		private void Click_function_close(object sender, MouseButtonEventArgs e)
		{
			Application.Current.Shutdown();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003524 File Offset: 0x00001724
		private void Click_function_minimize(object sender, MouseButtonEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x04000027 RID: 39
		public readonly ClientsWindow _clientsWindow = new ClientsWindow();

		// Token: 0x04000028 RID: 40
		private DispatcherTimer _processCheckTimer;

		// Token: 0x04000029 RID: 41
		private readonly ScriptsWindow _scriptsWindow;

		// Token: 0x0400002A RID: 42
		private readonly DispatcherTimer _timer;

		// Token: 0x0400002B RID: 43
		private string _lastContent;

		// Token: 0x0400002C RID: 44
		private const string SettingsFilePath = "settings.txt";

		// Token: 0x0400002D RID: 45
		private const string TopMostSettingKey = "TopMost";

		// Token: 0x0400002E RID: 46
		private const string TopMostDefault = "on";
	}
}
