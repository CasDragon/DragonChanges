using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public class IllFortuneBuff
{
    // edit
    internal const string buff = "illfortunebuff";
    internal const string buffguid = Guids.IllFortuneBuff;
    // don't edit
    [DragonLocalizedString(buffname, "Child of Ill Fortune")]
    internal const string buffname = $"{buff}.name";
    [DragonLocalizedString(buffdescription, "For the spell’s duration, the target suffers -2 penalties to all attack, damage (weapon and spell), skill check and ability check rolls. Any spells cast by the target have their DC decreased by 5. Child of ill fortune lasts a full 24 hours unless removed by dispel magic or remove curse.")]
    internal const string buffdescription = $"{buff}.description";
    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(buffname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        var x = BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(buffname)
            .SetDescription(buffdescription)
            .AddSpellDescriptorComponent(SpellDescriptor.Curse)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.AdditionalAttackBonus,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.AdditionalDamage,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillAthletics,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillKnowledgeArcana,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillKnowledgeWorld,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillLoreNature,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillLoreReligion,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillMobility,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillPerception,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillPersuasion,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillStealth,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillThievery,
                -2)
            .AddGenericStatBonus(ModifierDescriptor.UntypedStackable,
                StatType.SkillUseMagicDevice,
                -2)
            .AddIncreaseAllSpellsDC(ModifierDescriptor.UntypedStackable,
                spellsOnly: true,
                value: ContextValues.Constant(-5))
            .SetFlags(BlueprintBuff.Flags.Harmful, BlueprintBuff.Flags.IsFromSpell)
            .SetIcon(icon)
            .Configure();
        return x;
    }
}