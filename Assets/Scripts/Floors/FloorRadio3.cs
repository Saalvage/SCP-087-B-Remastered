using UnityEngine;

public class FloorRadio3 : Floor
{

	public override int ID
	{
		get
		{
			return 16;
		}
	}

    private void FixedUpdate()
    {
        if (floors[Globals.playerFloor] == this && Timer >= 0)
        {
            Timer = Timer + Time.fixedDeltaTime;

            if (Timer >= 3.5f)
            {
                Globals.radioSrc.PlayOneShot(Globals.mapGen.radioClips[2]);
                Timer = -1.0f;
            }
        }
    }
}