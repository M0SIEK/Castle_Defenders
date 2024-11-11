using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public enum Direction
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
    public Transform startPoint;
    public Transform nextTarget;
    public HitPointsBarController hitPointsBarController;
    public Animator animator;

    private Direction direction;
    private Direction currentDirection;
    private Vector3 currentTargetPosition;
    private bool isDead = false;
    private WavesController wavesController;
    void Start()
    {
        hitPointsBarController = GetComponentInChildren<HitPointsBarController>();
        wavesController = GameObject.FindGameObjectWithTag("WavesController").GetComponent<WavesController>();
        animator = GetComponent<Animator>();

        hitPointsBarController.UpdateHitPointsBar(hitPoints, maxHitPoints);

        currentTargetPosition = startPoint.position;
        currentDirection = Direction.right;

        //ustawienie kierunku poruszania sie
        SetDirection();

        var randTranslation = UnityEngine.Random.Range(-1.0f, 1.0f);

        //ustawienie poczatkowej pozycji
        this.transform.position = (currentDirection == Direction.right || currentDirection == Direction.left) ? new Vector3(this.transform.position.x, this.transform.position.y + randTranslation, this.transform.position.z) : new Vector3(this.transform.position.x + randTranslation, this.transform.position.y, this.transform.position.z);

        //InvokeRepeating("OnDamage", 10f, 8f); //tylko do testowania
    }

    void FixedUpdate()
    {
        //przemieszczenie sie przeciwnika jezeli zyje
        if (!isDead)
        { 
            MoveOnPath();
        }
    }

    public void OnTargetReached(Transform nextPathTarget)
    {
        if(nextPathTarget != null)
        {
            currentTargetPosition = nextPathTarget.position == nextTarget.position ? currentTargetPosition : nextTarget.position;
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
        float damage = 50f;
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
        if (nextTarget.position.x > currentTargetPosition.x && nextTarget.position.y == currentTargetPosition.y)
        {
            direction = Direction.right;
        }
        else if (nextTarget.position.x < currentTargetPosition.x && nextTarget.position.y == currentTargetPosition.y)
        {
            direction = Direction.left;
        }
        else if (nextTarget.position.y < currentTargetPosition.y && nextTarget.position.x == currentTargetPosition.x)
        {
            direction = Direction.bottom;
        }
        else if (nextTarget.position.y > currentTargetPosition.y && nextTarget.position.x == currentTargetPosition.x)  
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
        wavesController.DecrementEnemyNumber();
        Destroy(this.gameObject);
    }

    private void Passed()
    {
        wavesController.DecrementEnemyNumber();
        Destroy(this.gameObject);
    }
}
