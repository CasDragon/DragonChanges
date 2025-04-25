using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewAbilities;
using DragonChanges.NewEnchantments;
using DragonChanges.Utils;

namespace DragonChanges.NewItems
{
    internal class StorvalsThunder
    {
        // edit
        internal static string item = "StorvalsThunder";
        internal static string itemguid = Guids.ThunderHammer;
        // don't edit
        internal static string itemname = $"{item}.name";
        internal static string itemdescription = $"{item}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>(item.ToLower()))
            {
                Main.log.Log($"{item} item enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{item} disabled, configuring dummy");
                ConfigureDummy();
                StorvalsThunderEnchant.ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ItemWeaponConfigurator.New(item, itemguid).Configure();
            StorvalsThunderAbility.ConfigureDummy();
        }
        public static void ConfigureEnabled()
        {
            ItemWeaponConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetCR(5)
                .SetType(WeaponTypeRefs.EarthBreaker.Reference.Get())
                .SetVisualParameters(ItemWeaponRefs.EarthBreakerPlus1.Reference.Get().VisualParameters)
                .SetSize(Kingmaker.Enums.Size.Medium)
                .SetEnchantments([WeaponEnchantmentRefs.Enhancement1.Reference.Get(),
                                  StorvalsThunderEnchant.ConfigureEnabled()])
                .SetAbility(StorvalsThunderAbility.ConfigureEnabled())
                .SetSpendCharges(true)
                .SetCharges(3)
                .SetRestoreChargesOnRest(true)
                //.SetCasterLevel(20)
                .SetDC(20)
                .SetCost(5000)
                .Configure();
        }
    }
}
