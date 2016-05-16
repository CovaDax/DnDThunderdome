using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// ORDER
/// Aztoc makes Sneak Check Against Cova's Passive Perception
/// Aztoc Makes a Sneak Attack if he succeeds, and a regular attack if he doesn't
/// Cova Retaliates with an Attack, Spiritual Hammer and Maximized Spell
/// </summary>


namespace DnD_Combat_Simulator {
    class Program {
        static void Main(string[] args) {

            Cleric Cova = new Cleric(8, 6);
            Assassin Aztoc = new Assassin(8, 6);
            Cova.EquipArmor(18, 2);
            Aztoc.EquipArmor(12, 0, 0, 1000);

            var iter = 20000000;
            int covaWins = 0;
            int aztocWins = 0;
            //Console.WriteLine(String.Format("Cova: {0}, Aztoc: {1}", Cova.HitPoints, Aztoc.HitPoints));
            for (int i = 0; i < iter; i++) {
                Cova.Heal();
                Aztoc.Heal();

                Aztoc.StealthCheck(Cova.PassivePerception);
                Cova.TakeDamage(Aztoc.WeaponAttack(Cova.AC, 0, true, true, true));
                if (Cova.HitPoints < 1) {
                    aztocWins++;
                    continue;
                }
                Aztoc.TakeDamage(Cova.WrathOfTheStorm(true, Aztoc.ReflexSave(true)));
                Aztoc.TakeDamage(Cova.Shatter(3, true, Aztoc.FortitudeSave(false)));
                //Aztoc.TakeDamage(Cova.GuidingBolt(3, Aztoc.AC));
                Aztoc.TakeDamage(Cova.SpiritAttack(Aztoc.AC, 3, false));
                if (Aztoc.HitPoints < 1) {
                    covaWins++;
                    continue;
                }
                //Console.WriteLine(String.Format("Cova: {0}, Aztoc: {1}", Cova.HitPoints, Aztoc.HitPoints));

                Cova.TakeDamage(Aztoc.WeaponAttack(Cova.AC, 0, false, true, true));
                if (Cova.HitPoints < 1) {
                    aztocWins++;
                    continue;
                }
                Aztoc.TakeDamage(Cova.WrathOfTheStorm(false, Aztoc.ReflexSave(true)));
                Aztoc.TakeDamage(Cova.Shatter(3, true, Aztoc.FortitudeSave(false)));
                //Aztoc.TakeDamage(Cova.GuidingBolt(3, Aztoc.AC));
                Aztoc.TakeDamage(Cova.SpiritAttack(Aztoc.AC, 3, false));
                if (Aztoc.HitPoints < 1) {
                    covaWins++;
                    continue;
                }
            }
            Console.Write(String.Format("Cova won {0} times, Aztoc won {1} times", covaWins, aztocWins));
            Console.Read();
        }
    }

    //public class Die {
    //    private int _die;
    //    private static Random r = new Random();
    //    private int _total;

    //    public Die(int die) {
    //        _die = die;
    //        _total = 10000000;
    //    }

    //    public double testRogueDamage() {
    //        double damage = 0.0;

    //        for (int j = 0; j < 100000; j++) {
    //            for (int i = 0; i < 2; i++) {
    //                var crit = r.Next(1, 21);
    //                if (crit == 20) {
    //                    damage += Roll(8) + 3;
    //                    damage += Roll(6);
    //                    damage += Roll(6);
    //                    damage += Roll(6);
    //                }
    //                else if (crit + 3 < 20) {
    //                    continue;
    //                }
    //                damage += Roll(8) + 3;
    //                damage += Roll(6);
    //                damage += Roll(6);
    //                damage += Roll(6);
    //            }
    //        }
    //            return damage/100000;
    //    }

    //    public double RogueSneakAttack() {
    //        var damage = 0.0;
    //        var crit = r.Next(1, 21);
    //        if (crit == 20) {
    //            damage += Roll(8) + 3;
    //            damage += Roll(6);
    //            damage += Roll(6);
    //            damage += Roll(6);
    //        }
    //        else if (crit + 3 < 20) {
    //            return damage;
    //        }
    //        damage += Roll(8) + 3;
    //        damage += Roll(6);
    //        damage += Roll(6);
    //        damage += Roll(6);
    //        return damage;

    //    }

    //    public double RogueRapierAttack() {
    //        var damage = 0.0;
    //        var crit = r.Next(1, 21);
    //        if (crit == 20) {
    //            damage += Roll(8) + 3;
    //            damage += Roll(6);
    //            damage += Roll(6);
    //            damage += Roll(6);
    //        }
    //        else if (crit + 3 < 20) {
    //            return damage;
    //        }
    //        damage += Roll(8) + 3;
    //        damage += Roll(6);
    //        damage += Roll(6);
    //        damage += Roll(6);
    //        return damage;

    //    }

    //    public double CalculateRolls(int num, int level) {
    //        var rolls = new List<int>();
    //        for (int i = 0; i < _total; i++) {
    //            //regular damage
    //            for (int y = 0; y < num; y++) {
    //                rolls.Add(Roll(_die));
    //            }
    //            var crit = r.Next(1, 21);
    //            if (crit == 20) {
    //                //crit damage
    //                for (int y = 0; y < num; y++) {
    //                    rolls.Add(Roll(_die));
    //                }
    //                //feat crit damage - level 9
    //                if (level > 8) {
    //                    rolls.Add(Roll(_die));
    //                    //feat crit damage - level 11
    //                    if (level > 10) {
    //                        rolls.Add(Roll(_die));
    //                        //feat crit damage - level 15
    //                        if (level > 14) {
    //                            rolls.Add(Roll(_die));
    //                        }
    //                    }
    //                }
    //            };

    //        }
    //            double final = (double)rolls.Sum()/_total;
    //            return final;
    //    }

    //    private int Roll(int die) {
    //        var damage = r.Next(1, die+1);
    //        if (damage == 1) {
    //            damage = r.Next(1, die+1);
    //        }
    //        return damage;
    //    }

    //}


}
