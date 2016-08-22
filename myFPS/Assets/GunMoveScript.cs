using UnityEngine;
using System.Collections;

public class GunMoveScript : MonoBehaviour
{
    public Animation animation;
    // Use this for initialization
    enum PlayerState { IDLE, WALKING, SPRINT };
    PlayerState playerState = PlayerState.IDLE;
    void Start()
    {
        if (animation == null)
        {
            Debug.Log("forget to place animation in inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
        StateControl();
    }
    void PlayerControl()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Sprint") != 0)
            {
                playerState = PlayerState.SPRINT;
            }
            else
            {
                playerState = PlayerState.WALKING;
            }
        }
        else
        {
            playerState = PlayerState.IDLE;
        }

    }
    void StateControl()
    {
        Debug.Log(playerState.ToString());
        switch (playerState)
        {
            case PlayerState.IDLE:
                Debug.Log("idle");
                animation.animation.CrossFade("IdleAnimation", 0.4f);
                break;
            case PlayerState.WALKING:
                animation.animation.CrossFade("WalkingAnimation", 0.4f);
                break;
            case PlayerState.SPRINT:
                animation.animation.CrossFade("SprintAnimation", 0.4f);
                break;
            default:
                break;

        }
    }
}
