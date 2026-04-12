using System.Collections;
using UnityEngine;

public class SpellProjectilBehavior : SpellBehavior
{
    [SerializeField] float _bulletSpeed = 10f;

    private Vector2 _direction;


    public void Init(Vector2 dir, string tagTarget, int damage)
    {
        _direction = dir.normalized;
        transform.right = _direction;

        _tagTarget = tagTarget;
        _damage = damage;

        Debug.Log(_tagTarget);
    }

    protected override IEnumerator SpellCoroutine()
    {
        Vector2 startPosition = transform.position;
        float maxDistance = 15f;

        while (true)
        {
            transform.position += (Vector3)(_direction * _bulletSpeed * Time.deltaTime);

            float distance = Vector2.Distance(startPosition, transform.position);

            if (distance >= maxDistance)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touched: " + _tagTarget);

        if (_tagTarget == null) return;

        if (collision.CompareTag(_tagTarget))
        {
            if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
            {
                if (!playerController.IsCrouching())
                {
                    playerController.TakeDamage(_damage);

                    Destroy(gameObject);
                }
            }
            else if (collision.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.TakeDamage(_damage);

                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}