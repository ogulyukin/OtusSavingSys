using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem.Core
{
    public class SavingSystem
    {
        private readonly ISaverLoader saveLoader;
        private readonly List<Dictionary<string, string>> otherSceneEntries = new() ;


        public SavingSystem(ISaverLoader sl)
        {
            saveLoader = sl;
        }

        public bool RestoreState(List<ISaveAble> saveAbles)
        {
            var loadedData = saveLoader.Load();
            if (!loadedData.Any())
            {
                Debug.Log("SS: no data loaded!");
                return false;
            }
            var currentSceneData = SortLoadedData(loadedData);
            Debug.Log($"SS loaded: {loadedData.Count}, current scene: {currentSceneData.Count}, other scene: {otherSceneEntries.Count}");
            if (!currentSceneData.Any())
            {
                Debug.Log("SS: no data from current scene loaded!");
                return false;
            }
            
            foreach (var saveAble in saveAbles)
            {
                saveAble.RestoreState(currentSceneData);
            }

            return true;
        }

        private List<Dictionary<string, string>> SortLoadedData(List<Dictionary<string, string>> loadedData)
        {
            otherSceneEntries.Clear();
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            var currentSceneData = new List<Dictionary<string, string>>();
            foreach (var data in loadedData)
            {
                var index = data["Scene"];
                if (Convert.ToInt32(index) != sceneIndex)
                {
                    otherSceneEntries.Add(data);
                    continue;
                }

                currentSceneData.Add(data);
            }

            return currentSceneData;
        }

        public void CaptureState(List<ISaveAble> saveAbles)
        {
            var state = new List<Dictionary<string, string>>();
            
            foreach (var saveAble in saveAbles)
            {
                state.AddRange(saveAble.CaptureState());
            }
            state.AddRange(otherSceneEntries);
            Debug.Log($"SS: captured {state.Count} entries");
            saveLoader.Save(state);
        }
    }
}
