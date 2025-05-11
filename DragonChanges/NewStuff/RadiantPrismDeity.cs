using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.Alignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class RadiantPrismDeity
    {
        // edit
        internal const string feature = "LesbianPolyculeDeity";
        internal const string featureguid = Guids.LesbianPolyculeDeity;
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("newdeities"))
            {
                Main.log.Log($"{feature} feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{feature} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            AlignmentMaskType alignments = AlignmentMaskType.Good | AlignmentMaskType.TrueNeutral | AlignmentMaskType.NeutralGood |
                AlignmentMaskType.ChaoticGood | AlignmentMaskType.ChaoticNeutral;
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddPrerequisiteAlignment(group: Prerequisite.GroupType.All,
                    ingorePrerequisiteCheck: true,
                    alignment: alignments,
                    archetypeAlignment: false)
                .AddFeatureOnClassLevel(additionalClasses: [CharacterClassRefs.InquisitorClass.Reference.Get(), CharacterClassRefs.WarpriestClass.Reference.Get()],
                    clazz: CharacterClassRefs.ClericClass.Reference.Get(),
                    level: 1,
                    feature: FeatureRefs.GlaiveProficiency.Reference.Get())
                .AddFeatureOnClassLevel(additionalClasses: [CharacterClassRefs.InquisitorClass.Reference.Get(), CharacterClassRefs.WarpriestClass.Reference.Get()],
                    clazz: CharacterClassRefs.ClericClass.Reference.Get(),
                    level: 1,
                    feature: FeatureRefs.StarknifeProficiency.Reference.Get())
                .AddFeatureOnClassLevel(additionalClasses: [CharacterClassRefs.InquisitorClass.Reference.Get(), CharacterClassRefs.WarpriestClass.Reference.Get()],
                    clazz: CharacterClassRefs.ClericClass.Reference.Get(),
                    level: 1,
                    feature: FeatureRefs.ScimitarProficiency.Reference.Get())
                .AddFacts([FeatureRefs.GoodDomainAllowed.Reference.Get(), 
                            FeatureRefs.LuckDomainAllowed.Reference.Get(),
                            FeatureRefs.ChannelPositiveAllowed.Reference.Get(),
                            FeatureRefs.ProtectionDomainAllowed.Reference.Get(),
                            FeatureRefs.AirDomainAllowed.Reference.Get(),
                            FeatureRefs.ChaosDomainAllowed.Reference.Get(),
                            FeatureRefs.LiberationDomainAllowed.Reference.Get(),
                            FeatureRefs.TravelDomainAllowed.Reference.Get(),
                            FeatureRefs.HealingDomainAllowed.Reference.Get(),
                            FeatureRefs.FireDomainAllowed.Reference.Get(),
                            FeatureRefs.GloryDomainAllowed.Reference.Get(),
                            FeatureRefs.SunDomainAllowed.Reference.Get()])
                .AddForbidSpellbookOnAlignmentDeviation(alignments, ignoreFact: FeatureRefs.MythicIgnoreAlignmentRestrictions.Reference.Get(),
                    spellbooks: [SpellbookRefs.CrusaderSpellbook.Reference.Get(),
                        SpellbookRefs.ClericSpellbook.Reference.Get(),
                        SpellbookRefs.InquisitorSpellbook.Reference.Get()])
                .AddStartingEquipment(
                    basicItems: [ItemWeaponRefs.ColdIronGlaive.Reference.Get(),
                        ItemWeaponRefs.ColdIronStarknife.Reference.Get(),
                        ItemWeaponRefs.ColdIronScimitar.Reference.Get()],
                    restrictedByClass: [CharacterClassRefs.ClericClass.Reference.Get(),
                        CharacterClassRefs.InquisitorClass.Reference.Get(), 
                        CharacterClassRefs.WarpriestClass.Reference.Get()])
                .AddPrerequisiteNoArchetype(group: Prerequisite.GroupType.All,
                    characterClass: CharacterClassRefs.ClericClass.Reference.Get(),
                    archetype: ArchetypeRefs.PriestOfBalanceArchetype.Reference.Get())
                .AddPrerequisiteNoArchetype(group: Prerequisite.GroupType.All,
                    characterClass: CharacterClassRefs.WitchClass.Reference.Get(),
                    archetype: ArchetypeRefs.DarkSisterArchetype.Reference.Get())
                .AddPrerequisiteNoArchetype(group: Prerequisite.GroupType.All,
                    characterClass: CharacterClassRefs.WarpriestClass.Reference.Get(),
                    archetype: ArchetypeRefs.FeralChampionArchetype.Reference.Get())
                .AddPrerequisiteNoArchetype(group: Prerequisite.GroupType.All,
                    characterClass: CharacterClassRefs.ShamanClass.Reference.Get(),
                    archetype: ArchetypeRefs.ProphetOfPestilenceArchetype.Reference.Get())
                .AddPrerequisiteNoArchetype(group: Prerequisite.GroupType.All,
                    characterClass: CharacterClassRefs.WarpriestClass.Reference.Get(),
                    archetype: ArchetypeRefs.MantisZealotArchetype.Reference.Get())
                .AddPrerequisiteNoArchetype(group: Prerequisite.GroupType.All,
                    characterClass: CharacterClassRefs.SlayerClass.Reference.Get(),
                    archetype: ArchetypeRefs.BloodseekerArchetype.Reference.Get())
                .AddPrerequisiteNoFeature(ProgressionRefs.MagicDeceiverWayOfLivingGodProgression.Reference.Get())
                .AddPrerequisiteNoFeature(ProgressionRefs.MagicDeceiverWayOfRazmiriInfiltratorProgression.Reference.Get())
                .AddFeatureIfHasFact(checkedFact: FeatureRefs.WarpriestDeitySacredWeaponFeature.Reference.Get(),
                    feature: FeatureRefs.ShelynSacredWeaponFeature.Reference.Get())
                .AddFeatureIfHasFact(checkedFact: FeatureRefs.WarpriestDeitySacredWeaponFeature.Reference.Get(),
                    feature: FeatureRefs.DesnaSacredWeaponFeature.Reference.Get())
                .AddFeatureIfHasFact(checkedFact: FeatureRefs.WarpriestDeitySacredWeaponFeature.Reference.Get(),
                    feature: FeatureRefs.SarenraeSacredWeaponFeature.Reference.Get())
                .AddFacts([DeityNonsense.SeparatistAllowed.AirDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.AnimalDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.ArtificeDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.CharmDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.ChaosDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.CommunityDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.DarknessDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.DeathDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.DestructionDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.EarthDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.FireDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.GloryDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.HealingDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.IceDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.KnowledgeDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.LawDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.LiberationDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.MadnessDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.MagicDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.NobilityDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.PlantDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.ProtectionDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.ReposeDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.RuneDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.StrengthDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.SunDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.TravelDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.TrickeryDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.UndeadDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.MurderDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.WarDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.WaterDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.WeatherDomainAllowedSeparatist,
                    DeityNonsense.SeparatistAllowed.ScalykindDomainAllowedSeparatist])
                .SetGroups(Kingmaker.Blueprints.Classes.FeatureGroup.Deities)
                .SetRanks(1)
                .SetIsClassFeature(true)
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite("RadiantPrism.png"))
                .Configure();
        }
    }
}
