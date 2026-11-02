using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    //tutorial used - https://www.youtube.com/watch?v=GtX1p4cwYOc

    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    
    void Start()
    {
        transform.position = points[startingPoint].position; //setting position of platform to the chosen position in the startingPoint variable
    }


    void Update()
    {
        //checks distance of platform and point
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if(i == points.Length) //checks if platform was at last point after index increases
            {
                i = 0; //reset index
            }
        }

        //moves platform to the point at position i
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

}
