using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicPick : MonoBehaviour
{
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MakeKinematic()
    {
        _rigidbody.isKinematic = true;
    }

    public void RemoveKinematic()
    {
        _rigidbody.isKinematic = false;
    }
}
