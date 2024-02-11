using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float waitOnPickup = 0.2f;
    public float breakForce = 35f;
    [HideInInspector] public bool pickedUp = false;
    [HideInInspector] public PlayerInteractions playerInteractions;

    Renderer sphereRenderer;
    [SerializeField] Color originalColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] Color transparentColor = new Color(1f, 1f, 1f, 0.3f);

    private void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
        originalColor = sphereRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                playerInteractions.BreakConnection();
            }

        }
    }

    //this is used to prevent the connection from breaking when you just picked up the object as it sometimes fires a collision with the ground or whatever it is touching
    public IEnumerator PickUp()
    {
        yield return new WaitForSecondsRealtime(waitOnPickup);
        pickedUp = true;
        sphereRenderer.material.color = transparentColor;
    }

    public void Release()
    {
        pickedUp = false;
        sphereRenderer.material.color = originalColor;
    }
}