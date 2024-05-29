using System.Collections.Generic;
using Approacheth.UI.RealObjects.KindWindows.Prefabs;
using UnityEngine;
using UnityEngine.UI;

namespace Approacheth.UI.RealObjects.KindWindows
{

    public class BaseUIWindow : MonoBehaviour, IUIWindow
    {
        public Text titleText;
        public Image iconImage;
        public Text synopsisText;
        
        public GameObject segmentPrefab;
        public GameObject resourceDisplayBoxPrefab;
        

        private List<GameObject> _windowSegments;

        public virtual void Setup(UIConfig uiConfig, SpaceObject spaceObject)
        {
            titleText.text = spaceObject.name;
            iconImage.sprite = spaceObject.icon;
            synopsisText.text = spaceObject.synopsis;
            
            foreach (var resource in spaceObject.resources.resources)
            {
                GameObject resourceDisplayBox = Instantiate(resourceDisplayBoxPrefab, segmentPrefab.transform);
                ResourceDisplayBox boxDataStorageScript = resourceDisplayBox.GetComponent<ResourceDisplayBox>();
                boxDataStorageScript.symbol.text = resource.resource.symbol;
                boxDataStorageScript.amount.text = resource.count.ToString();
            }
        }

        public void Close()
        {
            Destroy(this.transform.gameObject);
        }
    }

}