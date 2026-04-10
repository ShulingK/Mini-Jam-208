using System;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public CharacterStatsData statsData;

    private float _life;
    protected Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color baseColor;

    public float Life
    {
        get => _life;
        set
        {
            float diff = value - _life;
            _life = Mathf.Clamp(value, 0, statsData.maxHP);

            if (diff < 0)
                OnDamage?.Invoke(-diff);
            else if (diff > 0)
                OnHeal?.Invoke(diff);

            if (_life <= 0)
                OnDeath?.Invoke();

            UpdateColor();
        }
    }

    public event Action<float> OnDamage;
    public event Action<float> OnHeal;
    public event Action OnDeath;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer != null ? spriteRenderer.color : Color.white;
    }

    protected virtual void Start()
    {
        if (statsData == null)
            Debug.LogError($"{name} : statsData non assigné");
        Life = statsData?.maxHP ?? 100;
    }

    public virtual void TakeDamage(float amount)
    {
        if (amount > 0)
            Life -= amount;
    }

    public virtual void Heal(float amount)
    {
        if (amount > 0)
            Life += amount;
    }

    void UpdateColor()
    {
        if (spriteRenderer == null) return;
        float ratio = _life / statsData.maxHP;

        if (ratio <= 0.25f)
            spriteRenderer.color = Color.red;
        else if (ratio <= 0.5f)
            spriteRenderer.color = Color.yellow;
        else
            spriteRenderer.color = baseColor;
    }

    protected virtual void OnEnable()
    {
        OnDeath += Die;
    }

    protected virtual void OnDisable()
    {
        OnDeath -= Die;
    }

    protected virtual void Die()
    {
        Debug.LogWarning($"{name} est mort");
        gameObject.SetActive(false);
    }

    public float GetMoveSpeed() => statsData?.moveSpeed ?? 0f;
}