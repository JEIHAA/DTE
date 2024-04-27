using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float scrollAmount;
    [SerializeField] public float moveSpeed;
    [SerializeField] public Vector3 moveDirection;
}
