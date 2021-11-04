using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Scripts.ActionSystem;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class ActionMenuController : MonoBehaviour
{
    [SerializeField] Button _attackButton = null;
    [SerializeField] Button _spellButton = null;
    [SerializeField] GameObject _actionMenu = null;
    [SerializeField] RectTransform _mainOptions = null;
    [SerializeField] RectTransform _secondaryOptions = null;
    [SerializeField] RectTransform _secondaryOptionsButtonsContainer = null;
    [SerializeField] ActionButton _genericButton = null;
    [SerializeField] InfoPanel _infoPanel = null;
    [SerializeField] float _transitionsTime = 0;

    [SerializeField] private PlayerInput inputComponent;

    public Sound open;
    public Sound select;
    public Sound submit;

    private GameObject last;
    
    //TODO:Refactor this to it separate script
    public PlayerAction[] availableActions;
     
    private EventSystem eventSystem;
    private Vector2 _secondaryInitPivot;
    private Vector2 _mainInitPivot;
    private List<ActionButton> _actionsInstantiated = new List<ActionButton>();
    private bool isInSecondaryMenu = false;

    private void Awake() {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
        //_attackButton.onClick.AddListener(() => ChooseOption(ActionType.Attack));
        //_spellButton.onClick.AddListener(() => ChooseOption(ActionType.Spell));

        //ChooseOption(ActionType.Attack);
        //ChooseOption(ActionType.Spell);
    }
    private void Start() {
        _mainInitPivot = _mainOptions.pivot;
        _secondaryInitPivot = _secondaryOptions.pivot;
    }
    
    
    // Activation now handled by UnityEvent (InputListener) in ActionMenu GameObject
    public void Activate()
    {
        AudioManager.Play(open);
        SetActionMenuActive(!_actionMenu.activeInHierarchy);
    }
    // TODO: Refatorar isso aqui
    void Update()
    {
        if (isInSecondaryMenu && eventSystem.currentSelectedGameObject != last)
        {
            AudioManager.Play(@select);
            last = eventSystem.currentSelectedGameObject;
        }
        
        //TODO: colocar para so mostrar se n tiver sido mostrado antes e
        if (isInSecondaryMenu && eventSystem.currentSelectedGameObject != null) {
            _infoPanel.SetInfoPanelText(eventSystem.currentSelectedGameObject.GetComponent<ActionButton>().action);
        }
    }

   void ChooseOption()
   {
       AudioManager.Play(submit);
       
        _mainOptions.DOPivotX(2f, _transitionsTime).SetUpdate(true).OnComplete(() => _mainOptions.gameObject.SetActive(false));

        PopulateSecondaryMenu();
        
        isInSecondaryMenu = true;
        _secondaryOptions.DOPivotX(0.5f, _transitionsTime).SetUpdate(true);
        _infoPanel.gameObject.SetActive(true);
    }

    
   void PopulateSecondaryMenu()
   {

        int cntActionsWithType = 0; 
        foreach (var action in availableActions) {
          
            var newButton = Instantiate<ActionButton>(_genericButton, _secondaryOptionsButtonsContainer.transform);
            newButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -1 * cntActionsWithType);
            newButton.name = action.name;
            newButton.action = action;
            newButton._actionMenuController = this;
            newButton.select = submit;

            _actionsInstantiated.Add(newButton);
            //Auto Select the first button
            if (cntActionsWithType == 0) {
                eventSystem.SetSelectedGameObject(null);
                eventSystem.SetSelectedGameObject(newButton.gameObject);
            }
            cntActionsWithType++;
        }
    }

   
    //TODO: Refatorar isso para quando mexer nos inputs
    //TODO: Ficar instanciado e destruindo objetos não é a melhor solução, mudar para pooling das opções
    public void SetActionMenuActive(bool status) 
    {
        // When action menu is activated
        if (status) {
            if (!ActionCaster.instance.isCasting) {
                _actionMenu.SetActive(true);
                Vector2 bufferScale = _actionMenu.transform.localScale;
                _actionMenu.transform.localScale = Vector2.one * 0.3f;
                Time.timeScale = 0.0f;
                //_mainOptions.gameObject.SetActive(true);
                _secondaryOptions.gameObject.SetActive(true);

                eventSystem.SetSelectedGameObject(null);
                eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);

                _mainOptions.pivot = _mainInitPivot;
                _secondaryOptions.pivot = _secondaryInitPivot;

                _actionMenu.transform.DOScale(bufferScale, _transitionsTime).SetUpdate(true);
            }

            ChooseOption();
        }
        else { //when action menu in deactivated
            foreach (var action in _actionsInstantiated) {
                Destroy(action.gameObject);
            }

            _actionsInstantiated.Clear();
            _infoPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
            _actionMenu.SetActive(false);

            isInSecondaryMenu = false;
        }

    }

}
