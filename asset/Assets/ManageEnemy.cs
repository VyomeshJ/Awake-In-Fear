using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManageEnemy : MonoBehaviour
{
    public navmeshAI navmeshai;
    public navmeshAI navmeshai1stfloor;
    bool callable;
    public GameObject enemy;
    public CharController_Motor movementcontroller;
    public Transform target;
    public Transform target1st;
    public Transform Camera;
    public Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        callable = true;
    }
    void spawnEnemy()
    {
        callable = false;
        CancelInvoke();
        
        GameObject EnemyDuplicate = GameObject.Instantiate(enemy);
        EnemyDuplicate.GetComponent<navmeshAI>().enabled = true;
        Debug.Log("check");
    }

    // Update is called once per frame
    void Update()
    {
        if (navmeshai.playerfloor == 2)
        {
            Vector3 targePosition = new Vector3(target.position.x, transform.position.y + 8, target.transform.position.z);
            if (navmeshai.enemyDefeated && callable == true)
            {

                Invoke(nameof(spawnEnemy), 25f);
            }



            if (navmeshai.playercaught)
            {
                movementcontroller.enabled = false;
                Camera.transform.LookAt(targePosition);
            }
        }
        if(navmeshai.playerfloor == 1)
        {
            Vector3 targeposition1 = new Vector3(target1st.position.x, transform.position.y + 8, target1st.position.z);

            if (navmeshai1stfloor.enemyDefeated && callable == true)
            {

                Invoke(nameof(spawnEnemy), 25f);
            }



            if (navmeshai1stfloor.playercaught)
            {
                
                movementcontroller.enabled = false;
                Camera.transform.LookAt(targeposition1);
            }
        }
    }
    
}
