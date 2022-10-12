using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.position = parent.position + offset;
    }
}
