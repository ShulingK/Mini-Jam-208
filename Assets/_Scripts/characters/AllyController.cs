using UnityEngine;
using UnityEngine.InputSystem;

public class AllyController : CharacterBase
{
    [SerializeField] public SpriteRenderer _head;

    private Transform _target;
    private int _index;

    [Header("Follow")]
    [SerializeField] private float spacing = 1f;

    private bool _isCrouching;
    [SerializeField] BoxCollider2D _boxCollider;

    protected override void OnEnable()
    {
        base.OnEnable();

        OnDeath += Death;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();

        OnDeath -= Death;
    }


    public void Init(Transform target, int index)
    {
        _target = target;
        _index = index;
    }

    private void Update()
    {
        if (_target == null) return;

        // direction du player (gauche / droite)
        // position cible uniquement en X

        float distance = _target.position.x - transform.position.x;

        if (Mathf.Abs(distance) > spacing)
        {
            float targetX = _target.position.x - spacing - (_index * 0.8f);

            float currentX = transform.position.x;

            float moveDir = Mathf.Sign(targetX - currentX);

            // si assez proche → stop
            if (Mathf.Abs(targetX - currentX) < 0.1f)
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
                return;
            }

            rb.linearVelocity = new Vector2(moveDir * GetMoveSpeed(), rb.linearVelocity.y);
            GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        
            if (rb.linearVelocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                _head.flipX = false;
            }
            else if (rb.linearVelocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                _head.flipX = true;
            }
        }

    }

    public void HandleCrouch()
    {
        bool fail = Random.Range(0, 20) == 1;

        if (!fail)
            OnCrouch();
    }

    public void HandleOnEndCrouch()
    {
        OnEndCrouch();
    }


    private void OnCrouch()
    {
        _isCrouching = true;

        Vector2 oldSize = _boxCollider.size;
        Vector2 newSize = oldSize;
        newSize.y /= 2f;

        _boxCollider.size = newSize;

        // Ajuster l'offset pour garder le bas au même endroit
        Vector2 offset = _boxCollider.offset;
        offset.y -= (oldSize.y - newSize.y) / 2f;

        _boxCollider.offset = offset;

        GetComponent<Animator>().SetBool("Crouch", true);
    }

    private void OnEndCrouch()
    {
        _isCrouching = false;

        Vector2 oldSize = _boxCollider.size;
        Vector2 newSize = oldSize;
        newSize.y *= 2f;

        _boxCollider.size = newSize;

        // Ajuster l'offset pour garder le bas au même endroit
        Vector2 offset = _boxCollider.offset;
        offset.y += (newSize.y - oldSize.y) / 2f;

        _boxCollider.offset = offset;

        GetComponent<Animator>().SetBool("Crouch", false);
    }

    public bool IsCrouching() => _isCrouching;


    private void Death()
    {
        GameManager.Instance.Kill(this);

        Destroy(gameObject);
    }
}