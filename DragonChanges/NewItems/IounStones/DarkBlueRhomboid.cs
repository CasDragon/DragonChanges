using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewItems.IounStones.Abilities;
using DragonChanges.NewItems.IounStones.Buffs;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Equipment;

namespace DragonChanges.NewItems.IounStones
{
    internal class DarkBlueRhomboid
    {
        // edit
        internal const string item = "DarkBlueRhomboidItem";
        internal const string itemguid = Guids.DarkBlueRhomboidItem;
        internal const string settingName = "iounstones";
        // don't edit
        [DragonLocalizedString(itemname, "Dark Blue Rhomboid - Ioun Stone")]
        internal const string itemname = $"{item}.name";
        [DragonLocalizedString(itemdescription, "This stone grants the wearer the effects of the Alertness feat.")]
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
                DarkBlueRhomboidAbility.ConfigureDummy();
                DarkBlueRhomboidBuff.ConfigureDummy();
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
                .SetActivatableAbility(DarkBlueRhomboidAbility.ConfigureEnabled())
                .Configure();
            AneviaVendor.AddItem(itembp, 20);
        }
    }
}
