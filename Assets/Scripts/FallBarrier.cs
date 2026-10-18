using UnityEngine;

public class FallBarrier : MonoBehaviour
{
    [SerializeField] private Player player;
    private void OnCollisionEnter2D(Collision2D other)
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
