using System;
using UnityEngine;

namespace Approacheth.UI
{

    using UnityEngine;

    public class UIWindowFactory : MonoBehaviour
    {
        public Transform canvasTransform;

        public Vector3 popupOffset = new Vector3(0, 0, 0);
    
        public void CreateWindow(UIConfig uiConfig, SpaceObject spaceObject)
        {
            Vector3 spritePosition = spaceObject.GetComponent<Transform>().position;
            
            // Convert the world position to screen space
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(spritePosition);

            screenPosition += popupOffset*2;//TODO: Make based off of size of sprite
                
            GameObject windowInstance = Instantiate(uiConfig.uiPrefab, screenPosition, Quaternion.identity, canvasTransform);
            IUIWindow uiWindow = windowInstance.GetComponent<IUIWindow>();
            
            
            if (uiWindow != null)
            {
                uiWindow.Setup(uiConfig, spaceObject);
            }
            else
            {
                Debug.LogError("The UI prefab does not implement IUIWindow interface.");
            }
        }
    }


}