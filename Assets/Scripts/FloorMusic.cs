using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FloorMusic : MonoBehaviour
{
    [Header("Variables")]
    public Vector2 deepRange = new Vector2(49, 100);
    public Vector2 beneathRange = new Vector2(99, 201);
    public bool isFading = false;

    private void Start()
    {
        Globals.musicSrc.clip = Globals.gatheringDarkness;
        Globals.musicSrc.Play();
    }

    private void FixedUpdate()
    {
        if(Globals.playerFloor > deepRange.x &&
            Globals.playerFloor < deepRange.y)
        {
            if (isFading == true) return;
            else if (Globals.musicSrc.clip == Globals.deepHell) return;
            else FadeOutToClip(Globals.musicSrc, 0.1f, Globals.deepHell);
        }
        else if(Globals.playerFloor > beneathRange.x &&
            Globals.playerFloor < beneathRange.y)
        {
            if (isFading == true) return;
            else if(Globals.musicSrc.clip == Globals.fromBeneath) return;
            else FadeOutToClip(Globals.musicSrc, 0.1f, Globals.fromBeneath);
        }
    }

    void FadeOutToClip(AudioSource audioSource, float FadeTime, AudioClip clip)
    {
        isFading = true;
        float startVolume = 0.3f;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
        }

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;
        }
        audioSource.volume = startVolume;
        isFading = false;
    }
}
