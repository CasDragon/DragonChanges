using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewItems.Scrolls;
using DragonChanges.NewSpells.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Utils.Types;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonChanges.NewSpells
{
    internal class SuperLongArm
    {
        // edit
        internal const string ability = "superlongarm";
        internal const string abilityguid = Guids.SuperLongArmSpell;
        internal const string settingName = "superlongarm";
        internal const string settingDescription = "Enables the Super Long Arm spell, a level 4 spell that gives 15ft reach";
        // don't edit
        [DragonLocalizedString(abilityname, "Super Long Arm")]
        internal const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "Your arms temporarily grow in length, increasing your reach with those limbs by 15 feet.")]
        internal const string abilitydescription = $"{ability}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewSpells, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{ability} spell enabled, configuring");
                var x = ConfigureEnabled();
                SuperLongArmScroll.ConfigureEnabled(x);
            }
            else
            {
                Main.log.Log($"{ability} disabled, configuring dummy");
                SuperLongArmBuff.ConfigureDummy();
                SuperLongArmScroll.ConfigureDummy();
                ConfigureDummy();
            }
        }
        public static BlueprintAbility ConfigureDummy()
        {
            return AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintAbility ConfigureEnabled()
        {
            var x = AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .AddSpellComponent(SpellSchool.Transmutation)
                .AddAbilityEffectRunAction(
                    ActionsBuilder.New()
                        .ApplyBuff(SuperLongArmBuff.ConfigureEnabled(),
                            ContextDuration.Variable(ContextValues.Property(UnitProperty.Level, true), 
                                DurationRate.Minutes, true),
                                isFromSpell: true))
                .AddToSpellList(4, SpellListRefs.AlchemistSpellList.ToString())
                .AddToSpellList(4, SpellListRefs.BloodragerSpellList.ToString())
                .AddToSpellList(4, SpellListRefs.MagusSpellList.ToString())
                .AddToSpellList(4, SpellListRefs.WizardSpellList.ToString())
                .AddToSpellList(4, SpellListRefs.WizardTransmutationSpellList.ToString())
                .AddToSpellList(4, SpellListRefs.WitchSpellList.ToString())
                .AddToSpellList(4, SpellListRefs.MagicDeceiverSpellList.ToString())
                .AddToSpellList(4, SpellListRefs.TricksterSpelllist.ToString())
                .AddCraftInfoComponent(spellType: Kingmaker.Craft.CraftSpellType.Buff, savingThrow: Kingmaker.Craft.CraftSavingThrow.None, aOEType: Kingmaker.Craft.CraftAOE.None)
                .SetLocalizedDuration(Duration.MinutePerLevel)
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite("Abilities.SuperLongArms.png"))
                .SetType(AbilityType.Spell)
                .SetRange(AbilityRange.Personal)
                .SetCanTargetEnemies(false)
                .SetCanTargetFriends(false)
                .SetCanTargetPoint(false)
                .SetCanTargetSelf(true)
                .SetSpellResistance(false)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Touch)
                .SetActionType(UnitCommand.CommandType.Standard)
                .SetAvailableMetamagic(Metamagic.Extend | Metamagic.Heighten | Metamagic.Quicken | Metamagic.CompletelyNormal)
                .Configure();
            RandomHelpers.AddToThassilonian(x, 4);
            return x;
        }
    }
}
