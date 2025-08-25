using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.NewItems.Scrolls;
using DragonChanges.Utils;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;

namespace DragonChanges.NewSpells
{
    internal class CommunalDeathWard
    {
        // edit
        internal const string ability = "CommunalDeathWard";
        internal const string abilityguid = Guids.CommunalDeathWard;
        internal const string settingName = "communaldeathward";
        internal const string settingDescription = "Adds a Communal version of Death Ward.";
        // don't edit
        [DragonLocalizedString(abilityname, "Death Ward, Communal")]
        internal const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "This {g|Encyclopedia:Spell}spell{/g} functions like Death Ward, except it affects all party members and it lasts for 4 hours.")]
        internal const string abilitydescription = $"{ability}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewSpells, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
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
            CommunalDeathWardScroll.ConfigureDummy();
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
                .AddSpellComponent(Kingmaker.Blueprints.Classes.Spells.SpellSchool.Necromancy)
                .AddAbilityEffectRunAction(
                    ActionsBuilder.New()
                        .ApplyBuff(BuffRefs.DeathWardBuff.Reference.Get(),
                            new Kingmaker.UnitLogic.Mechanics.ContextDurationValue()
                            {
                                Rate = Kingmaker.UnitLogic.Mechanics.DurationRate.Hours,
                                DiceType = Kingmaker.RuleSystem.DiceType.Zero,
                                DiceCountValue = new Kingmaker.UnitLogic.Mechanics.ContextValue()
                                {
                                    ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                                    ValueShared =  AbilitySharedValue.Damage,
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
                            .ApplyBuff(BuffRefs.DeathWardBuff.Reference.Get(),
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
                .AddAbilitySpawnFx(AbilitySpawnFxAnchor.SelectedTarget, orientationMode: AbilitySpawnFxOrientation.Copy, time: AbilitySpawnFxTime.OnApplyEffect, prefabLink: "cbfe312cb8e63e240a859efaad8e467c")
                .AddToSpellList(6, SpellListRefs.ClericSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.DruidSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.PaladinSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.AlchemistSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.InquisitorSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.ReposeDomainSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.KnowledgeDomainSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.WitchSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.SpiritWardenSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.LichWizardSpelllist.Reference.Get())
                .AddToSpellList(7, SpellListRefs.HunterSpelllist.Reference.Get())
                .AddToSpellList(7, SpellListRefs.AeonSpellList.Reference.Get())
                .AddToSpellList(7, SpellListRefs.MagicDeceiverSpellList.Reference.Get())
                .AddCraftInfoComponent(spellType: Kingmaker.Craft.CraftSpellType.Buff, savingThrow: Kingmaker.Craft.CraftSavingThrow.None, aOEType: Kingmaker.Craft.CraftAOE.AOE)
                .SetIcon(AbilityRefs.DeathWardCast.Reference.Get().Icon)
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
            CommunalDeathWardScroll.ConfigureEnabled(x);
            return x;
        }
    }
}
