using Newtonsoft.Json.Linq;

namespace gofileBruter
{
    public class Token
    {
        // Property that holds the token value
        public string? token { get; private set; }

        // Reference to the Requests class to make API calls
        private readonly Request request;

        // Token constructor that takes in an instance of the Requests class
        public Token(Request request)
        {
            this.request = request;
        }

        // Function to generate a new token
        async Task<string> generateToken()
        {
            // Parses the JSON response and retrieves the token value
            return JObject.Parse(await request.GET("https://api.gofile.io/createAccount"))!.SelectToken("data.token")!.ToString();
        }

        // Function to start token generation
        public async Task StartGeneration()
        {
            // Infinite loop to continuously generate new tokens
            while (true)
            {
                // Assigns the new token to the token property
                token = await generateToken();
                // Wait for 10 hours before generating a new token
                await Task.Delay(36000000);
            }
        }
    }
}
