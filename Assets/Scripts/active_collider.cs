using System.Collections;
using System.Collections.Generic;
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
