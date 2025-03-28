using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] PlayerMovement _playerMovement;
        [SerializeField] Lift _lift;
        private void Update()
        {
            _playerMovement.BootsUpdate();
            _lift.BootsUpdate();
        }
    }
}
