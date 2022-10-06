using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public bool Level;
    public int indexlevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Level)
        {
            ChangeLevel(indexlevel);
        }
        
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void ChangeLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

}
