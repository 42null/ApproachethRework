using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Approacheth
{
    
    [CreateAssetMenu(fileName = "NewSpaceObject", menuName = "ScriptableObjects/SpaceObject/Core")]
    public class SpaceObject : ScriptableObject
    {

        [Header("HeldResources")]
        public ResourceHolder resources;
        


        public void OnEnable()
        {
        }
    }
}