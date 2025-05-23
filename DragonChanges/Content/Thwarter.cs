﻿using BlueprintCore.Blueprints.Configurators.Items;
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
    internal class Thwarter
    {
        const string settingName = "thwarter";
        const string settingDescription = "Enables the Thwarter tongi item";
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
            BlueprintItemWeapon tongi = ItemWeaponConfigurator.For(ItemWeaponRefs.JustifierTongiItem)
                .SetVisualParameters(ItemWeaponRefs.TongiKeenPlus1.Reference.Get().VisualParameters)
                .Configure();
            AneviaVendor.AddItem(tongi);
        }
    }
}
