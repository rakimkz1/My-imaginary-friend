using Scripts.AI_Qween;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace Scripts
{
    public class AI : MonoBehaviour
    {


        public AIPromptBuilder promptBuilder;      // Тут хранится всё, что связано с промптами для ИИ. Там же происходит сериализация в JSON.
        public event Action AIResponsed;        // Ивент будет срабатывать, когда от ИИ получаем нужные респонсы или когда игрок сказал то, что нужно.
        public event Action ScriptEvent;        // Ивент будет срабатывать от внешних скриптов, на которые в этом классе нужно подписываться, чтобы менять промпт для нужных респонсов.


        private bool requestSend = false;                                   // Для того чтобы ждать, пока вернётся ответ от ИИ
        private Func<string, string> AIJsonHandler = null;                  // Обработчик ответа ИИ. Получаемый JSON каждый раз разный, поэтому каждый раз используем нужный обработчик.




        private void Awake()
        {
            StartingPoint();            // То, что даёт начало сюжету.
            AIResponsed += F_MeetExecute;       // То, что двигает сюжет дальше.
        }

        public async Task<string> Request(string _request)                              // Входные данные — слова игрока, выходные данные — слова ИИ. Внутри обрабатывается память и настраивается промпт для дальнейшего использования в сюжете.
        {
            if (requestSend == true || _request == null || _request.Length == 0)        // Check
                return null;

            requestSend = true;

            string jsonRequest = promptBuilder.ToString(_request);                              // Get json for send
            Debug.Log($"jsonRequest(server): {jsonRequest}");

            string jsonResponse = await AIRequest_Qween.SendRequestAsync(jsonRequest);          // Send and recieve
            Debug.Log($"jsonResponse(server): {jsonResponse}");

            ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>(jsonResponse);

            string mainResponceJson = apiResponse.choices[0].message.content;                   // тут хранятся так же параметры состояния ИИ.
            promptBuilder.HistoryAdd(_request, mainResponceJson);

            string aIAnswer = AIJsonHandler.Invoke(mainResponceJson);                           // Тут хранится только слова ИИ

            requestSend = false;

            //Debug.Log($"Ответ: {aIAnswer}");
            return aIAnswer;
        }

        private void StartingPoint()     // Настраивает параметры промпта для сюжета. Первая итерация.
        {

            AIJsonHandler += FirstMeetFromJson;
        }



        private string DefaultFromJson(string json)                         // Обрабатывает пришедший JSON от ИИ дефолтным способом.
        {
            DefaultJson _default = JsonUtility.FromJson<DefaultJson>(json);

            return _default.content;
        }

        private string FirstMeetFromJson(string json)                       // Обрабатывает пришедший JSON от ИИ для первой итерации.
        {
            FirstMeetingJsonResponce first = DefaultJsonResponce.FromJson<FirstMeetingJsonResponce>(json);

            if (first.player_name == "unknown")
            {
                //promptBuilder.additionalInformations += "\r\nОБЯЗАТЕЛЬНО заполни это поле **player_name**.";
            }
            if (first.player_goal == "unknown")
            {
                //promptBuilder.additionalInformations += "\r\nОБЯЗАТЕЛЬНО заполни это поле **player_goal**.";
            }

            if (first.player_name != "unknown" && first.player_goal != "unknown")
            {
                promptBuilder.player_name = first.player_name;
                AIResponsed?.Invoke();

            }

            return first.content;
        }

        private void F_MeetExecute()
        {
            Debug.Log("AIResponsed Invoked");

            promptBuilder.currentJsonVariety = JsonResponceVariety.Default;
            AIJsonHandler -= FirstMeetFromJson;
            AIJsonHandler += DefaultFromJson;
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
