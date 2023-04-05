using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace PJ_bot; 

public class Program {
    private ClientConfig _clientConfig;

    private DiscordSocketClient _client;

    private const char PREFIX = '!';

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
        if (message.Author.IsBot || message.Content[0] != PREFIX) {
            return Task.CompletedTask;
        }

        var userMessage = message.Content.ToLower().Substring(1)
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        var messageCommand = userMessage[0];
        var messageParameters = userMessage.Skip(1);

        if (messageCommand == UserCommands.Rand.ToString().ToLower()) {
            var botText = BotCommands.Rand(messageCommand, messageParameters);

            message.Channel.SendMessageAsync(botText);
        }

        return Task.CompletedTask;
    }

    private void SerializeJson() {
        _clientConfig = new ClientConfig();

        _clientConfig = JsonConvert.DeserializeObject<ClientConfig>(File.ReadAllText("json/config.json"));
    }
}