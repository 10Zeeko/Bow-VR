using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickingArrowToSurface : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField] private SphereCollider coll;
    [SerializeField] private GameObject stickyArrow;

    private void OnCollisionEnter(Collision other)
    {
        rb.isKinematic = true;
        coll.isTrigger = true;
        GameObject arrow = Instantiate(stickyArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        if (coll.GetComponent<Collider>().attachedRigidbody != null)
        {
            arrow.transform.parent = other.collider.attachedRigidbody.transform;
        }
        other.collider.GetComponent<Target.IHittable>()?.GetHit();
        
        Destroy(gameObject);
    }
}
