using UnityEngine;
using UnityEngine.UI;
public class MoveTextUpdate : MonoBehaviour
{
    public PlayerController player;
    public bool showGround;
    public bool showJump;
    public bool showGlide;
    public bool showWings;
    public bool showMouse;
    public Text thistext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showGlide)
        {
            thistext.text = "Glide: " + player.canGlide.ToString();
        }
        if (showGround)
        {
            thistext.text = "Sprint: " + player.canSprint.ToString()+" Speed: "+player.playerSpeed.ToString();
        }
        if (showJump)
        {
            if (player.airJumps > 0)
            {
                thistext.text = "AirJumps: " + player.airJumps.ToString() + " Force: " + player.jumpForce.ToString();
            } else
            {
                thistext.text = "Jump Force: " + player.jumpForce.ToString();
            }
            
        }
        if (showWings)
        {
            if (player.hasWings)
            {
                thistext.text = "Wings: " + player.hasWings.ToString() + " Force: " + player.wingPower.ToString() + " Freq: " + player.wingFlapFrequency.ToString();
            }
            else
            {
                thistext.text = "Wings: " + player.hasWings.ToString();
            }
        }

        if (showMouse)
        {
            thistext.text = "Look Sensitivity: "+Camera.main.GetComponent<ThirdPersonCamera>().mouseSensitivity.ToString();
        }

    }
}
