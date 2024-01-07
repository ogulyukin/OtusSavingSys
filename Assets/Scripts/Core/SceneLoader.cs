using GameEngine;
using SaveSystem.Core;
using UnityEngine;
using Zenject;

namespace Core
{
    public class SceneLoader : IInitializable
    {
        private readonly SceneSaveManager saveLoadManager;
        private readonly UnitManager unitManager;
        private readonly ResourceService resourceService;

        public SceneLoader(SceneSaveManager sceneSaveManager, UnitManager manager, ResourceService service, Transform unitsRoot)
        {
            saveLoadManager = sceneSaveManager;
            unitManager = manager;
            resourceService = service;
            manager.SetContainer(unitsRoot);
        }
        
        private void IfLoadSceneFailed()
        {
            unitManager.SetupUnits(Object.FindObjectsOfType<Unit>());
            resourceService.SetResources(Object.FindObjectsOfType<Resource>());
            Debug.Log("No scene save file found!");
        }

        public void Initialize()
        {
            if (!saveLoadManager.LoadScene())
            {
                IfLoadSceneFailed();
            }
        }
    }
}
