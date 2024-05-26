using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Approacheth
{
    [System.Serializable]
    public class ResourceAndAmount
    {
        public MaterialData resource;
        public int count;
    }

    [CreateAssetMenu(fileName = "newResourceHolder", menuName = "ScriptableObjects/SpaceObject/ResourceHolder")]
    public class ResourceHolder : ScriptableObject
    {

        [Header("Resources")]
        public List<ResourceAndAmount> resources = new List<ResourceAndAmount>();

        public bool hasResource(string resourceName, int quantity)
        {
            foreach (ResourceAndAmount resourceAndAmount in resources)
            {
                if(resourceAndAmount.resource.name.Equals(resourceName, StringComparison.OrdinalIgnoreCase) && resourceAndAmount.count >= quantity)
                {
                    return true;
                }
            }
            return false;
        }

        public void OnEnable()
        {
        }
    }
}