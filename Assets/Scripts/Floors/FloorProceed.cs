using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FloorProceed : Floor
{
	public override int ID
	{
		get
		{
			return 8;
		}
	}

    private void FixedUpdate()
    {
        if(floors[Globals.playerFloor] == this && Timer >= 0)
        {
			Timer = Timer + Time.fixedDeltaTime;

			if(Timer >= 1.5f)
            {
				Globals.radioSrc.PlayOneShot(Globals.M_GEN.radioClips[0]);
				Timer = -1.0f;
            }
        }
    }
}
