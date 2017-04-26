using UnityEngine;
using System.Collections;

public class CrossFade : MonoBehaviour
{
    public bool fadeIn = true, dynamic = false;

    AudioSource source;
    AudioSource[] sources;

    bool changedScene;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        switch (dynamic)
        {
            case true:
                sources = GetComponentsInChildren<AudioSource>();
                break;
            case false:
                source = GetComponent<AudioSource>();
                break;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(FadeOutAndDestroySelf());
    }

    IEnumerator FadeIn()
    {
        source.volume = 0;

        float elapsedTime = 0;
        float timer = 5;
        while (elapsedTime < timer)
        {
            source.volume = Mathf.Lerp(0, 1, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        source.volume = 1;
    }

    IEnumerator FadeOutAndDestroySelf()
    {
        switch (dynamic)
        {
            case true:
                print("should fade out");
                float elapsedTime = 0;
                float timer = 2;
                foreach (AudioSource a in sources)
                {
                    a.volume = 1;
                    
                    while (elapsedTime < timer)
                    {
                        a.volume = Mathf.Lerp(1, 0.1f, elapsedTime / timer);

                        elapsedTime += Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }

                    a.volume = 0.1f;
                }
                elapsedTime = 0;
                timer = 2;
                foreach (AudioSource a in sources)
                {
                    while (elapsedTime < timer)
                    {
                        a.volume = Mathf.Lerp(0.1f, 0, elapsedTime / timer);

                        elapsedTime += Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }

                    a.volume = 0;
                }
                break;
            case false:
                source.volume = 1;

                elapsedTime = 0;
                timer = 5;
                while (elapsedTime < timer)
                {
                    source.volume = Mathf.Lerp(1, 0.1f, elapsedTime / timer);

                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                source.volume = 0.1f;

                elapsedTime = 0;
                timer = 5;
                while (elapsedTime < timer)
                {
                    source.volume = Mathf.Lerp(0.1f, 0, elapsedTime / timer);

                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                source.volume = 0;
                break;
        }

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
