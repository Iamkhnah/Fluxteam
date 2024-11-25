using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace XenoUI
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	public partial class KeyEntryWindow : Window
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002B9B File Offset: 0x00000D9B
		public KeyEntryWindow()
		{
			this.InitializeComponent();
			this.CheckExistingKeys();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BAF File Offset: 0x00000DAF
		private void buttonClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BB8 File Offset: 0x00000DB8
		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			KeyEntryWindow.<SubmitButton_Click>d__4 <SubmitButton_Click>d__;
			<SubmitButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SubmitButton_Click>d__.<>4__this = this;
			<SubmitButton_Click>d__.<>1__state = -1;
			<SubmitButton_Click>d__.<>t__builder.Start<KeyEntryWindow.<SubmitButton_Click>d__4>(ref <SubmitButton_Click>d__);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BEF File Offset: 0x00000DEF
		private void GetKeyButton_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(new ProcessStartInfo("https://getzorara.online:2083/")
			{
				UseShellExecute = true
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C08 File Offset: 0x00000E08
		private Task<bool> CheckAndRedeemKey(string key)
		{
			KeyEntryWindow.<CheckAndRedeemKey>d__6 <CheckAndRedeemKey>d__;
			<CheckAndRedeemKey>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CheckAndRedeemKey>d__.key = key;
			<CheckAndRedeemKey>d__.<>1__state = -1;
			<CheckAndRedeemKey>d__.<>t__builder.Start<KeyEntryWindow.<CheckAndRedeemKey>d__6>(ref <CheckAndRedeemKey>d__);
			return <CheckAndRedeemKey>d__.<>t__builder.Task;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C4B File Offset: 0x00000E4B
		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C5C File Offset: 0x00000E5C
		private void SaveKeyToFile(string key)
		{
			try
			{
				File.WriteAllText("key.fluxteam", key + "\n- .gg/fluxus\n");
			}
			catch (IOException ex)
			{
				MessageBox.Show("Error saving key: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private void CheckExistingKeys()
		{
			if (File.Exists("key.fluxteam"))
			{
				string[] array = File.ReadAllLines("key.fluxteam");
				int num = 0;
				if (num < array.Length)
				{
					string text = array[num];
					this.KeyTextBox.Text = text.Trim();
					this.SubmitButton_Click(this, null);
					return;
				}
			}
		}

		// Token: 0x04000020 RID: 32
		private const string KeyFileName = "key.fluxteam";

		// Token: 0x04000021 RID: 33
		private const string KeyCheckFileName = "key.fluxteam";
	}
}
