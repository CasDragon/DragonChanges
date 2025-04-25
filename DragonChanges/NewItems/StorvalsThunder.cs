using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewAbilities;
using DragonChanges.NewEnchantments;
using DragonChanges.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Items.Shields;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.RuleSystem.Rules.Abilities;

namespace DragonChanges.NewItems
{
    [HarmonyPatch(typeof(IncreaseSpellDC))]
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
                var x = StorvalsFang.ConfigureEnabled();
                ConfigureEnabled(x);
            }
            else
            {
                Main.log.Log($"{item} disabled, configuring dummy");
                StorvalsFang.ConfigureDummy();
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ItemWeaponConfigurator.New(item, itemguid).Configure();
            StorvalsThunderEnchant.ConfigureDummy();
            StorvalsThunderAbility.ConfigureDummy();
        }
        public static void ConfigureEnabled(BlueprintItemShield shield)
        {
            ItemWeaponConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetCR(5)
                .SetType(WeaponTypeRefs.EarthBreaker.Reference.Get())
                .SetVisualParameters(ItemWeaponRefs.EarthBreakerPlus1.Reference.Get().VisualParameters)
                .SetSize(Kingmaker.Enums.Size.Medium)
                .SetEnchantments([WeaponEnchantmentRefs.Enhancement1.Reference.Get(),
                                  StorvalsThunderEnchant.ConfigureEnabled(shield)])
                .SetAbility(StorvalsThunderAbility.ConfigureEnabled())
                .SetSpendCharges(true)
                .SetCharges(3)
                .SetRestoreChargesOnRest(true)
                .SetDC(20)
                .SetCost(5000)
                .Configure();
        }
        [HarmonyPatch(nameof(IncreaseSpellDC.OnEventAboutToTrigger)), HarmonyPrefix]
        public static void Ihatelife(IncreaseSpellDC __instance, RuleCalculateAbilityParams evt)
        {
            Main.log.Log("debugging stupid dc thing - START -");
            Main.log.Log($"Spell/ability being cast is {evt.Spell.AssetGuid}");
            Main.log.Log($"Spell/ability being checked for is {__instance.Spell.AssetGuid}");
            Main.log.Log("debugging stupid dc thing - END -");
        }
    }
}
