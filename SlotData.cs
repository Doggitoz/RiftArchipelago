using System;
using System.Collections.Generic;
using BepInEx.Logging;

namespace RiftArchipelago {

    public class SlotData {
        public int diamondGoal {get; private set;}
        public string goalSong {get; private set;}
        public bool deathLink {get; private set;}
        public string gradeNeeded {get; private set;}

        public SlotData(Dictionary<string, object> slotData) {
            if(slotData.TryGetValue("diamondWinCount", out var diamond_goal)) {
                diamondGoal = ParseInt(diamond_goal);
            }

            goalSong = (string) slotData["victoryLocation"];
        }

        private int ParseInt(object i) {
            return int.TryParse(i.ToString(), out var result) ? result : -1;
        }
    }
}