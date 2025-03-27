using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{

    public class CameraRotate : MonoBehaviour
    {
        public float Sensitivity;
        public bool IsLookable;
        public float XRotation;
        public float LookingSpeed;

        [SerializeField] private Transform cam;

        private Vector3 _originPosition;
        private Quaternion _originRotation;
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        void Update()
        {
            float x = 0f, y = 0f;
            if (IsLookable) x = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
            if (IsLookable) y = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

            XRotation -= y;
            XRotation = Mathf.Clamp(XRotation, -80f, 80f);

            if(IsLookable) cam.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
            if(IsLookable) gameObject.transform.Rotate(Vector3.up * x);
        }
        
        public void SetLookingPosition(Vector3 position, Quaternion rotation, float duration)
        {
            _originPosition = cam.localPosition;
            _originRotation = cam.localRotation;
            cam.DOLocalMove(position, duration);
            cam.DOLocalRotateQuaternion(rotation, duration);
        }

        public void ResetLookingPosition(float duration)
        {
            cam.DOLocalMove(_originPosition, duration);
            cam.DOLocalRotateQuaternion(_originRotation, duration).OnComplete(()=>SetIsLooking(true));
        }
        void SetIsLooking(bool t) => IsLookable = t;
    }
}