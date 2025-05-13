using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.Content
{
    internal class TwinCrystal
    {
        const string settingName = "twincrystals";
        const string settingDescription = "Enables the Twin Crystals double-sword item";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewItems, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
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
                .Configure();
            ItemWeaponConfigurator.For(ItemWeaponRefs.TwinCrystalsSecondItem)
                .SetVisualParameters(ItemWeaponRefs.HavocHarbingerItemSecond.Reference.Get().VisualParameters)
                .Configure();
            AneviaVendor.AddItem(sword);
        }
    }
}
