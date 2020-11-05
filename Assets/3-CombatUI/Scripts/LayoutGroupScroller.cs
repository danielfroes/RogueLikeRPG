using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//Classe LayoutGroupScroller
//
//
namespace UnityEngine.UI
{
    public class LayoutGroupScroller : MonoBehaviour
    {
        [SerializeField]private RectTransform viewPort = null;
        private GameObject _lastSelected = null;
        private float contentSpacing = 0; 
        private RectTransform contentContainer;
        
        // Start is called before the first frame update
        void Awake()
        {
            contentSpacing = GetComponent<VerticalLayoutGroup>().spacing;
            contentContainer = gameObject.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            // Get the currently selected UI element from the event system.
            GameObject selected = EventSystem.current.currentSelectedGameObject;
    
            // Return if there are none.
            if (selected == null) {
                return;
            }

            // Return if the selected game object is not inside the scroll rect.
            if (selected.transform.parent != contentContainer.transform) {
                return;
            }
            
            // Return if the selected game object is the same as it was last frame,
            // meaning we haven't moved.
            if (selected == _lastSelected) {
                return;
            }


            RectTransform selectedRectTransform = EventSystem.current.currentSelectedGameObject.transform as RectTransform;  
            //height of the button
            float contentHeight = selectedRectTransform.rect.height;
            float offsetY = contentHeight + contentSpacing;

            // The position of the selected UI element is the absolute anchor position,
            // ie. the local position within the scroll rect + its height if we're
            // scrolling down. If we're scrolling up it's just the absolute anchor position.
            float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;
            
            // The upper bound of the scroll view is the anchor position of the content we're scrolling.
            float scrollViewMinY = viewPort.anchoredPosition.y;
            // The lower bound is the anchor position + the height of the scroll rect.
            float scrollViewMaxY = viewPort.anchoredPosition.y + viewPort.rect.height;
            print("Max: " + scrollViewMaxY +  "SelectedPos: " + selectedPositionY);
            // If the selected position is below the current lower bound of the scroll view we scroll down.
            if (selectedPositionY > scrollViewMaxY) {
                contentContainer.anchoredPosition += new Vector2(0, offsetY);
            }

            // If the selected position is above the current upper bound of the scroll view we scroll up.
            else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY) {
                contentContainer.anchoredPosition -= new Vector2(0, offsetY);
            }
 



        

            

        }
    }
}