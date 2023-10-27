using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CellNavmesh : MonoBehaviour
{
    
    public AudioSource AiHitSound;
    //public doorScript buttondoor;
    //public Transform playerPosition;
    public GameObject chasecollider;
    //public GameObject jumpscareGO;
    public ViewRange jumpscareCheck;
    public ViewRange viewRange;
    public Transform ainewpos;
    public Animator dooranim;
    public CharController_Motor CharController_Motor;
    public float Health;
    float speed;
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
    public doorScript doorscript;
    public OpenButton buttondoor;
    public Animator AiAnimation;
    bool ready;
    bool isTrapped = false;
    bool inContact;
    bool dead;
    bool running;
    public Transform player;
    public Transform target;
    void Start()

    {
        Physics.IgnoreCollision(viewRange.GetComponent<Collider>(), GetComponent<Collider>());
        running = false;
        dead = false;
        inContact = false;
        ready = false;
        Health = 100f;

        m_IsPatrol = true;
        //avMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void runAble()
    {
        AiAnimation.Play("Running2");
    }
    // Update is called once per frame
    void waiter()
    {

        running = true;
        m_IsPatrol = true;
        m_PlayerNear = false;

        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
    void Update()

    {

        if(viewRange.InRange == true)
        {
            m_playerInRange = true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
            m_IsPatrol = false;
        }
        else
        {
            m_playerInRange = false;
            m_IsPatrol = true;
        }

        if (playerscript.electricityopen == true && dead == false)
        {
            //jumpscareGO.SetActive(true);
            //if collider triggered
            //if (jumpscareCheck.InRange)
           // {

                //play audio
              //  transform.position = ainewpos.position;
             //Debug.Log("running");
                //running = true;
               // if (running) runAble();
                //doorscript.DoorOpen = false;

                //dead = true;
               // ready = true;
            //}

        }

        if (ready)
        {
            if (SaveVariables.PlayerHiding_Closet == true || SaveVariables.PlayerHiding_Bed == true)
            {


                m_IsPatrol = true;
                m_PlayerNear = false;

                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);


            }

            if (inContact)
            {
                AiHitSound.Play();
                navMeshAgent.isStopped = true;

                navMeshAgent.speed = 0;
                //
                running = false;
                AiAnimation.Play("Attack");
                CharController_Motor.enabled = false;
                player.LookAt(target);
                
            }
            else if (inContact == false && running == true)
            {
                runAble();
            }

            //EnvironmentView();
            if (!m_IsPatrol)
            {

                navMeshAgent.SetDestination(playerscript.PlayerPos);
            }
            else
            {

                Patrolling();
            }

            if (Health <= 0)
            {
                dead = true;
                navMeshAgent.speed = 0;
                running = false;
                AiAnimation.Play("Defeated");
            }
        }

    }
    public void LowCaffeine()
    {
        navMeshAgent.speed = 6;
        navMeshAgent.SetDestination(playerscript.PlayerPos);
    }
    public IEnumerator Chase()
    {
        chasecollider.SetActive(true);
        jumpscareCheck.InRange = true;
        navMeshAgent.speed += 8;
        yield return new WaitForSeconds(0.1f);
        if (jumpscareCheck.InRange)
        {
          
            //play audio
            transform.position = ainewpos.position;
            //Debug.Log("running");
            running = true;
            if (running) runAble();
            //doorscript.DoorOpen = false;

            dead = true;
            ready = true;
        }

        yield return new WaitForSeconds(6.5f);
      
        navMeshAgent.speed -= 8;
    }
    IEnumerator wait_to_die()
    {
     
        yield return new WaitForSeconds(2);
        player.gameObject.GetComponent<PlayerScript>().DeathScene();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
           
            inContact = true;
            StartCoroutine(wait_to_die());

        }
        if (other.gameObject.name == "Projectile(Clone)" && ready == true)
        {

            dealDamage(100);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        inContact = false;
    }

    void dealDamage(float damage)
    {
        Health -= damage;
    }
    void NextPoint()
    {

        if (buttondoor.opened == true)
        {
            //Debug.Log("working");
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
        else
        {
            //Debug.Log("notking");
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % 4;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, viewRadius);


    }

    void Patrolling()
    {
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
    //void EnvironmentView()
    //{
       // Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        //for (int i = 0; i < playerInRange.Length; i++)
        //{
            //Transform player = playerInRange[i].transform;
            //Vector3 dirToPlayer = (player.position - transform.position).normalized;
           // if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            //{

                //float dstToPlayer = Vector3.Distance(transform.position, player.position);
               // Debug.Log(dstToPlayer);
                //if (dstToPlayer < 1.82)
               // {

                    //Debug.Log(dstToPlayer);
                    //AiAnimation.Play("Attack");
                   // playercaught = true;
                    //set destination should be changed
                    //navMeshAgent.ResetPath();

               // }

                //this causes the problem for first floor enemy
                //if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer-3, obstacleMask))
                //{
                    //Debug.Log(dstToPlayer);
                    //navMeshAgent.speed = 2f;
                    //PlAY SOUND
                   // Debug.DrawRay(transform.position, dirToPlayer, Color.yellow);

                   // m_playerInRange = true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                    //m_IsPatrol = false;                 //  Change the state to chasing the player
                //}

                //else
               // {
                    /*
                     *  If the player is behind a obstacle the player position will not be registered
                     * */
                   //m_playerInRange = false;
                   // m_IsPatrol = true;
                //}

            //}
            //if (Vector3.Distance(transform.position, player.position) > viewRadius)

           // {

                /*
                 *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                 *  Or the enemy is a safe zone, the enemy will no chase
                 * */
                //m_IsPatrol = true;
                //m_playerInRange = false;                //  Change the sate of chasing
           // }
            //if (m_playerInRange)
           // {
                /*
                 *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                 * */
                //m_PlayerPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision

           // }
       // }
    //}

}