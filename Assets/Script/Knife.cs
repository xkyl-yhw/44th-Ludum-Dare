using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {
    [SerializeField]
    private GameObject StartGameObject;
    [SerializeField]
    private GameObject TargetGameObject;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private bool StopTime = false;
    private float StopTempTime = 0f;
    private float totTime = 3f;
    private bool once = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            once = !once;
            StopTempTime = Time.time;
            StopTime = true;
        }
        bool temp1 = StopTimeCount();
        if (StopTime && once && !temp1)
        {
            return;
        }
        rayCheck();
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, 0.2f);
    }

    private bool StopTimeCount()
    {
        if (Time.time - StopTempTime > totTime)
        {
            return true;
        }
        return false;

    }


    private void rayCheck()
    {
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, -transform.forward*3,3f);
        Debug.DrawRay(transform.position, -transform.forward*2, Color.red,0.1f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject == TargetGameObject)
            {
                Debug.Log(1); 
                GameObject temp = TargetGameObject;
                TargetGameObject = StartGameObject;
                StartGameObject = temp;
                transform.forward = -transform.forward;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.ReturnToLastPoint();
        }
    }
}
