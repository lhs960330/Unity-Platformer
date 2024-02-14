using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Coroutine gemCoroutine;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Collider2D collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gemCoroutine = StartCoroutine(GemRe());
        Player.GetComponent<PlayerController>().index++;
    }
    IEnumerator GemRe()
    {
        render.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(3);

        render.enabled = true;
        collider.enabled = true;
    }
}
