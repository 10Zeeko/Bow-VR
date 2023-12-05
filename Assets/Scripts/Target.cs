using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_rigidbody.isKinematic && other.gameObject.CompareTag("Arrow"))
        {
            audioSource.Play();
        }
    }

    public void GetHit()
    {
        
    }
    
    public interface IHittable
    {
        void GetHit();
    }
}
