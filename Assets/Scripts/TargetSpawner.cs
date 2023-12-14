using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] posiblePositions;
    [SerializeField] private GameObject target;
    [SerializeField] private int timer = 3;
    // Update is called once per frame
    void Start()
    {
        Invoke(nameof(SpawnTarget), timer);
    }

    private void SpawnTarget()
    {
        GameObject newTarget =Instantiate(target);
        int selectedPos = UnityEngine.Random.Range(0, posiblePositions.Length);
        newTarget.transform.position = posiblePositions[selectedPos].position;
        newTarget.transform.rotation = posiblePositions[selectedPos].rotation;
        newTarget.GetComponent<Target>().targetPosition = posiblePositions[selectedPos].position;
        Invoke(nameof(SpawnTarget), timer);
    }
}
