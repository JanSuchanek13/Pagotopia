using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour
{
    // tutorial base: https://www.youtube.com/watch?v=RXB7wKSoupI 

    //stats that worked: at 100 scale
    /* speed: .9
     * angular drag: 90
     * accell 8
     * stoppin .2
     * radius .0005
     * height 1e-05
     */

    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    [SerializeField] float minTimerForNewPath;
    [SerializeField] float maxTimerForNewPath;
    bool inCoRoutine;
    bool validPath;
    Vector3 target;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        
        // this part is to get cows Moving right on spawn
        minTimerForNewPath = .1f;
        maxTimerForNewPath = .1f;
        Invoke("StartMoving", .5f);
    }

    // here the MeshNav receives its real time-variables
    void StartMoving()
    {
        minTimerForNewPath = 3f;
        maxTimerForNewPath = 8f;
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-100f, 100);
        float z = Random.Range(-100f, 100);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }
    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(Random.Range(minTimerForNewPath, maxTimerForNewPath));
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);

        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }

    public void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }
    void Update()
    {
        if (!inCoRoutine)
        {
            StartCoroutine(DoSomething());
        }
    }
}
