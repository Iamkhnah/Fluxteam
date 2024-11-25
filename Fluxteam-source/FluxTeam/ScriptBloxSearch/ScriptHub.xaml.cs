using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace ScriptBloxSearch
{
	// Token: 0x02000002 RID: 2
	public partial class ScriptHub : Window
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
		public ScriptHub()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		[NullableContext(1)]
		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			ScriptHub.<SearchButton_Click>d__3 <SearchButton_Click>d__;
			<SearchButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SearchButton_Click>d__.<>4__this = this;
			<SearchButton_Click>d__.<>1__state = -1;
			<SearchButton_Click>d__.<>t__builder.Start<ScriptHub.<SearchButton_Click>d__3>(ref <SearchButton_Click>d__);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000209B File Offset: 0x0000029B
		[NullableContext(1)]
		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ButtonState == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020AC File Offset: 0x000002AC
		[NullableContext(1)]
		private void ScriptItem_MouseDown(object sender, MouseButtonEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;
			if (frameworkElement != null)
			{
				Script script = frameworkElement.DataContext as Script;
				if (script != null)
				{
					this.ScriptsListView.SelectedItem = script;
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		[NullableContext(1)]
		private Task SearchScripts(string query)
		{
			ScriptHub.<SearchScripts>d__6 <SearchScripts>d__;
			<SearchScripts>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SearchScripts>d__.<>4__this = this;
			<SearchScripts>d__.query = query;
			<SearchScripts>d__.<>1__state = -1;
			<SearchScripts>d__.<>t__builder.Start<ScriptHub.<SearchScripts>d__6>(ref <SearchScripts>d__);
			return <SearchScripts>d__.<>t__builder.Task;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000212B File Offset: 0x0000032B
		[NullableContext(1)]
		private void ScriptsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.CopyScriptButton.IsEnabled = (this.ScriptsListView.SelectedItem != null);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002148 File Offset: 0x00000348
		[NullableContext(1)]
		private void CopyScriptButton_Click(object sender, RoutedEventArgs e)
		{
			Script script = this.ScriptsListView.SelectedItem as Script;
			if (script != null)
			{
				Clipboard.SetText(script.ScriptContent);
				MessageBox.Show("Script copied to clipboard!");
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217F File Offset: 0x0000037F
		[NullableContext(1)]
		private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.PlaceholderText.Visibility = (string.IsNullOrEmpty(this.SearchBox.Text) ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A2 File Offset: 0x000003A2
		[NullableContext(1)]
		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000001 RID: 1
		[Nullable(1)]
		private const string ApiUrl = "https://scriptblox.com/api/script/search?q=";

		// Token: 0x04000002 RID: 2
		[Nullable(1)]
		private List<Script> _scripts = new List<Script>();

		// Token: 0x0200000D RID: 13
		public class EmptyStringToVisibilityConverter : IValueConverter
		{
			// Token: 0x06000080 RID: 128 RVA: 0x00003B8B File Offset: 0x00001D8B
			[NullableContext(1)]
			public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			{
				return string.IsNullOrEmpty(value as string) ? Visibility.Visible : Visibility.Collapsed;
			}

			// Token: 0x06000081 RID: 129 RVA: 0x00003BA3 File Offset: 0x00001DA3
			[NullableContext(1)]
			public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			{
				throw new NotImplementedException();
			}
		}
	}
}
