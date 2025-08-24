using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewItems.IounStones.Abilities;
using DragonChanges.NewItems.IounStones.Buffs;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Equipment;

namespace DragonChanges.NewItems.IounStones
{
    internal class AmberSpindle
    {
        // edit
        internal const string item = "AmberSpindleItem";
        internal const string itemguid = Guids.AmberSpindleItem;
        internal const string settingName = "iounstones";
        internal const string settingDescription = "Adds several Ioun Stones to the game and adds them to the Anevia vendor.";
        // don't edit
        [DragonLocalizedString(itemname, "Amber Spindle - Ioun Stone")]
        internal const string itemname = $"{item}.name";
        [DragonLocalizedString(itemdescription, "This stone grants you a +1 resistance bonus on saving throws. The bonuses from multiple amber spindles stack  (up to a maximum of a +5 resistance bonus to saving throws).")]
        internal const string itemdescription = $"{item}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewAbilities, settingName, settingDescription)]
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
                AmberSpindleAbility.ConfigureDummy();
                AmberSpindleBuff.ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ItemEquipmentUsableConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintItemEquipmentUsable itembp = ItemEquipmentUsableConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetIcon(ItemRefs.Moonstone.Reference.Get().Icon)
                .SetCost(10000)
                .SetWeight(0)
                .SetInventoryPutSound("CommonPut")
                .SetInventoryTakeSound("CommonTake")
                .SetInventoryEquipSound("CommonPut")
                .SetHideAbilityInfo(true)
                .SetType(UsableItemType.Other)
                .SetActivatableAbility(AmberSpindleAbility.ConfigureEnabled())
                .Configure();
            AneviaVendor.AddItem(itembp, 20);
        }
    }
}
