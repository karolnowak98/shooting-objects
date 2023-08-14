using UnityEngine;
using Zenject;
using ShootingObjects.Game.Data;
using ShootingObjects.Game.Logic;

namespace ShootingObjects.Cubes.Logic
{
    public class CubesInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_gameConfig).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<CubesManager>().AsSingle();
            Container.BindFactory<ProjectileMono, ProjectileFactory>().FromComponentInNewPrefab(_gameConfig.ProjectilePrefab).NonLazy();
        }
    }
}