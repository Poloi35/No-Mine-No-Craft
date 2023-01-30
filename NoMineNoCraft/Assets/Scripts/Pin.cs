using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public Chip parentChip { get; set; }
    public CircleCollider2D pinCollider { get; set; }
    public int index { get; set; }

    private void Awake()
    {
        pinCollider = GetComponent<CircleCollider2D>();
    }
}
