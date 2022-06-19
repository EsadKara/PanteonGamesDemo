using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset, offset2, newPos;
    [SerializeField] Transform player;
    bool moveAnim, follow;

    private void Start()
    {
        moveAnim = false;
        follow = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameManager.instance.isFinish && !gameManager.instance.isStart)
        {
            moveAnim = true;
        }
        if (moveAnim)
        {
            transform.DORotate(new Vector3(27, 0, 0), 2f);
            transform.DOMove(player.position + offset2, 1f).OnComplete(() =>
             transform.DOMove(player.position + offset, 0.1f)).OnComplete(() => follow = true);
            moveAnim = false;
        }
        if (follow)
        {
            transform.DOMove(player.position + offset, 1f);
        }
        if (gameManager.instance.isFinish)
        {
            follow = false;
            newPos = player.position;
            newPos.y += 0.3f;
            newPos.z -= 0.2f;
            transform.DORotate(new Vector3(0, 0, 0), 1.5f);
            transform.DOMove(newPos, 1.5f);
        }
    }
   
}
