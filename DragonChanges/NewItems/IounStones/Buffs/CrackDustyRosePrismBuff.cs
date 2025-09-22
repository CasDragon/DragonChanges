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
    internal class CrackDustyRosePrismBuff
    {
        // edit
        internal const string buff = "CrackDustyRosePrismBuff";
        internal const string buffguid = Guids.CrackDustyRosePrismBuff;
        // don't edit
        [DragonLocalizedString(buffname, "Cracked Dusty Rose Prism(buff)")]
        internal const string buffname = $"{buff}.name";
        [DragonLocalizedString(buffdescription, "This stone grants a +1 competence bonus on initiative checks.")]
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
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.Competence,
                    stat: Kingmaker.EntitySystem.Stats.StatType.Initiative,
                    value: 1)
                .SetRanks(1)
                .Configure();
        }
    }
}
