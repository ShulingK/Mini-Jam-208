using UnityEngine;

public abstract class SpellData : ScriptableObject
{
    public string spellName;
    public float cooldown;

    public abstract void Cast(Transform origin, Vector2 target, bool isFacingRight);
}