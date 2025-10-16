using System;
using System.Collections.Generic;
using BepInEx.Logging;

namespace RiftArchipelago {

    public class SlotData {
        public int diamondGoal {get; private set;}
        public string goalSong {get; private set;}
        public bool deathLink {get; private set;}
        public string gradeNeeded {get; private set;}
        public bool remix {get; private set;}
        public int mgMode {get; private set;}
        public int bbMode {get; private set;}

        public SlotData(Dictionary<string, object> slotData) {
            if(slotData.TryGetValue("diamondWinCount", out var diamond_goal)) {
                diamondGoal = ParseInt(diamond_goal);
            }
            if(slotData.TryGetValue("victoryLocation", out var victory)) {
                goalSong = (string) victory;
            }
            if(slotData.TryGetValue("deathLink", out var death_link)) {
                deathLink = Convert.ToBoolean(death_link);
            }
            if(slotData.TryGetValue("remixes", out var remixes)) {
                remix = Convert.ToBoolean(remixes);
            }
            if(slotData.TryGetValue("minigameMode", out var mg_mode)) {
                mgMode = ParseInt(mg_mode);
            }
            if(slotData.TryGetValue("bossMode", out var boss_mode)) {
                bbMode = ParseInt(boss_mode);
            }
        }

        private int ParseInt(object i) {
            return int.TryParse(i.ToString(), out var result) ? result : -1;
        }

        public void SetDeathLink(bool value) {
            deathLink = value;
        }
    }
}