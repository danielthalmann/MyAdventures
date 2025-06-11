using FMOD.Studio;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerSound : MonoBehaviour
{
    public float recurrenceWalk;
    public FMODUnity.EventReference walkSound;

    private NavMeshAgent agent;
    private float timelaps;
    private EventInstance soundInstance;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timelaps = 0;
        agent = GetComponent<NavMeshAgent>();
        soundInstance = AudioManager.instance.CreateInstance(walkSound);
        
    }

    private void Update()
    {
        timelaps += Time.fixedDeltaTime;
        
        PLAYBACK_STATE state;
        soundInstance.getPlaybackState(out state);

        if (agent.velocity.magnitude > 0)
        {
            if (state == PLAYBACK_STATE.STOPPED)
                soundInstance.start();
        } else
        {
            if (state == PLAYBACK_STATE.PLAYING)
                soundInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

}
