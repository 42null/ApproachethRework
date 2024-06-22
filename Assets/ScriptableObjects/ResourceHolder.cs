using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Approacheth
{
    

    // //TODO: Make into a Dictionary?
    // [System.Serializable]
    // public class ResourceAndAmount : ICloneable
    // {
    //     public MaterialData resource;
    //     public int count;
    //     
    //     // https://stackoverflow.com/a/5359336 used for cloning
    //     #region ICloneable Members
    //     public object Clone()
    //     {
    //         return this.MemberwiseClone();
    //     }
    //     #endregion
    // }
    
    [System.Serializable]

    public class ResourceHolder : MonoBehaviour
    {
        // [SerializeField]
        // public Dictionary<MaterialData, int> resources;
        
        [SerializeField] private List<MaterialData> keys = new List<MaterialData>();
        [SerializeField] private List<int> values = new List<int>();

        public Dictionary<MaterialData, int> resources = new Dictionary<MaterialData, int>();

        private void OnValidate()
        {
            resources = new Dictionary<MaterialData, int>();
            for (int i = 0; i < keys.Count; i++)
            {
                resources[keys[i]] = values[i];
            }
        }

        private void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (var kvp in resources)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        private void OnAfterDeserialize()
        {
            resources = new Dictionary<MaterialData, int>();
            for (int i = 0; i < keys.Count; i++)
            {
                resources[keys[i]] = values[i];
            }
        }
        

        public Dictionary<MaterialData, int> resoucesMissingFromBuildData(BuildData checkingFor)
        {
            Dictionary<MaterialData, int> resourcesMissing = new Dictionary<MaterialData, int>();
            
            
            
            foreach (KeyValuePair<MaterialData, int> checkingResourceAndAmount in checkingFor.builtFrom)
            {
                KeyValuePair<MaterialData, int> missingResourceAndAmount = ResourceMissing(checkingResourceAndAmount.Key, checkingResourceAndAmount.Value);
                if (missingResourceAndAmount.Value > 0)
                {
                    Debug.Log("Missing "+missingResourceAndAmount.Value+" of "+missingResourceAndAmount.Key.name);
                    resourcesMissing.Add(missingResourceAndAmount.Key, missingResourceAndAmount.Value);
                }
            
            }
            return resourcesMissing;
            
        }


        //TODO: Also make accept direct KeyValuePair
        public KeyValuePair<MaterialData, int> ResourceMissing(MaterialData lookingForMaterial, int lookingForAmount)
        {
            // ResourceAndAmount returnMissing = checkingFor.Clone() as ResourceAndAmount;
            // KeyValuePair<MaterialData, int> returnMissing = new KeyValuePair<MaterialData, int>(lookingForMaterial, lookingForAmount);

            int foundResourceAmount = resources[lookingForMaterial];
            int missingResourceAmount = lookingForAmount;
            
            if (foundResourceAmount != null)
            {
                if (foundResourceAmount >= lookingForAmount)
                {
                    missingResourceAmount = 0;
                }
                else
                {
                    missingResourceAmount = lookingForAmount - foundResourceAmount;
                }
            }
            
            return  new KeyValuePair<MaterialData, int>(lookingForMaterial, missingResourceAmount);
        }
        
        
        public void useUpResources(Dictionary<MaterialData, int> removingResources)
        {
            foreach (KeyValuePair<MaterialData, int> removeResource in removingResources)
            {
                this.resources[removeResource.Key] -= removeResource.Value;
            }
        }
        
        
        // private bool HasResource(string resourceName, int quantity)
        // {
        //     foreach (ResourceAndAmount resourceAndAmount in resources)
        //     {
        //         if(resourceAndAmount.resource.name.Equals(resourceName, StringComparison.OrdinalIgnoreCase) && resourceAndAmount.count >= quantity)
        //         {
        //             return true;
        //         }
        //     }
        //     return false;
        // }

    }
}