using UnityEngine;

public class FloorRadio4 : Floor
{

	public override int ID
	{
		get
		{
			return 17;
		}
	}

    private void FixedUpdate()
    {
        if (floors[Globals.playerFloor] == this && Timer >= 0)
        {
            Timer = Timer + Time.fixedDeltaTime;

            if (Timer >= 3.5f)
            {
                Globals.radioSrc.PlayOneShot(Globals.mapGen.radioClips[3]);
                Timer = -1.0f;
            }
        }
    }
}