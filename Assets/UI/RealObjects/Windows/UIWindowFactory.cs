using System;
using UnityEngine;

namespace Approacheth.UI
{

    using UnityEngine;

    public class UIWindowFactory : MonoBehaviour
    {
        public Transform canvasTransform;

        public void CreateWindow(UIConfig uiConfig, SpaceObject spaceObject)
        {
            Debug.Log("uiConfig.uiPrefab",uiConfig.uiPrefab);
            GameObject windowInstance = Instantiate(uiConfig.uiPrefab, canvasTransform);
            IUIWindow uiWindow = windowInstance.GetComponent<IUIWindow>();//@!@@@
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