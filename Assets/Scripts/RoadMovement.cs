using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    GameManager GM;
    float moveSpeed = 0.75f;
    Vector3 moveVec;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        moveVec = Vector3.left;
    }

    void Update()
    {
        if (GM.canPlay && !GM.damage)
        {
            transform.Translate(moveVec * Time.deltaTime * moveSpeed);
        }
    }
}
