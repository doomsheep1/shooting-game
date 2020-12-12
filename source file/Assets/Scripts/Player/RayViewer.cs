using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RayViewer: MonoBehaviour
{
    private Camera fpsCam;
    private float pistolRange = 100f;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, fpsCam.nearClipPlane));
        UnityEngine.Debug.DrawRay(rayOrigin, fpsCam.transform.forward * pistolRange, Color.green);
    }
}
