using UnityEngine;

public class FloorFlash : Floor
{

	public override int ID
	{
		get
		{
			return 3;
		}
	}

	public FloorFlash()
	{
		Timer = 1.0f + (float) (Floor.mapGenRandom.NextDouble() * 3); // 1 - 4
	}

}