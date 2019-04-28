using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSet : MonoBehaviour {

    [SerializeField]
    private Spear spear;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Spear")
        {
            spear.SetStart();
        }
    }
}
