using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 90;
    // Update is called once per frame
    void Update () {
        Vector3 differance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        differance.Normalize();
        float rotZ = Mathf.Atan2(differance.y, differance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
}
