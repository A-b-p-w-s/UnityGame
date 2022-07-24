using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reGame : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
