using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Weapons;

namespace DragonChanges.Content
{
    internal class SerpentPrince
    {
        const string settingName = "serpentprice";
        const string settingDescription = "Enables the Serpent Prince fauchard item";
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
            BlueprintItemWeapon sword = ItemWeaponConfigurator.For(ItemWeaponRefs.SerpentPrinceFauchItem)
                //.SetCost(100000)
                .Configure();
            AneviaVendor.AddItem(sword);
        }
    }
}
