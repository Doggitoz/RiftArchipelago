using UnityEngine;

namespace RiftArchipelago{
    public class ArchipelagoUI : MonoBehaviour {
        // Minimize state persisted between sessions
        private const string MinimizedPrefKey = "RiftArchipelago_UI_Minimized";
        private static bool isMinimized;

        private const int PanelX = 8;
        private const int PanelY = 8;
        private const int PanelWidth = 320;
        private const int PanelPadding = 8;

        // New layout constants
        private const int LeftPadding = 12;
        private const int RightPadding = 12;
        private const int LabelWidth = 120;
        private const int FieldGap = 8;
        private const int LineHeight = 20;

        private void Awake() {
            isMinimized = PlayerPrefs.GetInt(MinimizedPrefKey, 0) == 1;
        }

        private static void SetMinimized(bool value) {
            isMinimized = value;
            PlayerPrefs.SetInt(MinimizedPrefKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void OnGUI() {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftAlt) {
                Cursor.visible = !Cursor.visible;
            }

            // If minimized, show a small maximize button in the top-left corner and bail out
            if (isMinimized) {
                if (GUI.Button(new Rect(PanelX, PanelY, 32, 22), "AP")) {
                    SetMinimized(false);
                }
                return;
            }

            // Compute panel height based on visible content
            bool inMenu = ArchipelagoClient.state == APState.Menu;
            bool hasSession = ArchipelagoClient.session != null;
            bool isAuthed = ArchipelagoClient.isAuthenticated;
            bool showLogin = (!hasSession || !isAuthed) && inMenu;
            bool showConnectedMenu = inMenu && hasSession;

            int panelHeight = 60; // base for status only
            if (showLogin) panelHeight = 140;
            else if (showConnectedMenu) panelHeight = 110;

            // Precompute column positions to avoid overflow
            int labelX = LeftPadding;
            int fieldX = LeftPadding + LabelWidth + FieldGap;
            int fieldWidth = PanelWidth - fieldX - RightPadding;
            int contentWidth = PanelWidth - (LeftPadding + RightPadding);

            // Draw panel group so the minimize button is part of the UI
            GUI.BeginGroup(new Rect(PanelX, PanelY, PanelWidth, panelHeight));
            GUI.Box(new Rect(0, 0, PanelWidth, panelHeight), "Archipelago");

            // Minimize button on the panel (top-right corner)
            if (GUI.Button(new Rect(PanelWidth - PanelPadding - 24, PanelPadding, 24, 20), "—")) {
                SetMinimized(true);
                GUI.EndGroup();
                return;
            }

            // Status
            if (ArchipelagoClient.session != null) {
                if (ArchipelagoClient.isAuthenticated) {
                    GUI.Label(new Rect(LeftPadding, 16, contentWidth, LineHeight), "Status: Connected");
                }
                else {
                    GUI.Label(new Rect(LeftPadding, 16, contentWidth, LineHeight), "Status: Authentication failed");
                }
            }
            else {
                GUI.Label(new Rect(LeftPadding, 16, contentWidth, LineHeight), "Status: Not Connected");
            }

            if ((ArchipelagoClient.session == null || !ArchipelagoClient.isAuthenticated) && ArchipelagoClient.state == APState.Menu ) {
                // Labels
                GUI.Label(new Rect(labelX, 36, LabelWidth, LineHeight), "Host:");
                GUI.Label(new Rect(labelX, 56, LabelWidth, LineHeight), "Slot Name:");
                GUI.Label(new Rect(labelX, 76, LabelWidth, LineHeight), "Password:");

                bool submit = Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return;
                ArchipelagoInfo info = ArchipelagoClient.apInfo;

                // Inputs sized to fit within the panel
                info.address = GUI.TextField(new Rect(fieldX, 36, fieldWidth, LineHeight), info.address);
                info.slot = GUI.TextField(new Rect(fieldX, 56, fieldWidth, LineHeight), info.slot);
                info.password = GUI.TextField(new Rect(fieldX, 76, fieldWidth, LineHeight), info.password);

                if (submit && Event.current.type == EventType.KeyDown) {
                    // The text fields have not consumed the event, which means they were not focused.
                    submit = false;
                }

                if ((GUI.Button(new Rect(LeftPadding, 96, 100, LineHeight), "Connect") || submit) && info.Valid) {
                    ArchipelagoClient.Connect();
                    if(ArchipelagoClient.isAuthenticated) {
                        ItemHandler.Setup();
                    }
                }
            }
            else if(ArchipelagoClient.state == APState.Menu && ArchipelagoClient.session != null) {
                GUI.Label(new Rect(LeftPadding, 36, contentWidth, LineHeight), "Goal Song: " + ArchipelagoClient.slotData.goalSong);
                // GUI.Toggle(new Rect(16, 56, 100, 20), ArchipelagoClient.slotData.deathLink, "Death Link Toggle");

                if(GUI.Button(new Rect(LeftPadding, 76, 100, LineHeight), "Disconnect")) {
                    ArchipelagoClient.Disconnect();
                }
            }

            GUI.EndGroup();
        }
    }
}