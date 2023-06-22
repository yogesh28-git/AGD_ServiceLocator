using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocator.Bloon;

namespace ServiceLocator.Player
{
    public class MonkeyView : MonoBehaviour
    {
        private MonkeyController controller;
        private CircleCollider2D rangeTriggerCollider;
        private Animator monkeyAnimator;

        private void Awake()
        {
            rangeTriggerCollider =  GetComponent<CircleCollider2D>();
            monkeyAnimator = GetComponent<Animator>();
        }

        public void SetController(MonkeyController controller) => this.controller = controller;

        public void SetTriggerRadius(float radiusToSet) => rangeTriggerCollider.radius = radiusToSet;

        public void PlayAnimation(MonkeyAnimation animationToPlay) => monkeyAnimator.Play(animationToPlay.ToString(), 0);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<BloonView>() != null)
                controller.BloonEnteredRange(collision.GetComponent<BloonView>().Controller);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<BloonView>() != null)
                controller.BloonExitedRange(collision.GetComponent<BloonView>().Controller);
        }
    }

    public enum MonkeyAnimation
    {
        Idle,
        Shoot
    }
}
