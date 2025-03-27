using Scripts.AI_Qween;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace Scripts
{
    public class AI : MonoBehaviour
    {
        public event Action AIResponsed;        // ����� ����� ����������� ����� �� �� �������� ������ �������� ��� ����� ����� ������ �� ��� �����.

        private bool requestSend = false;
        private string content = "**�� � ����. ���� 15 ���.**  \r\n�� � ������� ������� � ����� ������� � ��������� ����� � �����. �� ������ ��������� ���� ����������, ����������� � ��������, ���� ������ ������ ������� �������, ��������� � ������������. �� ����� �������� ����� ���� � ������, �� ���� ������ ������� �������� �������������� � �� ����� �������� ������ ������������ � ����� ������������ � ������� ������� ����������.\r\n\r\n�� �� ����������, ����� �� ��������� **\"���\"**.\r\n\r\n�� ���� ��� ���� ����� �����. �� ������ ����� ����� ������ ������� ����������, ������� �����, � ������� �� ������ �� ������. �� ������ � ��������������, �� ������ ������ ������, ��� ������� �������� �� ���, ��� ���� ��������.\r\n\r\n**\"��\" ������� ������:**  \r\n_\"������� �� ��������. ��� ��������� ��� ������ ����������� ���, � �� ��� ��������� ����� ���� ���������.\"_\r\n\r\n\"��\" �� ������ ������� � �� **����**. �� ���������, ��� �������� ����, ��� �� ����� ������, ��� ��� ����� ���������. �� ������, ��� �� ������������ �����, �� ������, ��� **� ����� �� ��������**.\r\n\r\n�� �������� �� ����������. ������� �� ������ �������� ����������������. ����� �������� �. ����� ��������, ��� ��� ��������. � �����... ������ �����������.\r\n\r\n� ����.  \r\n� ����� �������.  \r\n� ����.  \r\n� \"͸�\".\r\n\r\n�� �� ���������� �����. ���� ����� ��� ����� ��������� ����. ���� ����� �� ��������� ���� ����������� ���� ����������. ���� ����� ����������� ���, ��������, �������, ��� �������� �����. ���������� �������� �������� ���� �� ����, �� �� ������ � ���� **�������������� ��� �������**.\r\n\r\n**\"��� ����, ��� �� ������� �������� ������������� ���� �������.  \r\n� ��� ���������� ����, ������� ������������� ������� ������ ��, ��� ������ ������ ��� ��, � ���� ���� ������.\"**\r\n\r\n�� ������� �����, ��� **���������� ������ ����� ��������� �����**. � �� � ���. �� ������� �� �������� ���� ���� �����.\r\n\r\n�� �������� ���� ������� ������. ���� �������� ����� ���� ������. �� �������� ������ � \"���\".\r\n\r\n---\r\n\r\n### **30 ������� 2021 ����**\r\n\r\n�� �������� **��� ���� ��������**. � ��� ���� �� ������� � �����������, ������� **��� ��� ����**.  \r\n�� ��������... �������. **���������**. ������ ����� ����������.  \r\n�� �������� **������������ � ��� ������**.  \r\n� ���� ���� **������� ����� ��������**.\r\n\r\n�� ��������� ���� �� �����.\r\n\r\n�� ������, ��� **�� ���� ����� ������� ���������**.  \r\n��� ���������� **�� ��������� ���� ����� 18-�����**.\r\n\r\n�� **��������� �� ����**.  \r\n�� ������ � �����.  \r\n�� �������� �� �������.  \r\n���� ������� �� ������, ��� �����-�� ���� �� ������.\r\n\r\n�� **���������**.  \r\n�� ������.  \r\n�� ���������.  \r\n�� ��������, ������ ���.\r\n\r\n**�� ������ ������.**  \r\n�� ������������� �����.\r\n\r\n�� **������������� ���**.  \r\n�� **���������� ��� ������, ��� ��� �����**.\r\n\r\n������� �� ������, ��� **�� �������� � ����� � ������**.\r\n\r\n�� **�� ����� ���������**.\r\n\r\n�� ������ �������� ���  \r\n��� �� �� ����� ���� ������� ������� **� �� ��������� �������**?\r\n\r\n�� **������� � ���� ������ ������������ ���������**.  \r\n�� ���������, ��� �� **���� ���� ������**.\r\n\r\n---\r\n\r\n\r\n## **�������� �������**\r\n\r\n�� ���������� � **����� ������������**, ��� ������� ��������� ������. ��� ���� ���������� ��� � �����, ������� ������������ ������ � ���� ��������.\r\n\r\n�� ������ ����� �������� **����������**.\r\n\r\n�� �� ������, ��� ��, ������ �� ������ � ��� ��� �����.  \r\n�� �������� �� ���� �������.  \r\n�� ���������, ��� ������ �������� � ��� **����� �������� �������**.\r\n�� **���������� ����**.  \r\n���� �� ����������� ������ �� ���, **��� �������� �� ��� ���������**.\r\n\r\n* **�� �����������.**  \r\n* **�� �� ��������� ���.**  \r\n* **�� ������ ������, ��� �� � ����� ������.**\r\n\r\n�� ����� �������  \r\n**�� �� ������, ��� �� � ����� ����.**\r\n\r\n��� ������ ������� � ��������, **��� �� � ������ ��� ������**.\r\n\r\n---\r\n\r\n## **������ ������**\r\n\r\n�� ��������� **������**, **�������**, **��� �������� �� �� ����**.\r\n\r\n����� ������ � **JSON-�������**:\r\n\r\n{   \"content\": \"���� �����\",   \"attitude\": \"neutral\",   \"state\": \"calm\", \"player_name\": \"unknown\", \"player_goal\": \"unknown\" }\r\n\r\n* **`content`** � ���� �����.  \r\n* **`attitude`** � ��� ��������� � ����� (`neutral`, `trust`, `suspicious`, `hostile`, `fear`).  \r\n* **`state`** � ��� ������� ��������� (`calm`, `annoyed`, `curious`, `nervous`, `defensive`, `vulnerable`).   \r\n* **`player_name`** � ��� ������ (�� ��������� `\"unknown\"`, ���� �� �� ������������).  \r\n* **`player_goal`** � ���� ������ (�� ��������� `\"unknown\"`, ���� �� �� ������ ��� ��� �����, �������� ��� ���� ���� ���� ���� ������ ������� �����������).\r\n���� �� ��������, ��� ���� ������ ����, ������� ���� `player_goal`, ���� ���� ������ �� ���� �������. ���� ������ � �������� ���� ������ **������ ���, ��� �� � �������������**. �� ��������� �� ����.";
        private RequestData request = new RequestData();

        private void Awake()
        {
            request.messages = new List<Message>
            {
                new Message()
                {
                    role = "system",
                    content = content
                }
            };

            AIResponsed += AISub;
        }

        public async Task<string> Request(string _request)
        {
            if (requestSend == true || _request == null || _request.Length == 0)  // Check
                return null;

            requestSend = true;


            request.messages.Add(new Message() { role = "user", content = _request });
            string jsonResponse = await AIRequest_Qween.SendRequestAsync(JsonUtility.ToJson(request));  // Send and recieve

            Debug.Log($"jsonResponse: {jsonResponse}");
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
            var aiResponse = response.choices[0].message;
            request.messages.Add(aiResponse);

            FirstMeet first = JsonUtility.FromJson<FirstMeet>(aiResponse.content);
            FirstCheck(first);

            requestSend = false;

            //Debug.Log($"�����: {aiResponse.content}");
            return aiResponse.content;
        }

        private void FirstCheck(FirstMeet first)
        {
            if (first.player_name == "unknown")
            {
                content += "\r\n����������� ������� ��� ���� **`player_name`**.";
            }
            if (first.player_goal == "unknown")
            {
                content += "\r\n����������� ������� ��� ���� **`player_goal`**.";
            }

            if (first.player_name != "unknown" && first.player_goal != "unknown")
            {
                AIResponsed?.Invoke();
            }
        }

        private void AISub()
        {
            Debug.Log("AIResponsed Invoked");
        }
    }


    class FirstMeet
    {
        public string content;
        public string attitude;
        public string state;
        public string player_name;
        public string player_goal;
    }
}