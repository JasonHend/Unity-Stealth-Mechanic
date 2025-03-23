using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the overall state of stealth between all agents included in a scene.
/// Can pass down function calls and change state of multiple or all agents in the game
/// </summary>
public class StealthManager : MonoBehaviour
{
    // Global stealth state
    public enum GAMESTATE
    {
        HIDDEN, // Player is hidden in global terms
        PARTIAL_ALERT,  // One or multiple agents are alerted to the players position
        FULL_ALERT // All agents can track the player
    }

    [SerializeField]
    GAMESTATE currentState = GAMESTATE.HIDDEN; // Depending on scene/game logic, change this to a desired state

    // All agents within the scene
    public StealthAgent[] stealthAgents = null;

    // Get references to all the agents
    private void Start()
    {
        stealthAgents = FindObjectsOfType<StealthAgent>();
    }

    /// <summary>
    /// Allows agents to change the global state of the scene
    /// </summary>
    /// <param name="newState">State to change to</param>
    public void ChangeState(GAMESTATE newState)
    {
        currentState = newState;

        // Changing states of all agents
        switch (newState)
        {
            case GAMESTATE.HIDDEN: // Make all agents idle
                foreach (var agent in stealthAgents)
                {
                    agent.ChangeDetectionState(StealthAgent.AGENTSTATE.IDLE);
                }
                break;

            case GAMESTATE.FULL_ALERT: // Make all agents fully alerted
                foreach (var agent in stealthAgents)
                {
                    agent.ChangeDetectionState(StealthAgent.AGENTSTATE.PURSUING);
                }
                break;

            default:
                break;
            
        }
    }
}
