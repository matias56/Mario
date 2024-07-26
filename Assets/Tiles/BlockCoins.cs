using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoins : MonoBehaviour
{

    private GameManager gm;
    private AudioSource s;
    public AudioClip coin;

    private void Start()
    {

        s = GetComponent<AudioSource>();
        s.clip = coin;
        s.Play();
        StartCoroutine(Animate());

        gm = FindObjectOfType<GameManager>();

        gm.AddCoin();
    }

    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        Destroy(gameObject);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }

}
