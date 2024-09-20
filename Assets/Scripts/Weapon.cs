using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string Name = "";
    public bool isActive = false;
    public bool isShooting = false;
    public float fireRate = 2f;
    public float nextTimeToFire = 0f;
}
