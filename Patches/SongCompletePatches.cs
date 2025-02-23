using HarmonyLib;
using Shared;
using Shared.RhythmEngine;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(StageFlowUiController), "ShowResults")]
    public static class APLocationSend {
        [HarmonyPostfix]
        public static void PostFix(StageFlowUiController __instance, ref StageFlowUiController.StageContextInfo ____stageContextInfo, StageInputRecord stageInputRecord, 
                                   float trackProgressPercentage, bool didNotFinish, bool cheatsDetected) {
            if (!ArchipelagoClient.isAuthenticated) return;
            if(____stageContextInfo.IsDailyChallenge && ____stageContextInfo.IsChallenge) return;

            RiftAP._log.LogInfo($"Song Progress: {trackProgressPercentage}");
            
            if(trackProgressPercentage > 0) {
                RiftAP._log.LogInfo("Song Complete, sending checks!");
                long locId = -1;

                if(____stageContextInfo.StageDisplayName == ArchipelagoClient.slotData.goalSong) {
                    var statusUpdatePacket = new StatusUpdatePacket();
                    statusUpdatePacket.Status = ArchipelagoClientState.ClientGoal;
                    ArchipelagoClient.session.Socket.SendPacket(statusUpdatePacket);
                }
                else if(____stageContextInfo.IsCustomTrack) { // Custom Track Handling
                    
                }

                else if(____stageContextInfo.LetterGradeDefinitions.IsBossBattle) { // Boss Battle Handling

                }

                else {
                    locId = ArchipelagoClient.session.Locations.GetLocationIdFromName("Rift of the Necrodancer", ____stageContextInfo.StageDisplayName + "-0");
                }

                RiftAP._log.LogInfo($"Sending {____stageContextInfo.StageDisplayName} {locId}");

                if(locId != -1) {
                    ArchipelagoClient.session.Locations.CompleteLocationChecksAsync([locId, locId + 1]);
                }
            }
        }
    }
}