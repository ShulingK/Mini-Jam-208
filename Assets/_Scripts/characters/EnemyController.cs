using System.Collections;
using UnityEngine;

public class EnemyController : CharacterBase
{
    [Header("Distances")]
    [SerializeField] private float attackRange = 6f;

    [Header("Combat")]
    [SerializeField] private SpellCaster spellCaster;


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

    protected void Update()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance._player == null) return;

        float distance = Vector2.Distance(transform.position, GameManager.Instance._player.transform.position);

        Vector2 dirToPlayer = (GameManager.Instance._player.transform.position - transform.position).normalized;

        // REGARDER LE JOUEUR
        Flip(dirToPlayer.x);

        // PHASE : APPROCHE
        if (distance < attackRange)
        {
            // TIR
            if (CanAttack() && spellCaster.CanCast(0))
            {
                Vector2 dir = GetComponent<SpriteRenderer>().flipX ? Vector2.left : Vector2.right; // ou left selon ton 

                StartCoroutine(Attack(dir));

            }
        }
    }

    private IEnumerator Attack(Vector2 dir)
    {
        GetComponent<Animator>().SetBool("Shoot", true);

        yield return new WaitForSeconds(1f);

        GetComponent<Animator>().SetBool("Shoot", false);

        spellCaster.CastSpell(0, dir);
    }

    private bool CanAttack()
    {
        // On check le cooldown du spell 0
        return spellCaster != null;
    }

    private void Flip(float dirX)
    {
        if (dirX == 0) return;

        GetComponent<SpriteRenderer>().flipX = dirX < 0;
    }

    protected virtual void Death()
    {
        GameManager.Instance.CreateNewAlly(transform.position);

        Destroy(gameObject);
    }
}