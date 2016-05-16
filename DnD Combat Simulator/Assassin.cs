using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Combat_Simulator {
    public class Assassin : PC {
        private Die _weapon = new Die(8);
        private int _sneakAttackDie;
        public int SneakAttackDice {
            get { return _sneakAttackDie; }
        }
        public int Stealth {
            get { return Proficiency * 2 + DEX.Mod(); }
        }
        private bool _suprise;
        public bool Suprise{
            get { return _suprise; }
        }

        public Assassin(int hitDie, int level) : base(hitDie, level) {
            _sneakAttackDie = (level / 2) + 1;
            CalculateRogueStats();
            CalculateHP();
        }

        public void CalculateRogueStats() {
            List<int> stats = CalculateStats();
            if (stats.Count > 0) {
                DEX = stats[0];
                CHA = stats[1];
                CON = stats[2];
                INT = stats[3];
                STR = stats[4];
                WIS = stats[5];
            }
        }
        
        public int WeaponAttack(int ac, int bonus = 0,
            bool advantage = false,  
            bool dualWield = false, bool dualWieldFeat = false){

            Die.Hit attack = Attack(ac, advantage, DEX.Mod(), bonus);
            if (Suprise && (attack != Die.Hit.Miss)) attack = Die.Hit.Crit;
            var damage = 0;
            switch (attack) {
                case Die.Hit.Miss:
                    return 0;
                case Die.Hit.Hit:
                    damage += _weapon.Roll() + DEX.Mod();
                    if (dualWield) damage += _weapon.Roll() + (dualWieldFeat ? DEX.Mod() : 0);
                    if (advantage) damage += SneakAttack();
                    return damage;
                case Die.Hit.Crit:
                    damage += _weapon.Roll(2) + DEX.Mod();
                    if (dualWield) damage += _weapon.Roll(2) + (dualWieldFeat ? DEX.Mod() : 0);
                    if (advantage) damage += SneakAttack() + SneakAttack();
                    return damage;
                default:
                    return 0;
            }
        }

        public void StealthCheck(int perception) {
            var check = Die.ToAttack(DEX.Mod(), Proficiency, false, Proficiency);
            if(check > perception) {
                _suprise = true;
            }
        }

        public int SneakAttack() {
            Die sneakAttack = new Die(6);
            return sneakAttack.Roll(_sneakAttackDie);
        }
    
    }

}
