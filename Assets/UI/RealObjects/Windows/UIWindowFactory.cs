using System;
using Approacheth.UI.RealObjects.KindWindows;
using Approacheth.UI.RealObjects.KindWindows.Prefabs;
using UnityEngine;

namespace Approacheth.UI
{

    using UnityEngine;

    public class UIWindowFactory : MonoBehaviour
    {
        public Transform canvasTransform;
        public Vector3 popupOffset = new Vector3(0, 0, 0);
        
        public enum SEGMENTS {
            MADE_FROM_RESOUCES,
            SUPPLIES,
            BUILD_BAY
        }

        public GameObject CreateWindow(UIConfig uiConfig, SpaceObject spaceObject)
        {
            Vector3 spritePosition = spaceObject.GetComponent<Transform>().position;
            
            // Convert the world position to screen space
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(spritePosition);

            screenPosition += popupOffset*2;//TODO: Make based off of size of sprite
                
            GameObject windowInstance = Instantiate(uiConfig.uiPrefab, screenPosition, Quaternion.identity, canvasTransform);
            BaseUIWindow uiWindow = windowInstance.GetComponent<IUIWindow>() as BaseUIWindow;//TODO: Do better
            
            
            if (uiWindow != null)
            {
                // Create segments 
                for (int i = 0; i < uiWindow.GetBuildSegments().Length; i++)
                {
                    GameObject segment = Instantiate(uiWindow.segmentPrefab, uiWindow.segmentHolder.transform.position, Quaternion.identity, uiWindow.segmentHolder.transform);
                    segment.name = "Built From Resources";

                    // Vector3 resourceDisplayBoxPrefabSize = uiWindow.resourceDisplayBoxPrefab.GetComponent<SpriteRenderer>().bounds.size;
                    // Debug.Log("resourceDisplayBoxPrefabSize "+resourceDisplayBoxPrefabSize.x);
                    Vector3 resourceDisplayBoxPrefabSize = new Vector3(100, 200, 0);
                    
                    for(int j = 0; j < spaceObject.resources.resources.Count; j++)
                    {
                        ResourceAndAmount resource = spaceObject.resources.resources[j];
                        
                        GameObject resourceDisplayBox = Instantiate(uiWindow.resourceDisplayBoxPrefab, segment.transform.position + new Vector3(resourceDisplayBoxPrefabSize.x*
                            (j-1),50,0), Quaternion.identity, segment.transform);
                        ResourceDisplayBox boxDataStorageScript = resourceDisplayBox.GetComponent<ResourceDisplayBox>();
                        boxDataStorageScript.symbol.text = resource.resource.symbol;
                        boxDataStorageScript.amount.text = resource.count.ToString();
                    }
                    
                    
                    uiWindow.segments.Add(segment);
                }
                // if (buildChildren.Contains(UIWindowFactory.))
                // {
                //
                // }                
                
                uiWindow.Setup(uiConfig, spaceObject);
            }
            else
            {
                Debug.LogError("The UI prefab does not implement IUIWindow interface.");
            }

            return windowInstance;
        }
    }


}