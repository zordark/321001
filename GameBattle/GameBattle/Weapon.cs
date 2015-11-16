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
            weapons["topor"] = new Weapon(2, "topor");
            weapons["mech"] = new Weapon(3, "mech");
            weapons["molot"] = new Weapon(2, "molot");
            weapons["knife"] = new Weapon(1, "knife");
            weapons["magia"] = new Weapon(4, "magia");
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
