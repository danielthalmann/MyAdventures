using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class AgentMoveTo : MonoBehaviour, PlayerMovementInterface
{
    protected NavMeshAgent agent;
    protected Vector3 destination;
    protected Vector3 objectPosition;
    protected float timeout;

    protected bool inMoving;

    public delegate void OnAgentMove();
    public OnAgentMove onAgentMove;

    public delegate void OnAgentStop();
    public OnAgentStop onAgentStop;
    
    public GameObject hitObject { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = this.transform.position;
        inMoving = false;
        hitObject = null;
        timeout = 0;
    }

    public void Update()
    {

        if (inMoving)
        {
            timeout += Time.deltaTime;
            if (timeout > 0.1  && agent.velocity == Vector3.zero)
            {
                UpdateOrientation();
                timeout = 0;
                inMoving = false;
                onAgentStop?.Invoke();
            }
        }
    }

    private void UpdateOrientation()
    {

        if (Mathf.Abs(transform.position.sqrMagnitude - destination.sqrMagnitude) > 0.01)
        {

            if (hitObject != null)
            {
                // rotate to object hit
                Vector3 targetDirection = hitObject.transform.position - transform.position;
                targetDirection.y = 0;

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(targetDirection);
            }
        }

    }

    public void FixedUpdate()
    {
        
    }

    public void SetDestination(Vector3 destination, GameObject hitObject)
    {
        timeout = 0;
        inMoving = true;
        onAgentMove?.Invoke();
        this.destination = destination;
        this.hitObject = hitObject;
        objectPosition = destination;
        agent.destination = destination;
    }

    private void OnDrawGizmos()
    {
        if (inMoving)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawSphere(destination, .2f);

        Gizmos.DrawLine(transform.position, destination);
    }

    private void OnChangePlayerEnabled(bool enabled)
    {
        this.enabled = enabled;
    }

}
