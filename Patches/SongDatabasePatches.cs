using HarmonyLib;
using Shared.TrackSelection;
using System.Collections.Generic;
using System;
using System.IO;
using Shared.DLC;
using MonoMod.Utils;
using Shared.TrackData;
using Shared.PlayerData;
using Shared;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(SongDatabase), "InitializeDictionary")]
    public static class APDatabase {
        [HarmonyPostfix]
        public static void PostFix(ref Dictionary<string, SongDatabaseData> ____songDatabaseDict) {
            ItemHandler.songDatabaseDict = ____songDatabaseDict;
        }
    }

    [HarmonyPatch(typeof(TrackSelectionSceneController), "GetTrackMetadataFromDatabase")]
    public static class GetDLCTracks {
        [HarmonyPrefix]
        public static void Prefix(ref Dictionary<string, ITrackMetadata> ____dynamicMetadataMap) {
            RiftAP._log.LogDebug("DynTracks: Test");
            foreach(LocalTrackMetadata song in ____dynamicMetadataMap.Values) {
                foreach(LocalTrackDifficulty diff in song.DifficultyInformation) {
                    diff.UnlockCriteria = new TrackUnlockCriteria();
                    diff.UnlockCriteria.Main = new UnlockCriteria();
                    if(!ItemHandler.dlcSongUnlocked.Contains(song.TrackName)) {
                        diff.UnlockCriteria.Main.Type = UnlockCriteriaType.AlwaysLocked;
                    }

                    diff.UnlockCriteria.Remix = new UnlockCriteria();
                    if(!ItemHandler.dlcSongUnlocked.Contains(song.TrackName)) {
                        diff.UnlockCriteria.Remix.Type = UnlockCriteriaType.AlwaysLocked;
                    }
                }
            }

            // string path = Directory.GetCurrentDirectory();
            // int i = 90;
            // foreach(ITrackMetadata song in ____dynamicMetadataMap.Values) {
            //     using(StreamWriter output = new StreamWriter(Path.Combine(path, "dlcdata.txt"), true)) {
            //         output.WriteLine($"{{\"{song.TrackName}\", \"{song.LevelId}\"}},");
            //     }

            //     using(StreamWriter output = new StreamWriter(Path.Combine(path, "dlcsongdata.txt"), true)) {
            //         output.WriteLine($"\"{song.TrackName}\": SongData({i}, \"{song.TrackName}\", \"{song.Counterpart}\", {song.GetDifficulty(Difficulty.Easy).Intensity}, {song.GetDifficulty(Difficulty.Medium).Intensity}, {song.GetDifficulty(Difficulty.Hard).Intensity}, {song.GetDifficulty(Difficulty.Impossible).Intensity}),");
            //     }
            //     i++;
            // }
        }
    }
}