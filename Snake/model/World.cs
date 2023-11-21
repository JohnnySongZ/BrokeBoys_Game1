using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model
{
    public class World
    {
        public Dictionary<int, Snake> Snakes { get; set; }

        public Dictionary<int, Wall> Walls { get; set; }
        public Dictionary<int, Powerup> Powerups { get; set; }
        public int size { get; private set; }

        public World(int _size)
        {
            Snakes = new Dictionary<int, Snake>();
            Walls = new Dictionary<int, Wall>();
            Powerups = new Dictionary<int, Powerup>();
            size = _size;
        }

        [JsonConstructor]
        public World(Dictionary<int, Snake> snakes, Dictionary<int, Wall> walls, Dictionary<int, Powerup> powerups)
        {
            this.Snakes = snakes;
            this.Walls = walls;
            this.Powerups = powerups;
        }
    }
}