using Discord.WebSocket;

namespace PJ_bot;

public static class BotCommands {
    private const int MIN_RAND_VALUE = 1;
    private const int MAX_RAND_VALUE = 100;

    public static string Help(SocketMessage message, string messageCommand) {
        var emojis = new Emojis();
        return ">>> "
               + $"{emojis.SmilingFace} This bot is **easy** to use, just type one of the commands in the chat! {emojis.SmilingFace}\r"
               + "\r**Commands:**\r"
               + "`!info` - Get information about the bot\r"
               + "\r`!rand` - This command returns a random value from 1 to 100\r"
               + "_This command has an optional parameter, you can set the upper bound, for example:_ `!rand 5`\r";
    }

    public static string Info(SocketMessage message, string messageCommand) {
        return $"Hello {message.Author.Mention}, this bot was created by {Information.DEVELOPER} \r"
               + $"**Github link**: {Information.DEVELOPER_GITHUB_LINK}";
    }

    public static string Rand(string messageCommand, IEnumerable<string> messageParameters) {
        int randNumber;

        if (messageParameters != null && messageParameters.Any()) {
            var randParameter = messageParameters.First();

            randNumber = new Random().Next(1,
                int.TryParse(randParameter, out var upperBoundGap)
                    ? Math.Clamp(1, upperBoundGap + 1, int.MaxValue)
                    : upperBoundGap = 100);

            if (upperBoundGap <= MIN_RAND_VALUE) {
                return "Dude, I can only randomize from one";
            }

            return $"Tweaking the value from {MIN_RAND_VALUE} to {upperBoundGap} ... Dropped out {randNumber}";
        }


        randNumber = new Random().Next(MIN_RAND_VALUE, MAX_RAND_VALUE + 1);
        return $"Tweaking the value from {MIN_RAND_VALUE} to {MAX_RAND_VALUE} ... Dropped out {randNumber}";
    }
}
