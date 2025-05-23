using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMOD.Studio;
using UnityEngine.EventSystems;

public class AgentMoveTo : MonoBehaviour
{
    public Camera cam;
    protected NavMeshAgent agent;
    protected Vector3 destination;

    protected EventInstance footSteps;

    // Start is called before the first frame update
    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();
        destination = this.transform.position;
        footSteps = AudioManager.instance.CreateInstance(AudioManager.instance.footSteps);
        GameManager.onChangePlayerEnabled += OnChangePlayerEnabled;
    }


    public void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hit))
                {
                    SetDestination(hit.point);
                }
            }
        }
    }

    public void FixedUpdate()
    {
        UpdateSound();
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.destination = destination;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(destination, .2f);
    }

    private void UpdateSound()
    {
        if(agent.velocity != Vector3.zero)
        {
            PLAYBACK_STATE playbackState;
            footSteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                footSteps.start();
            }

        } else
        {
            footSteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnChangePlayerEnabled(bool enabled)
    {
        if ( !enabled )
            footSteps.stop(STOP_MODE.ALLOWFADEOUT);

        this.enabled = enabled;
    }

}
