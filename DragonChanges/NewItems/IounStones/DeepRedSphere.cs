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
    internal class DeepRedSphere
    {
        // edit
        internal const string item = "DeepRedSphereItem";
        internal const string itemguid = Guids.DeepRedSphereItem;
        internal const string settingName = "iounstones";
        // don't edit
        [DragonLocalizedString(itemname, "Deep Red Sphere - Ioun Stone")]
        internal const string itemname = $"{item}.name";
        [DragonLocalizedString(itemdescription, "This stone grants you a +2 enhancement bonus to Dexterity. The bonuses from multiple crimson spheres stack (up to a maximum of a +10 enhancement bonus to Dexterity).")]
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
                DeepRedSphereAbility.ConfigureDummy();
                DeepRedSphereBuff.ConfigureDummy();
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
                .SetIcon(ItemRefs.Bloodstone.Reference.Get().Icon)
                .SetCost(24000)
                .SetWeight(0)
                .SetInventoryPutSound("CommonPut")
                .SetInventoryTakeSound("CommonTake")
                .SetInventoryEquipSound("CommonPut")
                .SetHideAbilityInfo(true)
                .SetType(UsableItemType.Other)
                .SetActivatableAbility(DeepRedSphereAbility.ConfigureEnabled())
                .Configure();
            AneviaVendor.AddItem(itembp, 20);
        }
    }
}
