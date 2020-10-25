using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{

    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] GameObject visualFeedbackObject;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] LayerMask damageLayers;
    [SerializeField] GameObject muzzelFlash;

        
    RaycastHit hit;       //stores info about our raycast hit
    // Update is called once per frame

    private void Update()
    {
        visualFeedbackObject.SetActive(false);
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            muzzelFlash.SetActive(true);
            Shoot();
        }
        else
        {
            muzzelFlash.SetActive(false);
        }
    }

    //fire the weapon using a raycast
    void Shoot()
    {
        //calculate direction to shoot the ray
        Vector3 rayDirection = cameraController.transform.forward;
        //cast a debug ray
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.red, 1f);
        //do the raycast
       
        if(Physics.Raycast(rayOrigin.position, rayDirection, out hit, shootDistance, damageLayers))
        {
           
            //Hit the name, hit thing dostuff() code
            Debug.Log("You HIT the " + hit.transform.name);

               //if (hit.transform.tag == "Test")
                //{
                // Debug.Log("DEAL DAMAGE");
                 //visual feedback
                 visualFeedbackObject.transform.position = hit.point;

                 //apply damage if its enemy
                 EnemyShooter enemyShooter = hit.transform.gameObject.GetComponent<EnemyShooter>();
                     if (enemyShooter != null)
                     {
                         visualFeedbackObject.SetActive(true);
                         enemyShooter.TakeDamage(weaponDamage);
                     }
                //}
        }   else
        {
            Debug.Log("Miss");
        }
    }
}
