using UnityEngine;

namespace SaveSystem.Data
{
    public class UnitsPrefabStorage : MonoBehaviour
    {
        [SerializeField] private GameObject[] unitPrefabs;

        public GameObject GetUnitPrefabByName(string prefabName)
        {
            foreach (var prefab in unitPrefabs)
            {
                if (prefab.name == prefabName)
                {
                    return prefab;
                }
            }
            
            throw new System.Exception($"No prefab was found bu name: {prefabName}");
        }
    }
}
