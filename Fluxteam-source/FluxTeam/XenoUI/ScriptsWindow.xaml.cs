using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace XenoUI
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	public partial class ScriptsWindow : Window
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000036F8 File Offset: 0x000018F8
		public ScriptsWindow(MainWindow mainWindow)
		{
			this.InitializeComponent();
			this._mainWindow = mainWindow;
			this.scriptsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
			this.scriptPanels = new Dictionary<string, UIElement>();
			base.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
			{
				base.DragMove();
			};
			Directory.CreateDirectory(this.scriptsDirectory);
			this.updateTimer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(0.5)
			};
			this.updateTimer.Tick += delegate([Nullable(2)] object sender, EventArgs e)
			{
				this.UpdateScripts();
			};
			this.updateTimer.Start();
			this.LoadScripts();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037A4 File Offset: 0x000019A4
		private void LoadScripts()
		{
			foreach (string filePath in Directory.GetFiles(this.scriptsDirectory))
			{
				this.AddScriptPanel(filePath);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000037D8 File Offset: 0x000019D8
		private void UpdateScripts()
		{
			HashSet<string> hashSet = new HashSet<string>(Directory.GetFiles(this.scriptsDirectory));
			foreach (string filePath in this.scriptPanels.Keys.Except(hashSet).ToList<string>())
			{
				this.RemoveScriptPanel(filePath);
			}
			foreach (string filePath2 in hashSet.Except(this.scriptPanels.Keys))
			{
				this.AddScriptPanel(filePath2);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003898 File Offset: 0x00001A98
		private void AddScriptPanel(string filePath)
		{
			ScriptsWindow.<>c__DisplayClass7_0 CS$<>8__locals1 = new ScriptsWindow.<>c__DisplayClass7_0();
			CS$<>8__locals1.filePath = filePath;
			CS$<>8__locals1.<>4__this = this;
			string fileName = Path.GetFileName(CS$<>8__locals1.filePath);
			Grid grid = new Grid
			{
				Margin = new Thickness(5.0),
				HorizontalAlignment = HorizontalAlignment.Stretch
			};
			grid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(1.0, GridUnitType.Star)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = GridLength.Auto
			});
			TextBlock element = new TextBlock
			{
				Text = fileName,
				Foreground = Brushes.White,
				VerticalAlignment = VerticalAlignment.Center,
				FontFamily = new FontFamily("Cascadia Code"),
				FontSize = 14.0
			};
			Grid.SetColumn(element, 0);
			Button button = new Button
			{
				Content = "Execute",
				Margin = new Thickness(10.0, 0.0, 0.0, 0.0),
				Tag = CS$<>8__locals1.filePath,
				Style = (Style)base.FindResource("DarkRoundedButtonStyle")
			};
			Grid.SetColumn(button, 1);
			button.Click += delegate(object sender, RoutedEventArgs e)
			{
				ScriptsWindow.<>c__DisplayClass7_0.<<AddScriptPanel>b__0>d <<AddScriptPanel>b__0>d;
				<<AddScriptPanel>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<AddScriptPanel>b__0>d.<>4__this = CS$<>8__locals1;
				<<AddScriptPanel>b__0>d.<>1__state = -1;
				<<AddScriptPanel>b__0>d.<>t__builder.Start<ScriptsWindow.<>c__DisplayClass7_0.<<AddScriptPanel>b__0>d>(ref <<AddScriptPanel>b__0>d);
			};
			Border element2 = new Border
			{
				BorderBrush = Brushes.Gray,
				BorderThickness = new Thickness(0.0, 0.0, 0.0, 1.0),
				Margin = new Thickness(0.0, 5.0, 0.0, 0.0)
			};
			Grid.SetColumn(element2, 0);
			Grid.SetColumnSpan(element2, 2);
			grid.Children.Add(element);
			grid.Children.Add(button);
			grid.Children.Add(element2);
			this.scripts_container.Children.Add(grid);
			this.scriptPanels[CS$<>8__locals1.filePath] = grid;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003AB8 File Offset: 0x00001CB8
		private void RemoveScriptPanel(string filePath)
		{
			UIElement element;
			if (this.scriptPanels.TryGetValue(filePath, out element))
			{
				this.scripts_container.Children.Remove(element);
				this.scriptPanels.Remove(filePath);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003AF3 File Offset: 0x00001CF3
		private void buttonClose_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x04000039 RID: 57
		private readonly string scriptsDirectory;

		// Token: 0x0400003A RID: 58
		private readonly DispatcherTimer updateTimer;

		// Token: 0x0400003B RID: 59
		private readonly Dictionary<string, UIElement> scriptPanels;

		// Token: 0x0400003C RID: 60
		private readonly MainWindow _mainWindow;
	}
}
