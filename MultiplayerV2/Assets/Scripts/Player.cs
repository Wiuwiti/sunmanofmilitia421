using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    [SerializeField]
    private int maxHealth = 100;

    [SyncVar] // sync the var with all clinets conected to the sserver
    private int currentHealth;

    void Awake()
    {
        SetDefaults();
    }

    public void TakeDamage(int _amount)
    {
        currentHealth -= _amount;
        Debug.Log(transform.name + " now has " + currentHealth + " health.");//server debugg for health 
    }


    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

}
