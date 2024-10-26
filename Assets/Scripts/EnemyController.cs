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
    public int level = 1;
    public float hitPoints = 100f;
    public float maxHitPoints = 100f;
    public float speed = 0.02f;
    public GameObject startPoint;
    public GameObject nextTarget;
    public HitPointsBarController hitPointsBarController;
    public Animator animator;

    private Direction direction;
    private Vector3 currentPosition;
    private bool isDead = false;
    void Start()
    {
        hitPointsBarController = GetComponentInChildren<HitPointsBarController>();
        animator = GetComponent<Animator>();

        hitPointsBarController.UpdateHitPointsBar(hitPoints, maxHitPoints);
        this.transform.position = startPoint.transform.position;
        currentPosition = startPoint.transform.position;

        //ustawienie kierunku poruszania sie
        SetDirection();

        InvokeRepeating("OnDamage", 4f, 4f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //przemieszczenie sie przeciwnika jezeli zyje
        if (!isDead)
        { 
            MoveOnPath();
        }
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
            Passed();
        }
    }

    public void OnDamage()
    {
        Debug.Log("Damage Taken");
        int damage = 40;
        hitPoints -= damage;
        hitPointsBarController.UpdateHitPointsBar(hitPoints, maxHitPoints);
        animator.SetTrigger("injured");
        if(hitPoints <= 0)
        {
            isDead = true;
            animator.SetTrigger("dead");
        }
    }

    private void SetDirection()
    {
        //Debug.Log("Current position" + (currentPosition).ToString());
        //Debug.Log("Next position" + (nextTarget.transform.position).ToString());
        if (nextTarget.transform.position.x > currentPosition.x && Math.Abs(nextTarget.transform.position.y) - Math.Abs(currentPosition.y) == 0)
        {
            direction = Direction.right;
        }
        else if (nextTarget.transform.position.x < currentPosition.x && Math.Abs(nextTarget.transform.position.y) - Math.Abs(currentPosition.y) == 0)
        {
            direction = Direction.left;
        }
        else if (nextTarget.transform.position.y < currentPosition.y && Math.Abs(nextTarget.transform.position.x) - Math.Abs(currentPosition.x) == 0)
        {
            direction = Direction.bottom;
        }
        else if (nextTarget.transform.position.y > currentPosition.y && Math.Abs(nextTarget.transform.position.x) - Math.Abs(currentPosition.x) == 0)
        {
            direction = Direction.top;
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

    private void Dead()
    {
        Destroy(this.gameObject);
    }

    private void Passed()
    {
        Destroy(this.gameObject);
    }
}
