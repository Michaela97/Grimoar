using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameManager : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        QuitGame();
    }

    private void QuitGame()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
