using UnityEngine;
using UnityEngine.UI;

namespace Approacheth.UI.RealObjects.KindWindows
{

    public class BaseUIWindow : MonoBehaviour, IUIWindow
    {
        public Text titleText;
        public Image iconImage;
        public Text synopsisText;

        public virtual void Setup(UIConfig uiConfig, SpaceObject spaceObject)
        {
            titleText.text = spaceObject.name;
            iconImage.sprite = spaceObject.icon;
            synopsisText.text = spaceObject.synopsis;
        }
    }

}