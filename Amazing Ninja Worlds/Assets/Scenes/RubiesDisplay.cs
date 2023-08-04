using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiesDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        public int levelNumber;
        public GameObject[] rubies;
    UpdateRubies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
public void UpdateRubies()
{
    GameObject.SetActive(PlayerPrefs.GetInt("Level" + levelNumber + "_Complete") != 0);
    for(int=0;int<3;int++)
    {
        RubiesDisplay[i].SetActive(PlayerPrefs.GetInt("Level" + levelNumber"_Gem" + (int + 1), 0) == 1);
    }
}
}
