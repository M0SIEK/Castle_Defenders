using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 15f;
    public float lifeTime = 5f;
    private Transform target;
    private Animator animator;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            Debug.Log($"Animator działa na obiekcie: {gameObject.name}");
            if (animator.runtimeAnimatorController != null)
            {
                // Dynamiczne odtworzenie stanu animacji, jeśli istnieje
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (!stateInfo.IsName("Tower 01 - Level 01 - Projectile_Clip")) // Zmień na domyślną nazwę stanu animacji, jeśli różna
                {
                    animator.Play("Base Layer.Tower 01 - Level 01 - Projectile_Clip", 0, 0f);
                    Debug.Log($"Animator ustawiony na {animator.GetCurrentAnimatorStateInfo(0).shortNameHash}");
                }
            }
            else
            {
                Debug.LogError($"Animator Controller nie przypisany na obiekcie: {gameObject.name}");
            }
        }
        else
        {
            Debug.LogError($"Animator nie znaleziony na obiekcie: {gameObject.name}");
        }

        Destroy(gameObject, lifeTime); // Zniszczenie po czasie życia
    }

    void Update()
    {
        if (target == null)
        {
            Debug.Log("Target lost!");
            Destroy(gameObject);
            return;
        }

        // Ruch pocisku w stronę celu
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        EnemyController enemy = target.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.OnDamage(damage);
        }

        Destroy(gameObject);
    }
}