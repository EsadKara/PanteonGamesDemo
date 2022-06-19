using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerControl : MonoBehaviour
{
    [SerializeField] float runSpeed, slideSpeed;
    [SerializeField] Transform startPos, paintPos;

    float inputHorizontal;

    Animator playerAnim;

    Vector2 movePos;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        if (gameManager.instance.isStart)
        {
            playerAnim.SetBool("isStart", true);
            transform.Translate(inputHorizontal * slideSpeed * Time.deltaTime, 0, runSpeed * Time.deltaTime);

            // Mobil Kontroller
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    movePos = touch.deltaPosition;
                }
                if (movePos.x > 5)
                {
                    transform.Translate(slideSpeed * Time.deltaTime, 0, 0);
                }
                else if (movePos.x < -5)
                {
                    transform.Translate(-slideSpeed * Time.deltaTime, 0, 0);
                }
            }
        }

        // FPS Kamerasýna Geçiþ
        if (gameManager.instance.isFinish)
        {
            transform.DOMove(paintPos.position, 1.5f).OnComplete(() => 
            transform.position=paintPos.position).OnComplete(() => playerAnim.SetBool("isStart", false));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            transform.position = startPos.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "FinishLine")
        {
            gameManager.instance.isFinish = true;
            gameManager.instance.isStart = false;
        }
    }
}
