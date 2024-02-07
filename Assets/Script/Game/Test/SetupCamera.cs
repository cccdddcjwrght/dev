using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEngine;

public class SetupCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var camera = GameCamera.camera;
        var setup = GetComponent<Camera>();
        camera.orthographic = setup.orthographic;
        camera.orthographicSize = setup.orthographicSize;
        camera.transform.position = transform.position;
        camera.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
