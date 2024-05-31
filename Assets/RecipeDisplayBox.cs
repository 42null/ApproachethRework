using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Approacheth
{
    public class RecipeDisplayBox : MonoBehaviour
    {
        public Image icon;
        public Text title;

        public BuildData buildDataRecipe;
        
        void Start()
        {
            title.text = buildDataRecipe.name;
        }

        void Update()
        {
        }
        
    }
}
