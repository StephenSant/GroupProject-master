﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;

    // Use this for initialization
    void Start ()
    {
        SwitchWeapon();
	}
	
	// Update is called once per frame
	void Update ()
    {
        int previousSelected = selectedWeapon;
		if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (previousSelected != selectedWeapon)
        {
            SwitchWeapon();
        }
	}
    void SwitchWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

}
