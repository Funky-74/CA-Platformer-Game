using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float floatTime = 0.5f;
    private float timePassed = 0;
    private int direction = 1;
    private float powerUpTime = 5f;
    private float powerUpCount = 0f;
    private bool isPower = false;
    private Player playerScript;
    private Color originalColor;
    void Update()
    {
        Vector2 position = transform.position;

        position.y += direction * 0.5f * Time.deltaTime; // 0.5f controls speed its going up and down at
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
        if(collision.tag == "Player" && !isPower)
        {
            GameObject player = collision.gameObject;
            playerScript = player.GetComponent<Player>();

            playerScript.speed = 10;
            isPower = true;
            powerUpCount = 0f;

            Destroy(gameObject);

            player.GetComponent<SpriteRenderer>().color = new Color(0.53f, 0.81f, 0.92f, 1f);




        }
    }
}
