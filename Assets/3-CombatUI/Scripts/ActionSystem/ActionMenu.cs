using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class ActionMenu : MonoBehaviour {
    [SerializeField] private GameObject actionMenu = null;
    [SerializeField] private RectTransform mainOptions = null;
    [SerializeField] private RectTransform secondaryOptions = null;
    [SerializeField] private RectTransform secondaryOptionsButtonsContainer = null;
    [SerializeField] private ActionButton genericButton = null;
    [SerializeField] private InfoPanel infoPanel = null;
    [SerializeField] private float transitionsTime = 0;
    public Action[] availableActions;
    private EventSystem es;
    private Vector2 _secondaryInitPivot;
    private Vector2 _mainInitPivot;
    private List<ActionButton> _actionsIntantiated = new List<ActionButton>();
    private bool isInSecondaryMenu = false;

    private void Awake() {
        es = GameObject.FindObjectOfType<EventSystem>();
    }
    private void Start() {
        _mainInitPivot = mainOptions.pivot;
        _secondaryInitPivot = secondaryOptions.pivot;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            SetActionMenuActive(!actionMenu.activeInHierarchy);
        }
        //colocar para so mostrar se n tiver sido mostrado antes
        if (isInSecondaryMenu) {
            infoPanel.SetInfoPanelText(es.currentSelectedGameObject.GetComponent<ActionButton>().action);
        }
    }

    public void ChooseOption(string type) {
        mainOptions.DOPivotX(2f, transitionsTime).SetUpdate(true).OnComplete(() => mainOptions.gameObject.SetActive(false));

        if (type.ToLower() == "attack")
            PopulateSecondaryMenu(ActionType.ATTACK);
        if (type.ToLower() == "spell")
            PopulateSecondaryMenu(ActionType.SPELL);
        if (type.ToLower() == "item")
            PopulateSecondaryMenu(ActionType.ITEM);

        isInSecondaryMenu = true;
        secondaryOptions.DOPivotX(0.5f, transitionsTime).SetUpdate(true);
        infoPanel.gameObject.SetActive(true);
    }

    private void PopulateSecondaryMenu(ActionType type) {
        // List<Action> availableActionsWithType = new List<Action>();
        int cntActionsWithType = 0;
        foreach (Action action in availableActions) {
            if (action.actionType == type) {
                ActionButton newButton = Instantiate<ActionButton>(genericButton, secondaryOptionsButtonsContainer.transform);
                newButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -1 * cntActionsWithType);
                newButton.name = action.name;
                newButton.action = action;
                newButton.actionMenu = this;

                _actionsIntantiated.Add(newButton);
                //Auto Select the first button
                if (cntActionsWithType == 0) {
                    es.SetSelectedGameObject(null);
                    es.SetSelectedGameObject(newButton.gameObject);
                }
                cntActionsWithType++;
            }
        }
    }

    //**Refatorar isso para quando mexer nos inputs
    //** Ficar instanciado e destruindo objetos não é a melhor solução, mudar para pooling das opções
    public void SetActionMenuActive(bool status) {
        // When action menu is activated
        if (status) {
            if (!ActionCaster.instance.isCasting) {
                actionMenu.SetActive(status);
                Vector2 bufferScale = actionMenu.transform.localScale;
                actionMenu.transform.localScale = Vector2.one * 0.3f;
                Time.timeScale = 0.0f;
                mainOptions.gameObject.SetActive(true);

                es.SetSelectedGameObject(null);
                es.SetSelectedGameObject(es.firstSelectedGameObject);

                mainOptions.pivot = _mainInitPivot;
                secondaryOptions.pivot = _secondaryInitPivot;

                actionMenu.transform.DOScale(bufferScale, transitionsTime).SetUpdate(true);
            }
        }
        else { //when action menu in deactivated
            foreach (ActionButton action in _actionsIntantiated) {
                Destroy(action.gameObject);
            }

            _actionsIntantiated.Clear();
            infoPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
            actionMenu.SetActive(status);

            isInSecondaryMenu = false;
        }

    }

}
