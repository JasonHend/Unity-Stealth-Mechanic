using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthManager : MonoBehaviour
{
    enum GAMESTATE
    {
        HIDDEN,
        PARTIAL_ALERT,
        FULL_ALERT
    }

    GAMESTATE currentState = GAMESTATE.HIDDEN

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
