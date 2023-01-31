using Newtonsoft.Json.Linq;
using Pastel;

namespace gofileBruter
{
    public class Bruter
    {
        // counter for the number of requests
        int counter, good, bad = 0;

        // instance of the Token class to get the token and Requests class to make requests
        private readonly Token token;
        private readonly Request request;

        // constructor that takes in a Token and Requests object
        public Bruter(Token token, Request request)
        {
            this.token = token;
            this.request = request;
        }

        // method that generates a random string of a specified length
        static string RandomString(int length)
        {
            return new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        // method that generates a random integer between a min and max value
        static int RandomInt(int min, int max)
        {
            return new Random().Next(min, max);
        }

        // method that runs the bruteforce
        public async Task Run()
        {
            while (true)
            {
                // check if a token is available
                if (token.token == null) continue;
                try
                {
                    Random random = new();
                    string generated = RandomString(6);

                    // output current status to console
                    Console.Write($"\r[{counter}] {generated} - ".Pastel("#FFFFFF"));
                    Console.Write($"Bad: {bad}".Pastel("#FF0000"));
                    Console.Write(" - ".Pastel("#FFFFFF"));
                    Console.Write($"Good: {good}".Pastel("#00FF00"));

                    // send GET request with generated string and token
                    JObject json = JObject.Parse(await request.GET($"https://api.gofile.io/getContent?contentId={generated}&token={token.token}&websiteToken=12345"));

                    // check if the request returned an error or is not public
                    if (json["status"]?.ToString() == "error-notFound" ||
                        json["status"]?.ToString() == "error-notPublic" ||
                        json["data"]?["contents"]?.ToString().Length < 3)
                        bad++;
                    else
                    {
                        good++;

                        // write good request to a file
                        if (!File.Exists("goods.txt"))
                            File.Create("goods.txt").Close();

                        // append the good link to the "goods.txt" file
                        File.AppendAllText("goods.txt", $"https://gofile.io/d/{generated}{Environment.NewLine}");
                    }
                    counter++;
                    await Task.Delay(RandomInt(845, 850));
                }
                catch (Exception) 
                {
                    // in case of an exception, wait 1.5 seconds before continuing
                    await Task.Delay(1500);
                    continue;
                }
            }
        }
    }
}
