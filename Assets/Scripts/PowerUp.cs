using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private float floatTime = 0.5f;
    private float timePassed = 0;
    private int direction = 1;
    private Player playerScript;
    void Update()
    {
        //controls direction and speed power up sprite moves in place
        Vector2 position = transform.position;

        position.y += direction * 0.5f * Time.deltaTime; 
        transform.position = position;

        timePassed += Time.deltaTime;

        if (timePassed >= floatTime)
        {
            timePassed = 0;
            direction *= -1;
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject player = collision.gameObject;
            playerScript = player.GetComponent<Player>();

            playerScript.ActivatePowerUp();
            Destroy(gameObject);
        }
    }
}
