using UnityEngine;
using System.Collections;

public class DynamicMusic : MonoBehaviour
{
    [SerializeField]
    float fadeTime = 1;

    [SerializeField]
    AudioSource bass, cymbals, guitar, hiHat, kickDrum, snare, toms;
    
	public IEnumerator ToggleTrack(bool on, string trackName)
    {
        AudioSource track = null;

        switch (trackName)
        {
            case "bass":
                track = bass;
                break;
            case "cymbals":
                track = cymbals;
                break;
            case "guitar":
                track = guitar;
                break;
            case "hiHat":
                track = hiHat;
                break;
            case "kickDrum":
                track = kickDrum;
                break;
            case "snare":
                track = snare;
                break;
            case "toms":
                track = toms;
                break;
            default:
                throw new System.Exception("you typed the string wrong when you tried to toggle a music track");
        }

        float originalVolume = track.volume;

        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            track.volume = Mathf.Lerp(originalVolume, on == false ? 0 : 1, elapsedTime / fadeTime);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //track.volume = on == false ? 0 : 1;
    }
}
