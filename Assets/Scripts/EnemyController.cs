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
    public int maxHitPoints = 100;
    public float speed = 0.02f;
    public GameObject startPoint;
    public GameObject nextTarget;
    // Start is called before the first frame update

    private Direction direction;
    private Vector3 currentPosition;
    void Start()
    {
        this.transform.position = startPoint.transform.position;
        currentPosition = startPoint.transform.position;
        //ustawienie kierunku poruszania sie
        SetDirection();
        //Debug.Log("Enemy created");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //przemieszczenie sie przeciwnika
        MoveOnPath();
    }

    void SetDirection()
    {
        Debug.Log("Current position" + (currentPosition).ToString());
        Debug.Log("Next position" + (nextTarget.transform.position).ToString());
        if (nextTarget.transform.position.x > currentPosition.x && (int)(Math.Abs(nextTarget.transform.position.y) - Math.Abs(currentPosition.y)) == 0)
        {
            direction = Direction.right;
        }
        else if(nextTarget.transform.position.x < currentPosition.x && (int)(Math.Abs(nextTarget.transform.position.y) - Math.Abs(currentPosition.y)) == 0)
        {
            direction = Direction.left;
        }
        else if (nextTarget.transform.position.y < currentPosition.y && (int)(Math.Abs(nextTarget.transform.position.x) - Math.Abs(currentPosition.x)) == 0)
        {
            direction = Direction.bottom;
        }
        else if (nextTarget.transform.position.y > currentPosition.y && (int)(Math.Abs(nextTarget.transform.position.x) - Math.Abs(currentPosition.x)) == 0)
        {
            direction = Direction.top;
        } else
        {
            Debug.Log("Else");
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
            currentPosition = nextPathTarget.transform.position == nextTarget.transform.position ? currentPosition : nextTarget.transform.position;
            nextTarget = nextPathTarget;
            SetDirection();
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
