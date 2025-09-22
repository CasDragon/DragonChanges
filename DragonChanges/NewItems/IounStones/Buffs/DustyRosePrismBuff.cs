using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonChanges.NewItems.IounStones.Buffs
{
    internal class DustyRosePrismBuff
    {
        // edit
        internal const string buff = "DustyRosePrismBuff";
        internal const string buffguid = Guids.DustyRosePrismBuff;
        // don't edit
        [DragonLocalizedString(buffname, "Dusty Rose Prism(buff)")]
        internal const string buffname = $"{buff}.name";
        [DragonLocalizedString(buffdescription, "This stone grants the wearer a +5 insight bonus to AC.")]
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
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.Insight,
                    stat: Kingmaker.EntitySystem.Stats.StatType.AC,
                    value: 5)
                .SetRanks(1)
                .Configure();
        }
    }
}
