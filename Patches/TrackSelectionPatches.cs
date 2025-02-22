using UnityEngine;
using HarmonyLib;
using Shared.TrackSelection;
using TMPro;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(TrackSelectionSceneController), "Update")]
    public static class UpdateDiamondText {
        [HarmonyPostfix]
        public static void PostFix(ref TMP_Text ____totalDiamondsText) {
            ____totalDiamondsText.text = $"x{ItemHandler.diamondCount}";
            RiftAP._log.LogInfo($"Settings diamond text to {ItemHandler.diamondCount}");
        }
    }
}