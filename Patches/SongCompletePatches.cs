using HarmonyLib;
using Shared;
using System.Threading.Tasks;

using Shared.RhythmEngine;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using Shared.Leaderboard;
using Shared.TrackSelection;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(StageFlowUiController), "ShowResults")]
    public static class APLocationSend {
        [HarmonyPostfix]
        public static void PostFix(StageFlowUiController __instance, ref StageFlowUiController.StageContextInfo ____stageContextInfo, StageInputRecord stageInputRecord, 
                                   float trackProgressPercentage, bool didNotFinish, bool cheatsDetected, bool isRemixMode, string bossName) {
            if (!ArchipelagoClient.isAuthenticated) return;
            if(____stageContextInfo.IsDailyChallenge && ____stageContextInfo.IsChallenge) return;

            RiftAP._log.LogInfo($"Song Progress: {trackProgressPercentage}");
            
            if(trackProgressPercentage > 0 || ____stageContextInfo.LetterGradeDefinitions.IsBossBattle) {
                RiftAP._log.LogInfo("Song Complete, sending checks!");
                long locId = -1;

                if(____stageContextInfo.IsCustomTrack) { // Custom Track Handling
                    
                }

                else if(____stageContextInfo.LetterGradeDefinitions.IsBossBattle) { // Boss Battle Handling
                    RiftAP._log.LogInfo("Boss Battle Cleared");
                    if(ArchipelagoClient.slotData.bbMode == 1) { 
                        if(____stageContextInfo.StageDisplayName == ArchipelagoClient.slotData.goalSong) {
                            ArchipelagoClient.GoalGame();
                        }

                        locId = ArchipelagoClient.session.Locations.GetLocationIdFromName("Rift of the Necrodancer", ____stageContextInfo.StageDisplayName + "-0");
                    }
                    else if(ArchipelagoClient.slotData.bbMode == 2) {
                        if($"{____stageContextInfo.StageDisplayName} ({____stageContextInfo.StageDifficulty})" == ArchipelagoClient.slotData.goalSong) {
                            ArchipelagoClient.GoalGame();
                        }

                        locId = ArchipelagoClient.session.Locations.GetLocationIdFromName("Rift of the Necrodancer", $"{____stageContextInfo.StageDisplayName} ({____stageContextInfo.StageDifficulty})-0");
                    }
                }

                else if(____stageContextInfo.LetterGradeDefinitions.name == "LetterGradeDefinitionsMG") { // Minigame Handling
                    RiftAP._log.LogInfo("Minigame Cleared");
                    if(ArchipelagoClient.slotData.mgMode == 1) { 
                        if(____stageContextInfo.StageDisplayName == ArchipelagoClient.slotData.goalSong) {
                            ArchipelagoClient.GoalGame();
                        }

                        locId = ArchipelagoClient.session.Locations.GetLocationIdFromName("Rift of the Necrodancer", ____stageContextInfo.StageDisplayName + "-0");
                    }
                    else if(ArchipelagoClient.slotData.mgMode == 2) {
                        if($"{____stageContextInfo.StageDisplayName} ({____stageContextInfo.StageDifficulty})" == ArchipelagoClient.slotData.goalSong) {
                            ArchipelagoClient.GoalGame();
                        }

                        locId = ArchipelagoClient.session.Locations.GetLocationIdFromName("Rift of the Necrodancer", $"{____stageContextInfo.StageDisplayName} ({____stageContextInfo.StageDifficulty})-0");
                    }
                }

                else { // Vanilla song
                    RiftAP._log.LogInfo("Remix Mode: " + isRemixMode);
                    if(!ArchipelagoClient.slotData.remix || !isRemixMode) {
                        if(____stageContextInfo.StageDisplayName == ArchipelagoClient.slotData.goalSong) {
                            ArchipelagoClient.GoalGame();
                        }

                        locId = ArchipelagoClient.session.Locations.GetLocationIdFromName("Rift of the Necrodancer", ____stageContextInfo.StageDisplayName + "-0");
                    }
                    else {
                        if(____stageContextInfo.StageDisplayName + " (Remix)" == ArchipelagoClient.slotData.goalSong) {
                            ArchipelagoClient.GoalGame();
                        }

                        locId = ArchipelagoClient.session.Locations.GetLocationIdFromName("Rift of the Necrodancer", ____stageContextInfo.StageDisplayName + " (Remix)-0");
                    }
                }

                RiftAP._log.LogInfo($"Sending {____stageContextInfo.StageDisplayName} {locId}");

                if(locId != -1) {
                    ArchipelagoClient.session.Locations.CompleteLocationChecksAsync([locId, locId + 1]);
                }
            }
        }
    }

    [HarmonyPatch(typeof(LeaderboardDataAccessor), "UploadScoreToLeaderboard")]
    public static class LeaderboardUploadOverride {
        private static bool Prefix(out Task<bool> __result) {
            RiftAP._log.LogInfo($"Uploading Score: {!ArchipelagoClient.isAuthenticated}");
            __result = Task.FromResult(false);
            return !ArchipelagoClient.isAuthenticated;
        }
}
}