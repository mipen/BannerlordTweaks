using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(ClanVM),"ExecuteLeaveKingdom")]
    public class ClanVMPatch
    {
        static bool Prefix()
        {
            try
            {
                if (Clan.PlayerClan.IsUnderMercenaryService)
                {
                    InformationManager.ShowInquiry(new InquiryData("Ending Mercenary Contract",
                        $"Your mercenary contract will end and you will no longer be affiliated with {Clan.PlayerClan.Kingdom.Name.ToString()}.",
                        true, true, new TextObject("{=5Unqsx3N}Confirm").ToString(),
                        GameTexts.FindText("str_cancel").ToString(),
                        () =>
                        {
                            ChangeKingdomAction.ApplyByLeaveKingdomAsMercenaryForNoPayment(Clan.PlayerClan, Clan.PlayerClan.Kingdom);
                            typeof(ClanVM).GetMethod("ExecuteClose", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                            .Invoke(__instance, null);
                        },
                        null));
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in the leaving Kingdom as mercenary fix:\n\n{ex.ToStringFull()}");
            }
            return true;
        }
    }
}
