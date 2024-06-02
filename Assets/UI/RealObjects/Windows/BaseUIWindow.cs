using System;
using System.Collections.Generic;
using System.Linq;
using Approacheth.UI.RealObjects.KindWindows.Prefabs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Approacheth.UI.RealObjects.KindWindows
{

    public class BaseUIWindow : MonoBehaviour, IUIWindow, IDragHandler, IBeginDragHandler
    {
        public UIWindowFactory.SEGMENTS[] buildChildren = new UIWindowFactory.SEGMENTS[1]
        {
            UIWindowFactory.SEGMENTS.MADE_FROM_RESOUCES,
        }; 
            
        public Text titleText;
        public Image iconImage;
        public Text synopsisText;
        
        public GameObject segmentHolder;
        public List<GameObject> segments;

        public GameObject draggableWindowBar;
        

        private List<GameObject> _windowSegments;
        private Vector2 _dragOffset;

        public virtual void Setup(UIConfig uiConfig, SpaceObject spaceObject)
        {
            titleText.text = spaceObject.name;
            iconImage.sprite = spaceObject.icon;
            synopsisText.text = spaceObject.synopsis;

        }

        public UIWindowFactory.SEGMENTS[] GetBuildSegments()
        {
            return buildChildren;
        }

        public void Close()
        {
            Destroy(this.transform.gameObject);
        }
        
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform as RectTransform, 
                eventData.position, 
                eventData.pressEventCamera, 
                out _dragOffset);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    transform as RectTransform, 
                    eventData.position, 
                    eventData.pressEventCamera, 
                    out Vector2 localPoint))
            {
                Vector2 offsetPosition = localPoint - _dragOffset;
                transform.localPosition += new Vector3(offsetPosition.x, offsetPosition.y, 0);
            }
        }
    }
    
}