using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    float speed;
    float rpm;
    [SerializeField] float horsePower = 20.0f;
    [SerializeField] float turningSpeed = 20.0f;

    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] List<WheelCollider> wheels;

    int wheelsOnGround;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountWheels();

        //Move the car forward based on vertical input
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(transform.forward * verticalInput * speed * Time.deltaTime);
        playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput * wheelsOnGround);

        //Rotates the car based on horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontalInput * turningSpeed * wheelsOnGround * Time.deltaTime);
        

        speed = (playerRb.velocity.magnitude * 3.6f);
        speedometerText.text = "Speed: " + speed.ToString("F2") + " km/h";

        rpm = (speed % 30)*40;
        rpmText.text = "RPM: " + rpm.ToString("F2");
    }

    void CountWheels()
    {
        wheelsOnGround = 0;
        foreach(WheelCollider wheel in wheels)
        {
            if (wheel.isGrounded)
                wheelsOnGround++;
        }
    }
}
