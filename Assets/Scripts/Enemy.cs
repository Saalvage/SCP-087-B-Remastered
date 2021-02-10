using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Spawner.STATE enemyState = Spawner.STATE.IDLE;
	public Spawner.ENEMYTYPE enemyType = Spawner.ENEMYTYPE.MENTAL;

	public Animator enemyAnimator;
	public SkinnedMeshRenderer enemyRenderer;
}
