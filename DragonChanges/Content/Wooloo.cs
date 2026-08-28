using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Localization;
using UnityEngine;

namespace DragonChanges.Content
{
    internal class Wooloo
    {
        
        [DragonLocalizedString(unitkey, "Wooloo")]
        internal const string unitkey = "Wooloo.Name";
        [DragonLocalizedString(featurenamekey, "Pokemon Companion — Wooloo")]
        internal const string featurenamekey = "wooloofeature.name";
        [DragonLocalizedString(featuredescriptionkey, "It's a pokeman")]
        internal const string featuredescriptionkey = "wooloofeature.description";
        public static void FixWoolooLocalization()
        {
            var shared = ScriptableObject.CreateInstance<SharedStringAsset>();
            shared.String = LocalizationTool.GetString("Wooloo.Name");
            FeatureConfigurator.For(Guids.WoolooFeature)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .Configure();
            UnitConfigurator.For(Guids.WoolooUnit)
                .SetDisplayName("Wooloo.Name")
                .SetLocalizedName(shared)
                .Configure();
        }
        [DragonConfigure]
        public static void AddWoolooToSelections()
        {
            Main.log.Log("Enabling Wooloo companion");

            Main.log.Log("Patching various animal selections to include Wooloo");
            BlueprintFeature pet = BlueprintTool.Get<BlueprintFeature>(Guids.WoolooFeature);
            PetUtils.AddPetToAll(pet);
        }
    }
}
