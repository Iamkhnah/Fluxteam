using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ScriptBloxSearch
{
	// Token: 0x02000005 RID: 5
	[NullableContext(1)]
	[Nullable(0)]
	public class Script
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002324 File Offset: 0x00000524
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000232C File Offset: 0x0000052C
		[JsonProperty("title")]
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002335 File Offset: 0x00000535
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000233D File Offset: 0x0000053D
		[JsonProperty("script")]
		public string ScriptContent
		{
			[CompilerGenerated]
			get
			{
				return this.<ScriptContent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ScriptContent>k__BackingField = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002346 File Offset: 0x00000546
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000234E File Offset: 0x0000054E
		[JsonProperty("imageUrl")]
		public string ImageUrl
		{
			[CompilerGenerated]
			get
			{
				return this.<ImageUrl>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ImageUrl>k__BackingField = value;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002357 File Offset: 0x00000557
		public Script()
		{
		}

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		private string <ScriptContent>k__BackingField;

		// Token: 0x0400000E RID: 14
		[CompilerGenerated]
		private string <ImageUrl>k__BackingField;
	}
}
