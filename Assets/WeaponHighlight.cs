using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHighlight : MonoBehaviour
{
    [SerializeField] GameObject _highLight1;
    [SerializeField] GameObject _highLight2;
    // Start is called before the first frame update

    public WeaponSwitching WeaponSwitching;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponSwitching.selectedWeapon == 0)
        {
            GunActivate();
            SwordDeactivate();
        }
        else
        {
            SwordActivate();


            GunDeactivate();
        }
    }

    void SwordActivate()
    {
        _highLight1.SetActive(true);
    }
    void GunActivate()
    {
        _highLight2.SetActive(true);
    }
    void SwordDeactivate()
    {
        _highLight1.SetActive(false);
    }
    void GunDeactivate()
    {
        _highLight2.SetActive(false);
    }
}
