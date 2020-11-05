using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{

   
    [HideInInspector]
    public Action action;
    [HideInInspector]
    public ActionMenu actionMenu;
    [SerializeField]
    private Color _uninteractableColor;

    private Color _interactableColor;
    private Button buttonGUI;
    
    TextMeshProUGUI[] texts;
    

    private void Start() {
        buttonGUI = GetComponent<Button>();
        texts = GetComponentsInChildren<TextMeshProUGUI>(); 
        foreach (TextMeshProUGUI text in texts)
        {
            if(text.gameObject.name == "ActionName")
            {
                text.SetText(action.actionName);
            }

            if(text.gameObject.name == "ActionQtt")
            {
                text.SetText( action.actionBarsNeeded.ToString() + "A");
            }
        }
        _interactableColor = texts[0].color;
    }


    private void Update() {
        if(action.actionBarsNeeded > ActionBar.instance.numActions)
        {
            buttonGUI.interactable = false;
            foreach (TextMeshProUGUI text in texts)
            {
                text.color = _uninteractableColor;
            }
        }
        else
        {
            buttonGUI.interactable = true;
            foreach (TextMeshProUGUI text in texts)
            {
                text.color = _interactableColor;
            }
        }
    }


    public void SelectAction()
    {
        // if(action.actionBarsNeeded <= ActionBar.instance.numActions)
        
        ActionBar.instance.SpendAction(action.actionBarsNeeded);
        ActionCaster.instance.CastAction(action);

        actionMenu.SetActionMenuActive(false);
        
        
    }

}
