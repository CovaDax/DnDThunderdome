using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Combat_Simulator {
    public class Cleric : PC {

        private Die _weapon = new Die(8);
        private int _saveDC;
        public int SpellDC {
            get { return _saveDC; }
            private set { _saveDC = value; }
        }

        public Cleric(int hitDie, int level) : base(hitDie, level) {
            CasterLevel = (level / 2) + 1;
            setStats(15,10,17,11,19,14);
            SpellDC = 8 + Proficiency + WIS.Mod();
            PassivePerception = 10 + Proficiency + WIS.Mod();
            CalculateHP();
        }

        public void CalculateClericStats() {
            List<int> stats = CalculateStats();
            if (stats.Count > 0) {
                WIS = stats[0];
                CON = stats[1];
                STR = stats[2];
                CHA = stats[3];
                DEX = stats[4];
                INT = stats[5];
            }
        }

        public int WeaponAttack(int ac, bool advantage = false, int bonus = 0) {
            Die.Hit attack = Attack(ac, advantage, STR.Mod(), bonus);
            switch (attack) {
                case Die.Hit.Miss:
                    return 0;
                case Die.Hit.Hit:
                    return _weapon.Roll() + STR.Mod();
                case Die.Hit.Crit:
                    return _weapon.Roll(2) + STR.Mod();
                default:
                    return 0;
            }
        }

        public int WrathOfTheStorm(bool max, int save) {
            var mult = (save < SpellDC) ? 1 : 2;
            if (max) return 16/mult;
            else return new Die(8).Roll(2)/mult;
        }

        public int GuidingBolt(int slot, int ac, bool advantage = false, int bonus = 0){
            Die.Hit attack = Attack(ac, advantage, WIS.Mod(), bonus);
            Die bolt = new Die(6);
            switch (attack) {
                case Die.Hit.Miss:
                    return 0;
                case Die.Hit.Hit:
                    return _weapon.Roll(3+slot) + WIS.Mod();
                case Die.Hit.Crit:
                    return _weapon.Roll((3+slot) * 2) + WIS.Mod();
                default:
                    return 0;
            }
        }

        public int Shatter(int slot, bool max, int save) {
            var mult = (save < SpellDC) ? 1 : 2;
            if (CasterLevel < 2) return 0;
            if (max) return ((slot + 1) * 8)/mult; ;
            return new Die(8).Roll(slot + 1)/mult;
        }

        public int CallLightning(int slot, bool max, int save) {
            var mult = (save < SpellDC) ? 1 : 2;
            if (CasterLevel < 3) return 0;
            if (max) return ((slot) * 10) / mult;
            return new Die(10).Roll(slot)/mult;
        }

        public int SpiritAttack(int ac, int slot, bool advantage = false, int bonus = 0) {
        Die weapon = new Die(8);
            Die.Hit attack = Attack(ac, advantage, WIS.Mod(), bonus);
            switch (attack) {
                case Die.Hit.Miss:
                    return 0;
                case Die.Hit.Hit:
                    return _weapon.Roll(slot-1) + WIS.Mod();
                case Die.Hit.Crit:
                    return _weapon.Roll((slot-1)*2) + WIS.Mod();
                default:
                    return 0;
            }
        }
    }

}
