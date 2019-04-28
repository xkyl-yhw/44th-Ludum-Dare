using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    public static float totTime=180;
    [SerializeField]
    private float CheckPointTime;
    private Vector3 CheckPointVector=Vector3.zero;
    private float tempTime;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float interval=1f;
    [SerializeField]
    private GameObject TimeCount;
    [SerializeField]
    private bool stop = false;
    [SerializeField]
    private GameObject EndUI;

    private void Start()
    {
        tempTime = Time.time;
        CheckPointVector = Player.transform.position;
        CheckPointTime = totTime;
    }

    private void Update()
    {
        if (Time.time - tempTime >= interval&&!stop)
        {
            tempTime = Time.time;
            totTime--;
            TimeCount.GetComponent<Text>().text = totTime.ToString();
        }
        if (totTime==0f)
        {
            EndUI.SetActive(true);
            stop = true;
            StartCoroutine(BackToMenu());
        }
    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Start");
    }

    public void RecordTime()
    {
        CheckPointTime = totTime;
    }

    public void RecordVector(GameObject CheckPoint)
    {
        CheckPointVector = CheckPoint.transform.position;
    }

    public void ReturnToLastPoint()
    {
        Player.transform.position = CheckPointVector;
        totTime = CheckPointTime;
    }

    public void SetIntervalTime()
    {
        if (interval == 1f)
        {
            interval = 0.5f;
        }
        else interval = 1f;
    }

    public Vector3 ReturnCurrrent()
    {
        return CheckPointVector;
    }

    public void SetIntervalTimeEnd()
    {
        stop = true;
    }
}
