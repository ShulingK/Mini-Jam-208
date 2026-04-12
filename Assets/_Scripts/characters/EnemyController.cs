using System.Collections;
using UnityEngine;

public class EnemyController : CharacterBase
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Distances")]
    [SerializeField] private float attackRange = 6f;

    [Header("Combat")]
    [SerializeField] private SpellCaster spellCaster;


    protected void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        Vector2 dirToPlayer = (player.position - transform.position).normalized;

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
}