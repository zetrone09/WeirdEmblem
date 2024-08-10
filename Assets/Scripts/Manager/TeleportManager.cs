using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager instance;
    public PlayerManager playerManager;
    public bool isTeleporting = false;
    [SerializeField] List<GameObject> teleportIndex;
    private void Awake()
    {
        if (instance == null)
        { instance = this; }
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerManager = FindObjectOfType<PlayerManager>();
    }
    public void StartTeleportPoint()
    {
        playerManager.transform.position = teleportIndex[0].transform.position;
    }
    private void TeleportPoint()
    {
        teleportIndex.Clear();     
    }
    
}
