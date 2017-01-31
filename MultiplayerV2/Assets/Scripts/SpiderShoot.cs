﻿
using UnityEngine;
using UnityEngine.Networking;

public class SpiderShoot : NetworkBehaviour
{

    private const string PLAYER_TAG = "Player";
    public SpiderWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;


    private void Start()
    {
        if (cam == null)
        {
            Debug.LogError("Spider atack : no camera refernced");
            this.enabled = false;
        }
    }

    private void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _dmg)
    {
        Debug.Log(_playerID + " has been shot");
        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_dmg);
    }
}