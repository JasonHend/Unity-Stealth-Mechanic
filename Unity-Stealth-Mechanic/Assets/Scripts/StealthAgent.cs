using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Detection data and logic that will allow agents to detect a player
/// </summary>
public class StealthAgent : MonoBehaviour
{
    // Outlines the different behavior states an agent will follow
    public enum AGENTSTATE
    {
        IDLE,
        CURIOUS,
        PURSUING
    }

    [SerializeField]
    AGENTSTATE currentState;

    // The main character or object that is being tracked
    [SerializeField]
    Collider target;

    // All colliders attached to the object
    [SerializeField]
    Collider[] colliders;

    // Determines the type and number of detection methods active
    [SerializeField]
    private bool colliderDetection = false;

    // Custom edits to collision
    [SerializeField]
    private float detectionTime;
    private float timer = 0.0f;
    private bool timerStarted;

    // Stealth manager
    private StealthManager manager;

    // Start is called before the first frame update
    void Start()
    {
        // Handle if target has not yet been set and disable collision detection
        if (target == null)
        {
            Debug.Log("Agent target is currently null for: " + this.ToString());
            colliderDetection = false;
        }

        // Enable correct functionality
        if (!colliderDetection)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;
            }
        }

        // Set timer to detection time
        timer = detectionTime;

        // Get reference to manager
        manager = FindObjectOfType<StealthManager>();
    }

    /// <summary>
    /// Allows for state to be changed by logic outside of simple detection timers, such as abilities that make the target invisible.
    /// </summary>
    /// <param name="newState">state to change to</param>
    public void ChangeDetectionState(AGENTSTATE newState)
    {
        currentState = newState;

        // Add any necessary logic to update other agents here
        switch (newState)
        {
            case AGENTSTATE.IDLE:
                break;

            case AGENTSTATE.CURIOUS:
                break;

            case AGENTSTATE.PURSUING:
                break;
        }

    }

    #region Collider Detection Logic
    /// <summary>
    /// Handles first instance of collider detection
    /// </summary>
    /// <param name="other">object colliding</param>
    private void OnTriggerEnter(Collider other)
    {
        // Return if collision is not with target
        if (other != target) return;

        // Timer logic
        if (detectionTime <= 0)
        {
            manager.ChangeState(StealthManager.GAMESTATE.FULL_ALERT);
            ChangeDetectionState(AGENTSTATE.PURSUING); // If no detection time has been set (instant detection) pursue target
        }
        else if (detectionTime > 0)
        {
            ChangeDetectionState(AGENTSTATE.CURIOUS);
            timerStarted = true; // If detection time has been set, run logic
        }
    }

    /// <summary>
    /// Decreases timer if detection time has been set
    /// </summary>
    /// <param name="other">object colliding</param>
    private void OnTriggerStay(Collider other)
    {
        // Return if collision is not with target
        if (other != target) return;

        // Check timer and handle logic
        if (timer <= 0)
        {
            timer = detectionTime;
            timerStarted = false;
            manager.ChangeState(StealthManager.GAMESTATE.FULL_ALERT);
            ChangeDetectionState(AGENTSTATE.PURSUING);
        }
        else if (currentState == AGENTSTATE.CURIOUS) timer -= Time.deltaTime;
    }

    /// <summary>
    /// Handles if target escaped collision before full detection
    /// </summary>
    /// <param name="other">object colliding</param>
    private void OnTriggerExit(Collider other)
    {
        // Return if collision is not with target
        if (other != target) return;

        // Reset timer and detection state
        if (timerStarted == true)
        {
            timer = detectionTime;
            timerStarted = false;
            ChangeDetectionState(AGENTSTATE.IDLE);
        }
    }
    #endregion
}
