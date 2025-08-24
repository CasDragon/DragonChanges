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
    internal class AmberSpindleBuff
    {
        // edit
        internal const string buff = "AmberSpindleBuff";
        internal const string buffguid = Guids.AmberSpindleBuff;
        // don't edit
        [DragonLocalizedString(buffname, "Amber Spinder (buff)")]
        internal const string buffname = $"{buff}.name";
        [DragonLocalizedString(buffdescription, "This stone grants you a +1 resistance bonus on saving throws.")]
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
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.Resistance,
                    stat: Kingmaker.EntitySystem.Stats.StatType.SaveFortitude,
                    value: 1)
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.Resistance,
                    stat: Kingmaker.EntitySystem.Stats.StatType.SaveReflex,
                    value: 1)
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.Resistance,
                    stat: Kingmaker.EntitySystem.Stats.StatType.SaveWill,
                    value: 1)
                .SetRanks(5)
                .Configure();
        }
    }
}
