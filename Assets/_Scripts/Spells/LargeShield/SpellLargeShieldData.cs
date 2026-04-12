using UnityEngine;

[CreateAssetMenu(fileName = "new Large Spell Shield", menuName = "Spell/Large Shield")]
public class SpellLargeShieldData : SpellData
{
    [SerializeField] private float _shieldTime = 1.5f;

    public override void Cast(Transform origin, Vector2 target, bool isFacingRight)
    {
        GameObject proj = Instantiate(_prefab, origin.transform.position + new Vector3(0, 5, 0), Quaternion.identity);

        if (proj.TryGetComponent<SpellLargeShieldBehavior>(out var behavior))
        {
            behavior.Init(_shieldTime);
            behavior.Cast();
        }
    }
}
