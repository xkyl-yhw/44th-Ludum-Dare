using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour {

    [SerializeField]
    private GameObject winUI;
    [SerializeField]
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            gameManager.SetIntervalTimeEnd();
            winUI.SetActive(true);
            StartCoroutine(BackToMenu());
        }
    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Start");
    }
}
