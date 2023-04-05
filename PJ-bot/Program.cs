using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace PJ_bot {
    public class Program {
        private ClientConfig _clientConfig;

        private DiscordSocketClient _client;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync() {
            SerializeJson();

            DiscordSocketConfig config = new DiscordSocketConfig();
            config.GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent;
            _client = new DiscordSocketClient(config);

            _client.MessageReceived += OnMessageReceived;
            _client.Log += OnLogReceived;

            await _client.LoginAsync(TokenType.Bot, _clientConfig.Token);
            await _client.StartAsync();

            Console.ReadLine();
        }

        private Task OnLogReceived(LogMessage message) {
            Console.WriteLine(message.ToString());

            return Task.CompletedTask;
        }

        private Task OnMessageReceived(SocketMessage message) {
            if (message.Author.IsBot) {
                return Task.CompletedTask;
            }

            var userMessage = message.Content.ToLower();

            if (userMessage == "hello") {
                message.Channel.SendMessageAsync("привет");
            }

            return Task.CompletedTask;
        }

        private void SerializeJson() {
            _clientConfig = new ClientConfig();

            _clientConfig = JsonConvert.DeserializeObject<ClientConfig>(File.ReadAllText("json/config.json"));
        }
    }
}