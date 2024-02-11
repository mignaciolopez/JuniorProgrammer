using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    GameManager gameManager;
    [Header("InteractableInfo")]
    public int interactableLayerIndex;
    private Vector3 raycastPos;
    public GameObject lookObject;
    private PhysicsObject physicsObject;
    private Camera mainCamera;

    [Header("Pickup")]
    [SerializeField] private Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;

    [Header("ObjectFollow")]
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float pickupDistance = 4f;
    [SerializeField] private float dropDistance = 2f;
    private float currentSpeed = 0f;
    private float currentDist = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;
    Quaternion lookRot;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCamera = Camera.main;
    }

    //A simple visualization of the point we're following in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pickupParent.position, 0.1f);
    }

    //Velocity movement toward pickup parent and rotation
    private void FixedUpdate()
    {
        if (currentlyPickedUpObject)
        {
            currentDist = Vector3.Distance(pickupParent.position, pickupRB.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDist / pickupDistance);
            currentSpeed *= Time.fixedDeltaTime;
            Vector3 direction = pickupParent.position - pickupRB.position;
            //direction.y /= pickupRB.mass;
            //direction /= (pickupRB.mass / 2f);
            pickupRB.velocity = direction.normalized * currentSpeed;
            //Rotation
            //lookRot = Quaternion.LookRotation(mainCamera.transform.position - pickupRB.position);
            //lookRot = Quaternion.Slerp(mainCamera.transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            //pickupRB.MoveRotation(lookRot);
        }
    }

    //Interactable Object detections and distance check
    void Update()
    {
        if (gameManager.isGameActive)
        {
            //Here we check if we're currently looking at an interactable object
            raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(raycastPos, mainCamera.transform.forward, out hit, pickupDistance, 1 << interactableLayerIndex))
            {
                lookObject = hit.collider.transform.root.gameObject;
            }
            else
            {
                lookObject = null;
            }

            //if we press the button of choice
            if (Input.GetButton("Fire2"))
            {
                //and we're not holding anything
                if (!currentlyPickedUpObject)
                {
                    //and we are looking an interactable object
                    if (lookObject)
                    {
                        PickUpObject();
                    }
                }
            }
            //if we release the pickup button and have something, we drop it
            else
            {
                if (currentlyPickedUpObject)
                    BreakConnection();
            }

            if (currentlyPickedUpObject && currentDist > dropDistance)
                BreakConnection();
        }
    }

    //Release the object
    public void BreakConnection()
    {
        pickupRB.constraints = RigidbodyConstraints.None;
        currentlyPickedUpObject = null;
        physicsObject.Release();
        currentDist = 0;
    }

    public void PickUpObject()
    {
        if (lookObject.CompareTag("PhysicsObject"))
        {
            physicsObject = lookObject.GetComponentInChildren<PhysicsObject>();
            currentlyPickedUpObject = lookObject;
            pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
            pickupRB.constraints = RigidbodyConstraints.FreezeRotation;
            physicsObject.playerInteractions = this;
            StartCoroutine(physicsObject.PickUp());
        }
    }
}