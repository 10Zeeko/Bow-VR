using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private float _targetSpeed;
    private Transform _windmill;
    [SerializeField] private int maxSpeed = 20;
    [SerializeField] private int minSpeed = 4;

    void Start()
    {
        _windmill = GetComponent<Transform>();
        rotationSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        StartCoroutine(ChangeSpeed());
    }
    void Update()
    {
        if (Mathf.Abs(rotationSpeed - _targetSpeed) > 0.01f)
        {
            rotationSpeed = Mathf.Lerp(rotationSpeed, _targetSpeed, Time.deltaTime * 0.1f);
        }

        _windmill.Rotate(rotationSpeed * Time.deltaTime, 0,0 );
    }
    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            _targetSpeed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        }
    }
}
