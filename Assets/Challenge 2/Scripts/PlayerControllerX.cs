using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float timeInterval = 0.5f;
    private float elapsedTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (elapsedTime >= timeInterval)
            {
                elapsedTime = 0.0f;
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            }
        }
    }
}
