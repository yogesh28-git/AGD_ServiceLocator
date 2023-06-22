using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Bloon
{
    public class BloonView : MonoBehaviour
    {
        private BloonController controller;
        public BloonController Controller { get => controller; set => controller = value; }

        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update() => controller.FollowWayPoints();

        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        public void PopBloon()
        {
            spriteRenderer.sprite = null;
            // TODO:
            // Disable the renderer & Play Bloon Pop Animation
        }
    }
}