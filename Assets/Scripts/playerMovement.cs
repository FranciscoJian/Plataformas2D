using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 1;
    [SerializeField]
    private float jumpForce = 300;
    [SerializeField]
    private float rayLength = 1;

    Rigidbody2D rb;
    SpriteRenderer sr;
    public Transform camPoint;

    public LayerMask mask;
    public List<Vector3> originPoints;

    [SerializeField]
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = rb.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ----------------------------------- MOVIMIENTO HORIZONTAL ----------------------------------- //

        float horizontal = Input.GetAxis("Horizontal");
        transform.position = transform.position + new Vector3(horizontal * Time.deltaTime * playerSpeed, 0);

        if (Input.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
            camPoint.transform.localPosition = new Vector3(-0.5f, camPoint.transform.localPosition.y, camPoint.transform.localPosition.z);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
            camPoint.transform.localPosition = new Vector3(0.5f, camPoint.transform.localPosition.y, camPoint.transform.localPosition.z);
        }

            // ----------------------------------- SALTO Y RAYCAST ----------------------------------- //

            grounded = false;

        for (int i = 0; i < originPoints.Count; i++)
        {
            Debug.DrawRay(transform.position + originPoints[i], Vector3.down * rayLength, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + originPoints[i], Vector3.down, rayLength, mask);
            if (hit.collider != null)
            {

                if (hit.collider.tag == "MobilePlatform")
                {
                    transform.parent = hit.transform;
                } else
                {
                    transform.parent = null;
                }

                // ----------------------------------- Detectar Plataformas Negativas ----------------------------------- //

                if (hit.collider.tag == "EnemyPlatform")
                {
                    transform.parent = hit.transform;
                    gameObject.SetActive(false);
                }
              

                // -----------------------------------  ----------------------------------- //

                grounded = true;
                Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.green);
            } else
            {
                transform.parent = null;
            }
        }

        if (Input.GetButtonDown("Jump") && grounded == true) {
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
