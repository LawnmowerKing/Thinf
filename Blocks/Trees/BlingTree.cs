﻿using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Tiles.Trees
{
	public class BlingTree : ModTree
	{
		private Mod mod => ModLoader.GetMod("Thinf");

		public override int CreateDust()
		{
			return DustID.GoldCoin;
		}

		//public override int GrowthFXGore()
		//{
		//	return mod.GetGoreSlot("Gores/ExampleTreeFX");
		//}

		public override int DropWood()
		{
			return ItemID.SilverCoin;
		}

		public override Texture2D GetTexture()
		{
			return mod.GetTexture("Blocks/Trees/BlingTree");
		}

		public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
		{
			return mod.GetTexture("Blocks/Trees/BlingTree_Tops");
		}

		public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
		{
			return mod.GetTexture("Blocks/Trees/BlingTree_Branches");
		}
	}
}