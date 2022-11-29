using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour {

    public static ObjectPooling instance;

    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public GameObject yellow;

    public int poolAmount;

    List<GameObject> Red;
    List<GameObject> Blue;
    List<GameObject> Green;
    List<GameObject> Yellow;

    void Awake()
    {
        MakeInstance();

        Red = new List<GameObject>();
        Blue = new List<GameObject>();
        Green = new List<GameObject>();
        Yellow = new List<GameObject>();

        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(red);
            obj.transform.parent = transform;
            Red.Add(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(blue);
            obj.transform.parent = transform;
            Blue.Add(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(green);
            obj.transform.parent = transform;
            Green.Add(obj);
            obj.SetActive(false);
        }

        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(yellow);
            obj.transform.parent = transform;
            Yellow.Add(obj);
            obj.SetActive(false);
        }
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start ()
    {
       

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject GetRed()
    {
        for (int i = 0; i < Red.Count; i++)
        {
            if (!Red[i].activeInHierarchy)
            {
                return Red[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(red);
        obj.transform.parent = transform;
        Red.Add(obj);
        obj.SetActive(false);
        return obj;

    }

    public GameObject GetBlue()
    {
        for (int i = 0; i < Blue.Count; i++)
        {
            if (!Blue[i].activeInHierarchy)
            {
                return Blue[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(blue);
        obj.transform.parent = transform;
        Blue.Add(obj);
        obj.SetActive(false);
        return obj;

    }

    public GameObject GetGreen()
    {
        for (int i = 0; i < Green.Count; i++)
        {
            if (!Green[i].activeInHierarchy)
            {
                return Green[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(green);
        obj.transform.parent = transform;
        Green.Add(obj);
        obj.SetActive(false);
        return obj;

    }

    public GameObject GetYellow()
    {
        for (int i = 0; i < Yellow.Count; i++)
        {
            if (!Yellow[i].activeInHierarchy)
            {
                return Yellow[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(yellow);
        obj.transform.parent = transform;
        Yellow.Add(obj);
        obj.SetActive(false);
        return obj;

    }



}
