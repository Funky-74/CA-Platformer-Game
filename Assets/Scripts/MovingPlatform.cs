using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    
    void Start()
    {
        transform.position = points[startingPoint].position; //setting position of platofrm to position of one of the points using index 'starting point'
    }

    // Update is called once per frame
    void Update()
    {
        //checks distance of platform and point
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if(i == points.Length) //checks if plaatform was at last point after index increases
            {
                i = 0; //reset index
            }
        }

        //moves platform to the point at position i
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }*/
}
