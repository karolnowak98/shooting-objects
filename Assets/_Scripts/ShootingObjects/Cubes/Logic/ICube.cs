using UnityEngine;

namespace ShootingObjects.Cubes.Logic
{
    public interface ICube
    {
        Transform Transform { get; }
        int InstanceId { get; }
        int Lives { get; }
        bool HasLives { get; }

        void Tick(float deltaTime);
        bool TryHitCube(int projectileId);
    }
}