using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Scripts
{
    public class PlayerObjectUse : MonoBehaviour
    {
        public bool CanUse = true;
        public bool isUsingSomething;

        [SerializeField] float _UseDistance;

        private IUsable _nowUsingObject;
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if(Input.GetKeyDown(KeyCode.E) && hit.collider != null && hit.collider.gameObject.GetComponent<IActivatable>() != null && CanUse)
            {
                hit.collider.gameObject.GetComponent<IActivatable>().Activate(gameObject);
                if(hit.collider.gameObject.GetComponent<IUsable>() != null)
                {
                    CanUse = false;
                    isUsingSomething = true;
                    _nowUsingObject = hit.collider.gameObject.GetComponent<IUsable>();
                }
            }
            if(isUsingSomething && Input.GetMouseButtonDown(1))
            {
                _nowUsingObject.Reset(gameObject);
                CanUse = true;
                isUsingSomething = false;
            }
        }
    }
}
