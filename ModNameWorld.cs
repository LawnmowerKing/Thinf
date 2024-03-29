using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using Thinf.Blocks;
using Thinf.NPCs.Core;
using Thinf.NPCs.TownNPCs;

namespace Thinf
{
    public class ModNameWorld : ModWorld
    {
        public static bool raisinCutscene = false;
        public static bool downedMom = false;
        public static bool willSkipTalking = false;
        public static bool timeLoop = false;
        public static bool downedJerry = false;
        public static bool hasRejectedJerry = false;
        public static bool hasReceivedBait = false;
        public static bool screenShake = false;
        public static bool downedAcornSpirit = false;
        public static bool downedThundercock = false;
        public static bool downedCacterus = false;
        public static bool downedCortal = false;
        public static bool downedSoulKeys = false;
        public static bool downedSpudLord = false;
        public static bool downedHypnoKeeper = false;
        public static bool downedBeenado = false;
        public static bool downedSoulCatcher = false;
        public static bool downedHerbalgamation = false;
        public static bool downedFlashlight = false;
        public static bool downedPM = false;
        public static bool downedBlizzard = false;
        public static bool downedBill = false;
        public static bool downedWall;
        public static bool coreDestroyed = false;
        public static bool coreRestored = false;
        public static bool spawnOrespud = false;
        public static bool spawnOrecarrot = false;
        public static int ChestWasteland = 0;
        public static int TomatoTown = 0;
        public static bool DungeonArmyUp = false;
        public static bool downedDungeonArmy = false;
        public static int timesCoveredHoney = 0;
        public static bool FrenzyMode = false;

        public override void Initialize()
        {
            raisinCutscene = false;
            downedHypnoKeeper = false;
            willSkipTalking = false;
            downedMom = false;
            timeLoop = false;
            hasRejectedJerry = false;
            downedJerry = false;
            coreRestored = false;
            downedBlizzard = false;
            downedBill = false;
            downedFlashlight = false;
            downedPM = false;
            hasReceivedBait = false;
            downedAcornSpirit = false;
            downedWall = false;
            FrenzyMode = false;
            downedThundercock = false;
            downedCacterus = false;
            downedSoulCatcher = false;
            downedCortal = false;
            downedSoulKeys = false;
            downedSpudLord = false;
            downedBeenado = false;
            Main.invasionSize = 0;
            DungeonArmyUp = false;
            downedDungeonArmy = false;
            coreDestroyed = false;
        }
        public override void PostWorldGen()
        {
            NPC npc = Main.npc[NPC.NewNPC((Main.maxTilesX / 2) * 16, (Main.maxTilesY / 2) * 16, ModContent.NPCType<Core>(), 0, 0f, 0f, 0f, 0f, 255)];
        }
        public override void TileCountsAvailable(int[] tileCounts)
        {
            Mod magicStorage = ModLoader.GetMod("MagicStorage");
            if (magicStorage == null)
            {
                ChestWasteland = tileCounts[TileID.Containers];
            }
            else
            {
                ChestWasteland = tileCounts[TileID.Containers] + tileCounts[magicStorage.TileType("StorageUnit")] + tileCounts[magicStorage.TileType("StorageHeart")];
            }
            TomatoTown = tileCounts[ModContent.TileType<TomatoBlockTile>()];
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Beaches"));
            if (genIndex == -1)
            {
                return;
            }
            tasks.Insert(genIndex + 1, new PassLegacy("Tomato Generation", delegate (GenerationProgress progress)
            {
                progress.Message = "Generating an ungodly amount of tomatoes";
                for (int i = 0; i < Main.maxTilesX / 2000; i++)
                {

                    int X = WorldGen.genRand.Next(1, Main.dungeonX + 300);
                    int islandX = X + WorldGen.genRand.Next(-250, 250);
                    if (Main.dungeonX > Main.maxTilesX / 2)
                    {
                        X = Main.dungeonX + 100;
                    }

                    if (Main.dungeonX < Main.maxTilesX / 2)
                    {
                        X = Main.dungeonX - 100;
                    }
                    int Y = Main.dungeonY - 10;
                    int TileType = ModContent.TileType<TomatoBlockTile>();

                    Thinf.TomatoFloatingIsland(X, (int)(Main.worldSurface * 0.35f));
                    Thinf.TomatoIslandHouse(X, (int)(Main.worldSurface * 0.35f));
                    WorldGen.TileRunner(X, Y, 100, 35, TileType, false, 0f, 0f, true, true);
                    WorldGen.TileRunner(X + 20, Y, 100, 35, TileType, false, 0f, 0f, true, true);
                    WorldGen.TileRunner(X - 20, Y, 100, 35, TileType, false, 0f, 0f, true, true);

                }

            }));
        }
        //Save downed data
        public override void PreUpdate()
        {
            // Update everything about spawning the traveling merchant from the methods we have in the Traveling Merchant's class
            Carolla.UpdateTravelingMerchant();
        }
        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedBill) downed.Add("Bill");
            if (raisinCutscene) downed.Add("Raisin");
            if (downedHypnoKeeper) downed.Add("HypnoKeeper");
            if (willSkipTalking) downed.Add("MNCutscene");
            if (downedMom) downed.Add("Mom");
            if (timeLoop) downed.Add("Timeloop");
            if (hasRejectedJerry) downed.Add("Heresy");
            if (downedJerry) downed.Add("Jerry");
            if (downedFlashlight) downed.Add("Flashlight");
            if (downedBlizzard) downed.Add("Blizzard");
            if (coreDestroyed) downed.Add("Core");
            if (downedPM) downed.Add("PM");
            if (downedDungeonArmy) downed.Add("DungeonArmy");
            if (downedWall) downed.Add("WallOfFlesh");
            if (downedAcornSpirit) downed.Add("AcornSpirit");
            if (downedSoulCatcher) downed.Add("SoulCatcher");
            if (downedCacterus) downed.Add("Cacterus");
            if (downedThundercock) downed.Add("ThundercockandballTorture");
            if (downedCortal) downed.Add("Cortal");
            if (downedSoulKeys) downed.Add("SoulKeys");
            if (downedSpudLord) downed.Add("SpudLord");
            if (downedBeenado) downed.Add("Beenado");
            if (downedHerbalgamation) downed.Add("HerbBoss");
            return new TagCompound {
                {"downed", downed}
            };
        }

        //Load downed data
        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedBill = downed.Contains("Bill");
            raisinCutscene = downed.Contains("Raisin");
            downedHypnoKeeper = downed.Contains("HypnoKeeper");
            willSkipTalking = downed.Contains("MNCutscene");
            timeLoop = downed.Contains("Timeloop");
            hasRejectedJerry = downed.Contains("Heresy");
            downedJerry = downed.Contains("Jerry");
            downedFlashlight = downed.Contains("Flashlight");
            downedBlizzard = downed.Contains("Blizzard");
            downedPM = downed.Contains("PM");
            coreDestroyed = downed.Contains("Core");
            downedDungeonArmy = downed.Contains("DungeonArmy");
            downedWall = downed.Contains("WallOfFlesh");
            downedCacterus = downed.Contains("Cacterus");
            downedSoulCatcher = downed.Contains("SoulCatcher");
            downedThundercock = downed.Contains("ThundercockandballTorture");
            downedCortal = downed.Contains("Cortal");
            downedAcornSpirit = downed.Contains("AcornSpirit");
            downedSoulKeys = downed.Contains("SoulKeys");
            downedSpudLord = downed.Contains("SpudLord");
            downedBeenado = downed.Contains("Beenado");
            downedHerbalgamation = downed.Contains("HerbBoss");
            Carolla.Load(tag.GetCompound("traveler"));
        }

        //Sync downed data
        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedDungeonArmy;
            writer.Write(flags);
        }

        //Sync downed data
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedDungeonArmy = flags[0];
        }
        //Allow to update invasion while game is running
        public override void PostUpdate()
        {
            if (DungeonArmyUp)
            {
                if (Main.invasionX == Main.spawnTileX)
                {
                    //Checks progress and reports progress only if invasion at spawn
                    DungeonArmy.CheckCustomInvasionProgress();
                }
                //Updates the custom invasion while it heads to spawn point and ends it
                DungeonArmy.UpdateCustomInvasion();
            }
        }
    }
}
