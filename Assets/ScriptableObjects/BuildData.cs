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

        // [Header("Built From")] 
        public Dictionary<MaterialData, int> builtFrom = new Dictionary<MaterialData, int>();
        [Header("Built From")]
        public List<MaterialData> builtFromKeys = new List<MaterialData>();
        public List<int> builtFromValues = new List<int>();

        [Header("Computed Constituents")]
        public Dictionary<MaterialData, int> constituents;
        
    public void OnBeforeSerialize()
    {
        builtFromKeys.Clear();
        builtFromValues.Clear();

        foreach (var kvp in builtFrom)
        {
            builtFromKeys.Add(kvp.Key);
            builtFromValues.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        builtFrom = new Dictionary<MaterialData, int>();

        for (int i = 0; i < builtFromKeys.Count; i++)
        {
            builtFrom[builtFromKeys[i]] = builtFromValues[i];
        }
    }

    private void OnValidate()
    {
        // constituents = new ResourceAndAmount[builtFrom.Count];
        // int i = 0;
        // foreach (ResourceAndAmount materialConstituent in builtFrom)
        // {
        //     constituents[i++] = materialConstituent;
        // }
        OnAfterDeserialize();
    }
        
    }
}