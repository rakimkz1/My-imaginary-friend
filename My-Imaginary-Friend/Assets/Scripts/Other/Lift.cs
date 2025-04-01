using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Lift : MonoBehaviour
    {
        [SerializeField] float speed;

        private Animator _anim;
        private bool isReady;
        private GameObject _player;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public void BootsUpdate()
        {
            if (isReady)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                _player.transform.position += Vector3.up * speed * Time.deltaTime;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                _player = other.gameObject;
                _anim.SetTrigger("Move");
            }
        }

        public void CarryLift()
        {
            isReady = true;
            _player.GetComponent<CharacterController>().enabled = false;
        }
    }
}
