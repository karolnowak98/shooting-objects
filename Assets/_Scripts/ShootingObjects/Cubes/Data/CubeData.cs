using UnityEngine;

namespace ShootingObjects.Cubes.Data
{
    public struct CubeData
    {
        public GameObject Prefab { get; private set; }
        public Transform Transform { get; set; }
        public Vector3 Position { get; private set; }
        public Vector3 BulletSpawnPoint { get; set; }
        public int InstanceId { get; set; }
        public int Lives { get; set; }
        public float XProjectileSpawnOffset { get; private set; }
        public float MaxRotationAngle { get; private set; }
        public float MaxShotDelay { get; private set; }
        public float MaxTimeToSpawn { get; private set; }
        
        public CubeData(GameObject prefab, Vector3 position, int lives, float xProjectileSpawnOffset, float maxRotationAngle, float maxShotDelay, float maxTimeToSpawn)
        {
            Prefab = prefab;
            Transform = null;
            Position = position;
            BulletSpawnPoint = position + new Vector3(xProjectileSpawnOffset, 0f, 0f);
            InstanceId = 0;
            Lives = lives;
            XProjectileSpawnOffset = xProjectileSpawnOffset;
            MaxRotationAngle = maxRotationAngle;
            MaxShotDelay = maxShotDelay;
            MaxTimeToSpawn = maxTimeToSpawn;
        }
    }
}