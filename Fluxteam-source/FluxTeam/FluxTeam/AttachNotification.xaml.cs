using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace FluxTeam
{
	// Token: 0x02000007 RID: 7
	public partial class AttachNotification : Window
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002384 File Offset: 0x00000584
		public AttachNotification()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002392 File Offset: 0x00000592
		[NullableContext(1)]
		private void AtttachAnimation_Completed(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
