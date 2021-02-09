using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGlobalsInitializer : MonoBehaviour
{
	[Header("Audio Sources")]
	public AudioSource matchSrc;

	[Header("Audio Clips")]
	public AudioClip fireOn;
	public AudioClip fireOff;

	[Header("Game Objects")]
	public Light matchLight;

	[Header("Data")]
	public bool isMatchOff = false;

    private void Awake()
    {
		MainMenuGlobals.matchSrc = matchSrc;
		MainMenuGlobals.fireOn = fireOn;
		MainMenuGlobals.fireOff = fireOff;
		MainMenuGlobals.matchLight = matchLight;
		MainMenuGlobals.isMatchOff = isMatchOff;
    }
}

public class MainMenuGlobals
{
	public static AudioSource matchSrc;

	public static AudioClip fireOn;
	public static AudioClip fireOff;

	public static Light matchLight;

	public static bool isMatchOff = false;
}
