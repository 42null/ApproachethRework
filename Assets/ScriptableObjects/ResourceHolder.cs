using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Approacheth
{
    
    [System.Serializable]
    //TODO: Make into a Dictionary?
    public class ResourceAndAmount : ICloneable
    {
        public MaterialData resource;
        public int count;
        
        // https://stackoverflow.com/a/5359336 used for cloning
        #region ICloneable Members
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
    }

    public class ResourceHolder : MonoBehaviour
    {

        public List<ResourceAndAmount> resources = new List<ResourceAndAmount>();



        public List<ResourceAndAmount> resoucesMissingFromBuildData(BuildData checkingFor)
        {
            List<ResourceAndAmount> resourcesMissing = new List<ResourceAndAmount>();
            foreach (ResourceAndAmount checkingResourceAndAmount  in checkingFor.builtFrom)
            {
                ResourceAndAmount missingResourceAndAmount = ResourceMissing(checkingResourceAndAmount);
                if (missingResourceAndAmount.count > 0)
                {
                    Debug.Log("Missing "+missingResourceAndAmount.count+" of "+missingResourceAndAmount.resource.name);
                    resourcesMissing.Add(new ResourceAndAmount());
                }

            }
            return resourcesMissing;
        }


        public ResourceAndAmount ResourceMissing(ResourceAndAmount checkingFor)
        {
            // ResourceAndAmount returnMissing = checkingFor.Clone() as ResourceAndAmount;
            ResourceAndAmount returnMissing = new ResourceAndAmount();
            returnMissing.resource = checkingFor.resource;
            returnMissing.count = checkingFor.count;
            
            ResourceAndAmount foundResource = resources.FirstOrDefault(ra => ra.resource == checkingFor.resource);

            if (foundResource != null)
            {
                if (foundResource.count >= checkingFor.count)
                {
                    returnMissing.count = 0;
                }
                else
                {
                    returnMissing.count -= foundResource.count;
                }
            }
            return returnMissing;
        }
        
        private bool HasResource(string resourceName, int quantity)
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