using SaveSystem.Core;
using UnityEngine;
using Zenject;
using Sirenix.OdinInspector;

namespace SaveSystem.Tools
{
    public class SavingSystemHelper : MonoBehaviour
    {
        private SceneSaveManager sceneSaveManager;
        [Inject]
        private void Construct(SceneSaveManager manager)
        {
            sceneSaveManager = manager;
        }

        [Button]
        public void Load()
        {
            sceneSaveManager.LoadScene();
        }

        [Button]
        public void Save()
        {
            sceneSaveManager.SaveScene();
        }
    }
}
