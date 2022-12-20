using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    bool alreadyTriggered=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered && other.gameObject.tag == "Player")
        {
            GetComponent<PlayableDirector>().Play();
            alreadyTriggered=true;
        }
    }
}
