using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemiesInScreen;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            DestroyEnemiesInScreen();
        }
    }

    public void DestroyEnemiesInScreen()
 {
    foreach (GameObject enemy in enemiesInScreen)
    {
        Destroy(enemy);
    }
 }
}
