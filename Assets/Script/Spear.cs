using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

    [SerializeField]
    private GameObject Spears;
    private bool Starts=false;
    [SerializeField]
    private bool StopTime = false;
    private float StopTempTime = 0f;
    private float totTime = 5f;
    private bool once = false;
    private Vector3 StartPos;
    private bool onces = false;

    private void Start()
    {
    }

    private void Update()
    {
        Debug.Log(StartPos);
        if (Spears.activeSelf && !onces)
        {
            onces = true;
            StartPos = Spears.transform.position;
        }
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
        if (Starts)
        {
            Spears.transform.position = Vector3.Lerp(Spears.transform.position, StartPos - 7 * Spears.transform.up, 0.05f);
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

    public void SetStart()
    {
        Spears.transform.position = StartPos;
        Starts = false;
        Spears.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Starts = true;
            Spears.SetActive(true);
        }
    }
}
