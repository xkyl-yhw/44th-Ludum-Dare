using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEmenyAl : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject TargetGameObject;
    [SerializeField]
    private GameObject StartGameObject;
    private float TimeInterval = 1f;
    private int RotateDir = 1;
    private bool StartMove = false;
    [SerializeField]
    private float RotateAngle = 5f;
    private float tempTime;
    private Rigidbody EmenyRig;
    private Vector3 StartPos;
    private Vector3 LastDir;
    [SerializeField]
    private int Dir = 1;
    [SerializeField]
    private bool StopTime = false;
    private float StopTempTime = 0f;
    private float totTime=3f;
    private bool once = false;

    private void Start()
    {
        StartPos = transform.position;
        EmenyRig = GetComponent<Rigidbody>();
        transform.right = transform.right * Dir;
    }

    private void Update()
    {
        Count();
        bool temp = RayShot();
        if (Input.GetKeyDown(KeyCode.K))
        {
            once = !once;
            StopTempTime = Time.time;
            StopTime = true;
        }
        bool temp1 = StopTimeCount();
        if (StopTime&&once&&!temp1)
        {
            return;
        }
        if (temp)
        {
            if (StartMove)
            {
                ChangeDir();
                EmenyRig.velocity = Vector3.zero;
                transform.position = Vector3.Lerp(transform.position, transform.position - 20 * transform.right, 0.01f);
            }

        }
        else
        {
            StartMove = false;
            RotateDir = -RotateDir;
        }
        if (!StartMove)
        {
            transform.position = StartPos - transform.right*0.5f;
            transform.Rotate(transform.forward, RotateDir*Time.deltaTime*RotateAngle, Space.Self);
            LastDir = transform.forward;
        }

    }

    private bool StopTimeCount()
    {
        if (Time.time - StopTempTime > totTime)
        {
            return true;
        }
        return false;
        
    }

    private void Count()
    {
        if (Time.time - tempTime > TimeInterval)
        {
            StartMove = true;
        }
    }

    private bool RayShot()
    {
        Debug.DrawRay(transform.position, -transform.right * 20, Color.red);
        RaycastHit[] hit=new RaycastHit[10];
        hit = Physics.RaycastAll(transform.position, -transform.right * 20);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject == TargetGameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void ChangeDir()
    {
        RaycastHit[] hit= new RaycastHit[10];
        hit = Physics.RaycastAll(transform.position, -transform.right, 1f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject == TargetGameObject)
            {
                StartPos = transform.position;
                GameObject Temp = TargetGameObject;
                TargetGameObject = StartGameObject;
                StartGameObject = Temp;
                transform.right = -transform.right;
                EmenyRig.velocity = Vector3.zero;
                StartMove = false;
                tempTime = Time.time;
                break;
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
