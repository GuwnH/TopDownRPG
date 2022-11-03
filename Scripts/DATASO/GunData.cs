using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGunData", menuName = "Data/GunData")]

public class GunData : ScriptableObject
{
    public float reloadDelay = 0.4f;
}
