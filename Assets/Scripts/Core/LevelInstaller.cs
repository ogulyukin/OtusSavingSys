using GameEngine;
using SaveSystem.Core;
using SaveSystem.Data;
using SaveSystem.Tools;
using UnityEngine;
using Zenject;

namespace Core
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private UnitsPrefabStorage prefabStorage;
        [SerializeField] private Transform unitManagerContainer;
        [SerializeField] private SavingSystemHelper helper;
        public override void InstallBindings()
        {
            Container.Bind<UnitManager>().AsSingle();
            Container.Bind<Transform>().FromInstance(unitManagerContainer).AsSingle();
            Container.Bind<ResourceService>().AsSingle();
            Container.Bind<UnitsPrefabStorage>().FromInstance(prefabStorage).AsSingle();
            Container.Bind<SavingSystemHelper>().FromInstance(helper).AsSingle();
            Container.Bind<SceneSaveManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            InstallISaveAbles();
        }

        private void InstallISaveAbles()
        {
            Container.BindInterfacesTo<UnitSavingManager>().AsSingle();
            Container.BindInterfacesTo<ResourceSavingManager>().AsSingle();
        }
    }
}