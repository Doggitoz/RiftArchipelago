using System;
using Shared.PlayerData;
using Shared.TrackSelection;
using System.Reflection;
using System.Collections.Generic;
using BepInEx.Logging;

namespace RiftArchipelago{
    public static class ItemHandler {
        public static Dictionary<string, SongDatabaseData> songDatabaseDict;
        public static int diamondCount {get; private set;}

        public static void Setup() {
            diamondCount = 0;

            foreach(KeyValuePair<string, SongDatabaseData> song in songDatabaseDict) {
                foreach(DifficultyInformation diff in song.Value.DifficultyInformation) {
                    diff.UnlockCriteria.Type = UnlockCriteriaType.AlwaysLocked;
                }
            }
        }

        public static void AddDiamond() {
            diamondCount += 1;
        }

        public static void UnlockSong() {
            
        }
    }
}