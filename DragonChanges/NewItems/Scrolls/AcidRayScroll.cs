using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Enums;

namespace DragonChanges.NewItems.Scrolls;

public class AcidRayScroll
{
    // edit
    private const string item = "AcidRayScroll";
    private const string itemguid = Guids.AcidRayScroll;
    private const string settingName = "elementalray";
    [DragonConfigure(priority: ConfigurePriority.Last)]
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
        ItemEquipmentUsableConfigurator.New(item, itemguid).Configure();
    }
    public static void ConfigureEnabled()
    {
        var scrollconfig = ItemEquipmentUsableConfigurator.New(item, itemguid)
            .SetCost(1650)
            .SetWeight(0.2f)
            .SetDestructible(true)
            .SetShardItem(ItemRefs.PaperShardItem.Reference.Get())
            .SetInventoryPutSound("ScrollPut")
            .SetInventoryTakeSound("ScrollTake")
            .SetTrashLootTypes(TrashLootType.Scrolls | TrashLootType.Scrolls_RE)
            .SetAbility(Guids.AcidRay)
            .SetSpendCharges(true)
            .SetCharges(1)
            .SetCasterLevel(11)
            .SetSpellLevel(6)
            .SetType(UsableItemType.Scroll)
            .AddCopyScroll()
            .SetIcon(ItemEquipmentUsableRefs.ScrollOfHellfireRay.Reference.Get().Icon);
        var scroll = scrollconfig.Configure();
        AneviaVendor.AddItem(scroll, 99);
    }
}