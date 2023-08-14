using UnityEngine;
using Zenject;
using ShootingObjects.Game.Data;

namespace ShootingObjects.Cubes.Logic
{
    public class ProjectileMono : MonoBehaviour
    {
        private CubesManager _cubesManager;
        private float _projectileSpeed;
        
        [Inject]
        private void Construct(CubesManager cubesManager, GameConfig gameConfig)
        {
            _cubesManager = cubesManager;
            _projectileSpeed = gameConfig.ProjectileSpeed;
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * _projectileSpeed));
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Cube"))
            {
                if(_cubesManager.TryHitCube(col.gameObject.GetInstanceID(), gameObject.GetInstanceID()))
                {
                    Destroy(gameObject);
                }
            }

            if (col.gameObject.CompareTag("Border"))
            {
                Destroy(gameObject);
            }
        }
    }
}