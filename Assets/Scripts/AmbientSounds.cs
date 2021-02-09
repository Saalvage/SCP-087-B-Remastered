using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmbientSounds : MonoBehaviour
{
    public AudioClip[] cAmbience;
    public GameObject goPlayer;

    private void Update()
    {
        if (Random.Range(1, 1501) < 2)
        {
            AudioSource.PlayClipAtPoint(cAmbience[Random.Range(1, cAmbience.Length)],
                new Vector3(goPlayer.transform.position.x + Random.Range(-1f, 25f),
                goPlayer.transform.position.y + Random.Range(-2f, -20f),
                goPlayer.transform.position.z + Random.Range(-1f, 30f)));
        }
    }
}
