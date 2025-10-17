using System;
using System.Collections.Generic;

namespace RiftArchipelago {

    public class SlotData {
        public int diamondGoal {get; private set;}
        public string goalSong {get; private set;}
        public bool deathLink {get; private set;}
        public Grade gradeNeeded {get; private set;}
        public bool remix {get; private set;}
        public int mgMode {get; private set;}
        public int bbMode {get; private set;}
        public bool fullComboNeeded {get; private set;}

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
                gradeNeeded = MapObjectToGrade(grade_needed);
            }
            if (slotData.TryGetValue("fullComboNeeded", out var fc_needed)) {
                fullComboNeeded = Convert.ToBoolean(fc_needed);
            }
        }

        private int ParseInt(object i) {
            return int.TryParse(i.ToString(), out var result) ? result : -1;
        }

        public static Grade MapObjectToGrade(object g) {
            if (g == null) return Grade.Any;
            var s = g.ToString().Trim().ToUpper();

            // If it gets sent as an enum value
            if (int.TryParse(s, out var n)) {
                return n switch {
                    0 => Grade.Any,
                    1 => Grade.C,
                    2 => Grade.B,
                    3 => Grade.A,
                    4 => Grade.S,
                    5 => Grade.SS,
                    _ => Grade.Any,
                };
            }

            // If it is sent as a string
            var up = s.ToUpperInvariant();
            if (up == "S_PLUS") return Grade.SS;
            else if (up == "SS") return Grade.SS;
            else if (up == "S") return Grade.S;
            else if (up == "A") return Grade.A;
            else if (up == "B") return Grade.B;
            else if (up == "C") return Grade.C;
            else return Grade.Any;
        }

        public enum Grade {
            Any = 0,
            C = 1,
            B = 2,
            A = 3,
            S = 4,
            SS = 5
        }
    }
}