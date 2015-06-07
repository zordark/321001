using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StepGame
{
    public class Weapon
    {
        public int Radius { get; set; }
        public string Name { get; set; }

        private Weapon(int radius, string name)
        {
            Radius = radius;
            Name = name;
        }

        public static Dictionary<string, Weapon> weapons;
        public static List<Weapon> values;
        public static List<string> keys;
        public static Random r;
        
        static Weapon()
        {
            weapons = new Dictionary<string, Weapon>();
            weapons["morkov"] = new Weapon(1, "morkov");
            weapons["serp"] = new Weapon(2, "serp");
            weapons["molot"] = new Weapon(4, "molot");
            weapons["knife"] = new Weapon(2, "knife");
            weapons["vedro"] = new Weapon(3, "vedro");
            weapons["sword"] = new Weapon(5, "sword");
            weapons["axe"] = new Weapon(4, "axe");
            weapons["gun"] = new Weapon(7, "gun");
            values = weapons.Values.ToList();
            keys = weapons.Keys.ToList();

            r = new Random();

        }

       

        static public string Random()
        {
            return keys[r.Next(keys.Count)];
        }

        public static Weapon Get(string name)
        {
            return weapons[name];
        }
    }
}
