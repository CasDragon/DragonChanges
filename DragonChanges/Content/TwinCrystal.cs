using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Weapons;

namespace DragonChanges.Content
{
    internal class TwinCrystal
    {
        const string settingName = "twincrystals";
        const string settingDescription = "Enables the Twin Crystals double-sword item";
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
                //ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            //ItemConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintItemWeapon sword = ItemWeaponConfigurator.For(ItemWeaponRefs.TwinCrystalsItem)
                .SetVisualParameters(ItemWeaponRefs.HavocHarbingerItem.Reference.Get().VisualParameters)
                .SetCost(120000)
                .Configure();
            ItemWeaponConfigurator.For(ItemWeaponRefs.TwinCrystalsSecondItem)
                .SetVisualParameters(ItemWeaponRefs.HavocHarbingerItemSecond.Reference.Get().VisualParameters)
                .Configure();
            AneviaVendor.AddItem(sword);
        }
    }
}
