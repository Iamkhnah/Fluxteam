using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FluxTeamWindows.Properties
{
	// Token: 0x02000006 RID: 6
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000235F File Offset: 0x0000055F
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x0400000F RID: 15
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
