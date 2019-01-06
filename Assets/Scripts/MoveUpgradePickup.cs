using UnityEngine;

public class MoveUpgradePickup : Interactable
{
    public ParticleSystem pickupParticles;

    public bool adjustGround;
    public bool adjustJump;
    public bool adjustGlide;
    public bool adjustWings;


    // temporary movement upgrades for picking up items
    public bool canSprint;
    public bool canGlide;
    public bool hasWings;
    public int airJumps;

    //temporary move characteristics 
    //movement characteristics
    public float playerSpeed;
    public float jumpForce;
    public float walkModifier;
    public float runModifier;
    public float glidingDescent;
    public float wingFlapFrequency;
    public float wingPower;

    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        if(base.isInRange)
        {
            Instantiate(pickupParticles, transform);
            if (adjustGround)
            {
                player.GetComponent<PlayerController>().playerSpeed = playerSpeed;
                player.GetComponent<PlayerController>().canSprint = canSprint;
            }
            if (adjustGlide)
            {
                player.GetComponent<PlayerController>().canGlide = canGlide;
                player.GetComponent<PlayerController>().glidingDescent = glidingDescent;
            }
            if (adjustJump)
            {
                player.GetComponent<PlayerController>().jumpForce = jumpForce;
                player.GetComponent<PlayerController>().airJumps = airJumps;

            }
            if (adjustWings)
            {
                player.GetComponent<PlayerController>().hasWings = hasWings;
                player.GetComponent<PlayerController>().wingFlapFrequency = wingFlapFrequency;
                player.GetComponent<PlayerController>().wingPower = wingPower;

            }
        }


        
    }
}
