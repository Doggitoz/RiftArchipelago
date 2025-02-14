namespace RiftArchipelago {
    public class ArchipelagoInfo {
        public string address;
        public string slot;
        public string password;

        public bool Valid {
            get => address != "" && slot != "";
        }
    }
}