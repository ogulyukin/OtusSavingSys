using System.Collections.Generic;
using SaveSystem.Data;
using UnityEngine;

namespace SaveSystem.Core
{  
    public class SceneSaveManager
    {
        private readonly List<ISaveAble> saveAbles;
        private readonly SavingSystem savingSystem;
        private readonly UnitsPrefabStorage prefabStorage;

        public SceneSaveManager(SavingSystem system, List<ISaveAble> saveAblesList)
        {
            savingSystem = system;
            saveAbles = saveAblesList;
            Debug.Log($"Found SaveAbles: {saveAbles.Count}");
        }

        public bool LoadScene()
        {
            if (!savingSystem.RestoreState(saveAbles))
            {
                return false;
            }
            Debug.Log("Scene restored");
            return true;
        }

        public void SaveScene()
        {
            savingSystem.CaptureState(saveAbles);
            Debug.Log("Scene saved");
        }
    }
}
