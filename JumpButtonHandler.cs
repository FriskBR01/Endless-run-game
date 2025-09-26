using UnityEngine;

public class JumpButtonHandler : MonoBehaviour
{
    public PlayerController player;

    public void OnJumpButton()
    {
        if (player != null && player.IsGrounded())
        {
            player.Jump();
        }
    }
}
