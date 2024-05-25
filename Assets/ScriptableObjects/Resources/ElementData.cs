using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Approacheth
{
    [CreateAssetMenu(fileName = "NewElement", menuName = "ScriptableObjects/Resources/Element Data")]
    public class ElementData : ScriptableObject
    {
        // [Header("Resource Graphics")]
        // public Sprite resourceImage;
        
        [Header("Name")]
        public string resourceName;

        [Header("Symbol")]
        public string symbol;
        
        [Header("Atomic Number")]
        public string atomicNumber;

        [Header("Approximate Atomic Weight")]
        public float atomicWeight;
    }
}