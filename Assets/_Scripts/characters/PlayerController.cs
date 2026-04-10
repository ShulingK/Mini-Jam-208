using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterBase
{
    private Player1 inputActions;  
    private Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new Player1();
    }

    protected override void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    protected override void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        float speed = GetMoveSpeed();
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }
}