using System.Collections;
using UnityEngine;

public class SpellChargedProjectilBehavior : SpellBehavior
{
    [SerializeField] float _bulletSpeed = 20f;

    private Vector2 _direction;

    public void Init(Vector2 dir)
    {
        _direction = dir.normalized;
        transform.right = _direction;
    }

    protected override IEnumerator SpellCoroutine()
    {
        Destroy(gameObject, 5f);

        while (true)
        {
            transform.position += (Vector3)(_direction * _bulletSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            // Take Damages;
        }
        else if (collision.transform.tag == "Shield")
        {
            Destroy(gameObject);
        }
    }
}
