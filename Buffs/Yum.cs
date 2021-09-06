﻿using Thinf.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Thinf.Buffs
{
	public class Yum : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Yum!");
			Description.SetDefault("Better stat improvements than Well Fed");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 6;
			player.meleeDamage *= 1.15f;
			player.magicDamage *= 1.15f;
			player.rangedDamage *= 1.15f;
			player.meleeSpeed *= 1.2f;
			player.pickSpeed *= 1.12f;
			player.magicCrit += 5;
			player.meleeCrit += 5;
			player.rangedCrit += 5;
			player.minionDamage *= 1.3f;
			player.minionKB *= 1.1f;
		}
	}
}