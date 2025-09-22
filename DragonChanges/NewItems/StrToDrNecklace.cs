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
    internal class StrToDrNecklace
    {
        // edit
        internal const string item = "Str2DRNecklace";
        internal const string itemguid = Guids.StrToDRNecklace;
        internal const string settingName = "str2drnecklace";
        internal const string settingDescription = "Adds a necklace that gives the user STR bonus to DR";
        // don't edit
        internal const string itemname = $"{item}.name";
        internal const string itemdescription = $"{item}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewItems, settingName, settingDescription)]
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
                StrDRNecklaceFeature.ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ItemEquipmentNeckConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintItemEquipmentNeck neck = ItemEquipmentNeckConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetIcon(ItemEquipmentNeckRefs.AmuletofArodenItem.Reference.Get().Icon)
                .SetCost(50000)
                .SetWeight(1)
                .SetDestructible(false)
                .SetInventoryPutSound("RingPut")
                .SetInventoryTakeSound("RingTake")
                .AddFactToEquipmentWielder(StrDRNecklaceFeature.ConfigureEnabled())
                .Configure();
            AneviaVendor.AddItem(neck);
        }
    }
}
