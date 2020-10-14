using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;


// In progress: To incorporate into BT
/* Credit to Bleinz for his Unlimited Wanderers Mod */
namespace BannerlordTweaks.Patches
{
	// Token: 0x02000002 RID: 2
	[HarmonyPatch(typeof(UrbanCharactersCampaignBehavior), "SpawnUrbanCharacters")]
	
	public static class SpawnUrbanCharactersPatch
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			list.RemoveRange(141, 3);
			return list.AsEnumerable<CodeInstruction>();
		}
	}
}
