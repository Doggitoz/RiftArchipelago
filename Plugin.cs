using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace RiftArchipelago;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("RiftOfTheNecroDancer.exe")]
public class Plugin : BaseUnityPlugin {
    internal static new ManualLogSource Logger;
        
    private void Awake() {
        var harmony = new Harmony("Archipelago");
        
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        harmony.PatchAll();
        Logger.LogInfo($"Harmony patches applied!");
    }
}
