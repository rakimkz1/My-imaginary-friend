using Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintingMachine : MonoBehaviour, IUsable, IActivatable
{
    public bool isActive;
    public string text;
    [SerializeField] Transform point;
    [SerializeField] Vector3 cameraPosition;
    [SerializeField] Vector3 camRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && isActive && text.Length > 0) text = text.Substring(0, text.Length - 1);
        //if (Input.GetKeyDown(KeyCode.KeypadEnter) && isActive && text.Length > 0) 
    }
    public void Activate(GameObject player)
    {
        isActive = true;
        player.GetComponent<PlayerMovement>().IsMovable = false;
        player.GetComponent<CameraRotate>().IsLookable = false;
        player.GetComponent<PlayerMovement>().SetPose(point.position,point.rotation, 0.8f);
        player.GetComponent<CameraRotate>().SetLookingPosition(cameraPosition,Quaternion.Euler(camRotation),0.8f);
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

    //public async SendMessage()
}
