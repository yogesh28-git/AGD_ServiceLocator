using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Player
{
    public class MonkeyView : MonoBehaviour
    {
        private MonkeyController controller;

        public void SetController(MonkeyController controller) => this.controller = controller;

        public void PlayShootAnimation()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check If Bloon.
            // Tell Controller that Bloon is in range.
        }
    } 
}
