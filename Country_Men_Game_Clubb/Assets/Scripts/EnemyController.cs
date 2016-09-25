using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public NavMeshAgent navMesh;
    public Transform[] navNodes;
    public int currentNode = 0;
    public int runVoice;
    public int attractVoice;

    public bool seesPlayer;
    public bool runningAway;
    public bool partolling;
    public bool investigate;
    public bool chasePlayer;

    public float partolSpeed;
    public float patrolView;

    public float investigateSpeed;
    public float investigateView;

    public float runSpeed;
    public float runView;

    public float chaseSpeed;
    public float chaseView;
    private SphereCollider hearingRange;

    public GameObject player;
    public EnemySight enemySight;
    public Animator anim;

    public enum EnemyType
    {
        Small,
        Medium,
        Large,
    }
    public EnemyType enemyType;

    // Use this for initialization
    void Start ()
    {
        hearingRange = GetComponent<SphereCollider>();
        IgnoreNodes(hearingRange, true);
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(navNodes[GetNewNode()].position);
        player = GameObject.FindGameObjectWithTag("Player");
        enemySight = transform.GetChild(0).GetComponent<EnemySight>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(investigate && !navMesh.hasPath)
        {
            Invoke("ToPatrolState", Random.Range(2, 6));
        }

    }

    void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Node":
                if (partolling)
                {
                    navMesh.SetDestination(navNodes[GetNewNode()].position);
                    navMesh.Stop();
                    anim.SetTrigger("Waiting");
                    if(enemyType.ToString() == "Small")
                    {
                        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Waiting");
                        transform.GetChild(1).GetComponent<Animator>().SetTrigger("Waiting");
                        transform.GetChild(2).GetComponent<Animator>().SetTrigger("Waiting");
                        transform.GetChild(3).GetComponent<Animator>().SetTrigger("Waiting");
                        transform.GetChild(4).GetComponent<Animator>().SetTrigger("Waiting");
                        transform.GetChild(5).GetComponent<Animator>().SetTrigger("Waiting");
                    }
                    Invoke("Move", Random.Range(0, 2));
                }
                break;

            case "Enemy":
                if(other.GetComponent<EnemyController>().enemyType.ToString() != this.enemyType.ToString())
                {
                    Destroy(this.gameObject);
                }
                break;

            case "Player":
                //switch case for audio and AI behavior
                if (!runningAway && player.GetComponent<MicInput>().playerVoice == runVoice)
                {
                    ToRunState();
                }
                else if (!runningAway && !investigate && player.GetComponent<MicInput>().playerVoice == attractVoice)
                {
                    ToInvestigateState(player.transform.position);
                }
                else if (enemySight.playerInSight && !runningAway)
                {
                    ToChaseState();
                }

                break;

            case "AudioRange":
                break;
        }
    }

    void Move()
    {
        anim.SetTrigger("Walk");
        if (enemyType.ToString() == "Small")
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(2).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(3).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(4).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(5).GetComponent<Animator>().SetTrigger("Walk");
        }
        navMesh.Resume();
    }

    int GetNewNode()
    {        
        int newNode = Random.Range(0, navNodes.Length);
        if (newNode != currentNode)
        {
            currentNode = newNode;
            return newNode;
        }
        else
        {
            newNode += 1;
            if (newNode > navNodes.Length-1)
            {
                newNode = 0;
            }
            currentNode = newNode;
        }
        return newNode;
    }

    void IgnoreNodes(Collider collider, bool ignore)
    {
       foreach(Transform node in navNodes)
        {
            Physics.IgnoreCollision(collider, node.GetComponent<Collider>(), ignore);
        }
    }

    void ToPatrolState()
    {
        anim.SetTrigger("Walk");
        if (enemyType.ToString() == "Small")
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(2).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(3).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(4).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(5).GetComponent<Animator>().SetTrigger("Walk");
        }
        runningAway = false;
        chasePlayer = false;
        investigate = false;
        navMesh.speed = partolSpeed;
        enemySight.fieldOfView = patrolView;
        navMesh.SetDestination(navNodes[GetNewNode()].position);
    }

    void ToInvestigateState(Vector3 InvestigatePoint)
    {
        anim.SetTrigger("Walk");
        if (enemyType.ToString() == "Small")
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(2).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(3).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(4).GetComponent<Animator>().SetTrigger("Walk");
            transform.GetChild(5).GetComponent<Animator>().SetTrigger("Walk");
        }
        runningAway = false;
        chasePlayer = false;
        investigate = true;
        navMesh.speed = investigateSpeed;
        enemySight.fieldOfView = investigateView;
        navMesh.SetDestination(InvestigatePoint);
        //return to patrol after reaching point and delay for 2-5 seconds
    }

    void ToChaseState()
    {
        anim.SetTrigger("Run");
        if (enemyType.ToString() == "Small")
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(2).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(3).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(4).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(5).GetComponent<Animator>().SetTrigger("Run");
        }
        chasePlayer = true;
        runningAway = false;
        investigate = false;
        navMesh.speed = chaseSpeed;
        enemySight.fieldOfView = chaseView;
        if(enemyType.ToString() == "Large")
        {
            //wait 2.1 seconds
            System.Threading.Thread.Sleep(2100);
        }
        else
        {
            navMesh.SetDestination(player.transform.position);
        }
        
    }

    void ToRunState()
    {
        anim.SetTrigger("Run");
        if (enemyType.ToString() == "Small")
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(2).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(3).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(4).GetComponent<Animator>().SetTrigger("Run");
            transform.GetChild(5).GetComponent<Animator>().SetTrigger("Run");
        }
        runningAway = true;
        chasePlayer = false;
        investigate = false;
        navMesh.speed = runSpeed;
        enemySight.fieldOfView = runView;

        //run away from sound x distance away
        Vector3 direction = Vector3.zero;

        //check which side I should run away to
        if (transform.position.x+ transform.position.z > player.transform.position.x+ player.transform.position.z)
        {
            direction = player.transform.position + transform.position;
        }
        else if (transform.position.x + transform.position.z < player.transform.position.x + player.transform.position.z)
        {
            direction = player.transform.position - transform.position;
        }

        Vector3 runTo = direction + transform.forward;

        Debug.DrawRay(transform.position + transform.up, direction.normalized * Random.Range(15,25), Color.red);

        NavMeshHit hit;
        NavMesh.SamplePosition(runTo,out hit, Random.Range(15, 25), 1);

        navMesh.SetDestination(hit.position);

        Invoke("ToPatrolState", Random.Range(3, 5));
    }
}
