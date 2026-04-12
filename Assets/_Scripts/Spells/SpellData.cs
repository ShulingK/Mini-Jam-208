using UnityEngine;

public abstract class SpellData : ScriptableObject
{
    public string spellName;
    public float cooldown;
    public GameObject _prefab;

    public int _damage;
    public string _tagTarget;

    public abstract void Cast(Transform origin, Vector2 target, bool isFacingRight);
}