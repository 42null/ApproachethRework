using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Approacheth
{
    
    [System.Serializable]
    public class ElementConstituent
    {
        public ElementData element;
        public int count;
    }

    [CreateAssetMenu(fileName = "NewMaterial", menuName = "ScriptableObjects/Resources/Material Data")]
    public class MaterialData : ScriptableObject
    {
        
        [Header("Name")]
        public string resourceName;

        [Header("Constituents")]
        public ElementConstituent[] constituents;

        [Header("Computed Symbol")]
        public string symbol = "";
        
        [Header("Computed Molecular Weight")]
        public float atomicWeight = -1;


        public void OnEnable()
        {

        }

        public void OnValidate()
        {
            // Calculate atomic weight
            // Calculate symbol
            float tmpWeight = 0.0f;
            string tmpSymbol = "";
            
            foreach (ElementConstituent constituent in constituents)
            {
                tmpWeight += constituent.element.atomicWeight * constituent.count;
                tmpSymbol += constituent.element.symbol + (constituent.count>1? constituent.count : "");
            }
            atomicWeight = tmpWeight;
            symbol       = tmpSymbol;
            
        }
    }
}