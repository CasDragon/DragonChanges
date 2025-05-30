﻿using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewAbilities;
using DragonChanges.NewEnchantments;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Items.Shields;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.RuleSystem.Rules.Abilities;
using WrathScalingItemDCs.ScalingDC;

namespace DragonChanges.NewItems
{
    internal class StorvalsThunder
    {
        // edit
        internal const string item = "StorvalsThunder";
        internal const string itemguid = Guids.ThunderHammer;
        internal const string settingName = "storvalsset";
        internal const string settingDescription = "Enable the Storval's Thunder weapon and the Storval's Fang shield.";
        // don't edit
        internal const string itemname = item + ".name";
        internal const string itemdescription = item + ".description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewItems, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{item} item enabled, configuring");
                var x = StorvalsFang.ConfigureEnabled();
                var y = ConfigureEnabled(x);
                try
                {
                    ModCompat.AddEquipmentToScalingDC(y);
                }
                catch { }
                AneviaVendor.AddItem(y);
            }
            else
            {
                Main.log.Log($"{item} disabled, configuring dummy");
                StorvalsFang.ConfigureDummy();
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ItemWeaponConfigurator.New(item, itemguid).Configure();
            StorvalsThunderEnchant.ConfigureDummy();
            StorvalsThunderAbility.ConfigureDummy();
        }
        public static BlueprintItemWeapon ConfigureEnabled(BlueprintItemShield shield)
        {
            return ItemWeaponConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetCR(5)
                .SetType(WeaponTypeRefs.EarthBreaker.Reference.Get())
                .SetVisualParameters(ItemWeaponRefs.EarthBreakerPlus1.Reference.Get().VisualParameters)
                .SetSize(Kingmaker.Enums.Size.Medium)
                .SetEnchantments([WeaponEnchantmentRefs.Enhancement1.Reference.Get(),
                                  StorvalsThunderEnchant.ConfigureEnabled(shield)])
                .SetAbility(StorvalsThunderAbility.ConfigureEnabled())
                .SetSpendCharges(true)
                .SetDC(20)
                .SetCharges(3)
                .SetRestoreChargesOnRest(true)
                .SetCost(5000)
                .Configure();
        }
    }
}
