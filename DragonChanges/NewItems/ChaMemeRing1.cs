using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.New_Classes.Redditor;
using DragonChanges.NewItems.StuffForItems;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewItems
{
    internal class ChaMemeRing1
    {
        // edit
        internal const string item = "ChaMemeRingAC";
        internal const string itemguid = Guids.ChaMemeRingAC;
        internal const string itemsettingname = "memerings";
        internal const string itemsettingdescription = "Enables a bunch of meme rings";
        // don't edit
        internal const string itemname = $"{item}.name";
        internal const string itemdescription = $"{item}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewItems, itemsettingname, itemsettingdescription)]
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
            MemeRing1Feature.ConfigureDummy();
        }
        public static void ConfigureEnabled()
        {
            BlueprintItemEquipmentRing ring = ItemEquipmentRingConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetIcon(ItemEquipmentRingRefs.CopperRing.Reference.Get().Icon)
                .SetCost(50000)
                .SetWeight(1)
                .SetDestructible(false)
                .SetInventoryEquipSound("ArmorPlateEquip")
                .SetInventoryPutSound("RingPut")
                .SetInventoryTakeSound("RingTake")
                .SetCR(10)
                .AddFactToEquipmentWielder(MemeRing1Feature.ConfigureEnabled())
                .Configure();
            AneviaVendor.AddItem(ring);
        }
    }
}
