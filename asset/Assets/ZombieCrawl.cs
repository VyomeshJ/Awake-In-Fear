using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCrawl : MonoBehaviour
{
    public float Health;
    public float speed;
    bool playercaught;
    bool enemyDefeated;
    float m_WaitTime;
    float m_TimeToRotate;
    bool m_playerInRange;
    bool m_PlayerNear;
    public bool m_IsPatrol;
    bool m_CaughtPlayer;
    public float viewRadius;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public NavMeshAgent navMeshAgent;
    public PlayerScript playerscript;
    float viewAngle = 360;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;
    public Vector3 TrapPos;

    bool isTrapped = false;
    // Start is called before the first frame update
    void Start()

    {
        Health = 100f;
        
        navMeshAgent.speed = 2.7f;
        m_IsPatrol = true;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {

        

            EnvironmentView();
            if (!m_IsPatrol)
            {

                navMeshAgent.SetDestination(playerscript.PlayerPos);
            }
            else Patrolling();
        
        

        if(Health <= 0)
        {
            navMeshAgent.speed = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //temporary scene
            // SceneManager.LoadScene("Scene_A");

        }
        if (other.gameObject.name == "Projectile(Clone)")
        {
            dealDamage(50);
        }

    }

    void dealDamage(float damage)
    {
        Health -= damage;
    }
    void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, viewRadius);


    }

    void Patrolling()
    {
        //Debug.Log("Patrolling");
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the enemy destination to the next waypoint
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
            if (m_WaitTime <= 0)
            {
                NextPoint();
                //Move(speedWalk);
                //m_WaitTime = startWaitTime;
            }
            else
            {
               // Stop();
                //m_WaitTime -= Time.deltaTime;
            }
        }
    }
    void PlayerCaught()
    {

    }
    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {

                float dstToPlayer = Vector3.Distance(transform.position, player.position);

                if (dstToPlayer < 2f)
                {
                    //Debug.Log(dstToPlayer);
                    //Debug.Log("stopped");
                    playercaught = true;
                    //set destination should be changed
                    navMeshAgent.ResetPath();
                    
                }
                //this causes the problem for first floor enemy
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    navMeshAgent.speed = 1.1f;
                    //PlAY SOUND
                    Debug.DrawRay(transform.position, dirToPlayer, Color.yellow);
                   
                    m_playerInRange = true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                    m_IsPatrol = false;                 //  Change the state to chasing the player
                }

                else
                {
                    /*
                     *  If the player is behind a obstacle the player position will not be registered
                     * */
                    m_playerInRange = false;
                    m_IsPatrol = true;
                }

            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)

            {

                /*
                 *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                 *  Or the enemy is a safe zone, the enemy will no chase
                 * */
                m_IsPatrol = true;
                m_playerInRange = false;                //  Change the sate of chasing
            }
            if (m_playerInRange)
            {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
                //m_PlayerPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision

            }
        }
    }
}