using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.Localization;
using Kingmaker.Modding;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.View;
using Kingmaker.Visual.Mounts;
using Owlcat.Runtime.Core.Utils;
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
