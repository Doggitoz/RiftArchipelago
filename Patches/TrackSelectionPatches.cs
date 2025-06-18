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
}