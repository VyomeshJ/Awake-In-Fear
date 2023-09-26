//using Newtonsoft.Json.Bson; 
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
//using System.Numerics;
using UnityEngine;
using UnityEngine.AI;



public class navmeshAI : MonoBehaviour
{
    bool incontact;
    bool runable;
    public CharController_Motor CharController_Motor;
    public Animator attacking;
    public GameObject enemy;
    public NavMeshAgent navMeshAgent;               
    public float startWaitTime = 4;                 
    public float timeToRotate = 2;                  
    public float speedWalk = 3;                     
    public float speedRun = 9;                      
    public float viewRadius;                
    public float viewAngle;                  
    public LayerMask playerMask;              
    public LayerMask obstacleMask;                 
    public float meshResolution = 1.0f;             
    //public int edgeIterations = 4;                  
    //public float edgeDistance = 0.5f;
    //public bool enemyDefeated;
    public bool playercaught;
    public Transform target;
    
    public Transform[] waypoints;                   
    int m_CurrentWaypointIndex;                     

    Vector3 playerLastPosition = Vector3.zero;      
    Vector3 m_PlayerPosition;                       

    float m_WaitTime;                               
    float m_TimeToRotate;                           
    bool m_playerInRange;                          
    bool m_PlayerNear;                              
    public bool m_IsPatrol;                                
    bool m_CaughtPlayer;

    Vector3 enemybox;

    //SOUND system
    float SoundIndex;
    float SoundDistance;
    float SoundDecibel;

    public isHiding ishiding;
    public PlayerScript playerScript;

    public GameObject instantiateObject;
    public Transform player;
    bool canIstantiate;
    private GameObject currentInstantiated;
    Vector3 EnemySoundDistance;
    bool closeEnough;
    public int playerfloor;
    int enemyfloor;
    void Start()
        
    {
        incontact = false;
        runable = true;
        closeEnough = false;    
        canIstantiate = true;
        playercaught = false;
        //enemyDefeated = false;
        enemybox = new Vector3(20f, 1.0f, 10.0f);

        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_playerInRange = false;
        m_PlayerNear = false;
        m_WaitTime = startWaitTime;                
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;                
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;            
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);   
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        
    }

   
    //void instantiateObjects()
    ///{
        /// randomize roaring sound
        //canIstantiate = false;
        //Instantiate(instantiateObject, player.position, Quaternion.identity);
        //instantiateObject.GetComponent<MeshRenderer>().enabled = false;
        
    //}


    private void Update()

    {
        if (Input.GetKeyDown(KeyCode.V)) incontact = true;
        //if (runable) attacking.Play("Walking");
        //else attacking.Play("Attack");
        if (transform.position.y > 20  && transform.position.y <26) enemyfloor = 1;
        else if (transform.position.y >26) enemyfloor = 2;

        if (player.transform.position.y > 22 && player.transform.position.y < 26) playerfloor = 1;
        else if (player.transform.position.y > 28) playerfloor = 2;


        //if (enemyfloor != playerfloor)
        //{
           // Debug.Log("ssS");
           // speedWalk = 10;
            //speedRun = 12;
        //}

        if (enemyfloor == playerfloor)
        {
            //speedWalk = 2;
            //speedRun = 3.5f;

            //SoundDecibel = playerScript.SoundDecibel;
            //SoundDistance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            //SoundIndex = SoundDecibel / SoundDistance;

            //if(enemyfloor.enemyFloorNum == playerfloornum) then
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }

            //if (SoundIndex > 0.5)
            ///{

                //if(!closeEnough) SoundIndex = 0.6f;

               // EnviromentView();

                //currentInstantiated = GameObject.Find("Instantiated Object(Clone)");
                //instantiateObject.GetComponent<MeshRenderer>().enabled = false;
                //instantiateObject.GetComponent<BoxCollider>().enabled = false;
                //Debug.Log("sjd");
                //if (canIstantiate && playerScript.madeSound)
               // {
                 //   instantiateObjects();

               // }
                
               // m_IsPatrol = false;
               /// Move(speedRun);
                //Debug.Log(navMeshAgent.transform.position);
                //EnemySoundDistance = navMeshAgent.transform.position - currentInstantiated.transform.position; // this part causes issue for first floor
               // navMeshAgent.SetDestination(currentInstantiated.transform.position);
                //Debug.Log(EnemySoundDistance);
                //LookingPlayer(playerScript.PlayerPos);

                //if (!closeEnough) 


               // if (EnemySoundDistance.x <= 0.5 && EnemySoundDistance.x > -1 && EnemySoundDistance.z >= -0.5 && EnemySoundDistance.z < 1)
                //{
                  //  Destroy(GameObject.Find("Instantiated Object(Clone)"));
                  //  SoundIndex = 0;
                  //  canIstantiate = true;
                  //  closeEnough = true;

                  //  m_IsPatrol = true;

                  //  m_PlayerNear = false;
                  //  Move(speedWalk);
                  //  m_TimeToRotate = timeToRotate;
                  //  m_WaitTime = startWaitTime;
                 // /  navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the enemy destination to the next waypoint


               // }


                //Debug.Log(waypoints[m_CurrentWaypointIndex].position);


           //}
             
        }

            if (ishiding.ishiding)
            {
                SoundIndex = 0;
                m_IsPatrol = true;
                m_PlayerNear = false;
            Debug.Log("HIDING");
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        if (incontact == false && runable == true)
        {
            attacking.Play("Walking");
        }
        else if (incontact)
        {
            
            navMeshAgent.isStopped = true;

            navMeshAgent.speed = 0;
            //
            runable = false;
            attacking.Play("Attack");
            CharController_Motor.enabled = false;
            player.LookAt(target);
        }
        EnviromentView();

            if (!m_IsPatrol && SoundIndex == 0 && m_playerInRange)
            {
                //Debug.Log("CHECKED");
                navMeshAgent.SetDestination(playerScript.PlayerPos);



                // Chasing();
            }

            else if (m_IsPatrol && SoundIndex == 0 && !m_playerInRange)
            {
                Patroling();
            }
        
    }

    private void Chasing()
    {
        //  The enemy is chasing the player
       
        m_PlayerNear = false;                       
        playerLastPosition = Vector3.zero;         

        if (!m_CaughtPlayer)

        {
            Debug.Log("SSSSSSSSSS");
            //randomize roaring sound
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);          //  set the destination of the enemy to the player location
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)    //  Control if the enemy arrive to the player location
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                //  Check if the enemy is not near to the player, returns to patrol after the wait time delay
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                    
                    //  Wait if the current position is not the player position
                    Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    private void Patroling()
    {
        if (m_PlayerNear)
        {
            //  Check if the enemy detect near the player, so the enemy will move to that position
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                //  The enemy wait for a moment and then go to the last player position
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;           //  The player is no near when the enemy is platroling
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    //  Set the enemy destination to the next waypoint
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                //  If the enemy arrives to the waypoint position then wait for a moment and go to the next
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }
    
        

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("touched");
            incontact=true;
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        incontact = false;
    }
    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void Stop()
    {
        Debug.Log("stopped");
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
        if (enemyfloor == playerfloor)
        {

            Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);   //  Make an overlap sphere around the enemy to detect the playermask in the view radius

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
                            //navMeshAgent.speed = 0;

                            attacking.speed = 0f;
                            //Stop();
                            //attacking.Play("Attacking");

                        }
                        else


                            //attacking.Play("Walking");

                        //this causes the problem for first floor enemy
                        if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                        {
                        //Debug.Log("Player in range");
                            Debug.DrawRay(transform.position, dirToPlayer, Color.yellow);
                            SoundIndex = 0;
                            m_playerInRange = true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                            m_IsPatrol = false;                 //  Change the state to chasing the player
                        }

                        else
                        {
                            /*
                             *  If the player is behind a obstacle the player position will not be registered
                             * */
                            m_playerInRange = false;
                        }
                    }
                    if (Vector3.Distance(transform.position, player.position) > viewRadius)

                    {
                    
                        /*
                         *  If the player is further than the view radius, then the enemy will no longer keep the player's current position.
                         *  Or the enemy is a safe zone, the enemy will no chase
                         * */
                        m_playerInRange = false;                //  Change the sate of chasing
                    }
                    if (m_playerInRange)
                    {
                        /*
                         *  If the enemy no longer sees the player, then the enemy will go to the last position that has been registered
                         * */
                        m_PlayerPosition = player.transform.position;       //  Save the player's current position if the player is in range of vision
                    }
                }
            
        }
    }
}