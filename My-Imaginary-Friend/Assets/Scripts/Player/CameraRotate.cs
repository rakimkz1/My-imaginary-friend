using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float Sensitivity;

    [SerializeField] private Transform camera;

    private float _x_rotation = 0f;


    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime; 
        float y = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        _x_rotation -= y;
        _x_rotation = Mathf.Clamp(_x_rotation, -80f, 80f);

        camera.localRotation = Quaternion.Euler(_x_rotation, 0f, 0f);

        transform.Rotate(Vector3.up * x);   
    }
}
