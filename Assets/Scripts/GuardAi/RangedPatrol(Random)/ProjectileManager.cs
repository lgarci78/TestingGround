using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.TakeHit();

                // Destroy the projectile
                Destroy(transform.parent.gameObject);

                // Check if the player is dead and handle it as needed
                //if (isDead)
                //{
                //    // Handle player death (e.g., play death animation, end game, etc.)
                //}
            }
        }
    }
    public void DestroyProjectile(GameObject projectile, float delay)
    {
        Destroy(projectile, delay);
    }
}
