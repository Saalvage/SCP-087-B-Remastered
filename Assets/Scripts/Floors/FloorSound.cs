using UnityEngine;

public class FloorSound : Floor
{

	public override int ID
	{
		get
		{
			return 1;
		}
	}

	private Vector3 pos;
	private Vector3 dir;

	private AudioClip AudioClip;
	private float OverflowValue;
	private float MinTimeOut;

	void Start()
	{
		if (Timer == 1)
		{
			AudioClip = Globals.M_GEN.loudBreath;
			OverflowValue = 22;
			MinTimeOut = 10;
		}
		else
		{
			AudioClip = Globals.M_GEN.loudStep;
			OverflowValue = 3;
			MinTimeOut = 1;
		}
		
		pos = transform.position + transform.forward * 6f + Random.insideUnitSphere * 10f;
		Vector2 Circle = Random.insideUnitCircle.normalized;
		dir = new Vector3(Circle.x, 0f, Circle.y);
		Timer = OverflowValue;
	}

	void FixedUpdate()
	{
		if (floors[Globals.playerFloor] == this && Timer >= 0)
		{
			Timer += Time.fixedDeltaTime;
			if (Timer > OverflowValue)
			{
				AudioSource.PlayClipAtPoint(AudioClip, pos);
				pos += dir;
				if (Random.value < 0.1f)
				{
					Timer = -1f;
				}
				else
				{
					Timer = Random.value * (OverflowValue - MinTimeOut);
				}
			}
		}
	}

}
