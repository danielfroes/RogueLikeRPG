using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.ActionSystem
{
    public class ActionButton : MonoBehaviour {

        [HideInInspector]
        public Action action;
        [FormerlySerializedAs("actionMenu")] [HideInInspector]
        public ActionMenuController _actionMenuController;
        [SerializeField]
        private ActionButtonColorSettings colorSettings = null;
        
        private Button buttonGUI;
        private 
    

        TextMeshProUGUI[] texts;


        private void Start() {
            buttonGUI = GetComponent<Button>();
            texts = GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in texts) {
                if (text.gameObject.name == "ActionName") {
                    text.SetText(action.actionName);
                }

                if (text.gameObject.name == "ActionQtt") {
                    text.SetText(action.actionBarsNeeded.ToString() + "A");
                }
            }
            colorSettings.interactableTextColor = texts[0].color;
            GetComponent<Image>().color = colorSettings.GetActionColor(action);

        }


        private void Update() {
            if (action.actionBarsNeeded > ActionBar.instance.numActions) {
                buttonGUI.interactable = false;
                foreach (var text in texts) {
                    text.color = colorSettings.uniteractableTextColor;
                }
            }
            else {
                buttonGUI.interactable = true;
                foreach (var text in texts) {
                    text.color = colorSettings.interactableTextColor;
                }
            }
        }


        public void SelectAction() {
            // if(action.actionBarsNeeded <= ActionBar.instance.numActions)

            ActionBar.instance.SpendAction(action.actionBarsNeeded);
            ActionCaster.instance.CastAction(action);

            _actionMenuController.SetActionMenuActive(false);


        }

    }
}