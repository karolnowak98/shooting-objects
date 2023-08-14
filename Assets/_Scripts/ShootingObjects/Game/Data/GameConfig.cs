using UnityEngine;

namespace ShootingObjects.Game.Data
{
    [CreateAssetMenu(menuName = "Configs/Game Config", fileName = "Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int[] _cubesToSpawn;
        [SerializeField] private Vector2 _fieldDimensions;
        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _maxRotationAngle;
        [SerializeField] private float _maxShotDelay;
        [SerializeField] private float _maxTimeToSpawn;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _xProjectileSpawnOffset;
        [SerializeField] private int _cubeLives;

        public int[] CubesToSpawn => _cubesToSpawn;
        public Vector2 FieldDimensions => _fieldDimensions;
        public GameObject CubePrefab => _cubePrefab;
        public GameObject ProjectilePrefab => _projectilePrefab;
        public float MaxRotationAngle => _maxRotationAngle;
        public float MaxShotDelay => _maxShotDelay;
        public float MaxTimeToSpawn => _maxTimeToSpawn;
        public float ProjectileSpeed => _projectileSpeed;
        public float XProjectileSpawnOffset => _xProjectileSpawnOffset;
        public int CubeLives => _cubeLives;
    }
}