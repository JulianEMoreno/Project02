using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathMenuUI;
    [SerializeField] public bool isPaused;
    [SerializeField] AudioClip _death;


    // Update is called once per frame
    
 
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
           // AudioManager.Instance.PlaySong(_death);
            isPaused = !isPaused;
        }
         
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {

            //DeactivateMenu();
        }
    }

    public void ActivateMenu()
    {
        // Debug.Log("Activate Menu");
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        AudioListener.pause = true;
        deathMenuUI.SetActive(true);
        AudioManager.Instance.PlaySong(_death);
        // isPaused = true;
    }
    /*
 public void DeactivateMenu()
    {
        // Debug.Log("Deactivate Menu");
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        AudioListener.pause = false;
        deathMenuUI.SetActive(false);
        isPaused = false;
    }
    */
}