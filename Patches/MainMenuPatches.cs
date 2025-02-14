using UnityEngine;
using HarmonyLib;
using Shared.Title;
using BepInEx.Logging;

namespace RiftArchipelago.Patches{
    [HarmonyPatch(typeof(MainMenuManager), "start")]
    public static class APUIPatch {
        private static void CreateUI(MainMenuManager _instance) {
            if(ArchipelagoClient.apUI) return;
            
            var guiGameObject = new GameObject("AP");
            ArchipelagoClient.apUI = guiGameObject.AddComponent<ArchipelagoUI>();
            // if(_instance.GetComponent)
            Object.DontDestroyOnLoad(guiGameObject);
        }
    }
}