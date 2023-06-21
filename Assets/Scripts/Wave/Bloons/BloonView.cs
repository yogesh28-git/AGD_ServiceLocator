using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Bloon
{
    public class BloonView : MonoBehaviour
    {
        private BloonController controller;
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetController(BloonController bloonController) => this.controller = bloonController;

        private void Update() => controller.FollowWayPoints();

        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        public void PopBloon()
        {
            // Disable the renderer & Play Bloon Pop Animation
        }
    }
}