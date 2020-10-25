using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;

  

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        } else
        {
            
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        // Debug.Log("Activate Menu");
         Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }

   public void DeactivateMenu()
    {
       // Debug.Log("Deactivate Menu");
      Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
       isPaused = false;
    }
}
