using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour {

    private string TagName6 = "tip";
    private string TagName2 = "checkPoint";
    [SerializeField]
    private Text DisPlayTip;
    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (this.tag == TagName2 && other.tag == "Player"&&gameManager.ReturnCurrrent()!=this.transform.position)
        {
            gameManager.RecordTime();
            gameManager.RecordVector(this.gameObject);
        }
        else if (this.tag == TagName6 && other.tag == "Player")
        {
            DisPlayTip.gameObject.SetActive(true);
            DisPlayTip.text = this.GetComponent<Text>().text;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.tag == TagName6&&other.tag=="Player")
        {
            DisPlayTip.gameObject.SetActive(false);
        }
    }
}
