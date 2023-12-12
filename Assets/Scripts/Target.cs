using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;
using Random = System.Random;

public enum TargetType
{
    Select, Normal, MoveToSides, MoveUpAndDown
}

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] AudioSource audioSource;
    [SerializeField] public TargetType targetType = TargetType.Select;
    [SerializeField] public Vector3 targetPosition;

    [SerializeField] int totalTime = 10;
    [SerializeField] private int speed = 10;
    private float _currentTime = 0;
    private Transform _startTransform;
    private GameObject _hitArrow;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startTransform = GetComponent<Transform>();
        if (targetType == TargetType.Select)
        {
            targetType = (TargetType)UnityEngine.Random.Range(1, Convert.ToInt16(Enum.GetValues(typeof(TargetType)).Cast<TargetType>().Max()));
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            //audioSource.Play();
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= 0.13f)
            {
                print("Diana");
            }
            else if (distance <= 0.23f)
            {
                print("Rojo");
            }
            else if (distance <= 0.31f)
            {
                print("Azul");
            }
            else
            {
                print("Blanco");
            }

            _hitArrow = other.gameObject;
            Invoke(nameof(destroyTarget), 1);
        }
    }

    private void destroyTarget()
    {
        print("SELF");
        Destroy(_hitArrow);
        Destroy(gameObject);
    }

    public void GetHit()
    {
        
    }
    
    public interface IHittable
    {
        void GetHit();
    }
}
