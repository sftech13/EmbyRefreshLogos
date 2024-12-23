using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace EmbyRefreshLogos
{
    public class EmbyRefreshLogos
    {
        private const string ApiFormat = "http://{2}:{3}/emby/LiveTv/Manage/Channels?api_key={0}";
        private const string GuideRefreshFormat = "http://{2}:{3}/emby/ScheduledTasks/Running/{1}?api_key={0}";
        private const string Agent = "EmbyRefreshLogos";

        static void Main(string[] args)
        {
            var program = new EmbyRefreshLogos();
            program.ClearLogos(args);
        }

        private void ClearLogos(string[] args)
        {
            const string logFile = "EmbyRefreshLogos.log";
            File.Delete(logFile);

            Log($"EmbyRefreshLogos version {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}");
            Log("");

            if (args.Length < 2)
            {
                Log("Usage: EmbyRefreshLogos API_KEY SCHEDULED_TASK_ID {server} {port}");
                Log("To get Emby API key, go to dashboard > advanced > security and generate one.");
                return;
            }

            string apiKey = args[0];
            string scheduledTaskId = args[1];
            string host = args.Length > 2 ? args[2] : "localhost";
            string port = args.Length > 3 ? args[3] : "8096";

            Log($"Connecting to Emby server at {host}:{port}...");
            string apiUrl = string.Format(ApiFormat, apiKey, scheduledTaskId, host, port);

            try
            {
                var restClient = new RestClient(new RestClientOptions($"http://{host}:{port}"));
                var restRequest = new RestRequest($"emby/LiveTv/Manage/Channels?api_key={apiKey}")
                    .AddHeader("user-agent", Agent);

                Log($"Fetching channels from: {apiUrl}");
                var restResponse = restClient.Get(restRequest);

                if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    Log($"Error retrieving channels: {restResponse.StatusCode} {restResponse.StatusDescription}");
                    return;
                }

                var channelsData = JsonConvert.DeserializeObject<ApiResponse>(restResponse.Content ?? string.Empty)
                                   ?? new ApiResponse();

                Log($"Channels retrieved: {channelsData.Items.Count}");
                if (channelsData.Items.Count == 0)
                {
                    Log("No channels found.");
                    return;
                }

                int count = 0;

                foreach (var channel in channelsData.Items)
                {
                    Log($"Deleting logo for channel: {channel.Name}");

                    var deleteRequest = new RestRequest($"emby/Items/{channel.Id}/Images/Primary?api_key={apiKey}")
                        .AddHeader("user-agent", Agent);

                    var deleteResponse = restClient.Delete(deleteRequest);

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

                Log($"EmbyRefreshLogos completed. Total logos deleted: {count}");
            }
            catch (Exception ex)
            {
                Log($"Exception occurred while deleting logos: {ex.Message}");
            }

            // Automatically refresh the guide
            RefreshGuide(host, port, scheduledTaskId, apiKey);
        }

        private void RefreshGuide(string host, string port, string taskId, string apiKey)
        {
            string refreshUrl = string.Format(GuideRefreshFormat, apiKey, taskId, host, port);
            Log($"Initiating Emby guide refresh using: {refreshUrl}");

            try
            {
                var restClient = new RestClient(new RestClientOptions(refreshUrl));
                var refreshRequest = new RestRequest()
                    .AddHeader("user-agent", Agent);

                var refreshResponse = restClient.Post(refreshRequest);

                // Handle NoContent as a success response
                if (refreshResponse.StatusCode == HttpStatusCode.OK || refreshResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    Log("Emby guide refresh triggered successfully.");
                }
                else
                {
                    Log($"Failed to trigger Emby guide refresh: {refreshResponse.StatusCode} {refreshResponse.StatusDescription}");
                    Log($"Response: {refreshResponse.Content}");
                }
            }
            catch (Exception ex)
            {
                Log($"Exception during guide refresh: {ex.Message}");
            }
        }

        private static void Log(string text)
        {
            Console.WriteLine(text);
            File.AppendAllText("EmbyRefreshLogos.log", text + Environment.NewLine);
        }
    }

    public class ApiResponse
    {
        public List<Channel> Items { get; set; } = new List<Channel>();
    }

    public class Channel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
