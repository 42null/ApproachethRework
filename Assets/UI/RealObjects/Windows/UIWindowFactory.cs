using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Approacheth.UI.RealObjects.KindWindows;
using Approacheth.UI.RealObjects.KindWindows.Prefabs;
using System.Linq;

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
                        refreshBuiltFrom(segment, spaceObject);
                        spaceObject.setCallRefreshBuiltFromWith(segment);
                        
                        // Vector3 resourceDisplayBoxPrefabSize = uiWindow.resourceDisplayBoxPrefab.GetComponent<SpriteRenderer>().bounds.size;
                        // Debug.Log("resourceDisplayBoxPrefabSize "+resourceDisplayBoxPrefabSize.x);


                    }
                    else if(segmentType == SEGMENTS.BUILD_BAY)
                    {
                        segment.name = "Build Bay";
                        GameObject buildBayGO = Instantiate(buildBayPrefab, segment.transform.position, Quaternion.identity, segment.transform);
                        Buildbay buildbaySc = buildBayGO.gameObject.GetComponent<Buildbay>();

                        buildbaySc.buildableRecipePrefab = buildableRecipePrefab;
                        buildbaySc.SetResources(spaceObject.resources);
                        buildbaySc.SetSpaceObject(spaceObject);
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


        public void refreshBuiltFrom(GameObject segment, SpaceObject spaceObject)
        {
            DestroyChildrenWithName(segment.transform, "ResourceDisplayBox Prefab(Clone)");
            // Delete all existing displays
            // foreach(var resourceDisplayBox in spaceObject.GetComponentInChildren<RecipeDisplayBox>().resourceDisplayBoxes)
            // {
            //     Destroy(resourceDisplayBox);
            // }

            // GameObject resourceDisplayBox = Instantiate(suppliesBoxesDisplayBoxPrefab, suppliesBoxesDisplay.transform.position + offset, Quaternion.identity, suppliesBoxesDisplay.transform);
            // resourceDisplayBoxes.Add(resourceDisplayBox);
            
            Vector3 resourceDisplayBoxPrefabSize = new Vector3(100, 200, 0);

            Dictionary<MaterialData, int>.Enumerator resourcesEnumerator = spaceObject.resources.resources.GetEnumerator();
            int j = 0;
            while (resourcesEnumerator.MoveNext())
            {
                KeyValuePair<MaterialData, int> resource = resourcesEnumerator.Current;
                                
                GameObject resourceDisplayBox = Instantiate(resourceDisplayBoxPrefab, segment.transform.position + new Vector3(resourceDisplayBoxPrefabSize.x*
                    (j) - 180,0,0), Quaternion.identity, segment.transform);
                ResourceDisplayBox boxDataStorageScript = resourceDisplayBox.GetComponent<ResourceDisplayBox>();
                boxDataStorageScript.symbol.text = resource.Key.symbol;
                boxDataStorageScript.amount.text = resource.Value.ToString();
                                
                j++;
            }
            resourcesEnumerator.Dispose();
        }
        
        // TODO: Better store and obtain as list to avoid searching 
        private void DestroyChildrenWithName(Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name)
                {
                    Destroy(child.gameObject);
                }

                DestroyChildrenWithName(child, name);
            }
        }
        
    }
}