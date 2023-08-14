using UnityEngine;
using System;
using System.Collections.Generic;
using Global.Logic;
using Zenject;
using ShootingObjects.Cubes.Data;
using ShootingObjects.Game.Data;
using ShootingObjects.Game.Logic;

namespace ShootingObjects.Cubes.Logic
{
    public class CubesManager : IDisposable, ITickable
    {
        private readonly List<ICube> _cubes = new();
        
        private GameManager _gameManager;
        private GameConfig _gameConfig;
        private ProjectileFactory _projectileFactory;
        
        private ICube FindCubeById(int cubeId) => _cubes.Find(cube => cube.InstanceId == cubeId);
        
        [Inject]
        private void Construct(GameManager gameManager, GameConfig gameConfig, ProjectileFactory projectileFactory)
        {
            _gameManager = gameManager;
            _gameConfig = gameConfig;
            _projectileFactory = projectileFactory;
            
            _gameManager.OnStartGame += SpawnCubes;
            _gameManager.OnEndGame += ClearCubes;
        }
        
        public void Dispose()
        {
            _gameManager.OnStartGame -= SpawnCubes;
            _gameManager.OnEndGame -= ClearCubes;
        }
        
        public void Tick()
        {
            foreach (var cube in _cubes)
            {
                cube.Tick(Time.deltaTime);
            }
        }

        public bool TryHitCube(int cubeId, int projectileId)
        {
            var cube = FindCubeById(cubeId);
            if (cube == null) return false;

            if (!cube.TryHitCube(projectileId)) return false;

            _cubes.Remove(cube);

            if (!cube.HasLives)
            {
                if (_cubes.Count == 1)
                {
                    _gameManager.EndGame();
                    _cubes.Clear();
                }
            }
            
            else
            {
                SpawnCube(cube.Lives, true);
            }

            return true;
        }

        private void SpawnCubes(int numberOfCubes)
        {
            for (var i = 0; i < numberOfCubes; i++)
            {
                SpawnCube(_gameConfig.CubeLives, false);
            }
        }
        
        private void SpawnCube(int lives, bool isRespawned)
        {
            var position = GetRandomSpawnPosition();
            var cubeData = new CubeData(_gameConfig.CubePrefab, position, lives, _gameConfig.XProjectileSpawnOffset, 
                _gameConfig.MaxRotationAngle, _gameConfig.MaxShotDelay, _gameConfig.MaxTimeToSpawn);
            
            _cubes.Add(new Cube(cubeData, _projectileFactory, isRespawned));
        }
        
        private Vector3 GetRandomSpawnPosition()
        {
            var position = Vector3.zero;
            position.Random(-GetHalfDimensions(), GetHalfDimensions());
            return position;
        }

        private Vector3 GetHalfDimensions()
        {
            var halfX = _gameConfig.FieldDimensions.x * 0.5f;
            var halfZ = _gameConfig.FieldDimensions.y * 0.5f;
            return new Vector3(halfX, 0f, halfZ);
        }

        private void ClearCubes() => _cubes.Clear();
    }
}