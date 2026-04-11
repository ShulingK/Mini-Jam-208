using JetBrains.Annotations;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : CharacterBase
{
    private Player1 inputActions;  
    private Vector2 moveInput;

    private bool isCrouching;
    [SerializeField] BoxCollider2D _boxCollider;
    [SerializeField] SpellCaster _spellCaster;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new Player1();
    }

    protected override void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Crouch.started += OnCrouch;
        inputActions.Player.Crouch.canceled += OnEndCrouch;

        inputActions.Player.Spell1.started += CastSpell1;
    }

    protected override void OnDisable()
    {
        inputActions.Player.Move.canceled -= ctx => moveInput = Vector2.zero;

        inputActions.Player.Crouch.started -= OnCrouch;
        inputActions.Player.Crouch.canceled -= OnEndCrouch;

        inputActions.Player.Spell1.started -= CastSpell1;

        inputActions.Disable();
    }


    private void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = true;

        Vector2 oldSize = _boxCollider.size;
        Vector2 newSize = oldSize;
        newSize.y /= 2f;

        _boxCollider.size = newSize;

        // Ajuster l'offset pour garder le bas au même endroit
        Vector2 offset = _boxCollider.offset;
        offset.y -= (oldSize.y - newSize.y) / 2f;

        _boxCollider.offset = offset;
    }
    private void OnEndCrouch(InputAction.CallbackContext context)
    {
        isCrouching = false;

        Vector2 oldSize = _boxCollider.size;
        Vector2 newSize = oldSize;
        newSize.y *= 2f;

        _boxCollider.size = newSize;

        // Ajuster l'offset pour garder le bas au même endroit
        Vector2 offset = _boxCollider.offset;
        offset.y += (newSize.y - oldSize.y) / 2f;

        _boxCollider.offset = offset;
    }

    private void FixedUpdate()
    {
        float crouchFactor = 1f;

        if (isCrouching)
            crouchFactor = 0.5f;

        moveInput = inputActions.Player.Move.ReadValue<Vector2>() * crouchFactor;

        if (moveInput.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (moveInput.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;

        float speed = GetMoveSpeed();
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }

    #region Spell

    private void CastSpell1(InputAction.CallbackContext context)
    {
        Vector2 dir = GetComponent<SpriteRenderer>().flipX? Vector2.left: Vector2.right; // ou left selon ton perso

        _spellCaster.CastSpell(0, dir);
    }
    private void CastSpell2(InputAction.CallbackContext context)
    {
        // _spellCaster.CastSpell(1);
    }
    private void CastSpell3(InputAction.CallbackContext context)
    {
        // _spellCaster.CastSpell(2);
    }
    private void CastSpell4(InputAction.CallbackContext context)
    {
        // _spellCaster.CastSpell(3);
    }

    #endregion


}