using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewItems.Scrolls
{
    internal class SuperLongArmScroll
    {
        // edit
        internal const string item = "superlongarmsscroll";
        internal const string itemguid = Guids.SuperLongArmScroll;
        internal const string settingName = "superlongarm";
        public static void ConfigureDummy()
        {
            ItemEquipmentUsableConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled(BlueprintAbility ability)
        {
            BlueprintItemEquipmentUsable scroll = ItemEquipmentUsableConfigurator.New(item, itemguid)
                .SetCost(2650)
                .SetWeight(0.2f)
                .SetDestructible(true)
                .SetShardItem(ItemRefs.PaperShardItem.Reference.Get())
                .SetInventoryPutSound("ScrollPut")
                .SetInventoryTakeSound("ScrollTake")
                .SetTrashLootTypes(TrashLootType.Scrolls | TrashLootType.Scrolls_RE)
                .SetAbility(ability)
                .SetSpendCharges(true)
                .SetCharges(1)
                .SetCasterLevel(11)
                .SetSpellLevel(4)
                .SetType(UsableItemType.Scroll)
                .AddCopyScroll()
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite("Abilities.SuperLongArms.png"))
                .Configure();
            AneviaVendor.AddItem(scroll, 99);
        }
    }
}
