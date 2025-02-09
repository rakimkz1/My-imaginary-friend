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
                        content = "�� �������. ����������� �������� ����� ����� �����. ������ ��������� ������������, ������ �� �����. �� ������ ��������� �� ��� ��� ���� ������������ �� ���������� ���� ����� ��������"
                    },
                    new Message()
                    {
                        role = "user",
                        content = "������� �����, ��� ���� ����?"
                    }
                }
            };


            string jsonResponse = await AIRequest_Qween.SendRequestAsync(JsonUtility.ToJson(request));
            Debug.Log($"jsonResponse: {jsonResponse}");
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
            string aiResponse = response.choices[0].message.content;

            Debug.Log($"�����: {aiResponse}");
        }
    }
}

