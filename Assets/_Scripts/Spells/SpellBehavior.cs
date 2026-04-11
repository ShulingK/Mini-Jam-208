using System.Collections;
using UnityEngine;

public abstract class SpellBehavior : MonoBehaviour
{
    public void Cast()
    {
        StartCoroutine(SpellCoroutine());
    }

    protected abstract IEnumerator SpellCoroutine();
}
