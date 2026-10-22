using UnityEngine;

public class FallBarrier : MonoBehaviour
{
    [SerializeField] private Player player;
    private void OnCollisionEnter2D(Collision2D other) //put in player script instead in collsision section, assign fallbarrier tag to barrier
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            Player p = other.gameObject.GetComponent<Player>();

            if (!p.isInvin)
            {
                p.PlayerHit();
            }
        }
    }
}
