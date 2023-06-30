using UnityEngine;
using ServiceLocator.Bloon;

namespace ServiceLocator.Player
{
    public class MonkeyView : MonoBehaviour
    {
        private MonkeyController controller;
        private CircleCollider2D rangeTriggerCollider;
        private Animator monkeyAnimator;
        [SerializeField] private SpriteRenderer rangeSpriteRenderer;

        private void Awake()
        {
            rangeTriggerCollider =  GetComponent<CircleCollider2D>();
            monkeyAnimator = GetComponent<Animator>();
        }

        public void SetController(MonkeyController controller) => this.controller = controller;

        public void SetTriggerRadius(float radiusToSet)
        {
            rangeTriggerCollider.radius = radiusToSet;
            rangeSpriteRenderer.transform.localScale = new Vector3(radiusToSet, radiusToSet, 1);
            MakeRangeVisible(false);
        }

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

        public void MakeRangeVisible(bool makeVisible) => rangeSpriteRenderer.color = makeVisible ? new Color(1, 1, 1, 0.25f) : new Color(1, 1, 1, 0);

        public bool IsInRange(Vector2 poistionToCheck)
        {
            float distance = Vector2.Distance(rangeTriggerCollider.bounds.center, poistionToCheck);
            return distance < rangeTriggerCollider.bounds.extents.x;
        }
    }

    public enum MonkeyAnimation
    {
        Idle,
        Shoot
    }
}
