using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using UnityEngine.Events;

public class StringController : MonoBehaviour
{
    [SerializeField]
    private String bowStringRenderer;

    [SerializeField]
    private Transform midPointGrabObject, midPointVisualObject, midPointParent;

    [SerializeField]
    private float bowStringStretchLimit = 1.0f;

    private Transform interactor;

    private float strength;

    public UnityEvent OnBowPulled;
    public UnityEvent<float> OnBowReleased;

    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject midPointGrabbable;

    public void ResetBowString()
    {
        OnBowReleased?.Invoke(strength);
        strength = 0.0f;

        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);

    }

    public void PrepareBowString(Hand interactorTransform)
    {
        interactor = interactorTransform.transform;
        OnBowPulled?.Invoke();
    }

    private void Update()
    {
        if (interactor != null)
        {
            //convert bow string mid point position to the local space of the MidPoint
            Vector3 midPointLocalSpace = midPointParent.InverseTransformPoint(midPointGrabObject.position); // localPosition

            //get the offset
            float midPointLocalZAbs = Mathf.Abs(midPointLocalSpace.z);

            HandleStringPushedBackToStart(midPointLocalSpace);

            HandleStringPulledBackTolimit(midPointLocalZAbs, midPointLocalSpace);

            HandlePullingString(midPointLocalZAbs, midPointLocalSpace);

            bowStringRenderer.CreateString(midPointVisualObject.position);
        }

        midPointGrabbable.transform.rotation = bow.transform.rotation;
    }


    private void HandlePullingString(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        //what happens when we are between point 0 and the string pull limit
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs < bowStringStretchLimit)
        {
            strength = Remap(midPointLocalZAbs, 0, bowStringStretchLimit, 0, 1);
            midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);
        }
    }

    private float Remap(float value, int fromMin, float fromMax, int toMin, int toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    private void HandleStringPulledBackTolimit(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        //We specify max pulling limit for the string. We don't allow the string to go any farther than "bowStringStretchLimit"
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs >= bowStringStretchLimit)
        {
            strength = 1;
            Vector3 direction = midPointParent.TransformDirection(new Vector3(0, 0, midPointLocalSpace.z));
            midPointVisualObject.localPosition = new Vector3(0, 0, -bowStringStretchLimit);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z >= 0)
        {
            strength = 0;
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }
    public void MakeKinematic(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>() != null)
        {
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void MakeNonKinematic(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>() != null)
        {
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}