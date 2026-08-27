using System.Runtime.Remoting.Contexts;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.MiscEx;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Experience;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Localization;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Interaction;
using UnityEngine;

namespace DragonChanges.NewStuff.VendorStuff;

public static class VendorUnit
{
    internal static bool isVendorSpawned = false;

    internal const string unitBPName = "dlcvender";
    [DragonLocalizedString(unitname, "PeePeePooPoo")]
    internal const string unitname = "dlcvendor.name";

    public static BlueprintUnit CreateVendorBlueprint(BlueprintSharedVendorTable loottable)
    {
        var aivu = TTTHelpers.CreateCopy<BlueprintUnit>(BlueprintTool.Get<BlueprintUnit>(Guids.WoolooUnit));
        var shared = ScriptableObject.CreateInstance<SharedStringAsset>();
        shared.String = LocalizationTool.GetString(unitname);
        var unit = UnitConfigurator.New(unitBPName, Guids.DLCVendorUnit)
            .CopyFrom(aivu, typeof(AddClassLevels))
            .SetLocalizedName(shared)
            .SetGender(aivu.Gender)
            .SetSize(aivu.Size)
            .SetAlignment(aivu.Alignment)
            .SetPortrait(aivu.PortraitSafe)
            .SetPrefab(aivu.Prefab)
            .SetVisual(aivu.Visual)
            .SetFaction(aivu.Faction)
            .SetBody(aivu.Body)
            .SetStrength(aivu.Strength)
            .SetIntelligence(aivu.Intelligence)
            .SetDexterity(aivu.Dexterity)
            .SetConstitution(aivu.Constitution)
            .SetWisdom(aivu.Wisdom)
            .SetCharisma(aivu.Charisma)
            .SetSpeed(aivu.Speed)
            .SetMaxHP(aivu.MaxHP)
            .AddSharedVendor(loottable)
            .AddActionsOnClick(new ActionsBuilder().StartTrade(new ClickedUnit()))
            .Configure();
        
        return unit;
    }
}