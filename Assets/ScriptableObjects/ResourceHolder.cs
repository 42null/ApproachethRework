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

    public class ResourceHolder : MonoBehaviour
    {

        public List<ResourceAndAmount> resources = new List<ResourceAndAmount>();

        public bool HasResource(string resourceName, int quantity)
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

    }
}