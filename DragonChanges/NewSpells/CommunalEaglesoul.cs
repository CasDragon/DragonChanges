using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
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
    internal class CommunalEaglesoul
    {
        // edit
        internal const string ability = "communaleaglesoul";
        internal const string abilityguid = Guids.communaleaglesoul;
        internal const string settingName = "communaleaglesoul";
        internal const string settingDescription = "Adds a Communal version of Eaglesoul.";
        // don't edit
        [DragonLocalizedString(abilityname, "Eaglesoul, Communal")]
        internal const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "This {g|Encyclopedia:Spell}spell{/g} functions like Eaglesoul, except it affects all party members and it lasts for 4 hours.")]
        internal const string abilitydescription = $"{ability}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewSpells, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{ability} enabled, configuring");
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
            CommunalEaglesoulScroll.ConfigureDummy();
            return AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintAbility ConfigureEnabled()
        {
            BlueprintAbility x = AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .AddSpellComponent(Kingmaker.Blueprints.Classes.Spells.SpellSchool.Conjuration)
                .AddAbilityEffectRunAction(
                    ActionsBuilder.New()
                        .ApplyBuff(BuffRefs.EaglesoulBuff.Reference.Get(),
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
                            .ApplyBuff(BuffRefs.EaglesoulBuff.Reference.Get(),
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
                .AddAbilitySpawnFx(AbilitySpawnFxAnchor.SelectedTarget, orientationMode: AbilitySpawnFxOrientation.Copy, time: AbilitySpawnFxTime.OnApplyEffect, prefabLink: "930c1a4aa129b8344a40c8c401d99a04")
                .AddToSpellList(9, SpellListRefs.ClericSpellList.Reference.Get())
                .AddToSpellList(9, SpellListRefs.AngelClericSpelllist.Reference.Get())
                .AddToSpellList(9, SpellListRefs.MagicDeceiverSpellList.Reference.Get())
                .AddCraftInfoComponent(spellType: Kingmaker.Craft.CraftSpellType.Buff, savingThrow: Kingmaker.Craft.CraftSavingThrow.None, aOEType: Kingmaker.Craft.CraftAOE.AOE)
                .SetIcon(AbilityRefs.Eaglesoul.Reference.Get().Icon)
                .SetType(AbilityType.Spell)
                .SetRange(AbilityRange.Personal)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(false)
                .SetCanTargetPoint(false)
                .SetCanTargetSelf(true)
                .SetSpellResistance(false)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                .SetAvailableMetamagic(Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal)
                .Configure();
            CommunalEaglesoulScroll.ConfigureEnabled(x);
            return x;
        }
    }
}
