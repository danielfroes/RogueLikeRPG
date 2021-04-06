using Squeak;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public OnMoveEvent onMovePerformed;
    public UnityEvent onConfirmPerformed;
    public UnityEvent onCancelPerformed;
    public UnityEvent onMenuPerformed;
    public UnityEvent onEscapePerformed;
    
    public Action shortcut1;
    public Action shortcut2;

    private Input _input;

    #region MonoBehaviourCallbacks
    void Awake()
    {
        _input = new Input();
    }

    void OnEnable()
    {
        _input.Enable();
        
        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Confirm.performed += OnConfirmPerformed;
        _input.Player.Cancel.performed += OnCancelPerformed;
        _input.Player.Menu.performed += OnMenuPerformed;
        _input.Player.Escape.performed += OnEscapePerformed;

        _input.Player.Shortcut1.performed += PerformS1;
        _input.Player.Shortcut2.performed += PerformS2;
    }

    void OnDisable()
    {
        _input.Disable();

        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Confirm.performed -= OnConfirmPerformed;
        _input.Player.Cancel.performed -= OnCancelPerformed;
        _input.Player.Menu.performed -= OnMenuPerformed;
        _input.Player.Escape.performed -= OnEscapePerformed;
    }
    #endregion

    protected virtual void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        if (dir == Vector2.zero || Time.timeScale == 0f)
            return;
        onMovePerformed.Invoke(dir);
    }

    protected virtual void OnConfirmPerformed(InputAction.CallbackContext ctx)
    {
        onConfirmPerformed.Invoke();
    }

    protected virtual void OnCancelPerformed(InputAction.CallbackContext ctx)
    {
        onCancelPerformed.Invoke();
    }

    protected virtual void OnMenuPerformed(InputAction.CallbackContext ctx)
    {
        onMenuPerformed.Invoke();
    }

    protected virtual void OnEscapePerformed(InputAction.CallbackContext ctx)
    {
        onEscapePerformed.Invoke();
    }

    private void PerformS1(InputAction.CallbackContext ctx)
    {
        if (ActionBar.instance.numActions < shortcut1.actionBarsNeeded)
            return;

        ActionBar.instance.SpendAction(shortcut1.actionBarsNeeded);
        ActionCaster.instance.CastAction(shortcut1);
        FindObjectOfType<PlayerController>().Cast(shortcut1);
    }
    
    private void PerformS2(InputAction.CallbackContext ctx)
    {
        if (ActionBar.instance.numActions < shortcut2.actionBarsNeeded)
            return;

        ActionBar.instance.SpendAction(shortcut2.actionBarsNeeded);
        ActionCaster.instance.CastAction(shortcut2);
        FindObjectOfType<PlayerController>().Cast(shortcut2);
    }
    
    [System.Serializable] public class OnMoveEvent : UnityEvent<Vector2> { }
}