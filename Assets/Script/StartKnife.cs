using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartKnife : MonoBehaviour {

    private bool Start = false;
    [SerializeField]
    private GameObject Knefe;
    [SerializeField]
    private GameObject Door1;
    [SerializeField]
    private GameObject Door2;
    [SerializeField]
    private GameObject Center;
    [SerializeField]

    private void Update()
    {
        if (Start)
        {
            Door1.transform.RotateAround(Center.transform.position, Center.transform.forward, 20f * Time.deltaTime);
            Door2.transform.RotateAround(Center.transform.position, Center.transform.forward, 20f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && !Start)
        {
            Start = true;
            Knefe.SetActive(true);
        }
    }
}
