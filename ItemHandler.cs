using System;
using Shared.PlayerData;
using Shared.TrackSelection;

namespace RiftArchipelago{
    public static class ItemHandler {
        private static SongDatabaseData[] songDatabaseDatas;

        public static void Setup() {
            songDatabaseDatas = SongDatabase.Instance.GetSongDatabaseDatas();

            foreach(SongDatabaseData song in songDatabaseDatas) {
                foreach(DifficultyInformation diff in song.DifficultyInformation) {
                    diff.UnlockCriteria.Type = UnlockCriteriaType.AlwaysLocked;
                }
            }
        }
    }
}