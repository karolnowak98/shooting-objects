using UnityEngine;
using Zenject;

namespace ShootingObjects.Cubes.Logic
{
    public class ProjectileFactory : PlaceholderFactory<ProjectileMono>
    {
        public ProjectileMono CreateWithPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            var projectile = base.Create();
            var transform = projectile.transform;
            transform.position = position;
            transform.rotation = rotation;
            return projectile;
        }
    }
}