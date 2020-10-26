using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController3 : MonoBehaviour
{
    public float lookRadius = 10f;
    [SerializeField] float damage = 10f;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    Transform target;
    NavMeshAgent agent;
    public int maxHealth = 100;
    public int currentHealth;

    public EnemyHealthBar healthBar;

    public Level03Controller controller3;
    [SerializeField] AudioClip _damage;

    // Start is called before the first frame update
    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
   private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        attackCooldown -= Time.deltaTime;
        if (currentHealth == 0)
        {
            controller3.IncreaseScore(5);
            Debug.Log("Killed enemy");
            Destroy();
        }
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {

                FaceTarget();

                if (attackCooldown <= 0f)
                {
                    AudioManager.Instance.PlaySong(_damage);
                    PlayerHealth.singleton.TakeDamage(damage);
                    Debug.Log("Player taking damage");
                    attackCooldown = 1f / attackSpeed;
                }
            }
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    public void TakeDamage(int _damageToTake)
    {

        currentHealth -= _damageToTake;
        healthBar.SetHealth(currentHealth);
        Debug.Log(currentHealth + " Health remaining");

        //check to make sure if dead, if so kill this enemy 
    }
}
