using System;
using Shared.PlayerData;
using Shared.TrackSelection;
using System.Reflection;
using System.Collections.Generic;
using BepInEx.Logging;

namespace RiftArchipelago{
    public static class ItemHandler {
        public static Dictionary<string, SongDatabaseData> songDatabaseDict;

        public static void Setup() {
            SongDatabase songDatabase = SongDatabase.Instance;

            // var type = typeof(SongDatabase);
            // var field = type.GetField("_songDatabaseDict", BindingFlags.NonPublic | BindingFlags.Instance);
            // var database = (Dictionary<string, SongDatabaseData>)field.GetValue(songDatabase);


            foreach(KeyValuePair<string, SongDatabaseData> song in songDatabaseDict) {
                foreach(DifficultyInformation diff in song.Value.DifficultyInformation) {
                    diff.UnlockCriteria.Type = UnlockCriteriaType.AlwaysLocked;
                }
            }
        }
    }
}