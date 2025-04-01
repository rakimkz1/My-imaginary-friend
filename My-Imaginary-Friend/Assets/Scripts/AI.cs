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
        public event Action ScriptEvent;        // ����� ����� ����������� �� ������� ��������, �� ������� � ���� ������ ����� ������������� ��� �� ������ ������ ��� ������ ���������


        private bool requestSend = false;                                   // ��� ���� ��� �� ����� ���� �������� ����� �� ��
        private AIPromptBuilder promptBuilder = new AIPromptBuilder();      // ��� �������� ��� ��� ������� � ��������� ��� ��. ��� �� ���������� ������������ � �������
        private Func<string, string> AIJsonHandler = null;                  // ���������� ������ ��. ���������� ������� ������ ��� ������, �� ����� ������ ��� ���������� ������ ����������





        private void Awake()
        {
            StartingPoint();            // �� ��� ���� ������ ������
            AIResponsed += AISub;       // �� ��� ������� ����� ������
        }

        public async Task<string> Request(string _request)                          // ������� ������ ����� ������, �������� ������, ����� ��. ������ �������������� ������ � ������������� ������ ��� ����������� ������������� � ������
        {
            if (requestSend == true || _request == null || _request.Length == 0)    // Check
                return null;

            requestSend = true;


            promptBuilder.HistoryAdd(new Message() { role = "user", content = _request });
            string jsonResponse = await AIRequest_Qween.SendRequestAsync(promptBuilder.ToString());  // Send and recieve

            Debug.Log($"jsonResponse: {jsonResponse}");

            /*ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
            var aiResponse = response.choices[0].message;
            promptBuilder.HistoryAdd(aiResponse);

            string aIAnswer = AIJsonHandler.Invoke(aiResponse.content);

            requestSend = false;
*/
            //Debug.Log($"�����: {aIAnswer}");
            return "ладно";
        }

        private void StartingPoint()     // ����������� ��������� ������� ��� ������. ������ ��������.
        {

            AIJsonHandler += FirstMeetFromJson;
        }



        private string DefaultFromJson(string json)                         // ������������ ��������� json �� �� ��������� ��������
        {
            DefaultJson _default = JsonUtility.FromJson<DefaultJson>(json);

            return _default.content;
        }

        private string FirstMeetFromJson(string json)                       // ������������ ��������� json �� �� ��� ������ ��������
        {
            FirstMeetJson first = JsonUtility.FromJson<FirstMeetJson>(json);

            if (first.player_name == "unknown")
            {
                promptBuilder.additionalInformations += "\r\n����������� ������� ��� ���� **`player_name`**.";
            }
            if (first.player_goal == "unknown")
            {
                promptBuilder.additionalInformations += "\r\n����������� ������� ��� ���� **`player_goal`**.";
            }

            if (first.player_name != "unknown" && first.player_goal != "unknown")
            {
                promptBuilder.player_name = first.player_name;
                AIResponsed?.Invoke();
            }

            AIJsonHandler -= FirstMeetFromJson;
            AIJsonHandler += DefaultFromJson;
            return first.content;
        }

        private void AISub()
        {
            Debug.Log("AIResponsed Invoked");

            promptBuilder.currentJsonVariety = JsonResponceVariety.Default;
        }



    }

    class DefaultJson
    {
        public string content;
        public string attitude;
        public string state;
    }

    class FirstMeetJson
    {
        public string content;
        public string attitude;
        public string state;
        public string player_name;
        public string player_goal;
    }

    public enum JsonResponceVariety
    {
        Default,
        Meeting
    }
}