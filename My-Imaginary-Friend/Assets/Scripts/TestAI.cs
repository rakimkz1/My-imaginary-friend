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
            }
        }

        private async void TestingAI ()
        {
            outputField.text = await aI.Request(inputField.text);
        }

    }
}

