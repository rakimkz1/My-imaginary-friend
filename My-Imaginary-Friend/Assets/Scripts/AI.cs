using Scripts.AI_Qween;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

 
namespace Scripts
{
    public class AI : MonoBehaviour
    {
        

        public event Action AIResponsed;        // Èâåíò áóäåò ñðàáàòûâàòü êîãäà îò ÈÈ ïîëó÷àåì íóæíûå ðåñïîíñû èëè êîãäà èãðîê ñêàçàë òî ÷òî íóæíî.
        public event Action ScriptEvent;        // Èâåíò áåäåò ñðàáàòûâàòü îò âíåøíèõ ñêðèïòîâ, íà êîòîðûå â ýòîì êëàññå íóæíî ïîäïèñûâàòüñÿ ÷òî áû ìåíÿòü ïðîìïò äëÿ íóæíûõ ðåñïîíñîâ


        private bool requestSend = false;                                   // Äëÿ òîãî ÷òî áû æäàòü ïîêà âåðíåòñÿ îòâåò îò ÈÈ
        private AIPromptBuilder promptBuilder = new AIPromptBuilder();      // Òóò õðàíèòñÿ âñå ÷òî ñâÿçàíî ñ ïðîìïòàìè äëÿ ÈÈ. Òàì æå ïðîèñõîäèò ñåðèàëèçàöèÿ â äæåéñîí
        private Func<string, string> AIJsonHandler = null;                  // Îáðàáîò÷èê îòâåòà ÈÈ. Ïîëó÷àåìûé äæåéñîí êàæäûé ðàç ðàçíûé, ïî ýòîìó êàæäûé ðàç èñïîëüçóåì íóæíûé îáðàáîò÷èê


        
        

        private void Awake()
        {
            StartingPoint();            // òî ÷òî äàåò íà÷àëî ñþæåòó
            AIResponsed += AISub;       // òî ÷òî äâèãàåò ñþæåò äàëüøå
        }

        public async Task<string> Request(string _request)                          // Âõîäíûå äàííûå ñëîâà èãðîêà, âûõîäíûå äàííûå, ñëîâà ÈÈ. Âíóòðè îáðàáàòûâàåòñÿ ïàìÿòü è íàñòðàèâàåòñÿ ïðîìïò äëÿ äàëüíåéøåãî èñïîëüçîâàíèé â ñþæåòå
        {
            if (requestSend == true || _request == null || _request.Length == 0)    // Check
                return null;

            requestSend = true;


            promptBuilder.HistoryAdd(new Message() { role = "user", content = _request });
            string jsonResponse = await AIRequest_Qween.SendRequestAsync(promptBuilder.ToString());  // Send and recieve

            Debug.Log($"jsonResponse: {jsonResponse}");

            ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
            var aiResponse = response.choices[0].message;
            promptBuilder.HistoryAdd(aiResponse);

            string aIAnswer = AIJsonHandler.Invoke(aiResponse.content);

            requestSend = false;

            //Debug.Log($"Îòâåò: {aIAnswer}");
            return aIAnswer;
        }

        private void StartingPoint()     // Íàñòðàèâàåò ïàðàìåòðû ïðîìïòà äëÿ ñþæåòà. Ïåðâàÿ èòåðàöèÿ.
        {
            
            AIJsonHandler += FirstMeetFromJson;
        }

        

        private string DefaultFromJson(string json)                         // Îáðàáàòûâàåò ïðèøåäøèé json îò ÈÈ äåâîëòíûì ñïîñîáîì
        {
            DefaultJson _default = JsonUtility.FromJson<DefaultJson>(json);

            return _default.content;
        }

        private string FirstMeetFromJson(string json)                       // Îáðàáàòûâàåò ïðèøåäøèé json îò ÈÈ äëÿ ïåðâîé èòåðàöèé
        {
            FirstMeetJson first = JsonUtility.FromJson<FirstMeetJson>(json);

            if (first.player_name == "unknown")
            {
                promptBuilder.additionalInformations += "\r\nÎÁßÇÀÒÅËÜÍÎ çàïîëíè ýòî ïîëå **`player_name`**.";
            }
            if (first.player_goal == "unknown")
            {
                promptBuilder.additionalInformations += "\r\nÎÁßÇÀÒÅËÜÍÎ çàïîëíè ýòî ïîëå **`player_goal`**.";
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
