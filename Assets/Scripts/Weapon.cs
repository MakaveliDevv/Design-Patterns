using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string Name = "";
    public bool isActive = false;
    public bool isShooting = false;
    public float fireRate = 2f;
    public float nextTimeToFire = 0f;

    // public object InRange(Transform transform, LayerMask layerMask) 
    // {
    //     Collider2D collider = this.GameObject().GetComponent<Collider2D>();
        
    //     if(Physics.OverlapSphere(this.GameObject(), 2f, layerMask))
        
    //     Transform _transform = transform;
    //     return _transform;
    // }
}
