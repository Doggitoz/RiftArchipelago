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
            if (slotData.TryGetValue("gradeNeeded", out var grade_needed)) {
                gradeNeeded = MapGradeValue(grade_needed);
            }
        }

        private int ParseInt(object i) {
            return int.TryParse(i.ToString(), out var result) ? result : -1;
        }

        private string MapGradeValue(object g) {
            if (g == null) return null;
            var s = g.ToString().Trim().ToUpper();

            // If it gets sent as an enum value
            if (int.TryParse(s, out var n)) {
                return n switch {
                    0 => "Any",
                    1 => "D",
                    2 => "C",
                    3 => "B",
                    4 => "A",
                    5 => "S",
                    _ => null,
                };
            }

            // If it is sent as a string
            var up = s.ToUpperInvariant();
            if (up == "S_plus") return "SS";
            if (up == "ANY" || up == "NONE") return "Any";

            return up;
        }
    }
}