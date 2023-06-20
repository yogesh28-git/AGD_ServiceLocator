using UnityEngine;

namespace ServiceLocator.Player.Projectile
{
    public class ProjectileView : MonoBehaviour
    {
        private ProjectileController controller;

        public void SetController(ProjectileController controller) => this.controller = controller;

        private void Update()
        {
            controller?.UpdateProjectileMotion();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if triggered with Bloons
            // If yes, inform Controller
        }
    }
}