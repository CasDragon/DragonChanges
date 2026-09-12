using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Weapons;

namespace DragonChanges.Content;
internal class Thwarter
{
    const string settingName = "thwarter";
    const string settingDescription = "Enables the Thwarter tongi item";
    [DragonConfigure]
    [DragonSetting(SettingCategories.NewItems, settingName, settingDescription)]
    public static void Configure()
    {
        if (SettingsAction.GetSetting<bool>(settingName))
        {
            Main.log.Log($"{settingName} item enabled, configuring");
            ConfigureEnabled();
        }
        else
        {
            Main.log.Log($"{settingName} disabled, skipping");
        }
    }
    public static void ConfigureEnabled()
    {
        BlueprintItemWeapon tongi = ItemWeaponConfigurator.For(ItemWeaponRefs.JustifierTongiItem)
            .SetVisualParameters(ItemWeaponRefs.TongiKeenPlus1.Reference.Get().VisualParameters)
            .Configure();
        AneviaVendor.AddItem(tongi);
    }
}
