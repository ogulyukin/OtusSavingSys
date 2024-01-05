using System.Collections.Generic;
using GameEngine;
using SaveSystem.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SaveSystem.Core
{
    public enum SaveAblesTypes
    {
        Unit,
        Resource
    }
    
    public class SceneSaveSystemManager
    {
        private readonly List<ISaveAble> saveAbles;
        private readonly SavingSystem savingSystem;
        private readonly UnitManager unitManager;
        private readonly ResourceService resourceService;
        private readonly UnitsPrefabStorage prefabStorage;

        public SceneSaveSystemManager(UnitManager manager, ResourceService service, UnitsPrefabStorage storage, SavingSystem system,  Transform unitsRoot)
        {
            savingSystem = system;
            unitManager = manager;
            resourceService = service;
            manager.SetContainer(unitsRoot);
            var unitSaveAble = new UnitSavingEntity(manager, storage, unitsRoot);
            var resourceSaveAble = new ResourceSavingEntity(service);
            saveAbles = new List<ISaveAble>()
            {
                unitSaveAble, resourceSaveAble
            };
        }

        public void RestoreScene()
        {
            if (!savingSystem.RestoreState(saveAbles))
            {
                IfNoSaveFileExist();
                return;
            }
            Debug.Log("Scene restored");
        }

        public void CaptureScene()
        {
            savingSystem.CaptureState(saveAbles);
            Debug.Log("Scene saved");
        }
        
        private void IfNoSaveFileExist()
        {
            unitManager.SetupUnits(Object.FindObjectsOfType<Unit>());
            resourceService.SetResources(Object.FindObjectsOfType<Resource>());
            Debug.Log("No scene save file found!");
        }
    }
}
