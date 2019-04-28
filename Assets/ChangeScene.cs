using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    private void Start()
    {
        GameManager.totTime = 180f;
    }

    public void Onclick()
    {
        SceneManager.LoadScene("Main");
    }
}
