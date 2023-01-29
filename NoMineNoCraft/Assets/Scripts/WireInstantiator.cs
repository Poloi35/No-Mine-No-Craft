using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private Transform wiresTransform;
    public Transform _startingPin;
    public Transform startingPin { get { return _startingPin; } set { _startingPin = value; } }

    public void InstantiateWire(Transform endingPin)
    {
        GameObject wire = (GameObject)Instantiate(wirePrefab, Vector3.zero, Quaternion.identity, wiresTransform);
        // Set the wire's two transforms to follow
        wire.GetComponent<Wire>().pinTransforms = new Transform[] { _startingPin, endingPin };
    }
}
