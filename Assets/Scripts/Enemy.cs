using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public enum STATE
	{
		IDLE,
		WALK,
	}

	public static void CreateEnemy(Vector3 pos, bool idle)
	{
		GameObject go = Instantiate(Globals.enemyPrefab, pos, Quaternion.identity);
		Animator a = go.GetComponent<Animator>();
		a.SetTrigger(idle ? "Idle" : "Walk");
		go.AddComponent<Enemy>();
	}

}
