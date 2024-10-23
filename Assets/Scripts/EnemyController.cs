using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

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
    }

    // Update is called once per frame
    void Update()
    {
        //przemieszczenie sie przeciwnika
        Move();
    }

    void SetDirection()
    {
        if (this.transform.position.x != nextTarget.transform.position.x)
        {
            direction = nextTarget.transform.position.x > this.transform.position.x ? Direction.right : Direction.left;
        }
        else
        {
            direction = nextTarget.transform.position.y > this.transform.position.y ? Direction.top : Direction.bottom;
        }
    }

    private void Move()
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

}
