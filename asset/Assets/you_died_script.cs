using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class you_died_script : MonoBehaviour
{
    public GameObject GoToMainMenuButton;
    public GameObject RestartGameButton;
    public void end_ani()
    {
        GoToMainMenuButton.SetActive(true);
        RestartGameButton.SetActive(true);
    }
}
