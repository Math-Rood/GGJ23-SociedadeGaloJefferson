using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 3f;
    [SerializeField] private Transform cam;


    private void Awake()
    {
        cam = Camera.main.transform;
    }
        

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = cam.position;
        newPosition.x += cameraSpeed * Time.deltaTime;
        cam.position = newPosition;
    }
}
