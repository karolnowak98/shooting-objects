using UnityEngine;
using System.Collections.Generic;
using ShootingObjects.Cubes.Data;

namespace ShootingObjects.Cubes.Logic
{
    public class Cube : ICube
    {
        private readonly Dictionary<int, GameObject> _projectiles = new();
        private readonly ProjectileFactory _projectileFactory;
        private CubeData _cubeData;
        private float _timeLeftToShot;
        private float _timeLeftToSpawn;
        private float _yRotation;
        private bool _isSpawning;

        public Transform Transform => _cubeData.Transform;
        public int InstanceId => _cubeData.InstanceId;
        public int Lives => _cubeData.Lives;
        public bool HasLives => Lives > 0;

        public Cube(CubeData cubeData, ProjectileFactory projectileFactory, bool isRespawned)
        {
            _cubeData = cubeData;
            _projectileFactory = projectileFactory;
            
            if (isRespawned)
            {
                ResetSpawnTimer();
            }
            
            else
            {
                SpawnCube();
            }
            
            ResetShotTimer();
        }
        
        public void Tick(float deltaTime)
        {
            HandleSpawning(deltaTime);

            if (_cubeData.Transform == null) return;

            HandleShooting(deltaTime);
        }
        
        private void HandleSpawning(float deltaTime)
        {
            if (_isSpawning)
            {
                _timeLeftToSpawn -= deltaTime;

                if (_timeLeftToSpawn > 0) return;
                
                SpawnCube();
                _isSpawning = false;
            }
        }

        private void HandleShooting(float deltaTime)
        {
            _timeLeftToShot -= deltaTime;

            if (_timeLeftToShot > 0) return;
            
            _cubeData.Transform.Rotate(0, _yRotation, 0);
            
            var projectile = _projectileFactory.CreateWithPositionAndRotation(_cubeData.BulletSpawnPoint, _cubeData.Transform.rotation).gameObject;
            _projectiles.Add(projectile.GetInstanceID(), projectile);
            
            ResetShotTimer();
        }
        
        public bool TryHitCube(int projectileId)
        {
            if (_projectiles.ContainsKey(projectileId)) return false;
            
            _cubeData.Lives--;
            Object.Destroy(_cubeData.Transform.gameObject);
            return true;
        }

        private void SpawnCube()
        {
            var cube = Object.Instantiate(_cubeData.Prefab, _cubeData.Position, Quaternion.identity);
            _cubeData.Transform = cube.transform;
            _cubeData.BulletSpawnPoint = _cubeData.Transform.position + new Vector3(_cubeData.XProjectileSpawnOffset, 0f, 0f);
            _cubeData.InstanceId = cube.GetInstanceID();
        }
        
        private void ResetSpawnTimer()
        {
            _timeLeftToSpawn = _cubeData.SpawnDelay;
            _isSpawning = true;
        }

        private void ResetShotTimer()
        {
            _yRotation = Random.Range(0, _cubeData.MaxRotationAngle);
            _timeLeftToShot = Random.Range(0, _cubeData.MaxShotDelay);
        }
    }
}