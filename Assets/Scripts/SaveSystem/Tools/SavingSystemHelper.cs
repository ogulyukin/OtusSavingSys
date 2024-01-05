using SaveSystem.Core;
using UnityEngine;
using Zenject;
using Sirenix.OdinInspector;

namespace SaveSystem.Tools
{
    public class SavingSystemHelper : MonoBehaviour
    {
        private SceneSaveSystemManager sceneSaveSystemManager;
        [Inject]
        private void Construct(SceneSaveSystemManager manager)
        {
            sceneSaveSystemManager = manager;
        }

        private void Start()
        {
            sceneSaveSystemManager.RestoreScene();
        }
        
        [Button]
        public void Load()
        {
            sceneSaveSystemManager.RestoreScene();
        }

        [Button]
        public void Save()
        {
            sceneSaveSystemManager.CaptureScene();
        }
    }
}
