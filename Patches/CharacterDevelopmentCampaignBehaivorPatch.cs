using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.Towns;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(CharacterDevelopmentCampaignBehaivor), "DistributeUnspentFocusPoints")]
    public class CharacterDevelopmentCampaignBehaivorPatch
    {
        static bool Prefix(Hero hero)
        {
            List<SkillObject> list = (from x in SkillObject.All
                                      where hero.HeroDeveloper.GetFocus(x) < 5
                                      select x).ToList();

            Func<SkillObject, float> weightFunction;

            while (hero.HeroDeveloper.UnspentFocusPoints > 0 && list.Any<SkillObject>())
            {
                IEnumerable<SkillObject> candidates = list;
                weightFunction = (SkillObject skill) => (float)(hero.GetSkillValue(skill) + 20 - 20 * hero.HeroDeveloper.GetFocus(skill));

                SkillObject skillObject = MBRandom.ChooseWeighted<SkillObject>(candidates, weightFunction);

                if (skillObject != null)
                    hero.HeroDeveloper.AddFocus(skillObject, 1, true);
                else
                    list.Remove(skillObject);

                if (skillObject != null && hero.HeroDeveloper.GetFocus(skillObject) == 5)
                {
                    list.Remove(skillObject);
                }

            }
            return false;
        }

        static bool Prepare()
        {
            return Settings.Instance.AttributeFocusPointTweakEnabled;
        }
    }
}
