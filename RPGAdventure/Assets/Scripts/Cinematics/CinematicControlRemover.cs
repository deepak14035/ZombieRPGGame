using RPG.control;
using RPG.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    private void Start()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped+= EnableControl;
    }
    // Start is called before the first frame update
    void DisableControl(PlayableDirector pd)
    {
        GameObject player=GameObject.FindWithTag("Player");
        player.GetComponent<ActionScheduler>().cancelAction();
        player.GetComponent<PlayerController>().enabled = false;
    }

    // Update is called once per frame
    void EnableControl(PlayableDirector pd)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().enabled = true;
    }
}
