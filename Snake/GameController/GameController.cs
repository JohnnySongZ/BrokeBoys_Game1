using System.Diagnostics;
using System.Net.WebSockets;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using NetworkUtil;
using Model;
namespace GameController
{
    public class GameController
    {
        public delegate void MessageHandler(IEnumerable<string> messages);
        public event MessageHandler? OnMessageArrived;

        public delegate void UpdateHandler();
        public event UpdateHandler? OnUpdate;

        public delegate void ConnectSuccessHandler();
        public event ConnectSuccessHandler? OnConnectionSuccess;

        public delegate void ConnectFailureHandler(string err);
        public event ConnectFailureHandler? OnConnectionFailure;
        SocketState? server = null;
        World theWorld = new World(2000);

        string? name;

        public void GetName(string name)
        {
            this.name = name;
        }

        public void OnConnect(SocketState state)
        {
            if (state.ErrorOccurred)
            {
                OnConnectionFailure?.Invoke("Error Connectig to server!");
                return;
            }
            OnConnectionSuccess?.Invoke();
            server = state;
            state.OnNetworkAction = MessageReceived;
            Networking.GetData(state);
            Networking.Send(server.TheSocket, name);
            Console.WriteLine("Connected to server");
        }


        private void MessageReceived(SocketState state)
        {
            if (state.ErrorOccurred)
            {
                OnConnectionFailure?.Invoke("Lost connection from Server");
                return;
            }
            ProcessMessages(state);
            Networking.GetData(state);
        }

        private void ProcessMessages(SocketState state)
        {
            //To Do (interpret JSON)
            string completeData = state.GetData();
            string[] individualParts = Regex.Split(completeData, "/n");

            foreach (string part in individualParts)
            {
                JsonDocument jsonDocument = JsonDocument.Parse(part);
                if (jsonDocument.RootElement.TryGetProperty("snake", out _))
                {
                    Snake? s = JsonSerializer.Deserialize<Snake>(part);
                    theWorld.Snakes.Add(s.snake, s);
                }
                if (jsonDocument.RootElement.TryGetProperty("wall", out _))
                {
                    Wall? s = JsonSerializer.Deserialize<Wall>(part);
                    theWorld.Walls.Add(s.wall, s);
                }
                if (jsonDocument.RootElement.TryGetProperty("power", out _))
                {
                    Powerup? s = JsonSerializer.Deserialize<Powerup>(part);
                    theWorld.Powerups.Add(s.power, s);

                }
                if (jsonDocument.RootElement.TryGetProperty("world", out _))
                {
                    World? s = JsonSerializer.Deserialize<World>(part);
                    theWorld = s;
                }
            }
        }

        public void CloseServer()
        {
            server?.TheSocket.Close();
        }

        public void MessageEntered(string message)
        {
            if (server is not null)
            {
                Networking.Send(server.TheSocket, message + "\n");
            }
        }
    }
}
