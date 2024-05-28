using System;
using System.Collections;
using System.Collections.Generic;
using Approacheth.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Approacheth
{
    
    public class SpaceObject : MonoBehaviour
    {
        public UIConfig uiConfig;
        private UIWindowFactory _uiFactory;
        
        
        
        public ResourceHolder resources;
        
        public Sprite icon;
        
        public String synopsis;

        void Start()
        {
            // Find the UI factory in the scene
            _uiFactory = FindObjectOfType<UIWindowFactory>();
        }

        void OnMouseDown()
        {
            // Open the UI window when the object is clicked
            if (_uiFactory != null)
            {
                _uiFactory.CreateWindow(uiConfig, this);
            }
        }
    }
    
    public class Asteroid : SpaceObject
    {
        public enum TYPES { CHONDRITE, STONY, METALIC }

        public TYPES type;
    }
    
    // [CreateAssetMenu(fileName = "Spaceship", menuName = "Space Objects/Spaceship")]
    // public class Spaceship : SpaceObject
    // {
    //     public float maxspeed;
    // }
    //
    

}