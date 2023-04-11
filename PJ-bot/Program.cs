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
        SerializeConfigJson();

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

        RunCommand(message);

        return Task.CompletedTask;
    }

    private void SerializeConfigJson() {
        _clientConfig = new ClientConfig();

        _clientConfig = JsonConvert.DeserializeObject<ClientConfig>(File.ReadAllText("json/config.json"));
    }

    private void RunCommand(SocketMessage message) {
        var userMessage = message.Content.ToLower().Substring(1)
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        var messageCommand = userMessage[0];
        var messageParameters = userMessage.Skip(1);

        if (messageCommand == UserCommands.Help.ToString().ToLower()) {
            var botText = BotCommands.Help(message, messageCommand);

            message.Channel.SendMessageAsync(botText);
        }

        if (messageCommand == UserCommands.Info.ToString().ToLower()) {
            var botText = BotCommands.Info(message, messageCommand);

            message.Channel.SendMessageAsync(botText);
        }

        if (messageCommand == UserCommands.Rand.ToString().ToLower()) {
            var botText = BotCommands.Rand(messageCommand, messageParameters);

            message.Channel.SendMessageAsync(botText);
        }
    }
}