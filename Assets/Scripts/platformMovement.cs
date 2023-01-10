using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour
{

    public List<Transform> points;
    int actualPoint;

    [SerializeField]
    private float platformSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, points[actualPoint].position) < 0.1f)
        {
            actualPoint++;
            if (actualPoint >= points.Count)
            {
            actualPoint = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, points[actualPoint].position, platformSpeed * Time.deltaTime);
    }
}
