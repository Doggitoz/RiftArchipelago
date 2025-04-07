using UnityEngine;
using HarmonyLib;
using Shared.TrackSelection;
using TMPro;
using Shared.PlayerData;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(TrackSelectionSceneController), "Update")]
    public static class UpdateDiamondText {
        [HarmonyPostfix]
        public static void PostFix(ref TMP_Text ____totalDiamondsText) {
            if(!ArchipelagoClient.isAuthenticated) return;
            ____totalDiamondsText.text = $"x{ItemHandler.diamondCount} / {ArchipelagoClient.slotData.diamondGoal}";
        }
    }

    // [HarmonyPatch(typeof(BBSelectionSceneController), "Start")]
    // public static class UpdateBossUnlocks {
    //     [HarmonyPostfix]
    //     public static void PostFix(ref BBTrackMetaData[] ____trackMetaDatas) {
    //         for(int i = 0; i < ____trackMetaDatas.Length; i++) {
    //             BBTrackMetaData track = ____trackMetaDatas[i];
    //             track.IsLocked = true;
    //             track.UnlockCriteria.Type = UnlockCriteriaType.None;
    //         }
    //     }
    // }
}