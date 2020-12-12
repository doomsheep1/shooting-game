using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class ShootRayCast : MonoBehaviour
{
    //private int pistolDmg = 1;
    private float pistolRange = 100f;
    private float fireRate = .25f;
    private float nextFire;
    //private float hitForce = 1000f;
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource pistolAudio;
    private LineRenderer laserLine;

    public Transform pistolEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = 0.05f;
        laserLine.endWidth = 0.05f;
        pistolAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate; //need to track real time passed to increment time to fire for fireRate
            Shoot();
        }
    }

    private void Shoot()
    {
        StartCoroutine(ShotEffect());
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, fpsCam.nearClipPlane));
        RaycastHit hit;
        laserLine.SetPosition(0, pistolEnd.position);
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, pistolRange))
        {
            laserLine.SetPosition(1, hit.point);
            UnityEngine.Debug.Log(hit.transform.name);
            Cube enemy = hit.collider.GetComponent<Cube>();
            if (enemy != null)
                enemy.Destroy(enemy.ToString());
            /*if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * hitForce);*/
        }
        else
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * pistolRange));
    }

    private IEnumerator ShotEffect()
    {
        pistolAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
