﻿using R2API;
using RoR2;
using RoR2.Items;

namespace Moonstorm.Starstorm2.Items
{
    public sealed class Fork : ItemBase
    {
        public override ItemDef ItemDef { get; } = SS2Assets.LoadAsset<ItemDef>("Fork");

        [ConfigurableField(ConfigDesc = "Bonus damage per fork. (1 = 100%)")]
        [TokenModifier("SS2_ITEM_FORK_DESC", StatTypes.Percentage, 0)]
        public static float damageBonus = 0.25f;

        public sealed class Behavior : BaseItemBodyBehavior, IBodyStatArgModifier
        {
            [ItemDefAssociation]
            private static ItemDef GetItemDef() => SS2Content.Items.Fork
                ;
            public void ModifyStatArguments(RecalculateStatsAPI.StatHookEventArgs args)
            {
                //Percentage of Total Damage behavior
                //works like Armor-Piercing Rounds but on all targets, defaults to 8%
                //Level 1 commando M1: 12 > 12.96
                //level 5 loader punch: 648 > 699.84
                //args.damageMultAdd += damageBonus * stack;

                //Percentage of Base Damage behavior
                //added a percentage of base damage before levels, defaults to 25%
                //level 1 commando m1: 12 > 15
                //level 5 loader punch 648 > 729
                args.baseDamageAdd += body.baseDamage * damageBonus * stack;

                //Flat addition to base damage behavior
                //adds a flat amount to base damage, scales with level, defaults to 1
                //Level 1 commando m1: 12 > 13
                //level 5 loader punch 648 > 702
                //args.baseDamageAdd += damageBonus * stack;

                //G- sorry for changing it without properly consulting the team
            }
        }
    }
}
