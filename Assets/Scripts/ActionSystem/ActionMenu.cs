using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class ActionMenu : MonoBehaviour
{
    [SerializeField] private GameObject actionMenu = null;
    [SerializeField] private RectTransform mainOptions = null;
    [SerializeField] private RectTransform secondaryOptions = null;
    [SerializeField] private GameObject genericButton = null;
    [SerializeField] private float transitionsTime = 0;

    public Action[] availableActions ;
    private EventSystem es;

    private Vector2 _secondaryInitPivot;    
    
    private Vector2 _mainInitPivot;

    private void Awake() {
       es = GameObject.FindObjectOfType<EventSystem>();
       
    }

    private void Start() {
        _mainInitPivot = mainOptions.pivot;
        _secondaryInitPivot = secondaryOptions.pivot;
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
        mainOptions.DOPivotX(1.5f, transitionsTime).SetUpdate(true).OnComplete(() => mainOptions.gameObject.SetActive(false));

        if(type.ToLower() == "attack")
            PopulateSecondaryMenu(ActionType.attack);
        if(type.ToLower() == "spell")
            PopulateSecondaryMenu(ActionType.spell);
        if(type.ToLower() == "item")
            PopulateSecondaryMenu(ActionType.item);

        secondaryOptions.DOPivotX(0.5f, transitionsTime).SetUpdate(true);
    }
    
    

    private void PopulateSecondaryMenu(ActionType type)
    {
        // List<Action> availableActionsWithType = new List<Action>();
        int cntActionsWithType = 0;
        foreach (Action action in availableActions)
        {
            if(action.actionType == type)
            {   
                GameObject newButton = Instantiate(genericButton, secondaryOptions.transform);
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
            Vector2 bufferScale = actionMenu.transform.localScale;
            actionMenu.transform.localScale = Vector2.one * 0.3f;
            Time.timeScale = 0f;
            mainOptions.gameObject.SetActive(true);
           
            es.SetSelectedGameObject(null);
            es.SetSelectedGameObject(es.firstSelectedGameObject);

            mainOptions.pivot = _mainInitPivot;
            secondaryOptions.pivot =_secondaryInitPivot;
            
            actionMenu.transform.DOScale(bufferScale, transitionsTime).SetUpdate(true);

        }
        if(!actionMenu.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
        
    }
}
