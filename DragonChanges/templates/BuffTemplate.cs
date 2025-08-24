using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using DragonChanges.Utils;

namespace DragonChanges.templates
{
    internal class BuffTemplate
    {
        // edit
        internal const string buff = "feature";
        internal const string buffguid = Guids.Template;
        internal const string settingName = "";
        internal const string settingDescription = "";
        // don't edit
        //[DragonLocalizedString(buffname, "")]
        internal const string buffname = $"{buff}.name";
        //[DragonLocalizedString(buffdescription, "")]
        internal const string buffdescription = $"{buff}.description";
        //[DragonConfigure]
        //[DragonSetting(settingCategories.NewAbilities, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{buff} feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{buff} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            BuffConfigurator.New(buff, buffguid)
                .SetDisplayName(buffname)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static void ConfigureEnabled()
        {
            BuffConfigurator.New(buff, buffguid)
                .SetDisplayName(buffname)
                .SetDescription(buffdescription)
                .Configure();
        }
    }
}
