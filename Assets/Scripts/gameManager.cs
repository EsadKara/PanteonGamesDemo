using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public bool isStart, isFinish;

    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] TextMeshProUGUI brushTxt;
    [SerializeField] TextMeshProUGUI playTxt;
    [SerializeField] TextMeshProUGUI completeTxt;
    [SerializeField] GameObject brushPref, brushParent, clickObj;

    float second, minute, brushCount;

    RaycastHit hit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        isStart = false;
        isFinish = false;
        second = 0;
        minute = 0;
        brushCount = 0;
        brushTxt.text = "% " + brushCount;
        completeTxt.enabled = false;
    }

    
    void Update()
    {
        PaintTheWall();
        if (Input.GetMouseButtonDown(0) && !isFinish)
        {
            isStart = true;
        }
        if (isStart)
        {
            StartGame();
        }
       
    }

    void WriteTheTime()
    {
        if (minute < 10)
        {
            if (second < 10)
            {
                timeTxt.text = "0" + (int)minute + " : " + "0" + (int)second;
            }
            else
            {
                timeTxt.text = "0" + (int)minute + " : " + (int)second;
            }
        }
        else
        {
            if (second < 10)
            {
                timeTxt.text = (int)minute + " : " + "0" + (int)second;
            }
            else
            {
                timeTxt.text = (int)minute + " : " + (int)second;
            }
        }
    }

    void PaintTheWall()
    {
        Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, 5f))
            {
                Vector3 newPos = hit.point;
                newPos.z -= 0.01f;
                if (hit.collider.gameObject.tag == "Wall")
                {
                    GameObject brush = Instantiate(brushPref, newPos, Quaternion.identity);
                    brush.transform.parent = brushParent.transform;
                    brushCount += 1.35f;
                    if (brushCount >= 100)
                    {
                        brushCount = 100;
                        completeTxt.enabled = true;
                    }
                    brushTxt.text = "% " + (int)brushCount;
                }
            }
        }
    }

    void StartGame()
    {
        clickObj.SetActive(false);
        playTxt.enabled = false;
        second += Time.deltaTime;
        if (second >= 60)
        {
            second = 0;
            minute++;
        }
        WriteTheTime();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
