using System;
using System.Collections.Generic;
using GameEngine;
using SaveSystem.Core;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace SaveSystem.Data
{
    public class ResourceSavingManager : ISaveAble
    {
        private readonly ResourceService resourceService;
        private readonly List<Dictionary<string, string>> resources = new();

        public ResourceSavingManager(ResourceService service)
        {
            resourceService = service;
        }
        
        public List<Dictionary<string, string>> CaptureState()
        {
            resources.Clear();
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            foreach (var res in resourceService.GetResources())
            {
                var state = new Dictionary<string, string>
                {
                    {"ID", res.ID},
                    {"Amount", res.Amount.ToString()},
                    {"SaveAbleType", Convert.ToInt32(SaveAblesTypes.Resource).ToString()},
                    {"Scene", sceneIndex.ToString()}
                };
                resources.Add(state);
            }

            return resources;
        }

        public void RestoreState(List<Dictionary<string, string>> loadedData)
        {
            resources.Clear();
            var existingResources = Object.FindObjectsOfType<Resource>();
           
            foreach (var entry in loadedData)
            {
                if ((SaveAblesTypes) Convert.ToInt32(entry["SaveAbleType"]) == SaveAblesTypes.Resource)
                {
                    resources.Add(entry);
                }
            }
            foreach (var res in existingResources)
            {
                foreach (var resource in resources)
                {
                    if (res.ID == resource["ID"])
                    {
                        res.Amount = Convert.ToInt32(resource["Amount"]);
                    }
                }
            }
            resourceService.SetResources(existingResources);
        }
    }
}
