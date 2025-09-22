using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.Items;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.templates
{
    internal class ItemTemplate
    {
        // edit
        internal const string item = "item";
        internal const string itemguid = Guids.Template;
        internal const string settingName = "";
        internal const string settingDescription = "";
        internal const string itemname = "";
        internal const string itemdescription = "";
        // don't edit
        //[DragonLocalizedString(itemnamekey, itemname)]
        internal const string itemnamekey = $"{item}.name";
        //[DragonLocalizedString(itemdescriptioney, itemdescription)]
        internal const string itemdescriptionkey = $"{item}.description";
        //[DragonConfigure]
        //[DragonSetting(SettingCategories.NewAbilities, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{item} item enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{item} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ItemConfigurator.New(item, itemguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            ItemConfigurator.New(item, itemguid)
                .SetDisplayNameText(itemname)
                .SetDescriptionText(itemdescription)
                .Configure();
        }
    }
}
