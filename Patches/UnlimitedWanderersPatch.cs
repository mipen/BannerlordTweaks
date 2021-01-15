using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;


namespace BannerlordTweaks.Patches
{
	[HarmonyPatch(typeof(UrbanCharactersCampaignBehavior), "SpawnUrbanCharacters")]
	
	public static class SpawnUrbanCharactersPatch
	{
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			list.RemoveRange(141, 3);
			return list.AsEnumerable<CodeInstruction>();
		}

		static bool Prepare()
		{
			return BannerlordTweaksSettings.Instance.UnlimitedWanderersPatch;
		}

	}
}
