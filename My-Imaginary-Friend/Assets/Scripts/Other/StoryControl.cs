using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class StoryControl : MonoBehaviour
    {
        public static StoryControl Instance;
        public ScenesName scene;

        [SerializeField] GameObject player;
        [SerializeField] Vector3 liftHight;
        [SerializeField] float liftDuraction;
        [SerializeField] GameObject lift;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            if (scene == ScenesName.SecondScene)
            {
                player.GetComponent<PlayerMovement>().IsMovable = false;
                player.GetComponent<CharacterController>().enabled = false;
                lift.transform.DOMove(lift.transform.position + liftHight, liftDuraction).SetEase(Ease.Linear).OnComplete(()=>End());
                player.transform.DOMove(player.transform.position + liftHight, liftDuraction).SetEase(Ease.Linear);
            }
        }
        void End()
        {
            player.GetComponent<PlayerMovement>().IsMovable = true;
            player.GetComponent<CharacterController>().enabled = true;
            lift.GetComponent<Animator>().SetTrigger("Open");
        }
    }
    public enum ScenesName
    {
        FirstScene,
        SecondScene
    }
}
