using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
    if(Input.GetButtonDown("Jump"))
        {
        if(MovingCube.LastCube !=null)
        MovingCube.CurrentCube.Stop();
        
        FindObjectOfType<CubeSpawner>().SpawnCube();
        }
    }
}