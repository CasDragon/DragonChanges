using System;
using System.Collections.Generic;
using System.Linq;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewItems.StuffForItems;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Equipment;

namespace DragonChanges.NewItems
{
    internal class RangedCleaveBracers
    {
        // edit
        internal const string item = "RangedCleaveBracers";
        internal const string itemguid = Guids.RangedCleaveBracers;
        internal const string settingName = "rangedcleavebracers";
        internal const string settingDescription = "Enable the Bracers of Cleaving Rafanat";
        // don't edit
        internal const string itemname = $"{item}.name";
        internal const string itemdescription = $"{item}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewItems, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
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
            ItemEquipmentWristConfigurator.New(item, itemguid).Configure();
            RangedCleaveBracersFeature.ConfigureDummy();
        }
        public static void ConfigureEnabled()
        {
            BlueprintItemEquipmentWrist bracer = ItemEquipmentWristConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetIcon(ItemEquipmentWristRefs.BracersOfArchery.Reference.Get().Icon)
                .SetCost(70000)
                .SetWeight(2)
                .SetDestructible(false)
                .SetInventoryEquipSound("ArmorPlateEquip")
                .SetInventoryPutSound("ArmorPlatePut")
                .SetInventoryTakeSound("ArmorPlateTake")
                .SetCR(10)
                .AddFactToEquipmentWielder(RangedCleaveBracersFeature.ConfigureEnabled())
                .SetEquipmentEntity(ItemEquipmentWristRefs.BracersOfArchery.Reference.Get().m_EquipmentEntity)
                .Configure();
            AneviaVendor.AddItem(bracer);
        }
    }
}
