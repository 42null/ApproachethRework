using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Approacheth
{
    
    [System.Serializable]
    public class MaterialConstituent
    {
        public MaterialData material;
        public int count;
    }
    
    [System.Serializable]
    public class Recipe
    {
        public Image icon;
    }

    
    [CreateAssetMenu(fileName = "NewBuild", menuName = "ScriptableObjects/Builds/BuildData")]
    public class BuildData : ScriptableObject
    {

        [Header("Recipe")]
        public Recipe recipe;
        
        [Header("Built From")]
        public List<MaterialConstituent> builtFrom;

        [Header("Computed Constituents")]
        public MaterialConstituent[] constituents;
        

        

        public void OnValidate()
        {
            constituents = new MaterialConstituent[builtFrom.Count];
            int i = 0;
            foreach (MaterialConstituent materialConstituent in builtFrom)
            {
                constituents[i++] = materialConstituent;
            }
        }
    }
}