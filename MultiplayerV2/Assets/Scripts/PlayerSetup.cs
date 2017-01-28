using UnityEngine;
using UnityEngine.Networking;



[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";
    [SerializeField]
    GameObject playerGraphics;

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;


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
            // Disable player graphics for local player 
            SetLayerRecursevily(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            //Create Player UI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
        }
        GetComponent<Player>().Setup();
    }

    void SetLayerRecursevily(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach(Transform child in obj.transform)
        {
            SetLayerRecursevily(child.gameObject, newLayer);
        }
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
        Destroy(playerUIInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }





}
