using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Approacheth
{
    

    [CreateAssetMenu(fileName = "NewBuild", menuName = "ScriptableObjects/Builds/BuildData")]
    public class BuildData : ScriptableObject
    {

        [Header("Recipe Icon")] public Sprite recipeIcon;

        [Header("Unmodified time to complete (seconds)")] public float timeNoModifiers;

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