using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] Transform playerPosition;

    private float xMin, xMax;
    private float camY,camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;
    private Vector3 smoothPos;
    [SerializeField] private float smoothSpeed;
    private GameObject player;
    //private Transform playerPos;

    private void Start()
    {

        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = (xMax + camOrthsize) / 2.0f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log("Player position is now in scene, assigning camera position");
        camY = playerPosition.position.y;
        camX = playerPosition.position.x;
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);
        this.transform.position = smoothPos;

        
    }

}

