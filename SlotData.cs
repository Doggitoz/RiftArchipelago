using System;
using System.Collections.Generic;
using BepInEx.Logging;

namespace RiftArchipelago {

    public class SlotData {
        public int diamondGoal {get; private set;}
        public string goalSong {get; private set;}

        public SlotData(Dictionary<string, object> slotData, ManualLogSource log) {

        }
    }
}