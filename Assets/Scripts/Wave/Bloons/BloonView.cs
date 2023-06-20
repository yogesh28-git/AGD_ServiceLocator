using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Bloon
{
    public class BloonView : MonoBehaviour
    {
        private BloonController bloonController;

        public void SetController(BloonController bloonController) => this.bloonController = bloonController;

        public void PopBloon()
        {
            // Disable the renderer & Play Pop VFX
        }
    }
}
