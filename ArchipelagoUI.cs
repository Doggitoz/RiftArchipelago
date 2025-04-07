using UnityEngine;

namespace RiftArchipelago{
    public class ArchipelagoUI : MonoBehaviour {
        private void OnGUI() {
            Cursor.visible = false;

            // Yoinked directly from Corn Kidz randomizer, which was yoinked directly from Subnautica randomizer
            // https://github.com/Berserker66/ArchipelagoSubnauticaModSrc/blob/master/mod/Archipelago.cs
            if (ArchipelagoClient.session != null) {
                if (ArchipelagoClient.isAuthenticated) {
                    GUI.Label(new Rect(16, 16, 300, 20), ArchipelagoClient.GetVersion() + " Status: Connected");
                }
                else {
                    GUI.Label(new Rect(16, 16, 300, 20), ArchipelagoClient.GetVersion() + " Status: Authentication failed");
                }
            }
            else {
                GUI.Label(new Rect(16, 16, 300, 20), ArchipelagoClient.GetVersion() + " Status: Not Connected");
            }

            if ((ArchipelagoClient.session == null || !ArchipelagoClient.isAuthenticated) && ArchipelagoClient.state == APState.Menu ) {
                GUI.Label(new Rect(16, 36, 150, 20), "Host: ");
                GUI.Label(new Rect(16, 56, 150, 20), "Slot Name: ");
                GUI.Label(new Rect(16, 76, 150, 20), "Password: ");

                bool submit = Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return;
                ArchipelagoInfo info = ArchipelagoClient.apInfo;

                info.address = GUI.TextField(new Rect(150 + 16 + 8, 36, 150, 20), info.address);
                info.slot = GUI.TextField(new Rect(150 + 16 + 8, 56, 150, 20), info.slot);
                info.password = GUI.TextField(new Rect(150 + 16 + 8, 76, 150, 20), info.password);

                if (submit && Event.current.type == EventType.KeyDown) {
                    // The text fields have not consumed the event, which means they were not focused.
                    submit = false;
                }

                if ((GUI.Button(new Rect(16, 96, 100, 20), "Connect") || submit) && info.Valid) {
                    ArchipelagoClient.Connect();
                    if(ArchipelagoClient.isAuthenticated) {
                        ItemHandler.Setup();
                    }
                }
            }
            else if(ArchipelagoClient.state == APState.Menu && ArchipelagoClient.session != null) {
                GUI.Label(new Rect(16, 56, 150, 20), "Goal Song: " + ArchipelagoClient.slotData.goalSong);

                if(GUI.Button(new Rect(16, 36, 100, 20), "Disconnect")) {
                    ArchipelagoClient.Disconnect();
                }
            }
        }
    }
}