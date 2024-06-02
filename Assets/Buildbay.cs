using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Approacheth
{
    public class Buildbay : MonoBehaviour
    {
        public GameObject contentsArea;
        public Text HeadbarText;
        
        public List<BuildData> buildables;
        public ResourceHolder resources;
        public List<GameObject> recipeObjects;
        
        public GameObject buildableRecipePrefab;

        public GameObject recipeObsHolder;

        
        void Start()
        {

        }

        public void SetupWithCheckingResources()
        {
            List<GameObject>.Enumerator recipeObjectsEnumeration = recipeObjects.GetEnumerator();
            recipeObjectsEnumeration.MoveNext();
            Debug.Log(recipeObjects.Count);
            foreach (BuildData buildablePossibility in buildables)
            {
                List<ResourceAndAmount> missingResources = resources.resoucesMissingFromBuildData(buildablePossibility);
                Debug.Log("recipeObjectsEnumeration.Current.name, "+recipeObjectsEnumeration.Current.ToString());
                RecipeDisplayBox recipeDisplayBoxSc = recipeObjectsEnumeration.Current.GetComponent<RecipeDisplayBox>();
                
                if (missingResources.Count == 0)
                {
                    Debug.Log("CAN BUILD "+buildablePossibility.name);
                    recipeDisplayBoxSc.SetAsAvailable(true);
                    // buildableRecipePrefab.;
                }
                else
                {
                    Debug.Log("CAN'T BUILD "+buildablePossibility.name);
                    recipeDisplayBoxSc.SetAsAvailable(false);
                }

                recipeObjectsEnumeration.MoveNext();
            }
            recipeObjectsEnumeration.Dispose();
        }
        
        public void SetResources(ResourceHolder resources)
        {
            this.resources = resources;
            
            Vector3 offset = new Vector3(0, -40,0);
            // for(int j = 0; j < buildbaySc.buildables.Count; j++)
            foreach (BuildData buildableRecipe in buildables)
            {
                GameObject buildableRecipeDisplay = Instantiate(buildableRecipePrefab, this.transform.position + offset, Quaternion.identity, recipeObsHolder.transform);
                recipeObjects.Add(buildableRecipeDisplay);
                buildableRecipeDisplay.name = "Buildable Recipe - "+buildableRecipe.name;
                RecipeDisplayBox recipeDisplayBox = buildableRecipeDisplay.gameObject.GetComponent<RecipeDisplayBox>();
                recipeDisplayBox.buildDataRecipe = buildableRecipe;
                
                recipeDisplayBox.SetResourcesMissing();

                offset.y -= 100;
            }

            SetupWithCheckingResources();
        }

        void Update()
        {
            
        }
    }
}
