using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float maxInteractDistance = 20f;
    [SerializeField] private float maximumViewAngles = 45;
    private Rigidbody rb;
    private Camera FPCam;
    private float camTurnTracker = 0f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector3 moveDirection;


    public int Health { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        FPCam = GetComponentInChildren<Camera>();
        Health = 3;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 direction = new Vector3(move.x, 0f, move.y).normalized;
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        if (Input.mousePosition.x < FPCam.scaledPixelWidth / 4)
        {
            transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0));
        }
        else if(Input.mousePosition.x > (FPCam.scaledPixelWidth / 4) * 3)
        {
            transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
        }

        if (Input.mousePosition.y < FPCam.scaledPixelHeight / 4 && camTurnTracker > -maximumViewAngles)
        {
            camTurnTracker -= turnSpeed * Time.deltaTime;
            FPCam.transform.Rotate(new Vector3(turnSpeed * Time.deltaTime, 0, 0));
        }
        else if (Input.mousePosition.y > (FPCam.scaledPixelHeight / 4) * 3 && camTurnTracker < maximumViewAngles)
        {
            camTurnTracker += turnSpeed * Time.deltaTime;
            FPCam.transform.Rotate(new Vector3(-turnSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if(Physics.Raycast(FPCam.transform.position, transform.forward, out hit, maxInteractDistance))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }

    }
    private void FixedUpdate()
    { 
        rb.velocity = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime * 10;
    }

    public void TakeHit()
    {
        Health -= 1;
        Debug.Log(Health);
        EventSystem.CallEvent(EventType.ON_PLAYER_DAMAGED);
        if(Health == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

}
