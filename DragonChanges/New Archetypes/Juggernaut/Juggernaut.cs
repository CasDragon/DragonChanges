using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.EntitySystem.Stats;

namespace DragonChanges.New_Archetypes.Juggernaut
{
    internal class Juggernaut
    {
        // edit
        internal const string archetype = "Juggernaut";
        internal const string archetypeguid = Guids.Juggernaut;
        internal const string settingName = "juggernaut";
        internal const string settingDescription = "Enables the Juggernaut archetype";
        internal const string archetypename = "Juggernaut";
        internal const string archetypedescription = "Juggernaut, DR focused Barbarian";
        internal const string archetypeshortdescription = "Juggernaut, DR focused Barbarian";

        // don't edit
        [DragonLocalizedString(archetypenamekey, archetypename)]
        internal const string archetypenamekey = $"{archetype}.name";
        [DragonLocalizedString(archetypedescriptionkey, archetypedescription, true)]
        internal const string archetypedescriptionkey = $"{archetype}.description";
        [DragonLocalizedString(archetypeshortdescriptionkey, archetypeshortdescription, true)]
        internal const string archetypeshortdescriptionkey = $"{archetype}.shortdescription";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewArchetypes, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{archetype} archetype enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{archetype} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ArchetypeConfigurator.New(archetype, archetypeguid)
                .SetLocalizedName(archetypenamekey)
                .SetLocalizedDescription(LocalizedStringHelper.disabledcontentstring)
                .SetLocalizedDescriptionShort(LocalizedStringHelper.disabledcontentstring)
                .Configure();
            Invincible.ConfigureDummy();
            JuggernautProficiencies.ConfigureDummy();
        }
        public static void ConfigureEnabled()
        {
            var removeFeatures = LevelEntryBuilder.New()
                .AddEntry(1, FeatureRefs.BarbarianProficiencies.Reference.Get())
                .AddEntry(2, FeatureRefs.UncannyDodgeChecker.Reference.Get())
                .AddEntry(5, FeatureRefs.ImprovedUncannyDodge.Reference.Get())
                .AddEntry(7, FeatureRefs.DamageReduction.Reference.Get())
                .AddEntry(10, FeatureRefs.DamageReduction.Reference.Get())
                .AddEntry(13, FeatureRefs.DamageReduction.Reference.Get())
                .AddEntry(16, FeatureRefs.DamageReduction.Reference.Get())
                .AddEntry(19, FeatureRefs.DamageReduction.Reference.Get())
                .GetEntries();
            var addFeatures = LevelEntryBuilder.New()
                .AddEntry(1, JuggernautProficiencies.ConfigureEnabled())
                .AddEntry(2, Invincible.ConfigureEnabled())
                .GetEntries();
            ArchetypeConfigurator.New(archetype, archetypeguid)
                .SetLocalizedName(archetypenamekey)
                .SetLocalizedDescription(archetypedescriptionkey)
                .SetLocalizedDescriptionShort(archetypeshortdescriptionkey)
                .SetAddFeatures(addFeatures)
                .SetRemoveFeatures(removeFeatures)
                .SetClass(CharacterClassRefs.BarbarianClass)
                .SetRecommendedAttributes([StatType.Strength, StatType.Constitution])
                .Configure();
        }
    }
}
