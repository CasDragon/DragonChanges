using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using DragonChanges.Utils;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonChanges.NewItems.IounStones.Buffs
{
    internal class CrimsonSphereBuff
    {
        // edit
        internal const string buff = "CrimsonSphereBuff";
        internal const string buffguid = Guids.CrimsonSphereBuff;
        // don't edit
        [DragonLocalizedString(buffname, "Crimson Sphere(buff)")]
        internal const string buffname = $"{buff}.name";
        [DragonLocalizedString(buffdescription, "This stone grants you a +2 enhancement bonus to Intelligence.")]
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
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.Enhancement,
                    stat: Kingmaker.EntitySystem.Stats.StatType.Intelligence,
                    value: 2)
                .SetRanks(5)
                .Configure();
        }
    }
}
