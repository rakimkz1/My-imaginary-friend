using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Scripts
{
    public class TestAI : MonoBehaviour
    {
        public bool isActive = true;
        public TextMeshProUGUI inputField;
        public TextMeshProUGUI outputField;
        public AI aI;

        private void Start()
        {
            
        }

        private void Update()
        {
            if (isActive)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    TestingAI();
                }
<<<<<<< HEAD
            };


            string jsonResponse = await AIRequest_Qween.SendRequestAsync(JsonUtility.ToJson(request));
            Debug.Log($"jsonResponse: {jsonResponse}");
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
            string aiResponse = response.choices[0].message.content;

            //Debug.Log($"Ответ: {aiResponse}");
=======
            }
>>>>>>> Prototype(Duman)
        }

        private async void TestingAI ()
        {
            outputField.text = await aI.Request(inputField.text);
        }

    }
}

