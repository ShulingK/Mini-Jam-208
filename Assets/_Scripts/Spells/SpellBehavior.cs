using System.Collections;
using UnityEngine;

public abstract class SpellBehavior : MonoBehaviour
{
    protected int _damage;
    protected string _tagTarget;

    public void Cast()
    {
        StartCoroutine(SpellCoroutine());
    }

    protected abstract IEnumerator SpellCoroutine();
}
