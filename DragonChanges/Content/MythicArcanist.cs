using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonChanges.Content;

public class MythicArcanist
{
    
    const string SettingName = "ma-archmage";
    const string SettingDescription = "Allow Archmage Armor to work with Mythic Arcanist's Mage Armor spells";
    [DragonConfigure]
    [DragonSetting(SettingCategories.ModCompatability, SettingName, SettingDescription)]
    public static void PatchArchmage()
    {
        if (!SettingsAction.GetSetting<bool>(SettingName)) return;
        if (!ModCompat.IsModEnabled("MythicArcanist")) return;
        try
        {
            Main.log.Log("Patching Archmage Armor to work with Mythic Arcanist's Mage Armor spells");
            FeatureConfigurator.For(FeatureRefs.ArchmageArmor)
                .EditComponent<AddAbilityUseTrigger>(doThingy)
                .Configure();
            BuffConfigurator.For(BuffRefs.MageArmorBuffMythic)
                .EditComponent<SuppressBuffs>(doOther)
                .Configure();
        }
        catch
        {
            Main.log.Log("Error patching Archmage Armor , skipping");
        }
    }

    private static readonly BlueprintAbilityReference MageArmor2Spell = new() {deserializedGuid = BlueprintGuid.Parse(OtherModGuids.MageArmor2Spell)};
    private static readonly BlueprintAbilityReference MageArmor3Spell = new() {deserializedGuid = BlueprintGuid.Parse(OtherModGuids.MageArmor3Spell)};
    private static readonly BlueprintAbilityReference MageArmor4Spell = new() {deserializedGuid = BlueprintGuid.Parse(OtherModGuids.MageArmor4Spell)};

    private static void doThingy(AddAbilityUseTrigger component)
    {
        component.ForOneSpell = false;
        component.ForMultipleSpells = true;
        component.Abilities = [..component.Abilities, AbilityRefs.MageArmor.Reference.Get().ToReference<BlueprintAbilityReference>(),
            MageArmor2Spell, MageArmor3Spell, MageArmor4Spell];
    }

    private static readonly BlueprintBuffReference MageArmor2Buff = new() {deserializedGuid = BlueprintGuid.Parse(OtherModGuids.MageArmor2Buff)};
    private static readonly BlueprintBuffReference MageArmor3Buff = new() {deserializedGuid = BlueprintGuid.Parse(OtherModGuids.MageArmor3Buff)};
    private static readonly BlueprintBuffReference MageArmor4Buff = new() {deserializedGuid = BlueprintGuid.Parse(OtherModGuids.MageArmor4Buff)};

    private static void doOther(SuppressBuffs component)
    {
        component.m_Buffs = [.. component.m_Buffs, MageArmor2Buff, MageArmor3Buff, MageArmor4Buff];
    }
}