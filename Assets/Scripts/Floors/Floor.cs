using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Floor : MonoBehaviour {

	public virtual int ID
	{
		get
		{
			return 0;
		}
	}

	public float Timer = 0.0f;
	
	public static Floor[] floors;
	public static Random mapGenRandom;

	public GameObject actualFloor;
	public MeshRenderer meshRenderer;
	public NavMeshSurface navSurface;

    #region ACTS
    public const int ACT_NONE = 0;
	public const int ACT_STEPS = 1;
	public const int ACT_LIGHTS = 2;
	public const int ACT_FLASH = 3;
	public const int ACT_WALK = 4;
	public const int ACT_RUN = 5;
	public const int ACT_KALLE = 6;
	public const int ACT_BREATH = 7;
	public const int ACT_PROCEED = 8;
	public const int ACT_TRAP = 9;
	public const int ACT_173 = 11;
	public const int ACT_CELL = 12;
	public const int ACT_LOCK = 13;
	public const int ACT_RADIO2 = 15;
	public const int ACT_RADIO3 = 16;
	public const int ACT_RADIO4 = 17;
	public const int ACT_TRICK1 = 18;
	public const int ACT_TRICK2 = 19;
	public const int ACT_ROAR = 20;
	public const int ACT_DARKNESS = 21;
	public const int ACT_BEHIND = 22;
    #endregion

    public static void CreateFloor<T>(int i) where T : Floor
	{
		if (floors[i] != null)
		{
			int dir = mapGenRandom.Next(0, 1) * -2 + 1; // -1 or 1
			int inc = 1;
			while (i > 0 && i < floors.Length && floors[i] != null)
			{
				i += inc * dir;
				inc++;
				dir *= -1;
			}
			if (floors[i] != null) // Could not find a location!
			{
				throw new UnityException("Could not set floor action!");
			}
		}
		floors[i] = Instantiate(Globals.mapGen.floorPrefab, Globals.mapParent.transform).AddComponent<T>();
	}

	public static void CreateFloor<T>(int i, int timer) where T : Floor
	{
		CreateFloor<T>(i);
		floors[i].Timer = timer;
	}

}
