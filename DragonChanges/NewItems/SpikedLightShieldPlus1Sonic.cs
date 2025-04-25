using BlueprintCore.Blueprints.Configurators.Items.Weapons;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewAbilities;
using DragonChanges.NewEnchantments;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewItems
{
    internal class SpikedLightShieldPlus1Sonic
    {
        // edit
        internal static string item = "SpikedLightShieldPlus1Sonic";
        internal static string itemguid = Guids.SpikedLightShieldSonic;
        // don't edit
        internal static string itemname = $"{item}.name";
        internal static string itemdescription = $"{item}.description";
        public static void ConfigureDummy()
        {
            ItemWeaponConfigurator.New(item, itemguid).Configure();
        }
        public static BlueprintItemWeapon ConfigureEnabled()
        {
            return ItemWeaponConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .SetType(WeaponTypeRefs.SpikedLightShield.Reference.Get())
                .SetSize(Kingmaker.Enums.Size.Medium)
                .SetEnchantments([WeaponEnchantmentRefs.Enhancement1.Reference.Get(),
                                  WeaponEnchantmentRefs.Thundering.Reference.Get()])
                .Configure();
        }
    }
}
