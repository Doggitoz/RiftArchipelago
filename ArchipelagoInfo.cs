namespace RiftArchipelago {
    public class ArchipelagoInfo {
        public string address = string.Empty;
        public string slot = string.Empty;
        public string password = string.Empty;

        public bool Valid {
            get => address != "" && slot != "";
        }
    }
}