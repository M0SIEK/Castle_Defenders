using System.Collections;
using System.Collections.Generic;
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
    public float hitPoints = 100f; // Aktualne punkty życia przeciwnika
    public float maxHitPoints = 100f; // Maksymalne punkty życia przeciwnika
    public float speed = 0.02f; // Prędkość poruszania się przeciwnika
    public Transform startPoint; // Punkt początkowy
    public Transform nextTarget; // Kolejny punkt trasy
    public HitPointsBarController hitPointsBarController; // Kontroler paska życia
    public Animator animator; // Animator przeciwnika

    private Direction direction;
    private Direction currentDirection;
    private Vector3 currentTargetPosition;
    private bool isDead = false;
    private WavesController wavesController;
    private HitPointsBarController playerHitPointsBarController;
    private static float playerHitPoints = 1000;

    void Start()
    {
        hitPointsBarController = GetComponentInChildren<HitPointsBarController>();
        wavesController = GameObject.FindGameObjectWithTag("WavesController").GetComponent<WavesController>();
        animator = GetComponent<Animator>();
        playerHitPointsBarController = GameObject.FindGameObjectWithTag("GUI").GetComponentInChildren<HitPointsBarController>();

        hitPointsBarController.UpdateHitPointsBar(hitPoints, maxHitPoints);

        currentTargetPosition = startPoint.position;
        currentDirection = Direction.right;

        SetDirection();

        var randTranslation = UnityEngine.Random.Range(-0.35f, 0.35f);

        // Początkowa pozycja przeciwnika
        this.transform.position = (direction == Direction.right || direction == Direction.left)
            ? new Vector3(this.transform.position.x, this.transform.position.y + randTranslation, this.transform.position.z)
            : new Vector3(this.transform.position.x + randTranslation, this.transform.position.y, this.transform.position.z);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            MoveOnPath();
        }
    }

    public void OnTargetReached(Transform nextPathTarget)
    {
        if (nextPathTarget != null)
        {
            currentTargetPosition = nextPathTarget.position == nextTarget.position ? currentTargetPosition : nextTarget.position;
            nextTarget = nextPathTarget;
            SetDirection();
        }
        else
        {
            Passed();
        }
    }

    public void OnDamage(float damage)
    {
        if (isDead) return;

        Debug.Log("Enemy took damage: " + damage);
        hitPoints -= damage;
        hitPointsBarController.UpdateHitPointsBar(hitPoints, maxHitPoints);
        animator.SetTrigger("injured");

        if (hitPoints <= 0)
        {
            isDead = true;
            animator.SetTrigger("dead");
            Invoke(nameof(Dead), 1f); // Czekaj na zakończenie animacji przed usunięciem obiektu
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

        if (currentDirection == Direction.left && direction == Direction.right || currentDirection == Direction.right && direction == Direction.left)
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
        playerHitPoints -= 100;
        playerHitPointsBarController.UpdateHitPointsBar(playerHitPoints, 1000);
        Destroy(this.gameObject);
    }
}