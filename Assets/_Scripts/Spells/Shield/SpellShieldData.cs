using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new Spell Shield", menuName = "Spell/Shield")]
public class SpellShieldData : SpellData
{
    [SerializeField] private float _shieldTime = 1f;

    public override void Cast(Transform origin, Vector2 target, bool isFacingRight)
    {
        GameObject proj = Instantiate(_prefab, origin.transform.position + new Vector3(target.x, target.y, 0), Quaternion.identity);

        if (proj.TryGetComponent<SpellShieldBehavior>(out var behavior))
        {
            behavior.Init(_shieldTime);
            behavior.Cast();
        }
    }
}
