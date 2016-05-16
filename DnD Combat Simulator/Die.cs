using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Combat_Simulator {
    public class Die {
        public enum Hit { Hit, Crit, Miss };

        private int _die;
        public int Dice {
            get { return _die; }
        }
        private static Random r = new Random();

        public Die(int die) {
            _die = die;
        }

        private static int d20(bool advantage) {
            var roll = r.Next(1, 21);
            var adv = advantage ? r.Next(1, 21) : 0;
            if (adv > roll) adv = roll;
            return roll;
        }

        public static int ToAttack(int stat, int prof, bool advantage = false, int bonus = 0){
            return d20(advantage) + stat + prof + bonus;
        }

        public static int RollStat() {
            var stat = new List<int>();
            Die d6 = new Die(6);
            for(int i = 0; i < 4; i++) {
                var die = d6.Roll();
                if (die < 2) continue;
                stat.Add(die);
            }
            stat.Sort();
            stat.RemoveAt(0);
            return stat.Sum();
        }

        public int Roll() {
            return r.Next(1, _die+1);
        }

        public int Roll(int num) {
            var result = 0;
            for (int i = 0; i < num; i++) {
                result += Roll();
            }
            return result;
        }

    }

}
