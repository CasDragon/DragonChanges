using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonChanges.Content
{
    internal class AlterMod
    {
        const string DJSettingName = "mc-deadly-juggernaut-dr";
        const string DJSettingDescription = "Allow Deadly Juggernaut spell to have stacking DR.";
        [DragonConfigure]
        [DragonSetting(SettingCategories.ModCompatability, DJSettingName, DJSettingDescription, false)]
        public static void PatchDeadlyJuggernaut()
        {

            if (SettingsAction.GetSetting<bool>(DJSettingName))
            {
                if (ModCompat.microscopic)
                {
                    try
                    {
                        Main.log.Log("Patching Alter's Deadly Juggernaut spell to allow DR stacking");
                        BlueprintBuff buff = BlueprintTool.Get<BlueprintBuff>(OtherModGuids.DeadlyJuggernautStatBonusBuff);
                        DragonHelpers.RemoveComponent<AddDamageResistancePhysical>(buff);
                        var x = BuffConfigurator.For(buff);
                        if (ModCompat.tttbase)
                        {
                            try
                            {
                                x.AddTTTAddDamageResistancePhysical(ContextValues.Shared(Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Heal));
                            }
                            catch
                            {
                                x.AddDRComponent(stackable: true, value: ContextValues.Shared(Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Heal));
                            }
                        }
                        else
                        {
                            x.AddDRComponent(stackable: true, value: ContextValues.Shared(Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Heal));
                        }
                        x.Configure();
                    }
                    catch
                    {
                        Main.log.Log("Error patching Deadly Juggernaut, skipping");
                    }
                }
            }
        }

        const string settingName = "mc-microscopic-horse";
        const string settingDescription = "Adds the Nightmare animal companion (MicroscopicContent) to other pet lists";
        [DragonConfigure]
        [DragonSetting(SettingCategories.ModCompatability, settingName, settingDescription)]
        public static void PatchHorse()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                if (ModCompat.microscopic)
                {
                    try
                    {
                        Main.log.Log("Patching various animal selections to include Nightmare horse (Microscopic)");
                        BlueprintFeature nightmare = BlueprintTool.Get<BlueprintFeature>(OtherModGuids.AnimalCompanionFeatureNightmare);
                        PetUtils.AddPetToAll(nightmare);
                    }
                    catch
                    {
                        Main.log.Log("Error adding Nightmare mount to selections");
                    }
                }
                else
                {
                    Main.log.Log("Nightmare patch (Microscopic) is enabled but Microscopic isn't detected, skipping patch");
                }
            }
        }
    }
}
