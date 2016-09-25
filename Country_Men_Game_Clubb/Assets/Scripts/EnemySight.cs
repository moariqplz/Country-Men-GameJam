using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfView = 100;
    public float sightRange = 30;
    public bool playerInSight;

    private NavMeshAgent navMesh;
    

    private EnemyController enemyController;

    void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        enemyController = GetComponentInParent<EnemyController>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = enemyController.player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfView * 0.5f)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + transform.up, direction.normalized * sightRange, Color.green );
            if (Physics.Raycast(transform.position + transform.up, direction.normalized * sightRange, out hit))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    //check if invisible else chase
                    playerInSight = true;
                }
                else
                {
                    playerInSight = false;
                }
            }
            else
            {
                playerInSight = false;
            }
        }
        else
        {
            playerInSight = false;
        }
    }
}
