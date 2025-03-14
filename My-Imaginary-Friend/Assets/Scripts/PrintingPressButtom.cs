using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintingPressButtom : MonoBehaviour
{
    public KeyCode key;
    [SerializeField] private float _time;
    [SerializeField] private float _endPosition;
    private float _startPosition;

    private void Start()
    {
        _startPosition = transform.position.y;
    }
    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            transform.DOMoveY(_endPosition + transform.position.y, _time);
        }
        if (Input.GetKeyUp(key))
        {
            transform.DOMoveY(_startPosition, _time);
        }
    }
}
