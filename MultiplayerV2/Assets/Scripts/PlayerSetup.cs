using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    string remoteLayerName = "RemotePlayer";


    Camera sceneCamera;

    private void Start()
    {
        //Check if local player
        if (!isLocalPlayer)
        {
            DIsableComponents();
            AssignRemoteLayer(); // assign the specif playet tag to the local client if the player is local or server sided
            
        }else //Dissable main camera from the game view 
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

        RegisterPlayer();
    }
    void RegisterPlayer()
    {
        string _ID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }


    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
    void DIsableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            //Disable all of components to dissable 
            componentsToDisable[i].enabled = false;
        }
    }



    void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }





}
