using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ScriptBloxSearch
{
	// Token: 0x02000003 RID: 3
	[NullableContext(1)]
	[Nullable(0)]
	public class ScriptApiResponse
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022F2 File Offset: 0x000004F2
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000022FA File Offset: 0x000004FA
		[JsonProperty("result")]
		public ScriptResult Result
		{
			[CompilerGenerated]
			get
			{
				return this.<Result>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Result>k__BackingField = value;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002303 File Offset: 0x00000503
		public ScriptApiResponse()
		{
		}

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		private ScriptResult <Result>k__BackingField;
	}
}
