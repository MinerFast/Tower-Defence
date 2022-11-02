using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressManager : MonoBehaviour
{
    public GameObject defaultFortrest;
    public GameObject[] upgradeFortrests;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var ft in upgradeFortrests)
        {
          
            ft.SetActive(false);
        }

        defaultFortrest.SetActive(false);

        if (GlobalValue.UpgradeStrongWall > 0)
            upgradeFortrests[GlobalValue.UpgradeStrongWall - 1].SetActive(true);
        else
            defaultFortrest.SetActive(true);
    }
}
