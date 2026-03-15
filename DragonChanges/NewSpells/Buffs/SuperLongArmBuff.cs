using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewSpells.Buffs
{
    internal class SuperLongArmBuff
    {
        // edit
        internal const string buff = "superlongarmbuff";
        internal const string buffguid = Guids.SuperLongArmBuff;
        internal const string settingName = "superlongarm";
        // don't edit
        [DragonLocalizedString(buffname, "Super Long Arm")]
        internal const string buffname = $"{buff}.name";
        [DragonLocalizedString(buffdescription, "Your arms temporarily grow in length, increasing your reach with those limbs by 15 feet.")]
        internal const string buffdescription = $"{buff}.description";
        public static void ConfigureDummy()
        {
            BuffConfigurator.New(buff, buffguid)
                .SetDisplayName(buffname)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintBuff ConfigureEnabled()
        {
            return BuffConfigurator.New(buff, buffguid)
                .SetDisplayName(buffname)
                .SetDescription(buffdescription)
                .AddStatBonus(descriptor: ModifierDescriptor.Enhancement, 
                    stat: StatType.Reach,
                    value: 15)
                //.SetIcon()
                .SetFlags(BlueprintBuff.Flags.IsFromSpell)
                .Configure();
        }
    }
}
