using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace RiftArchipelago;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("RiftOfTheNecroDancer.exe")]
public class RiftAP : BaseUnityPlugin {
    public static ManualLogSource _log;
        
    private void Awake() {
        var harmony = new Harmony("Archipelago");
        
        // Plugin startup logic
        _log = Logger;
        _log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        harmony.PatchAll();
        _log.LogInfo($"Harmony patches applied!");
    }
}
