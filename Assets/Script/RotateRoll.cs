using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoll : MonoBehaviour {

    private Rigidbody RollRig;
    [SerializeField]
    private float forceAmount = 5f;
    
    private void Start()
    {
        RollRig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Quaternion temp = transform.rotation;
        Quaternion target = temp * Quaternion.AngleAxis(90, Vector3.forward);
        Debug.Log(target);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, 0.02f);
        transform.Rotate(-Vector3.forward*forceAmount*Time.deltaTime, Space.Self);
    }
}
