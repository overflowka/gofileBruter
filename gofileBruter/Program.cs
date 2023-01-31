namespace gofileBruter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Create a new instance of the requests class
            var request = new Request();
            //Create a new instance of the token class and pass in the requests object
            var token = new Token(request);
            //Create a new instance of the bruter class and pass in the token and requests objects
            var bruter = new Bruter(token, request);
            //Start the token generation and brute forcing tasks simultaneously
            await Task.WhenAll(token.StartGeneration(), bruter.Run());
        }
    }
}