using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    private Rigidbody rb;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction = transform.forward * move.y;
        if (Input.mousePosition.x < Camera.main.scaledPixelWidth / 4)
        {
            transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0));
            //rb.MoveRotation(transform.rotation);
        }
        else if(Input.mousePosition.x > (Camera.main.scaledPixelWidth / 4) * 3)
        {
            transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
            //rb.MoveRotation(transform.rotation);
        }
    }
    private void FixedUpdate()
    { 
        rb.velocity = direction.normalized * moveSpeed * Time.fixedDeltaTime * 10;
    }
}
