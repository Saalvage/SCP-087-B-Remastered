using UnityEngine;

public class Globals
{
	public static Dreamfilter dFilter;
	public static MapGen mapGen;

	public static AudioSource matchSrc;
	public static AudioSource radioSrc;
	public static AudioSource musicSrc;

	public static AudioClip fireOn;
	public static AudioClip fireOff;
	public static AudioClip gatheringDarkness;
	public static AudioClip deepHell;
	public static AudioClip fromBeneath;

	public static Light matchLight;
	public static GameObject mapParent;
	public static GameObject enemyPrefab;
	public static GameObject playerObject;

	public static Material mentalMaterial;
	public static Material redmistMaterial;
	public static Material eyekillerMaterial;

	public static bool isMatchOff = false;
	public static int playerFloor = 0;
	public static int mapSeed;
}

public class GlobalsInitializer : MonoBehaviour
{
	[Header("Classes")]
	public Dreamfilter dFilter;
	public MapGen mapGen;

	[Header("Audio Sources")]
	public AudioSource matchSrc;
	public AudioSource radioSrc;
	public AudioSource musicSrc;

	[Header("Audio Clips")]
	public AudioClip fireOn;
	public AudioClip fireOff;
	public AudioClip gatheringDarkness;
	public AudioClip deepHell;
	public AudioClip fromBeneath;

	[Header("Game Objects")]
	public Light matchLight;
	public GameObject mapParent;
	public GameObject enemyPrefab;
	public GameObject playerObject;

	[Header("Materials")]
	public Material mentalMaterial;
	public Material redmistMaterial;
	public Material eyekillerMaterial;

	[Header("Data")]
	public bool isMatchOff = false;
	public int playerFloor = 0;
	public int mapSeed;

	private void Awake()
    {
		Globals.dFilter = dFilter;
		Globals.mapGen = mapGen;

		Globals.matchSrc = matchSrc;
		Globals.radioSrc = radioSrc;
		Globals.musicSrc = musicSrc;

		Globals.fireOff = fireOff;
		Globals.fireOn = fireOn;
		Globals.gatheringDarkness = gatheringDarkness;
		Globals.deepHell = deepHell;
		Globals.fromBeneath = fromBeneath;

		Globals.matchLight = matchLight;
		Globals.mapParent = mapParent;
		Globals.enemyPrefab = enemyPrefab;
		Globals.playerObject = playerObject;

		Globals.mentalMaterial = this.mentalMaterial;
		Globals.redmistMaterial = this.redmistMaterial;
		Globals.eyekillerMaterial = this.eyekillerMaterial;

		Globals.isMatchOff = isMatchOff;
		Globals.playerFloor = playerFloor;
		Globals.mapSeed = mapSeed;
    }

    private void FixedUpdate()
    {
        matchLight = Globals.matchLight;
        mapParent = Globals.mapParent;
        enemyPrefab = Globals.enemyPrefab;
        playerObject = Globals.playerObject;

        isMatchOff = Globals.isMatchOff;
        playerFloor = Globals.playerFloor;
		mapSeed = Globals.mapSeed;
	}
}