using System.Collections;
using System.Collections.Generic;
using Approacheth.UI.RealObjects.KindWindows.Prefabs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Approacheth
{
    public class RecipeDisplayBox : MonoBehaviour
    {
        public Image icon;
        public Image backgroundImage;
        public Text title;
        public GameObject suppliesBoxesDisplay;
        public GameObject suppliesBoxesDisplayBoxPrefab;

        private bool _enableMake;

        public BuildData buildDataRecipe;
        
        void Start()
        {
            title.text = buildDataRecipe.name;
            Vector3 offset = new Vector3(0, 0,0);

            foreach (ResourceAndAmount buildMaterial in buildDataRecipe.builtFrom){
                GameObject resourceDisplayBox = Instantiate(suppliesBoxesDisplayBoxPrefab, suppliesBoxesDisplay.transform.position + offset, Quaternion.identity, suppliesBoxesDisplay.transform);

                // resourceDisplayBox.transform.position = new Vector3(500,500,0);
                ResourceDisplayBox boxDataStorageScript = resourceDisplayBox.GetComponent<ResourceDisplayBox>();
                // Debug.Log(buildMaterial.count);
                boxDataStorageScript.symbol.text = buildMaterial.resource.symbol;
                boxDataStorageScript.amount.text = buildMaterial.count.ToString();

                offset.x += 70;
            }
        }

        public void SetResourcesMissing()
        {
            // .amountMissing.text = "buildMaterial.count.ToString()";
        }
        
        public void SetAsAvailable(bool available)
        {
            this._enableMake = !available;
            if (available)
            {
                backgroundImage.color = new Color32(0,255,125,200);

            }
            else
            {
                backgroundImage.color = new Color32(255,0,125,200);
            }
        }
        
        void OnMouseDown()
        {
            if (this._enableMake)
            {
            }
        }
        
        void Update()
        {
        }
        
    }
}
