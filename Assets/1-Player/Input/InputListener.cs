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

    [System.Serializable] public class OnMoveEvent : UnityEvent<Vector2> { }
}