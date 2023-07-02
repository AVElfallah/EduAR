using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    private Vector3 cameraLocation;
    // Start is called before the first frame update
    void Start()
    {
        cameraLocation = this.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        this.transform.position = player.transform.position+cameraLocation;
    }
}
