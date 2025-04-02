﻿using Scripts.AI_Qween;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

 
namespace Scripts
{
    public class AI : MonoBehaviour
    {
        

        public event Action AIResponsed;        // Ивент будет срабатывать, когда от ИИ получаем нужные респонсы или когда игрок сказал то, что нужно.
        public event Action ScriptEvent;        // Ивент будет срабатывать от внешних скриптов, на которые в этом классе нужно подписываться, чтобы менять промпт для нужных респонсов.


        private bool requestSend = false;                                   // Для того чтобы ждать, пока вернётся ответ от ИИ
        private AIPromptBuilder promptBuilder = new AIPromptBuilder();      // Тут хранится всё, что связано с промптами для ИИ. Там же происходит сериализация в JSON.
        private Func<string, string> AIJsonHandler = null;                  // Обработчик ответа ИИ. Получаемый JSON каждый раз разный, поэтому каждый раз используем нужный обработчик.





        private void Awake()
        {
            StartingPoint();            // То, что даёт начало сюжету.
            AIResponsed += AISub;       // То, что двигает сюжет дальше.
        }

        public async Task<string> Request(string _request)                          // Входные данные — слова игрока, выходные данные — слова ИИ. Внутри обрабатывается память и настраивается промпт для дальнейшего использования в сюжете.
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
            FirstMeetJson first = JsonUtility.FromJson<FirstMeetJson>(json);

            if (first.player_name == "unknown")
            {
                promptBuilder.additionalInformations += "\r\nОБЯЗАТЕЛЬНО заполни это поле **player_name**.";
            }
            if (first.player_goal == "unknown")
            {
                promptBuilder.additionalInformations += "\r\nОБЯЗАТЕЛЬНО заполни это поле **player_goal**.";
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
