using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Parts;
using static DragonChanges.Patches.ActivatableAbilityGroupPatch;

namespace DragonChanges.Patches
{
    internal static class ActivatableAbilityGroupPatch
    {
        // Add new ActivatableAbilityGroups here
        public enum DCActivatableAbilityGroup : int
        {
            TenguSwordmasterTrance = 2468
        }

        private static bool IsDCGroup(this ActivatableAbilityGroup group)
        {
            return Enum.IsDefined(typeof(DCActivatableAbilityGroup), (int)group);
        }

        [HarmonyPatch(typeof(UnitPartActivatableAbility), nameof(UnitPartActivatableAbility.IncreaseGroupSize), new Type[] { typeof(ActivatableAbilityGroup) })]
        static class UnitPartActivatableAbility_IncreaseGroupSize_Patch
        {

            static bool Prefix(UnitPartActivatableAbility __instance, ActivatableAbilityGroup group)
            {
                if (group.IsDCGroup())
                {
                    var extensionPart = __instance.Owner.Ensure<UnitPartActivatableAbilityGroupDC>();
                    extensionPart.IncreaseGroupSize((DCActivatableAbilityGroup)group);
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(UnitPartActivatableAbility), nameof(UnitPartActivatableAbility.DecreaseGroupSize), new Type[] { typeof(ActivatableAbilityGroup) })]
        static class UnitPartActivatableAbility_DecreaseGroupSize_Patch
        {

            static bool Prefix(UnitPartActivatableAbility __instance, ActivatableAbilityGroup group)
            {
                if (group.IsDCGroup())
                {
                    var extensionPart = __instance.Owner.Ensure<UnitPartActivatableAbilityGroupDC>();
                    extensionPart.DecreaseGroupSize((DCActivatableAbilityGroup)group);
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(UnitPartActivatableAbility), nameof(UnitPartActivatableAbility.GetGroupSize), new Type[] { typeof(ActivatableAbilityGroup) })]
        static class UnitPartActivatableAbility_GetGroupSize_Patch
        {

            static bool Prefix(UnitPartActivatableAbility __instance, ActivatableAbilityGroup group, ref int __result)
            {
                if (group.IsDCGroup())
                {
                    var extensionPart = __instance.Owner.Ensure<UnitPartActivatableAbilityGroupDC>();
                    __result = extensionPart.GetGroupSize((DCActivatableAbilityGroup)group);
                    return false;
                }
                return true;
            }
        }
    }

    internal class UnitPartActivatableAbilityGroupDC : OldStyleUnitPart
    {
        public void IncreaseGroupSize(DCActivatableAbilityGroup group)
        {
            if (m_GroupsSizeIncreases.ContainsKey(group))
            {
                this.m_GroupsSizeIncreases[group] += 1;
            }
            else
            {
                m_GroupsSizeIncreases.Add(group, 1);
            }
        }

        public void DecreaseGroupSize(DCActivatableAbilityGroup group)
        {
            if (m_GroupsSizeIncreases.ContainsKey(group))
            {
                this.m_GroupsSizeIncreases[group] -= 1;
            }
        }

        public int GetGroupSize(DCActivatableAbilityGroup group)
        {
            this.m_GroupsSizeIncreases.TryGetValue(group, out int result);
            return result + 1;
        }

        private SortedDictionary<DCActivatableAbilityGroup, int> m_GroupsSizeIncreases = new SortedDictionary<DCActivatableAbilityGroup, int>();
    }
    }
}
