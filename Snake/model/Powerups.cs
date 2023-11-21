using SnakeGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model
{
    public class Powerup
    {
        public int power { get; set; }
        public Vector2D loc { get; set; }
        public bool died { get; set; }
        [JsonConstructor]
        public Powerup(int power, Vector2D loc, bool died)
        {
            this.power = power;
            this.loc = loc;
            this.died = died;
        }
    }
}
