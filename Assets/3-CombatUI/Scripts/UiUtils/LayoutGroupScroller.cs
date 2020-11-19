using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


//Classe LayoutGroupScroller
// Função: Faz o scroll de um layout group.
// Uso: Inserir junto a um objeto com um componente de vertical layout group.
//
namespace UnityEngine.UI
{
    public class LayoutGroupScroller : MonoBehaviour
    {
        // [SerializeField] private RectTransform viewPort = null;
        [SerializeField] private int numContentsInView = 3;
        [SerializeField] private float lerpDuration = 0.5f;
        private GameObject _lastSelected = null;
        private float contentSpacing = 0; 
        private RectTransform contentContainer;

        private int _buttonIndex = 1;
        
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
            else if (selected == _lastSelected) {
                return;
            }

            RectTransform selectedRectTransform = selected.transform as RectTransform;  
            //height of the button
            float contentHeight = selectedRectTransform.rect.height;
            //espaço entre os dois botões
            float offsetY = contentHeight + contentSpacing;

            //Verifica se a seleção subiu ou desceu e atualiza o índice
            if(_lastSelected != null)
            {
                float selectedPositionY = selected.transform.position.y;

                float lastPositionY = _lastSelected.transform.position.y;
            
                //Se o ultimo botão for mais alto do que o atual quer dizer que o botão desceu na lista,
                //caso contrário, subiu
                _buttonIndex += selectedPositionY < lastPositionY? 1: -1 ; 

               
            }


            if (_buttonIndex > numContentsInView) {
    
                Vector2 endPos = contentContainer.anchoredPosition + Vector2.up*offsetY;
                contentContainer.DOAnchorPos(endPos, lerpDuration).SetUpdate(true);
                _buttonIndex = 3;
            }
            else if (_buttonIndex < 1) {
             
                Vector2 endPos = contentContainer.anchoredPosition - Vector2.up*offsetY;
                contentContainer.DOAnchorPos(endPos, lerpDuration).SetUpdate(true);
                _buttonIndex = 1;
            }
  
 



            if(_lastSelected != selected)
                _lastSelected = selected;


        }
    }
}