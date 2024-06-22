using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Approacheth
{
    public class TimedActionPrefab : MonoBehaviour
    {
        public float timeRemaining;
        public Image icon;
        public Text timeRemainingDisplay;
        
        
        public delegate void ConditionMetEventHandler(BuildData recipe);
        // Define an event based on the delegate.
        public event ConditionMetEventHandler OnConditionTimeOverMet;

        private bool notAlreadyTriggered = true;
        public BuildData buildData;
        
        void Start()
        {
        
        }
        
        // Update is called once per frame
        void Update()
        {
            if (notAlreadyTriggered)
            {
                if (timeRemaining > 0)
                {
                    this.timeRemaining -= Time.deltaTime;
                }
                else
                {
                    this.timeRemaining = 0f;
                    notAlreadyTriggered = false;
                    // Trigger the event if there are subscribers.
                    if (OnConditionTimeOverMet != null)
                    {
                        OnConditionTimeOverMet(buildData);
                    }
                }
                timeRemainingDisplay.text = $"{this.timeRemaining:F2}";
            }
        }
    }
}
