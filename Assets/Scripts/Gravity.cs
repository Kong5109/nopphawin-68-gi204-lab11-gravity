using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    Rigidbody rb;
    const float G = 0.006674f;

    public static List<Gravity> otherObjectlist;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectlist == null)
        {
            otherObjectlist = new List<Gravity>();
        }

        otherObjectlist.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (var obj in otherObjectlist)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }   
    }

    private void Attract(Gravity other)
    {
        Rigidbody otherRB = other.rb;
        Vector3 direction = rb.position - otherRB.position;
        float distance = direction.magnitude;

        if (distance == 0f) { return; }

        float forceMagnitude = G * (rb.mass * otherRB.mass) / Mathf.Pow(distance, 2);

        Vector3 gravityForce = forceMagnitude * direction;

        otherRB.AddForce(gravityForce);
    }
}
