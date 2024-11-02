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
    public float hitPoints = 100f;
    public float maxHitPoints = 100f;
    public float speed = 0.02f;
    public GameObject startPoint;
    public GameObject nextTarget;
    public HitPointsBarController hitPointsBarController;
    public Animator animator;

    private Direction direction;
    private Direction currentDirection;
    private Vector3 currentPosition;
    private bool isDead = false;
    void Start()
    {
        hitPointsBarController = GetComponentInChildren<HitPointsBarController>();
        animator = GetComponent<Animator>();

        hitPointsBarController.UpdateHitPointsBar(hitPoints, maxHitPoints);
        this.transform.position = startPoint.transform.position;
        currentPosition = startPoint.transform.position;
        currentDirection = Direction.right;

        //ustawienie kierunku poruszania sie
        SetDirection();

        InvokeRepeating("OnDamage", 10f, 8f); //tylko do testowania
    }

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

    //ponizsza metoda OnDamage() sluzy do testowania otrzymywania obrazen do czasu implementacji atakow wiezy
    public void OnDamage()
    {
        Debug.Log("Damage Taken");
        float damage = 40f;
        hitPoints -= damage;
        hitPointsBarController.UpdateHitPointsBar(hitPoints, maxHitPoints);
        animator.SetTrigger("injured");
        if(hitPoints <= 0)
        {
            isDead = true;
            animator.SetTrigger("dead");
        }
    }

    //wlasciwa metoda OnDamage(float) do obslugi otrzymania obrazen
    public void OnDamage(float damage)
    {
        Debug.Log("Damage Taken");
        hitPoints -= damage;
        hitPointsBarController.UpdateHitPointsBar(hitPoints, maxHitPoints);
        animator.SetTrigger("injured");
        if (hitPoints <= 0)
        {
            isDead = true;
            animator.SetTrigger("dead");
        }
    }

    private void SetDirection()
    {
        if (nextTarget.transform.position.x > currentPosition.x && nextTarget.transform.position.y == currentPosition.y)
        {
            direction = Direction.right;
        }
        else if (nextTarget.transform.position.x < currentPosition.x && nextTarget.transform.position.y == currentPosition.y)
        {
            direction = Direction.left;
        }
        else if (nextTarget.transform.position.y < currentPosition.y && nextTarget.transform.position.x == currentPosition.x)
        {
            direction = Direction.bottom;
        }
        else if (nextTarget.transform.position.y > currentPosition.y && nextTarget.transform.position.x == currentPosition.x)  
        {
            direction = Direction.top;
        }
        
        //odbicie postaci w osi X gdy zmieniany jest kierunek na przeciwny
        if(currentDirection == Direction.left && direction == Direction.right || currentDirection == Direction.right && direction == Direction.left)
        {
            Vector3 newScale = new Vector3(this.transform.localScale.x * (-1), this.transform.localScale.y, this.transform.localScale.z);
            this.transform.localScale = newScale;
            this.currentDirection = direction;
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
