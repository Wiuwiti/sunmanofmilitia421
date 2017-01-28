using UnityEngine;
using UnityEngine.Networking;



[RequireComponent(typeof(Player))]
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
        GetComponent<Player>().Setup();
    }

    public override void OnStartClient()
    {
        // this method is called everything a client is registered to enter a client 

        base.OnStartClient();
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);

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

        GameManager.UnRegisterPlayer(transform.name);
    }





}
