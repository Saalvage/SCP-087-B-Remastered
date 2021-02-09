using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
	public GameObject glimp;

	public MapGen mgen;
	public Material[] glimpseMats;
	public Camera maincam;

	public List<GameObject> glimpses = new List<GameObject>();
	public AudioClip noClip;

	private void FixedUpdate()
	{
		UpdateGlimpses();
	}

	public void UpdateGlimpses()
	{
		foreach (GameObject gp in glimpses.ToList())
		{
			RaycastHit hit;
			if (Physics.Raycast(maincam.transform.position, maincam.transform.TransformDirection(Vector3.forward), out hit))
			{
				if ((gp.transform.position - maincam.transform.position).sqrMagnitude < 5.30f)
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
		for (int i = 1; i < (mgen.floorAmount - 1); i++)
		{
			if (Floor.floors[i].ID == Floor.ACT_NONE && Random.Range(1, 10) == 1)
			{
				// TODO exceptions for certain rooms
				GameObject g = Instantiate(glimp) as GameObject;
				g.GetComponent<MeshRenderer>().material = glimpseMats[Random.Range(0, glimpseMats.Length)];
				g.transform.position = new Vector3(Random.Range(-0.75f, -11.25f), -i * 3 - 0.75f, i % 2 == 0 ? 0.75f : 9.75f);
				g.transform.parent = Floor.floors[i].transform;
				glimpses.Add(g);
			}
		}
	}
}
