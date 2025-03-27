using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class PrintingPressButtom : MonoBehaviour
    {
        public KeyCode key;

        [SerializeField] PrintingMachine machine;
        [SerializeField] private float _time;
        [SerializeField] private float _endPosition;

        private float _startPosition;

        private void Start()
        {
            _startPosition = transform.position.y;
        }
        private void Update()
        {
            if (Input.GetKeyDown(key) && machine.isActive)
            {
                transform.DOMoveY(_endPosition + transform.position.y, _time);
                machine.InputSymbol();
            }
            if (Input.GetKeyUp(key) && machine.isActive)
            {
                transform.DOMoveY(_startPosition, _time);
            }
        }
    }
}