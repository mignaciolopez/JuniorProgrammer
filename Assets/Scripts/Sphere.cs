using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : Object
{
    [SerializeField] private float m_speedMultiplier = 5.0f;

    //Inheritance
    protected override void Move()
    {
        //Polymorphism
        transform.Translate(Vector3.forward * speed * m_speedMultiplier * Time.deltaTime);
    }
}
