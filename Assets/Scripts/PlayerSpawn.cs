using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    public Transform classicSpawn;
    public Transform alternateSpawn;


    private void Awake()
    {

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("LastScene")))
        {
            if ((SceneManager.GetActiveScene().name == "LivingRoom" && PlayerPrefs.GetString("LastScene") == "Office")
                || (SceneManager.GetActiveScene().name == "Office" && PlayerPrefs.GetString("LastScene") == "Library")
                || (SceneManager.GetActiveScene().name == "Library" && PlayerPrefs.GetString("LastScene") == "Lab"))
            {
                transform.position = alternateSpawn.position;
            }
            else
            {
                transform.position = classicSpawn.position;
            }
        }
    }

    




    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

}
