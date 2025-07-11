﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewItems.StuffForItems;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Equipment;

namespace DragonChanges.NewItems
{
    internal class MemeRing3
    {
        // edit
        internal const string item = "FTKMemeRing";
        internal const string itemguid = Guids.FTKMemeRing;
        internal const string itemsettingname = "memerings";
        // don't edit
        internal const string itemname = $"{item}.name";
        internal const string itemdescription = $"{item}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(itemsettingname))
            {
                Main.log.Log($"{item} item enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{item} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ItemEquipmentRingConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintItemEquipmentRing ring = ItemEquipmentRingConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetIcon(ItemEquipmentRingRefs.CopperRing.Reference.Get().Icon)
                .SetCost(35000)
                .SetWeight(1)
                .SetDestructible(false)
                .SetInventoryEquipSound("ArmorPlateEquip")
                .SetInventoryPutSound("RingPut")
                .SetInventoryTakeSound("RingTake")
                .SetCR(10)
                .AddFactToEquipmentWielder(FeatureRefs.CavalierForTheKing.Reference.Get())
                .Configure();
            AneviaVendor.AddItem(ring);
        }
    }
}
