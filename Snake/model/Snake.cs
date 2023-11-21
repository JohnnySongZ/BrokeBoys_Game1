using System.Numerics;
using System.Reflection;
using System.Text.Json.Serialization;
using SnakeGame;
namespace Model
{
    public class Snake
    {
        public int snake { get; set; }
        public string name { get; set; }
        public List<Vector2D> body { get; set; }
        public Vector2D dir { get; set; }
        public int score { get; set; }
        public bool died { get; set; }
        public bool alive { get; set; }
        public bool dc { get; set; }
        public bool join { get; set; }

        [JsonConstructor]
        public Snake(int snake, string name, List<Vector2D> body, Vector2D Dir, int Score, bool Died, bool Alive, bool Dc, bool Join)
        {
            this.snake = snake;
            this.name = name;
            this.body = body;
            this.dir = Dir;
            this.died = Died;
            this.alive = Alive;
            this.dc = Dc;
            this.join = Join;
        }

    }
}