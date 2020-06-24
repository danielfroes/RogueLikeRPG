using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class ActionMenu : MonoBehaviour
{
    [SerializeField] private GameObject actionMenu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject secondaryMenu;
    [SerializeField] private GameObject genericButton;
    [SerializeField] private float transitionsTime;

    public Action[] availableActions;
    private EventSystem es;
    

    private void Awake() {
       es = GameObject.FindObjectOfType<EventSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {

            ToggleActionMenu();
            
        }
    }


    public void ChooseOption(string type)
    {
        options.transform.DOMoveX(-3, transitionsTime).SetUpdate(true).OnComplete(() => options.SetActive(false));


        if(type.ToLower() == "attack")
            PopulateSecondaryMenu(ActionType.attack);
        if(type.ToLower() == "spell")
            PopulateSecondaryMenu(ActionType.spell);
        if(type.ToLower() == "item")
            PopulateSecondaryMenu(ActionType.item);

       
        secondaryMenu.GetComponent<RectTransform>().DOPivotX(0.5f, transitionsTime).SetUpdate(true);
    }
    
    

    private void PopulateSecondaryMenu(ActionType type)
    {
        // List<Action> availableActionsWithType = new List<Action>();
        int cntActionsWithType = 0;
        foreach (Action action in availableActions)
        {
            if(action.actionType == type)
            {   
                GameObject newButton = Instantiate(genericButton, secondaryMenu.transform);
                newButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, - 1*cntActionsWithType);
                newButton.name = action.name;
                newButton.GetComponentInChildren<TextMeshProUGUI>().SetText(action.actionName);
                if(cntActionsWithType == 0)
                {
                    es.SetSelectedGameObject(null);
                    es.SetSelectedGameObject(newButton);
                }

                cntActionsWithType++;
            }
        }



    }

    private void ToggleActionMenu()
    {
        actionMenu.SetActive(!actionMenu.activeInHierarchy);
        
        if(actionMenu.activeInHierarchy)
        {
            Time.timeScale = 0f;
            es.SetSelectedGameObject(null);
            es.SetSelectedGameObject(es.firstSelectedGameObject);
        }
        if(!actionMenu.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
    }
}
