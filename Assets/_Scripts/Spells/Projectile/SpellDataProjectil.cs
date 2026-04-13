using UnityEngine;

[CreateAssetMenu(fileName = "New Projectil", menuName = "Spell/Projectil")]
public class SpellDataProjectil : SpellData
{
    public override void Cast(Transform origin, Vector2 dir, bool isFacingRight)
    {
        GameObject proj = Instantiate(_prefab, origin.position, Quaternion.identity);

        AudioManager.Instance.PlayOneShot(AudioEvent.Instance._shoot1);

        if (proj.TryGetComponent<SpellProjectilBehavior>(out var behavior))
        {
            behavior.Init(dir, _tagTarget, _damage);
            behavior.Cast();
        }
    }
}
