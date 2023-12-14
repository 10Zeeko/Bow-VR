using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class active_collider : MonoBehaviour
{
    [SerializeField]
    BoxCollider arrowCollider;

    public void ActiveCollider()
    {
        arrowCollider.enabled = true;
    }
    public void DisableCollider()
    {
        arrowCollider.enabled = false;
    }
}
