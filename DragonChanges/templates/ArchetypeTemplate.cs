using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;

namespace DragonChanges.templates
{
    internal class ArchetypeTemplate
    {
        // edit
        internal const string archetype = "";
        internal const string archetypeguid = Guids.Template;
        internal const string settingName = "";
        internal const string settingDescription = "";
        internal const string archetypename = "";
        internal const string archetypedescription = "";
        internal const string archetypeshortdescription = "";

        // don't edit
        //[DragonLocalizedString(archetypenamekey, archetypename)]
        internal const string archetypenamekey = $"{archetype}.name";
        //[DragonLocalizedString(archetypedescriptionkey, archetypedescription, true)]
        internal const string archetypedescriptionkey = $"{archetype}.description";
        //[DragonLocalizedString(archetypedescriptionkey, archetypedescription, true)]
        internal const string archetypeshortdescriptionkey = $"{archetype}.shortdescription";
        //[DragonConfigure]
        //[DragonSetting(settingCategories.NewArchetypes, settingName, settingDescription)]
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
        }
        public static void ConfigureEnabled()
        {
            ArchetypeConfigurator.New(archetype, archetypeguid)
                .SetLocalizedName(archetypenamekey)
                .SetLocalizedDescription(archetypedescriptionkey)
                .SetLocalizedDescriptionShort(archetypeshortdescriptionkey)
                .Configure();
        }
    }
}
