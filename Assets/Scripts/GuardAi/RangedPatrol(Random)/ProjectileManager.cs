using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public void DestroyProjectile(GameObject projectile, float delay)
    {
        Destroy(projectile, delay);
    }
}
