﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHuman : MonoBehaviour
{
    protected bool isMoving = false;
    //移动目标点
    private Vector3 targetPosition;

    public float speed = 1.2f;

    private Animator animator;
    //描述
    public string desc = "";
    // Start is called before the first frame update
    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        MoveUpdate();
    }

    public void MoveTo(Vector3 pos)
    {
        targetPosition = pos;
        isMoving = true;
        animator.SetBool("isMoving", true);
    }

    public void MoveUpdate()
    {
        if (!isMoving)
            return;
        Vector3 pos = transform.position;
        transform.localPosition = Vector3.MoveTowards(pos, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
        if (Vector3.Distance(pos, targetPosition) < 0.05f)
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
        }
    }

}
