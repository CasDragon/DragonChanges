using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.NewItems.Scrolls;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewSpells
{
    internal class CommunalSenseVitals
    {
        // edit
        internal const string ability = "CommnualSenseVitals";
        internal const string abilityguid = Guids.CommnualSenseVitals;
        internal const string settingName = "communalsensevitals";
        internal const string settingDescription = "Adds a communal version of Sense Vitals";
        // don't edit
        [DragonLocalizedString(abilityname, "Sense Vitals, Communal")]
        internal const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "This {g|Encyclopedia:Spell}spell{/g} functions like Sense Vitals, except it affects all party members and it lasts for 4 hours.")]
        internal const string abilitydescription = $"{ability}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewAbilities, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{ability} item enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{ability} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static BlueprintAbility ConfigureDummy()
        {
            CommnualSenseVitalsScroll.ConfigureDummy();
            return AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintAbility ConfigureEnabled()
        {
            var crc = ContextRankConfigs.CasterLevel();
            crc.m_StartLevel = 3;
            crc.m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.AsIs;
            var x = AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .AddSpellComponent(Kingmaker.Blueprints.Classes.Spells.SpellSchool.Divination)
                .AddAbilityEffectRunAction(
                    ActionsBuilder.New()
                        .ApplyBuff(BuffRefs.SenseVitalsBuff.Reference.Get(),
                            new Kingmaker.UnitLogic.Mechanics.ContextDurationValue()
                            {
                                Rate = Kingmaker.UnitLogic.Mechanics.DurationRate.Hours,
                                DiceType = Kingmaker.RuleSystem.DiceType.Zero,
                                DiceCountValue = new Kingmaker.UnitLogic.Mechanics.ContextValue()
                                {
                                    ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None
                                },
                                BonusValue = new Kingmaker.UnitLogic.Mechanics.ContextValue()
                                {
                                    ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Simple,
                                    Value = 4,
                                    ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            isFromSpell: true)
                        .PartyMembers(ActionsBuilder.New()
                            .ApplyBuff(BuffRefs.SenseVitalsBuff.Reference.Get(),
                                new Kingmaker.UnitLogic.Mechanics.ContextDurationValue()
                                {
                                    Rate = Kingmaker.UnitLogic.Mechanics.DurationRate.Hours,
                                    DiceType = Kingmaker.RuleSystem.DiceType.Zero,
                                    DiceCountValue = new Kingmaker.UnitLogic.Mechanics.ContextValue()
                                    {
                                        ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Simple,
                                        Value = 0,
                                        ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                                        ValueShared = AbilitySharedValue.Damage,
                                        Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None
                                    },
                                    BonusValue = new Kingmaker.UnitLogic.Mechanics.ContextValue()
                                    {
                                        ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Simple,
                                        Value = 4,
                                        ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                                        ValueShared = AbilitySharedValue.Damage,
                                        Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None
                                    },
                                    m_IsExtendable = true
                                },
                                isFromSpell: true)))
                .AddToSpellList(level: 6, spellList: SpellListRefs.WizardSpellList.Reference.Get())
                .AddToSpellList(level: 6, spellList: SpellListRefs.BardSpellList.Reference.Get())
                .AddToSpellList(level: 6, spellList: SpellListRefs.RangerSpellList.Reference.Get())
                .AddToSpellList(level: 6, spellList: SpellListRefs.HunterSpelllist.Reference.Get())
                .AddContextRankConfig(crc)
                .AddAbilitySpawnFx(AbilitySpawnFxAnchor.SelectedTarget, orientationMode: AbilitySpawnFxOrientation.Copy, time: AbilitySpawnFxTime.OnApplyEffect, prefabLink: "c388856d0e8855f429a83ccba67944ba")
                .AddCraftInfoComponent(spellType: Kingmaker.Craft.CraftSpellType.Buff, savingThrow: Kingmaker.Craft.CraftSavingThrow.None, aOEType: Kingmaker.Craft.CraftAOE.AOE)
                .SetIcon(AbilityRefs.SenseVitals.Reference.Get().Icon)
                .SetType(AbilityType.Spell)
                .SetRange(AbilityRange.Touch)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(true)
                .SetCanTargetPoint(false)
                .SetCanTargetSelf(true)
                .SetSpellResistance(false)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                .SetAvailableMetamagic(Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal)
                .Configure();
            CommnualSenseVitalsScroll.ConfigureEnabled(x);
            return x;
        }
    }
}
