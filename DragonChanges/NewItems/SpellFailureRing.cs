using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewItems.StuffForItems;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Equipment;

namespace DragonChanges.NewItems
{
    internal class SpellFailureRing
    {
        // edit
        internal const string item = "SpellFailureRing";
        internal const string itemguid = Guids.SpellFailureRing;
        internal const string settingName = "memerings";
        // don't edit
        internal const string itemname = $"{item}.name";
        internal const string itemdescription = $"{item}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
            SpellFailureRingFeature.ConfigureDummy();
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
                .AddFactToEquipmentWielder(SpellFailureRingFeature.ConfigureEnabled())
                .AddFactToEquipmentWielder(SpellFailureRingFeature2.ConfigureEnabled())
                .Configure();
            AneviaVendor.AddItem(ring);
        }
    }
}
