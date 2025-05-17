using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // The amount of damage this object will deal to others
    // [SerializeField] makes it editable in Unity's Inspector while keeping it private
    [SerializeField] int damage = 10;

    // Public method to get the damage value (other scripts can read but not modify it)
    public int GetDamage(){
        return damage;
    }

    // Called when this object hits something (like a bullet hitting an enemy)
    public void Hit(){
        // Destroy this game object (e.g., bullet disappears after hitting)
        Destroy(gameObject);
    }
}