using Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Scripts
{
    public class PrintingMachine : MonoBehaviour, IUsable, IActivatable
    {
        public bool isActive;
        public string text;

        [SerializeField] AI ai;
        [SerializeField] Transform point;
        [SerializeField] Vector3 cameraPosition;
        [SerializeField] Vector3 camRotation;

        private string answer="";

        async void Start()
        {
            await ai.Request("Hello");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace) && isActive && text.Length > 0) text = text.Substring(0, text.Length - 1);
            if (Input.GetKeyDown(KeyCode.Return) && isActive && text.Length > 0) SendMessage();
    }
        public void Activate(GameObject player)
        {
            isActive = true;
            player.GetComponent<PlayerMovement>().IsMovable = false;
            player.GetComponent<CameraRotate>().IsLookable = false;
            player.GetComponent<PlayerMovement>().SetPose(point.position, point.rotation, 0.8f);
            player.GetComponent<CameraRotate>().SetLookingPosition(cameraPosition, Quaternion.Euler(camRotation), 0.8f);
        }
        public void InputSymbol()
        {
            if (Input.inputString.Length > 0) text += Input.inputString;
        }

        public void Reset(GameObject player)
        {
            isActive = false;
            player.GetComponent<PlayerMovement>().IsMovable = true;
            player.GetComponent<CameraRotate>().ResetLookingPosition(0.4f);
        }

        public async void SendMessage()
        {
            await Task.Run(GetResponse);
            text = "";
            //Debug.Log();
        }
        async Task GetResponse()
        {
            answer = await ai.Request(text);
            //Thread.Sleep(3000);
        }
    }
}
