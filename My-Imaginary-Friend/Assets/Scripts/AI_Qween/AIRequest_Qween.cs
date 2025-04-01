using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Scripts.AI_Qween
{
    public class AIRequest_Qween
    {
        private static readonly HttpClient httpClient = new HttpClient();


        /// <summary>
        /// ���������� ������ �� ������ � ��.
        /// </summary>
        /// <param name="jsonContent">JSON-������, ���������� ����� ������������ ������� <see cref="RequestData"/>.</param>
        /// <returns>JSON-������, ���������� ������ <see cref="ApiResponse"/>.</returns>
        public static async Task<string> SendRequestAsync(string jsonContent)
        {
            // ��� �� ������������ ��� ������ �������� environment variable �� ���� ����������� https://www.alibabacloud.com/help/en/model-studio/getting-started/first-api-call-to-qwen?spm=a2c63.p38356.help-menu-2400256.d_0_1_0.66051d09jjzz97#e4cd73d544i3r
            string? apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY", EnvironmentVariableTarget.Machine);

            if (string.IsNullOrEmpty(apiKey))
            {
                return "API Key not set. Please ensure the 'DASHSCOPE_API_KEY' environment variable is configured.";
            }

            string url = "https://dashscope-intl.aliyuncs.com/compatible-mode/v1/chat/completions";
            // For a list of models, see: https://www.alibabacloud.com/help/en/model-studio/getting-started/models

            /*
            string jsonContent = @"{
                        ""model"": ""qwen-plus"",
                        ""messages"": [
                            {
                                ""role"": ""system"",
                                ""content"": ""You are a helpful assistant.""
                            },
                            {
                                ""role"": ""user"", 
                                ""content"": ""Who are you?""
                            }
                        ]
                    }";
            */

            // Send the request and return the result
            string result = await SendPostRequestAsync(url, jsonContent, apiKey);
            return result;
        }

        private static async Task<string> SendPostRequestAsync(string url, string jsonContent, string apiKey)
        {
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            {
                // Configure request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Execute the request and process the response
                HttpResponseMessage response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    //return jsonContent;
                    return $"Request failed: {response.StatusCode}";
                }
            }
        }
    }
}