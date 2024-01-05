using System;
using System.Collections.Generic;
using GameEngine;
using SaveSystem.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace SaveSystem.Data
{
    public class UnitSavingEntity : ISaveAble
    {
        private readonly UnitManager unitsManager;
        private readonly UnitsPrefabStorage prefabStorage;
        private readonly List<Dictionary<string, string>> units = new();
        private Transform unitsRoot;

        public UnitSavingEntity(UnitManager manager, UnitsPrefabStorage storage, Transform root)
        {
            unitsManager = manager;
            prefabStorage = storage;
            unitsRoot = root;
        }

        public List<Dictionary<string, string>> CaptureState()
        {
            units.Clear();
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            foreach (var unit in unitsManager.GetAllUnits())
            {
                var state = new Dictionary<string, string>
                {
                    {"PositionX", unit.Position.x.ToString()},
                    {"PositionY", unit.Position.y.ToString()},
                    {"PositionZ", unit.Position.z.ToString()},
                    {"RotationX", unit.Rotation.x.ToString()},
                    {"RotationY", unit.Rotation.y.ToString()},
                    {"RotationZ", unit.Rotation.z.ToString()},
                    {"HP", unit.HitPoints.ToString()},
                    {"Type", unit.Type},
                    {"SaveAbleType", Convert.ToInt32(SaveAblesTypes.Unit).ToString()},
                    {"Scene", sceneIndex.ToString()}
                };
                units.Add(state);
            }

            return units;
        }

        public void RestoreState(List<Dictionary<string, string>> loadedData)
        {
            DestroyAllExistingUnits();
            InitUnitsList(loadedData);

            var unitsList = new List<Unit>();
            foreach (var state in units)
            {
                var unitInstance = Object.Instantiate(prefabStorage.GetUnitPrefabByName(state["Type"]), unitsRoot, true);
                unitInstance.transform.position = new Vector3(float.Parse(state["PositionX"]),
                    float.Parse(state["PositionY"]),float.Parse(state["PositionZ"]));
                unitInstance.transform.eulerAngles = new Vector3(float.Parse(state["RotationX"]),
                    float.Parse(state["RotationY"]),float.Parse(state["RotationZ"]));
                var unit = unitInstance.GetComponent<Unit>();
                unit.HitPoints = Convert.ToInt32(state["HP"]);
                unitsList.Add(unit);
            }
            unitsManager.SetupUnits(unitsList);
        }

        private void InitUnitsList(List<Dictionary<string, string>> loadedData)
        {
            units.Clear();
            foreach (var data in loadedData)
            {
                if ((SaveAblesTypes) Convert.ToInt32(data["SaveAbleType"]) == SaveAblesTypes.Unit)
                {
                    units.Add(data);
                }
            }
        }

        private void DestroyAllExistingUnits()
        {
            foreach (var unit in unitsManager.GetAllUnits())
            {
                //Clearing unitManager if scene was not fully reloaded
                unitsManager.DestroyUnit(unit);
            }
            var existingUnits = Object.FindObjectsOfType<Unit>();
            foreach (var unit in existingUnits)
            {
                Object.Destroy(unit.gameObject);
            }
        }
    }
}
