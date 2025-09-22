using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonChanges.New_Archetypes.Juggernaut
{
    internal class Invincible
    {
        // edit
        internal const string feature = "Invincible";
        internal const string featureguid = Guids.JuggernautInvincibleFeature;
        internal const string featurename = "Invincible";
        internal const string featuredescription = "The Juggernaut's bodily consituation is so great, she can shrug off attacks.\nAt 2nd level, she gains DR/- equal to her CON bonus. This stacks with other sources of DR/-.";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintFeature ConfigureEnabled()
        {
            BlueprintFeature feat;
            if (ModCompat.tttbase)
            {
                feat = FeatureConfigurator.New(feature, featureguid)
                    .SetDisplayName(featurenamekey)
                    .SetDescription(featuredescriptionkey)
                    .AddRecalculateOnStatChange(stat: StatType.Constitution)
                    .AddTTTAddDamageResistancePhysical(value: ContextValues.Property(UnitProperty.StatBonusConstitution))
                    .SetIsClassFeature(true)
                    .Configure();
            }
            else
            {
                feat = FeatureConfigurator.New(feature, featureguid)
                    .SetDisplayName(featurenamekey)
                    .SetDescription(featuredescriptionkey)
                    .AddRecalculateOnStatChange(stat: StatType.Constitution)
                    .AddDRComponent(stackable: true, value: ContextValues.Property(UnitProperty.StatBonusConstitution), usePool: false)
                    .SetIsClassFeature(true)
                    .Configure();
            }
            return feat;
        }
    }
}
