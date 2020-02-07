using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttributes : MonoBehaviour
{
    [Header("Hit Scanning")]
    [SerializeField] Camera cam;
    [SerializeField] float scanRange;

    [Header("Fire Rate")]
    [SerializeField] float fireCooldown;
    [SerializeField] bool autoFire;
    float fireTimeStamp;

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("Sound")]
    public SoundRandomizer shotAudio;

    void Update()
    {
        if ((autoFire ? Input.GetButton("Shoot") : Input.GetButtonDown("Shoot")) && Time.time - fireTimeStamp >= fireCooldown)
        {
            fireTimeStamp = Time.time;
            Shoot();
        }
    }

    //Casts a ray through the reticle, returns the position of the first hit. If nothing is hit, it returns a position in front of the reticle determined by scanRange.
    Vector3 GetHitScanPosition()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, scanRange))
            return hit.point;
        else return cam.transform.position + (cam.transform.forward * scanRange);
    }

    void Shoot()
    {
        animator.SetTrigger("Shoot");
        shotAudio.PlayRandomSound();
    }
}