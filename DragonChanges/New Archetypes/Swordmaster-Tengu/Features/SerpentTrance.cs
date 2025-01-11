using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.New_Archetypes.Swordmaster_Tengu.Features
{
    internal class SerpentTrance
    {
        // edit
        internal static string featureprefix = "swordmaster-tengu.cranetrance";
        internal static string featuretype = "feature";
        internal static string featureguid = Guids.CraneTranceFeature;
        // don't edit
        internal static string feature = $"{featureprefix}.{featuretype}";
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";

        public static BlueprintFeature ConfigureFeatureDummy()
        {
            ConfigureAbilityDummy();
            ConfigureBuffDummy();
            return FeatureConfigurator.New(feature, featureguid)
                .Configure();
        }
        public static BlueprintFeature ConfigureFeature()
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddFacts(new() { ConfigureAbility() })
                .SetIsClassFeature(true)
                .Configure();
        }
        // edit
        internal static string abilityprefix = "swordmaster-tengu.cranetrance";
        internal static string abilitytype = "ability";
        internal static string abilityguid = Guids.CraneTranceAbility;
        // don't edit
        internal static string ability = $"{abilityprefix}.{abilitytype}";
        internal static string abilityname = $"{ability}.name";
        internal static string abilitydescription = $"{ability}.description";

        public static BlueprintActivatableAbility ConfigureAbilityDummy()
        {
            return ActivatableAbilityConfigurator.New(ability, abilityguid)
                .Configure();
        }
        public static BlueprintActivatableAbility ConfigureAbility()
        {
            return ActivatableAbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .SetBuff(ConfigureBuff())
                .Configure();
        }
        // edit
        internal static string buffprefix = "swordmaster-tengu.cranetrance";
        internal static string bufftype = "buff";
        internal static string buffguid = Guids.CraneTranceBuff;
        // don't edit
        internal static string buff = $"{buffprefix}.{bufftype}";
        internal static string buffname = $"{buff}.name";
        internal static string buffdescription = $"{buff}.description";

        public static BlueprintBuff ConfigureBuffDummy()
        {
            return BuffConfigurator.New(buff, buffguid)
                .Configure();
        }
        public static BlueprintBuff ConfigureBuff()
        {
            return BuffConfigurator.New(buff, buffguid)
                .SetDisplayName(buffname)
                .SetDescription(buffdescription)
                .Configure();
        }
    }
}
