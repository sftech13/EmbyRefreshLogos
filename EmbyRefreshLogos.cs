using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace EmbyRefreshLogos
{
    public class EmbyClearLogos
    {
        private const string ApiFormat = "http://{0}:{1}/emby/LiveTv/Manage/Channels?api_key={2}";
        private const string Agent = "EmbyClearLogos";

        static void Main(string[] args)
        {
            var program = new EmbyClearLogos();
            program.ClearLogos(args);
        }

        private void ClearLogos(string[] args)
        {
            const string logFile = "EmbyClearLogos.log";
            File.Delete(logFile);

            Log($"EmbyClearLogos version {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}");
            Log("");

            if (args.Length < 1)
            {
                Log("Usage: EmbyClearLogos API_KEY {server} {port}");
                Log("To get Emby API key, go to dashboard > advanced > security and generate one.");
                return;
            }

            string apiKey = args[0];
            string host = args.Length > 1 ? args[1] : "localhost";
            string port = args.Length > 2 ? args[2] : "8096";

            Log($"Connecting to Emby server at {host}:{port}...");

            string apiUrl = string.Format(ApiFormat, host, port, apiKey);
            try
            {
                var restClient = new RestClient($"http://{host}:{port}");
                var restRequest = new RestRequest($"emby/LiveTv/Manage/Channels?api_key={apiKey}", Method.Get)
                    .AddHeader("user-agent", Agent);

                var restResponse = restClient.Execute(restRequest);

                if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    Log($"Error retrieving channels: {restResponse.StatusCode} {restResponse.StatusDescription}");
                    return;
                }

                var channelsData = JsonConvert.DeserializeObject<ApiResponse>(restResponse.Content ?? string.Empty) 
                                   ?? new ApiResponse();

                if (channelsData.Items.Count == 0)
                {
                    Log("No channels found.");
                    return;
                }

                int count = 0;

                foreach (var channel in channelsData.Items)
                {
                    Log($"Deleting logo for channel: {channel.Name}");

                    var deleteRequest = new RestRequest($"emby/Items/{channel.Id}/Images/Primary?api_key={apiKey}", Method.Delete)
                        .AddHeader("user-agent", Agent);

                    var deleteResponse = restClient.Execute(deleteRequest);

                    if (!deleteResponse.IsSuccessful)
                    {
                        Log($"Failed to delete logo for {channel.Name}. Error: {deleteResponse.ErrorMessage}");
                    }
                    else
                    {
                        count++;
                        Log($"Logo deleted for channel: {channel.Name}");
                    }
                }

                Log($"EmbyClearLogos completed. Total logos deleted: {count}");
            }
            catch (Exception ex)
            {
                Log($"Exception occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs messages to the console and a log file.
        /// </summary>
        /// <param name="text"></param>
        private static void Log(string text)
        {
            Console.WriteLine(text);
            File.AppendAllText("EmbyClearLogos.log", text + Environment.NewLine);
        }
    }

    /// <summary>
    /// Represents the API response structure for channels.
    /// </summary>
    public class ApiResponse
    {
        public List<Channel> Items { get; set; } = new List<Channel>(); // Default empty list
    }

    /// <summary>
    /// Represents a single channel in the API response.
    /// </summary>
    public class Channel
    {
        public string Id { get; set; } = string.Empty; // Default value
        public string Name { get; set; } = string.Empty; // Default value
    }
}
