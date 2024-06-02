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
        public GameObject segmentPrefab;
        public GameObject resourceDisplayBoxPrefab;
        public GameObject buildBayPrefab;
        public GameObject buildableRecipePrefab;


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

            screenPosition += popupOffset*2; //TODO: Make based off of size of sprite
                
            GameObject windowInstance = Instantiate(uiConfig.uiPrefab, screenPosition, Quaternion.identity, canvasTransform);
            BaseUIWindow uiWindow = windowInstance.GetComponent<IUIWindow>() as BaseUIWindow;//TODO: Do better
            
            
            if (uiWindow != null)
            {
                // Store to avoid repeat calling
                UIWindowFactory.SEGMENTS[] buildSegments = uiWindow.GetBuildSegments();
                // uiWindow.segmentPrefab.transform.up-uiWindow.segmentPrefab.transform.;
                Vector3 segmentOffset = new Vector3(0,0, 0);
                
                // Create segments 
                for(int i = 0; i < buildSegments.Length; i++)
                {
                    GameObject segment = Instantiate(segmentPrefab, uiWindow.segmentHolder.transform.position + segmentOffset, Quaternion.identity, uiWindow.segmentHolder.transform);
                    UIWindowFactory.SEGMENTS segmentType = buildSegments[i];
                    if (segmentType == SEGMENTS.MADE_FROM_RESOUCES)
                    {
                        
                        segment.name = "Built From Resources";

                        // Vector3 resourceDisplayBoxPrefabSize = uiWindow.resourceDisplayBoxPrefab.GetComponent<SpriteRenderer>().bounds.size;
                        // Debug.Log("resourceDisplayBoxPrefabSize "+resourceDisplayBoxPrefabSize.x);
                        Vector3 resourceDisplayBoxPrefabSize = new Vector3(100, 200, 0);
                        
                        for(int j = 0; j < spaceObject.resources.resources.Count; j++)
                        {
                            ResourceAndAmount resource = spaceObject.resources.resources[j];
                            
                            GameObject resourceDisplayBox = Instantiate(resourceDisplayBoxPrefab, segment.transform.position + new Vector3(resourceDisplayBoxPrefabSize.x*
                                (j) - 140,0,0), Quaternion.identity, segment.transform);
                            ResourceDisplayBox boxDataStorageScript = resourceDisplayBox.GetComponent<ResourceDisplayBox>();
                            boxDataStorageScript.symbol.text = resource.resource.symbol;
                            boxDataStorageScript.amount.text = resource.count.ToString();
                        }
                    
                    }
                    else if(segmentType == SEGMENTS.BUILD_BAY)
                    {
                        segment.name = "Build Bay";
                        GameObject buildBayGO = Instantiate(buildBayPrefab, segment.transform.position, Quaternion.identity, segment.transform);
                        Buildbay buildbaySc = buildBayGO.gameObject.GetComponent<Buildbay>();

                        buildbaySc.buildableRecipePrefab = buildableRecipePrefab;
                        buildbaySc.SetResources(spaceObject.resources);

                    }
                    uiWindow.segments.Add(segment);
                    segmentOffset.y -= 200;

                }
                
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