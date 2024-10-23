using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;
using System;

enum Direction
{
    top,
    right,
    bottom,
    left
}

public class EnemyController : MonoBehaviour
{
    public int hitPoints = 100;
    public float speed = 0.02f;
    public GameObject startPoint;
    public GameObject nextTarget;
    // Start is called before the first frame update

    private Direction direction;
    void Start()
    {
        this.transform.position = startPoint.transform.position;
        //ustawienie kierunku poruszania sie
        SetDirection();
        Debug.Log("Enemy created");
    }

    // Update is called once per frame
    void Update()
    {
        //przemieszczenie sie przeciwnika
        MoveOnPath();
    }

    void SetDirection()
    {
        if (nextTarget.transform.position.x > this.transform.position.x && (int)(Math.Abs(nextTarget.transform.position.y) - Math.Abs(this.transform.position.y)) == 0)
        {
            direction = Direction.right;
        }
        else if(nextTarget.transform.position.x < this.transform.position.x && (int)(Math.Abs(nextTarget.transform.position.y) - Math.Abs(this.transform.position.y)) == 0)
        {
            direction = Direction.left;
        }
        else if (nextTarget.transform.position.y < this.transform.position.y && (int)(Math.Abs(nextTarget.transform.position.x) - Math.Abs(this.transform.position.x)) == 0)
        {
            direction = Direction.bottom;
        }
        else if (nextTarget.transform.position.y > this.transform.position.y && (int)(Math.Abs(nextTarget.transform.position.x) - Math.Abs(this.transform.position.x)) == 0)
        {
            direction = Direction.bottom;
        }
    }

    private void MoveOnPath()
    {
        Vector3 nextPosition = this.transform.position;
        switch (direction)
        {
            case Direction.top:
                nextPosition = new Vector3(this.transform.position.x, this.transform.position.y + speed, this.transform.position.z);
                break;
            case Direction.right:
                nextPosition = new Vector3(this.transform.position.x + speed, this.transform.position.y, this.transform.position.z);
                break;
            case Direction.bottom:
                nextPosition = new Vector3(this.transform.position.x, this.transform.position.y - speed, this.transform.position.z);
                break;
            case Direction.left:
                nextPosition = new Vector3(this.transform.position.x - speed, this.transform.position.y, this.transform.position.z);
                break;
        }
        this.transform.position = nextPosition;
    }

    public void OnTargetReached(GameObject nextPathTarget)
    {
        if(nextPathTarget != null)
        {
            Debug.Log("TargetReached!");
            nextTarget = nextPathTarget;
            SetDirection();
        }
    }
}
