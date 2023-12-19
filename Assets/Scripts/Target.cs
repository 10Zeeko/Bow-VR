using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;
using Random = System.Random;

public enum TargetType
{
    Select, Normal, MoveToSides, MoveUpAndDown, Circles
}

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private StringController bow;
    [SerializeField] AudioSource audioSource;
    [SerializeField] public TargetType targetType = TargetType.Select;
    [SerializeField] public Vector3 targetPosition;

    [SerializeField] int totalTime = 4;
    [SerializeField] private int speed = 10;
    float radius = 0.5f;
    private float _currentTime = 0;
    private Transform _startTransform;
    private GameObject _hitArrow;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startTransform = GetComponent<Transform>();
        if (targetType == TargetType.Select)
        {
            targetType = (TargetType)UnityEngine.Random.Range(0, Convert.ToInt16(Enum.GetValues(typeof(TargetType)).Cast<TargetType>().Max() + 1));
        }
    }

    private void FixedUpdate()
    {
        Vector3 newTargetPos = transform.position;
        var transformRotation = transform.rotation;
        transformRotation.x = -90;
        switch (targetType)
        {
            case TargetType.Circles:
                // Move in circles using cos and sin
                float newX = Mathf.Cos(Time.time * speed) * radius;
                float newY = Mathf.Sin(Time.time * speed) * radius + 0.5f;
                transform.position = new Vector3(newX, newY, newTargetPos.z);
                break;
            case TargetType.MoveToSides:
                // Move horizontally using cos
                newX = Mathf.Cos(Time.time * speed) * radius;
                transform.position = new Vector3(newX, newTargetPos.y,newTargetPos.z);
                break;
            case TargetType.MoveUpAndDown:
                // Move vertically using sin
                newY = Mathf.Sin(Time.time * speed) * radius + 0.5f;
                transform.position = new Vector3(newTargetPos.x, newY, newTargetPos.z);
                break;
            default:
                transform.position = Vector3.MoveTowards(newTargetPos, targetPosition, speed);
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            int totalPoints = 0;
            //audioSource.Play();
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= 0.13f)
            {
                totalPoints = 7;
                print("Diana");
            }
            else if (distance <= 0.23f)
            {
                totalPoints = 5;
                print("Rojo");
            }
            else if (distance <= 0.31f)
            {
                totalPoints = 3;
                print("Azul");
            }
            else
            {
                totalPoints = 1;
                print("Blanco");
            }
            _hitArrow = other?.gameObject;
            other?.gameObject.GetComponent<StickingArrowToSurface>().bowParent.UpdatePoints(totalPoints);
            
            Invoke(nameof(destroyTarget), 1);
        }
    }

    private void destroyTarget()
    {
        print("SELF");
        Destroy(_hitArrow);
        Destroy(gameObject);
    }
}
