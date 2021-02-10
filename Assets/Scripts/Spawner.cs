using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
	public GameObject glimpsePrefab;

	public MapGen mapGen;
	public Material[] glimpseMats;
	public Camera mainCam;

	public List<GameObject> glimpses = new List<GameObject>();
	public AudioClip noClip;

	public enum ENEMYTYPE
	{
		MENTAL = 0,
		REDMIST = 1,
		EYEKILLER = 2
	}
	public enum STATE
	{
		IDLE = 0,
		WALK = 1,
		RUN = 2,
	}

	private void FixedUpdate()
	{
		UpdateGlimpses();
	}

	public void CreateEnemy(ENEMYTYPE type, Vector3 pos, STATE state)
	{
		GameObject go = Instantiate(Globals.enemyPrefab, pos, Quaternion.identity);
		go.AddComponent<Enemy>();

		Enemy enemy = go.GetComponent<Enemy>();
		enemy.enemyRenderer = go.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
		enemy.enemyAnimator = go.GetComponent<Animator>();
		enemy.enemyType = type;
		enemy.enemyState = state;

		switch (type)
		{
			case ENEMYTYPE.MENTAL:
				enemy.enemyRenderer.material = Globals.mentalMaterial;
				break;
			case ENEMYTYPE.REDMIST:
				enemy.enemyRenderer.material = Globals.redmistMaterial;
				break;
			case ENEMYTYPE.EYEKILLER:
				enemy.enemyRenderer.material = Globals.eyekillerMaterial;
				break;
			default:
				enemy.enemyRenderer.material = Globals.mentalMaterial;
				break;
		}
		switch (state)
		{
			case STATE.IDLE:
				enemy.enemyAnimator.SetTrigger("Idle");
				break;
			case STATE.RUN:
				enemy.enemyAnimator.SetTrigger("Run");
				break;
			case STATE.WALK:
				enemy.enemyAnimator.SetTrigger("Walk");
				break;
		}
	}

	public void UpdateGlimpses()
	{
		foreach (GameObject gp in glimpses)
		{
			RaycastHit hit;
			if (Physics.Raycast(mainCam.transform.position, mainCam.transform.TransformDirection(Vector3.forward), out hit))
			{
				if ((gp.transform.position - mainCam.transform.position).sqrMagnitude < 5.30f)
				{ // 2.3f
					AudioSource.PlayClipAtPoint(noClip, gp.transform.position);
					glimpses.Remove(gp);
					Destroy(gp);
				}
			}
		}
	}

	public void CreateGlimpses()
	{
		for (int i = 1; i < (mapGen.floorAmount - 1); i++)
		{
			if (Floor.floors[i].ID == Floor.ACT_NONE && Random.Range(1, 10) == 1)
			{
				// TODO exceptions for certain rooms
				GameObject g = Instantiate(glimpsePrefab) as GameObject;
				g.GetComponent<MeshRenderer>().material = glimpseMats[Random.Range(0, glimpseMats.Length)];
				g.transform.position = new Vector3(Random.Range(-0.75f, -11.25f), -i * 3 - 0.75f, i % 2 == 0 ? 0.75f : 9.75f);
				g.transform.parent = Floor.floors[i].transform;
				glimpses.Add(g);
			}
		}
	}
}
