using HarmonyLib;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Parts;

namespace DragonChanges.Patches;

[HarmonyPatch]
public static class Immunities
{
    [HarmonyPatch(typeof(BuffDescriptorImmunity), nameof(BuffDescriptorImmunity.IsImmune))]
    [HarmonyPostfix]
    public static void BypassImmunity(BuffDescriptorImmunity __instance, ref bool __result, MechanicsContext context)
    {
        if (!__result) return;
        if (context.MaybeCaster == null) return;
        var bypassComponents =
            context.MaybeCaster.Facts.List.Where(c => c.Blueprint.Components.OfType<IgnoreSpellImmunity>().Any());
        foreach (var ignoreBP in bypassComponents)
        {
            var comp = ignoreBP.Blueprint.Components.First(c => c is IgnoreSpellImmunity) as IgnoreSpellImmunity;
            var hasDescriptor = __instance.Descriptor.HasAnyFlag(comp!.SpellDescriptor);
            if (!hasDescriptor) continue;
            __result = false;
            return;
        }
    }

    [HarmonyPatch(typeof(UnitPartSpellResistance), nameof(UnitPartSpellResistance.IsImmune), [typeof(BlueprintAbility), typeof(UnitEntityData)])]
    [HarmonyPrefix]
    public static bool BypassImmunity2(UnitPartSpellResistance __instance, ref bool __result,
        BlueprintAbility ability, UnitEntityData? caster)
    {
        var bypassComponents =
            caster?.Facts.List.Where(c => c.Blueprint.Components.OfType<IgnoreSpellImmunity>().Any()).ToList();
        foreach (var spellImmunity in __instance.m_SpellImmunities)
        {
            if (caster != null && bypassComponents != null)
            {
                foreach (var ignoreBP in bypassComponents)
                {
                    var comp =
                        ignoreBP.Blueprint.Components.First(c => c is IgnoreSpellImmunity) as IgnoreSpellImmunity;
                    var hasDescriptor = spellImmunity.SpellDescriptor.HasAnyFlag(comp!.SpellDescriptor);
                    if (!hasDescriptor) continue;
                    __result = false;
                    return false;
                }
            }

            if (!__instance.CanApply(spellImmunity, null, caster, null, ability)) continue;
            __result = true;
            return false;
        }
        __result = false;
        return false;
    }

    [HarmonyPatch(typeof(UnitPartSpellResistance), nameof(UnitPartSpellResistance.IsImmune), [typeof(MechanicsContext), typeof(bool)])]
    [HarmonyPrefix]
    public static bool BypassImmunity3(UnitPartSpellResistance __instance, ref bool __result, MechanicsContext? context,
        bool spellDescriptorOnly)
    {
        if (context != null && context.MaybeCaster == context.MainTarget)
        {
            __result = false;
            return false;
        }
        var bypassComponents =
            context?.MaybeCaster?.Facts.List.Where(c => c.Blueprint.Components.OfType<IgnoreSpellImmunity>().Any()).ToList();
        foreach (UnitPartSpellResistance.SpellImmunity spellImmunity in __instance.m_SpellImmunities)
        {
            if (spellDescriptorOnly && bypassComponents != null)
            {
                foreach (var ignoreBP in bypassComponents)
                {
                    var comp =
                        ignoreBP.Blueprint.Components.First(c => c is IgnoreSpellImmunity) as IgnoreSpellImmunity;
                    var hasDescriptor = spellImmunity.SpellDescriptor.HasAnyFlag(comp!.SpellDescriptor);
                    if (!hasDescriptor) continue;
                    __result = false;
                    return false;
                }
            }

            if ((spellDescriptorOnly && spellImmunity.Type != SpellImmunityType.SpellDescriptor) ||
                !__instance.CanApply(spellImmunity, context)) continue;
            __result = true;
            return false;
        }
        __result = false;
        return false;
    }
}