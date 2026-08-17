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

    private static void doThingy(AddAbilityUseTrigger component)
    {
        component.ForOneSpell = false;
        component.ForMultipleSpells = true;
        component.Abilities = [..component.Abilities, AbilityRefs.MageArmor.Reference.Get().ToReference<BlueprintAbilityReference>(),
            DragonHelpers.MakeRef<BlueprintAbilityReference>("247d983c8ca04197bb2a1be55ce1d982"),
            DragonHelpers.MakeRef<BlueprintAbilityReference>("fdf0f15eab73419784f207ddcbdae4cf"),
            DragonHelpers.MakeRef<BlueprintAbilityReference>("3533d9e73bfa4d6e9bc44b5469d00f40")];
    }

    private static void doOther(SuppressBuffs component)
    {
        component.m_Buffs = [.. component.m_Buffs, 
            DragonHelpers.MakeRef<BlueprintBuffReference>("f59bea4023804838bd5c86608dd40081"),
            DragonHelpers.MakeRef<BlueprintBuffReference>("e9c795aea6c741a7b3e425c52aa2674c"),
            DragonHelpers.MakeRef<BlueprintBuffReference>("9052af53d26e469cb2fe8f7111dfe7d9")];
    }
}