using UnityEngine;

public class Interactable : MonoBehaviour, IInterActable
{
    public ScriptableObject scriptableObject;
    public bool InRange() 
    {
        if(!transform.TryGetComponent<CircleCollider2D>(out var collider)) return false;
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, collider.radius);
        
        if(hitCollider != null && hitCollider.TryGetComponent<Player>(out var player)) 
        {
            player.inRange = true;
            
            Debug.Log("Made collision with the Player");
            return true;
        }

        return false;
    }
}