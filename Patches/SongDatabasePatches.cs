using UnityEngine;
using HarmonyLib;
using Shared.Title;
using Shared.TrackSelection;
using System.Collections.Generic;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(SongDatabase), "InitializeDictionary")]
    public static class APDatabase {
        [HarmonyPostfix]
        public static void PostFix(ref Dictionary<string, SongDatabaseData> ____songDatabaseDict) {
            ItemHandler.songDatabaseDict = ____songDatabaseDict;
        }
    }
    
}