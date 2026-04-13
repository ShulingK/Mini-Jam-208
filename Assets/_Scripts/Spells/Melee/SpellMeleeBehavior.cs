using System.Collections;
using UnityEngine;

public class SpellMeleeBehavior : SpellBehavior
{
    private Transform _origin;
    private Vector2 _target;
    private float _radius;
    private LayerMask _enemyLayer;
    private GameObject _prefabVFX;

    [SerializeField] private float _timeBeforeDeleting = 0.8f; 

    public void Init(Transform origin, Vector2 target, float radius,int damages, LayerMask enemyLayer, GameObject prefabVFX, string tagTarget)
    {
        _origin = origin;
        _target = target;
        _radius = radius;
        _damage = damages;
        _enemyLayer = enemyLayer;
        _prefabVFX = prefabVFX;
        _tagTarget = tagTarget;
    }


    protected override IEnumerator SpellCoroutine()
    {
        transform.SetParent(_origin);

        Collider2D[] hits = Physics2D.OverlapCircleAll(_origin.transform.position + new Vector3(_target.x, _target.y, 0), _radius, _enemyLayer);

        transform.localPosition = new Vector3(1.14f, -0.19f, 0f);

        foreach (Collider2D hit in hits)
        {
            Debug.Log("target : " + _tagTarget + "  hit : " + hit.transform.tag);

            if (hit.transform.tag == _tagTarget)
            {
                if (hit.TryGetComponent<PlayerController>(out PlayerController playerController))
                {
                    playerController.TakeDamage(_damage);

                    GameObject VFX = Instantiate(_prefabVFX, hit.transform);

                    yield return new WaitForSeconds(_timeBeforeDeleting);

                    AudioManager.Instance.PlayOneShot(AudioEvent.Instance._melee);

                    Destroy(VFX);
                }
                else if (hit.TryGetComponent<EnemyController>(out EnemyController enemyController))
                {
                    enemyController.TakeDamage(_damage);

                    GameObject VFX = Instantiate(_prefabVFX, hit.transform);

                    yield return new WaitForSeconds(_timeBeforeDeleting);

                    AudioManager.Instance.PlayOneShot(AudioEvent.Instance._melee);

                    Destroy(VFX);
                }
            }
        }

        Destroy(gameObject, _timeBeforeDeleting);

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

