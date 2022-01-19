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
        float horizontalRotationValue = Mathf.Clamp(Input.mousePosition.x, 0, FPCam.scaledPixelWidth);
        if (horizontalRotationValue < FPCam.scaledPixelWidth / 4)
        {
            float scaledHorizontalRotation = 1 - (horizontalRotationValue / (FPCam.scaledPixelWidth / 4));
            transform.Rotate(new Vector3(0, -turnSpeed * scaledHorizontalRotation * Time.deltaTime, 0));
        }
        else if(horizontalRotationValue > (FPCam.scaledPixelWidth / 4) * 3)
        {
            float scaledHorizontalRotation = (horizontalRotationValue - ((FPCam.scaledPixelWidth / 4) * 3)) / (FPCam.scaledPixelWidth / 4);
            transform.Rotate(new Vector3(0, turnSpeed * scaledHorizontalRotation * Time.deltaTime, 0));
        }
        float verticalRotationValue = Mathf.Clamp(Input.mousePosition.y, 0, FPCam.scaledPixelHeight);
        if (verticalRotationValue < FPCam.scaledPixelHeight / 4 && camTurnTracker > -maximumViewAngles)
        {
            float scaledVerticalRotation = 1 - (verticalRotationValue / (FPCam.scaledPixelHeight / 4));
            camTurnTracker -= turnSpeed * scaledVerticalRotation * Time.deltaTime;
            FPCam.transform.Rotate(new Vector3(turnSpeed * scaledVerticalRotation * Time.deltaTime, 0, 0));
        }
        else if (verticalRotationValue > (FPCam.scaledPixelHeight / 4) * 3 && camTurnTracker < maximumViewAngles)
        {
            float scaledVerticalRotation = (verticalRotationValue - ((FPCam.scaledPixelHeight/4) * 3)) / (FPCam.scaledPixelHeight / 4);
            camTurnTracker += turnSpeed * scaledVerticalRotation * Time.deltaTime;
            FPCam.transform.Rotate(new Vector3(-turnSpeed * scaledVerticalRotation * Time.deltaTime, 0, 0));
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray screenRay = FPCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenRay, out hit, maxInteractDistance))
            {
                Debug.Log(hit.transform.gameObject.name);
                hit.transform.GetComponent<IInteractable>()?.Interact();
            }
        }
      /*  if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if(Physics.Raycast(FPCam.transform.position, transform.forward, out hit, maxInteractDistance))
            {
                hit.transform.GetComponent<PuzzleButton>()?.OnButtonInteraction();
            }
        }
      */
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
