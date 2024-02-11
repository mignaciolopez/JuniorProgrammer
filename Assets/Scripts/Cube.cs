using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Object
{
    [SerializeField] private float m_speedMultiplier = 5.0f;

    //Inheritance
    protected override void Move()
    {
        //Polymorphism
        transform.Rotate(new Vector3(1, 1, 1) * speed * m_speedMultiplier * Time.deltaTime);
    }
}
