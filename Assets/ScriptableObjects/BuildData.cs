using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Approacheth
{
    
    [System.Serializable]
    // public class Recipe
    // {
    //     public Image icon;
    // }

    
    [CreateAssetMenu(fileName = "NewBuild", menuName = "ScriptableObjects/Builds/BuildData")]
    public class BuildData : ScriptableObject
    {

        // [Header("Recipe")]
        // public Recipe recipe;

        [Header("Built From")] public List<ResourceAndAmount> builtFrom = new List<ResourceAndAmount>();

        [Header("Computed Constituents")]
        public ResourceAndAmount[] constituents;
        

        

        public void OnValidate()
        {
            // constituents = new ResourceAndAmount[builtFrom.Count];
            // int i = 0;
            // foreach (ResourceAndAmount materialConstituent in builtFrom)
            // {
            //     constituents[i++] = materialConstituent;
            // }
        }
    }
}