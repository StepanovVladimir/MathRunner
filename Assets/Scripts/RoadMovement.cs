using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public GameManager GM;

    Vector3 moveVec;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        moveVec = Vector3.left;
    }

    void Update()
    {
        if (GM.canPlay)
        {
            transform.Translate(moveVec * Time.deltaTime * GM.moveSpeed);
        }
    }
}
