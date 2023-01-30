using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject wirePrefab;
    [SerializeField] private Transform wiresTransform;
    public Pin startingPin { get; set; }

    public void InstantiateWire(Pin endingPin)
    {
        GameObject wire = (GameObject)Instantiate(wirePrefab, Vector3.zero, Quaternion.identity, wiresTransform);
        // Set the wire's two transforms to follow
        wire.GetComponent<Wire>().pins = new Pin[] { startingPin, endingPin };
    }
}
