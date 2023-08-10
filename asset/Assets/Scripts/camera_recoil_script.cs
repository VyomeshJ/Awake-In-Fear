using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_recoil_script : MonoBehaviour
{
    public float rotationSpeed = 6;
    public float returnSpeed = 25;
    public Vector3 RecoilRotation;
    public Vector3 RecoilRotationAiming = new Vector3(0.5f, 0.5f, 1.5f);
    public bool aiming;
    private Vector3 currentRotation;
    private Vector3 Rot;
    bool shooting;

    void FixedUpdate()
    {
        if (shooting)
        {
            currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
            Rot = Vector3.Slerp(Rot, currentRotation, rotationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(Rot);
        }
    }
    public IEnumerator Shake()
    {
        shooting = true;
        currentRotation += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
        yield return new WaitForSeconds(1f);
        shooting = false;
    }
   
}

