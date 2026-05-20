using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.New_Archetypes
{
    internal class CoolerDrakeRider
    {
        // edit
        internal const string archetype = "DrakeboundRider";
        internal const string archetypeguid = Guids.drakecavarchetype;
        internal const string settingName = "drakecavarchetype";
        internal const string settingDescription = "New archetype for a Cavalier, Cooler Drake Rider. Requires Expanded Content mod";
        internal const string archetypename = "Drakebound Rider";
        internal const string archetypedescription = "The Drakebound Rider lives the dream of many Cavaliers, riding a drake into battle.";
        internal const string archetypeshortdescription = "The Drakebound Rider lives the dream of many Cavaliers, riding a drake into battle.";

        // don't edit
        [DragonLocalizedString(archetypenamekey, archetypename)]
        internal const string archetypenamekey = $"{archetype}.name";
        [DragonLocalizedString(archetypedescriptionkey, archetypedescription, true)]
        internal const string archetypedescriptionkey = $"{archetype}.description";
        [DragonLocalizedString(archetypeshortdescriptionkey, archetypeshortdescription, true)]
        internal const string archetypeshortdescriptionkey = $"{archetype}.shortdescription";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewArchetypes, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                if (!ModCompat.expandedcontent)
                {
                    Main.log.Log($"{archetype} archetype enabled but Expanded Content isn't found, configuring dummy");
                    ConfigureDummy();
                    return;
                }
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
        }
        public static void ConfigureEnabled()
        {
            LevelEntry[] toRemove = LevelEntryBuilder.New()
                .AddEntry(1, [FeatureSelectionRefs.CavalierMountSelection.ToString()])
                .AddEntry(5, [FeatureRefs.CavalierBanner.ToString()])
                .AddEntry(14, [FeatureRefs.CavalierBannerGreater.ToString()])
                .GetEntries();
            LevelEntry[] toAdd = LevelEntryBuilder.New()
                .AddEntry(1, ["e7817c7d813e46a4959f35cfa5d1c53d", "2f74db96593042cdb3d0b5272fac8410"]) // DrakeRiderBondFeature, DrakeRiderMountTrainingFeature
                .GetEntries();
            ArchetypeConfigurator.New(archetype, archetypeguid)
                .SetLocalizedName(archetypenamekey)
                .SetLocalizedDescription(archetypedescriptionkey)
                .SetLocalizedDescriptionShort(archetypeshortdescriptionkey)
                .AddToRemoveFeatures(toRemove)
                .SetAddFeatures(toAdd)
                .SetClass(CharacterClassRefs.CavalierClass)
                .Configure();
        }
    }
}
