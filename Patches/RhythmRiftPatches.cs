using HarmonyLib;
using RhythmRift;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(RRStageController), "HandlePlayerDefeat")]
    public static class SendDeathLink {
        [HarmonyPostfix]
        public static void PostFix() {
            if(ArchipelagoClient.deathLink != ArchipelagoClient.DeathLinkState.Off) {
                ArchipelagoClient.SendDeathLink();
            }
        }
    } 

    // This was throwing an error, so commenting out for now.
    // [HarmonyPatch(typeof(RRStageController), "Start")]
    // public static class RecieveDeathLink {
    //     [HarmonyPostfix]
    //     public static void PostFix(RRStageController __instance) {
    //         // ArchipelagoClient.stageController = __instance;
    //     }
    // } 
}