using System.Collections;
using UnityEngine;

public class SpellProjectilBehavior : SpellBehavior
{
    [SerializeField] float _bulletSpeed = 10f;

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
}