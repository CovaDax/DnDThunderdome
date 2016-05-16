using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD_Combat_Simulator {
    public class PC {
        #region "Properties"
        private int _level;
        public int Level {
            get { return _level; }
            protected set { _level = value; }
        }
        private int _str;
        public int STR
        {
            get { return _str; }
            protected set { _str = value; }
        }
        private int _dex;
        public int DEX
        {
            get { return _dex; }
            protected set { _dex = value; }
        }
        private int _con;
        public int CON
        {
            get { return _con; }
            protected set { _con = value; }
        }
        private int _int;
        public int INT
        {
            get { return _int; }
            protected set { _int = value; }
        }
        private int _wis;
        public int WIS
        {
            get { return _wis; }
            protected set { _wis = value; }
        }
        private int _cha;
        public int CHA
        {
            get { return _cha; }
            protected set { _cha = value; }
        }
        private int _ac;
        public int AC
        {
            get { return _ac; }
        }
        private int _hp;
        public int HitPoints
        {
            get { return _hp; }
            protected set { _hp = value; }
        }
        private int _proficiency;
        public int Proficiency
        {
            get { return _proficiency; }
        }
        private int _casterLevel;
        public int CasterLevel {
            get { return _casterLevel; }
            protected set { _casterLevel = value; }
        }
        private int _passivePerception;
        public int PassivePerception {
            get { return _passivePerception; }
            protected set { _passivePerception = value; }
        }
        protected Die _hitDie;
        #endregion

        public PC(int hitDie, int level = 1) {
            _level = level;
            CalculateProficiency();
            _hitDie = new Die(hitDie);
            
        }

        #region "Initialization"
        //Set Stats
        public void setStats(int str, int dex, int con, int inte, int wis, int cha) {
            _str = str;
            _dex = dex;
            _con = con;
            _int = inte;
            _wis = wis;
            _cha = cha;
        }
        //calculate health
        protected void CalculateHP() {
            _hp = _hitDie.Dice + CON.Mod();
            for (int i = 1; i < _level; i++)
                _hp += _hitDie.Dice / 2 + 1 + CON.Mod();
        }
        //calculate proficiency
        protected void CalculateProficiency() {
            _proficiency = 2;
            if (_level > 4) _proficiency++;
            if (_level > 9) _proficiency++;
        }
        //calculate casterlevel
        protected void CalculateCasterLevel() {
            _casterLevel = Level / 2 + 1;
        }
        //calculate passive perception
        protected void CalculatePerception() {
            _passivePerception = 8 + WIS.Mod() + Proficiency;
        }
        #endregion

        #region "Actions"
        //rollstats
        public List<int> CalculateStats() {
            List<int> stats = new List<int>();
            for (int i = 0; i < 6; i++) {
                stats.Add(Die.RollStat());
            }
            stats.Sort();
            stats.Reverse();

            return stats;
        }

        //attack
        public Die.Hit Attack(int ac, bool advantage, int mod, int bonus = 0) {
            int toHit = Die.ToAttack(mod, Proficiency, advantage, bonus);
            if (toHit == 1) return Die.Hit.Miss;
            else if (toHit == 20) return Die.Hit.Crit;
            else if (toHit < ac) return Die.Hit.Miss;
            else return Die.Hit.Hit;
        }

        //takedamage
        public void EquipArmor(int armor, int shield, int bonus = 0, int maxDex = 0) {
            _ac = armor + shield + bonus;
            if (maxDex == 0 || DEX.Mod() < 0) {
                return;
            } else if (DEX.Mod() > maxDex) {
                _ac += maxDex;
            } else {
                _ac += DEX.Mod();
            }
        }

        public void TakeDamage(int damage) {
            _hp = _hp - damage;
        }

        //saving throw
        public int ReflexSave(bool proficient) {
            return Die.ToAttack(DEX.Mod(), proficient ? Proficiency : 0);
        }

        public int FortitudeSave(bool proficient) {
            return Die.ToAttack(CON.Mod(), proficient ? Proficiency : 0);
        }

        public int WillpowerSave(bool proficient) {
            return Die.ToAttack(WIS.Mod(), proficient ? Proficiency : 0);
        }
        #endregion

        public void Heal() {
            CalculateHP();
        }

    }
    static class Extensions {
        public static int Mod(this int stat) {
            return (stat / 2 - 5);
        }
    }
}
