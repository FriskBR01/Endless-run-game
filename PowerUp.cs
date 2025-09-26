using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 5f; // duração do power-up

    public void Collect(PlayerController player)
    {
        player.StartGravityPower(duration);
        Destroy(gameObject);
    }
}
