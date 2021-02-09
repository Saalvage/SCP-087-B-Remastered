using UnityEngine;

public class FloorRadio2 : Floor
{

    public override int ID
    {
        get
        {
            return 15;
        }
    }

    private void FixedUpdate()
    {
        if (floors[Globals.playerFloor] == this && Timer >= 0)
        {
            Timer = Timer + Time.fixedDeltaTime;

            if (Timer >= 3.5f)
            {
                Globals.radioSrc.PlayOneShot(Globals.M_GEN.radioClips[1]);
                Timer = -1.0f;
            }
        }
    }
}