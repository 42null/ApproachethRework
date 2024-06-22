using UnityEngine;
using UnityEngine.UI;
namespace Approacheth.UI.RealObjects.KindWindows
{
    using UnityEngine;

    public class AsteroidUIWindow : BaseUIWindow
    {
        public Transform resourcesListParent;
        public GameObject resourceTextPrefab;

        public override void Setup(UIConfig uiConfig, SpaceObject spaceObject)
        {
            base.Setup(uiConfig, spaceObject);
            Asteroid asteroid = spaceObject as Asteroid;
            if (asteroid != null)
            {
                // Clear existing resources
                foreach (Transform child in resourcesListParent)
                {
                    Destroy(child.gameObject);
                }

                // Add new resources
                foreach (var resource in asteroid.resources.resources)
                {
                    GameObject resourceTextInstance = Instantiate(resourceTextPrefab, resourcesListParent);
                    Text resourceText = resourceTextInstance.GetComponent<Text>();
                    resourceText.text = resource.Key.name + ": " + resource.Value;
                }
            }
        }
    }

}
