using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ScriptBloxSearch
{
	// Token: 0x02000004 RID: 4
	[NullableContext(1)]
	[Nullable(0)]
	public class ScriptResult
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000230B File Offset: 0x0000050B
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002313 File Offset: 0x00000513
		[JsonProperty("scripts")]
		public List<Script> Scripts
		{
			[CompilerGenerated]
			get
			{
				return this.<Scripts>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Scripts>k__BackingField = value;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000231C File Offset: 0x0000051C
		public ScriptResult()
		{
		}

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private List<Script> <Scripts>k__BackingField;
	}
}
