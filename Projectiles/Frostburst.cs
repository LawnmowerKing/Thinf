﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Thinf.Projectiles
{
	// The unique behaviors of Flamethrower projectiles are shown in this class.
	// Simply put, the projectile is actually not drawn and what the player sees is just dust spawning to give the look of a stream of flames.
	public class Frostburst : ModProjectile
	{
		private readonly string Thing = "technically im not copying from examplemod because im the one who pr'd this projectile";
		private readonly string Thing2 = "checkmate atheists";
		// Since the texture is useless and not drawn, we can reuse the vanilla texture
		public override string Texture => "Terraria/Projectile_" + ProjectileID.Flames;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ok how did you genuinely die from this"); // The English name of the projectile
		}

		public override void SetDefaults()
		{
			projectile.width = 6; // The width of projectile hitbox
			projectile.height = 6; // The height of projectile hitbox
			projectile.alpha = 255; // This makes the projectile invisible, only showing the dust.
			projectile.friendly = false; // Can the projectile deal damage to enemies?
			projectile.hostile = true; // Can the projectile deal damage to the player?
			projectile.penetrate = -1; // How many monsters the projectile can penetrate. Change this to make the flamethrower pierce more mobs.
			projectile.timeLeft = 180; // A short life time for this projectile to get the flamethrower effect.
			projectile.ignoreWater = false;
			projectile.tileCollide = false;
			projectile.extraUpdates = 2;
		}

		public override void AI()
		{
			/*if (projectile.wet)
			{
				projectile.Kill(); //This kills the projectile when touching water. However, since our projectile is a cursed flame, we will comment this so that it won't run it. If you want to test this, feel free to uncomment this.
			}*/
			// Using a timer, we scale the earliest spawned dust smaller than the rest.
			float dustScale = 1f;
			if (projectile.ai[0] == 0f)
				dustScale = 0.25f;
			else if (projectile.ai[0] == 1f)
				dustScale = 0.5f;
			else if (projectile.ai[0] == 2f)
				dustScale = 0.75f;

            for (int i = 0; i < 7; ++i)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Snow, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);

				// Some dust will be large, the others small and with gravity, to give visual variety.
				if (Main.rand.NextBool(3))
				{
					dust.noGravity = true;
					dust.scale *= 3f;
					dust.velocity.X *= 2f;
					dust.velocity.Y *= 2f;
				}

				dust.scale *= 1.5f;
				dust.velocity *= 1.2f;
				dust.scale *= dustScale;
			}
			projectile.ai[0] += 1f;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (!target.HasBuff(BuffID.Frostburn))
			{
				target.AddBuff(BuffID.Frozen, 120, false);
			}
			target.AddBuff(BuffID.Frostburn, 900, false);
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			// By using ModifyDamageHitbox, we can allow the flames to damage enemies in a larger area than normal without colliding with tiles.
			// Here we adjust the damage hitbox. We adjust the normal 6x6 hitbox and make it 66x66 while moving it left and up to keep it centered.
			int size = 30;
			hitbox.X -= size;
			hitbox.Y -= size;
			hitbox.Width += size * 2;
			hitbox.Height += size * 2;
		}
	}
}