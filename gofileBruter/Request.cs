namespace gofileBruter
{
    public class Request
    {
        // Method for making GET requests
        public Task<string> GET(string url)
        {
            // Create new instance of HttpClient
            HttpClient client = new HttpClient();

            // Add headers to the request
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:108.0) Gecko/20100101 Firefox/108.0");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            client.DefaultRequestHeaders.Add("Origin", "https://gofile.io");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
            client.DefaultRequestHeaders.Add("Sec-GPC", "1");
            client.DefaultRequestHeaders.Add("DNT", "1");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");

            // Make GET request to specified url and return the result as a Task<string>
            return client.GetStringAsync(url);
        }
    }
}
