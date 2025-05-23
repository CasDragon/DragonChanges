﻿using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Weapons;

namespace DragonChanges.NewItems
{
    internal class SpikedLightShieldPlus1Sonic
    {
        // edit
        internal const string item = "SpikedLightShieldPlus1Sonic";
        internal const string itemguid = Guids.SpikedLightShieldSonic;
        // don't edit
        internal const string itemname = item + ".name";
        internal const string itemdescription = item + ".description";
        public static void ConfigureDummy()
        {
            ItemWeaponConfigurator.New(item, itemguid).Configure();
        }
        public static BlueprintItemWeapon ConfigureEnabled()
        {
            return ItemWeaponConfigurator.New(item, itemguid)
                .CopyFrom(ItemWeaponRefs.MasterworkSpikedLightShield)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetEnchantments([WeaponEnchantmentRefs.Enhancement1.Reference.Get(),
                                  WeaponEnchantmentRefs.Thundering.Reference.Get()])
                .Configure();
        }
    }
}
