using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUI : MonoBehaviour
{
    public void Shoot()
    {
        ArcherManager.Instance.Shoot();
    }
}
