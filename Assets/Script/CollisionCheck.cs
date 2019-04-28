using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionCheck : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;
    private string TagName1 = "Trap";

    private string TagName3 = "Boat";
    private string TagName4 = "ChangeScenes";
    private string TagName5 = "Spear";

    [SerializeField]
    private GameObject boat;
    private bool StartMove = false;
    private Vector3 StartPos;
    [SerializeField]
    private Spear spear;

    private void Start()
    {
        if(boat!=null)
        StartPos = boat.transform.position;
    }

    private void Update()
    {
        if (StartMove)
        {
            boat.transform.position = Vector3.Slerp(boat.transform.position, StartPos + Vector3.right * 20, 0.01f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == TagName1)
        {
            gameManager.ReturnToLastPoint();
        }else if (collision.collider.tag == TagName3)
        {
            StartMove = true;
        }else if (collision.collider.tag == TagName4)
        {
            SceneManager.LoadScene("Final");
        }else if (collision.collider.tag == TagName5&&spear!=null)
        {
            spear.SetStart();
            gameManager.ReturnToLastPoint();
        }
    }

}
