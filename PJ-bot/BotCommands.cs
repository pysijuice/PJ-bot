namespace PJ_bot {
    public static class BotCommands {
        private const int MIN_RAND_VALUE = 1;
        private const int MAX_RAND_VALUE = 100;

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
}