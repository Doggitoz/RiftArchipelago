using System;
using Shared.PlayerData;
using Shared.TrackSelection;
using System.Reflection;
using System.Collections.Generic;
using BepInEx.Logging;
using HarmonyLib;
using Shared.TrackData;
using Shared;

namespace RiftArchipelago{
    public static class ItemHandler {
        private static Dictionary<string, string> songMapping = new Dictionary<string, string>() {
            {"Disco Disaster", "RRDiscoDisaster"} ,
            {"Elusional", "RRElusional"},
            {"Visualize Yourself", "RRVisualizeYourself"},
            {"Spookhouse Pop", "RRSpookhousePop"},
            {"Om and On", "RROmandOn"},
            {"Morning Dove", "RRMorningDove"},
            {"Heph's Mess", "RRHephsMess"},
            {"Amalgamaniac", "RRAmalgamaniac"},
            {"Hang Ten Heph", "RRHangTenHeph"},
            {"Count Funkula", "RRCountFunkula"},
            {"Overthinker", "RROverthinker"},
            {"Cryp2que", "RRCryp2que"},
            {"Nocturning", "RRNocturning"},
            {"Glass Cages (feat. Sarah Hubbard)", "RRGlassCages"},
            {"Hallow Queen", "RRHallowQueen"},
            {"Progenitor", "RRProgenitor"},
            {"Matriarch", "RRMatriarch"},
            {"Under the Thunder", "RRThunder"},
            {"Eldritch House", "RREldritchHouse"},
            {"RAVEVENGE (feat. Aram Zero)", "RRRavevenge"},
            {"Rift Within", "RRRiftWithin"},
            {"Suzu's Quest", "RRSuzusQuest"},
            {"Necropolis", "RRNecropolis"},
            {"Baboosh", "RRBaboosh"},
            {"Necro Sonatica", "RRNecroSonatica"},
            {"She Banned", "RRHarmonie"},
            {"King's Ruse", "RRDeepBlues"},
            {"What's in the Box", "RRMatron"},
            {"Brave the Harvester", "RRReaper"},
            {"Final Fugue", "RRFinalFugue"},
            {"Twombtorial", "RRTwombtorial"},
            {"Portamello", "RRPortamello"},
            {"Slugger's Refrain", "DLCApricot01"},
            {"Got Danged", "DLCApricot02"},
            {"Bootus Bleez", "DLCApricot03"},
            {"Resurrections (dannyBstyle Remix)", "DLCBanana01"},
            {"Scattered and Lost", "DLCBanana02"},
            {"Reach for the Summit", "DLCBanana03"},
            {"Confronting Myself", "DLCBanana04"},
            {"Resurrections", "DLCBanana05"},
            {"It's Pizza Time!", "DLCCherry01"},
            {"The Death That I Deservioli", "DLCCherry02"},
            {"Unexpectancy, Pt. 3", "DLCCherry03"},
            {"World Wide Noise", "DLCCherry04"},
            {"Pizza Tower", "DLCCherryPromo"},
            {"Too Real", "DLCKiwi01"},
            {"M@GICAL☆CURE! LOVE ♥ SHOT!", "DLCKiwi02"},
            {"Intergalactic Bound", "DLCKiwi03"},
            {"Just 1dB Louder", "DLCKiwi04"},
            {"MikuFiesta", "DLCKiwi05"},
            {"Radiant Revival", "DLCKiwi06"},
            {"Hatsune Miku", "DLCKiwiPromo"},
            {"Too Real", "DLCKiwiPromo01"},
            {"Crypteque", "DLCOG02"},
            {"Power Cords", "DLCOG06"},
            {"Fungal Funk", "DLCOG07"},
        };

        public static Dictionary<string, SongDatabaseData> songDatabaseDict;
        public static List<string> dlcSongUnlocked = [];
        public static bool databaseInit = false;
        public static bool dlcDatabaseInit = false;
        public static int diamondCount {get; private set;}

        public static void Setup() {
            diamondCount = 0;
            RiftAP._log.LogInfo($"Setting up song Dict");

            foreach(SongDatabaseData song in songDatabaseDict.Values) {
                foreach(DifficultyInformation diff in song.DifficultyInformation) {
                    diff.UnlockCriteria.Type = UnlockCriteriaType.AlwaysLocked;
                    diff.RemixUnlockCriteria.Type = UnlockCriteriaType.AlwaysLocked;
                }
            }
        }

        public static void AddDiamond() {
            diamondCount += 1;
            RiftAP._log.LogInfo($"Adding Diamond | New Total: {diamondCount}");

            if(diamondCount >= ArchipelagoClient.slotData.diamondGoal) {
                UnlockSong(ArchipelagoClient.slotData.goalSong);
            }
        }

        public static void UnlockSong(string songName) {
            songMapping.TryGetValue(songName, out var levelName);

            if(songDatabaseDict.TryGetValue(levelName, out var value)) {
                RiftAP._log.LogInfo($"Unlocking \"{songName}\"");
        
                foreach(DifficultyInformation diff in value.DifficultyInformation) {
                    diff.UnlockCriteria.Type = UnlockCriteriaType.None;
                    diff.RemixUnlockCriteria.Type = UnlockCriteriaType.None;
                }
            }

            else if(songMapping.TryGetValue(levelName, out var value2)) {
                RiftAP._log.LogInfo($"Unlocking \"{songName}\" (Post PT DLC Song)");
                dlcSongUnlocked.Add(value2);
            }

            else {
                RiftAP._log.LogInfo($"Song \"{songName}\" could not be found");
            }
        }
    }
}