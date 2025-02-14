using UnityEngine;
using HarmonyLib;
using Shared.Title;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(MainMenuManager), "Awake")]
    public static class APUIPatch {
        [HarmonyPostfix]
        public static void PostFix() {
            CreateUI();
        }

        private static void CreateUI() {
            if(ArchipelagoClient.apUI) return;
            
            var guiGameObject = new GameObject("AP");
            ArchipelagoClient.apUI = guiGameObject.AddComponent<ArchipelagoUI>();
            Object.DontDestroyOnLoad(guiGameObject);
        }
    }
}