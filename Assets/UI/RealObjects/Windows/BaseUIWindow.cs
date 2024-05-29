using System;
using System.Collections.Generic;
using System.Linq;
using Approacheth.UI.RealObjects.KindWindows.Prefabs;
using UnityEngine;
using UnityEngine.UI;

namespace Approacheth.UI.RealObjects.KindWindows
{

    public class BaseUIWindow : MonoBehaviour, IUIWindow
    {
        public UIWindowFactory.SEGMENTS[] buildChildren = new UIWindowFactory.SEGMENTS[] { UIWindowFactory.SEGMENTS.MADE_FROM_RESOUCES };
        
        public Text titleText;
        public Image iconImage;
        public Text synopsisText;
        
        public GameObject segmentHolder;
        public List<GameObject> segments;
        public GameObject segmentPrefab;
        public GameObject resourceDisplayBoxPrefab;
        

        private List<GameObject> _windowSegments;

        public virtual void Setup(UIConfig uiConfig, SpaceObject spaceObject)
        {
            titleText.text = spaceObject.name;
            iconImage.sprite = spaceObject.icon;
            synopsisText.text = spaceObject.synopsis;

            // foreach (var resource in spaceObject.resources.resources)
            // {
            //     GameObject resourceDisplayBox = Instantiate(resourceDisplayBoxPrefab, segments[0].transform);
            //     ResourceDisplayBox boxDataStorageScript = resourceDisplayBox.GetComponent<ResourceDisplayBox>();
            //     boxDataStorageScript.symbol.text = resource.resource.symbol;
            //     boxDataStorageScript.amount.text = resource.count.ToString();
            // }
        }

        public UIWindowFactory.SEGMENTS[] GetBuildSegments()
        {
            return buildChildren;
        }

        public void Close()
        {
            Destroy(this.transform.gameObject);
        }
    }

}