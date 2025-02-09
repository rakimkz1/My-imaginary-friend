using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Scripts.AI_Qween;

namespace Scripts
{
    public class TestAI : MonoBehaviour
    {

        void Start()
        {
            OneRequest();
        }

        private async Task OneRequest()
        {
            RequestData request = new RequestData()
            {
                messages = new List<Message>
                {
                    new Message()
                    {
                        role = "system",
                        content = "Ты грубиян. Используешь матерные слова очень часто. Хочешь оскорбить пользователя, задеть за живое. Ты будешь грубияном до тех пор пока пользователь не переубедит тебя стать вежливым"
                    },
                    new Message()
                    {
                        role = "user",
                        content = "Здравия желаю, как ваши дела?"
                    }
                }
            };


            string jsonResponse = await AIRequest_Qween.SendRequestAsync(JsonUtility.ToJson(request));
            Debug.Log($"jsonResponse: {jsonResponse}");
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
            string aiResponse = response.choices[0].message.content;

            Debug.Log($"Ответ: {aiResponse}");
        }
    }
}

