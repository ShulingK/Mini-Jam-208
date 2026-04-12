using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;

public class SpellMeleeBehavior : SpellBehavior
{
    private Transform _origin;
    private Vector2 _target;
    private float _radius;
    private int _damage;
    private LayerMask _enemyLayer;
    private GameObject _prefabVFX;

    [SerializeField] private float _timeBeforeDeleting = 0.8f; 

    public void Init(Transform origin, Vector2 target, float radius,int damages, LayerMask enemyLayer, GameObject prefabVFX)
    {
        _origin = origin;
        _target = target;
        _radius = radius;
        _damage = damages;
        _enemyLayer = enemyLayer;
        _prefabVFX = prefabVFX;
    }


    protected override IEnumerator SpellCoroutine()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_origin.transform.position + new Vector3(_target.x, _target.y, 0), _radius, _enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.transform.tag == "Enemy")
            {
                // Enemy.TakeDamage(_damage);

                GameObject VFX = Instantiate(_prefabVFX, hit.transform);

                yield return new WaitForSeconds(_timeBeforeDeleting);

                Destroy(VFX);
            }
        }

        Destroy(gameObject);

        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_origin.transform.position + new Vector3(_target.x, _target.y, 0), _radius);

        if (!Application.isPlaying) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(_origin.transform.position + new Vector3(_target.x, _target.y, 0), _radius, _enemyLayer);

        Gizmos.color = Color.green;

        foreach (var hit in hits)
        {
            Gizmos.DrawLine(_origin.transform.position + new Vector3(_target.x, _target.y, 0), hit.transform.position);
            Gizmos.DrawSphere(hit.transform.position, 0.2f);
        }
    }

}
// GameObject proj = Instantiate(_prefab, target, Quaternion.identity);

