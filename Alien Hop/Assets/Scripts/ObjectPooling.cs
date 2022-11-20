using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This script creates the clone of required objects and provide them as required
/// </summary>
/// 

public class ObjectPooling : MonoBehaviour
{

    public static ObjectPooling instance;

    public GameObject deathEffect, starPrefab; //ref to death effect prefabs
    public GameObject platform1, platform2, platform3, platform4, platform5,
        platform6, platform7, platform8, platform9, platform10;

    public int count = 5; //total clones of each object to be spawned

    List<GameObject> DeathEffect = new List<GameObject>();
    List<GameObject> StarPrefab = new List<GameObject>();
    List<GameObject> SpawnPlatform1 = new List<GameObject>();
    List<GameObject> SpawnPlatform2 = new List<GameObject>();
    List<GameObject> SpawnPlatform3 = new List<GameObject>();
    List<GameObject> SpawnPlatform4 = new List<GameObject>();
    List<GameObject> SpawnPlatform5 = new List<GameObject>();
    List<GameObject> SpawnPlatform6 = new List<GameObject>();
    List<GameObject> SpawnPlatform7 = new List<GameObject>();
    List<GameObject> SpawnPlatform8 = new List<GameObject>();
    List<GameObject> SpawnPlatform9 = new List<GameObject>();
    List<GameObject> SpawnPlatform10 = new List<GameObject>();

    void Awake()
    {
        MakeInstance();
        InitializeSpawning();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void InitializeSpawning()
    {
        //DeathEffect
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(deathEffect);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            DeathEffect.Add(obj);
        }

        //StarPrefab
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(starPrefab);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            StarPrefab.Add(obj);
        }

        //Platform1
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform1);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform1.Add(obj);
        }

        //Platform2
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform2);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform2.Add(obj);
        }

        //Platform3
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform3);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform3.Add(obj);
        }

        //Platform4
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform4);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform4.Add(obj);
        }

        //Platform5
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform5);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform5.Add(obj);
        }

        //Platform6
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform6);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform6.Add(obj);
        }

        //Platform7
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform7);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform7.Add(obj);
        }

        //Platform8
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform8);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform8.Add(obj);
        }

        //Platform9
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform9);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform9.Add(obj);
        }

        //Platform10
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(platform10);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform10.Add(obj);
        }

    }

    //method which is used to call from other scripts to get the clone object

    //DeathEffect
    public GameObject GetDeathEffect()
    {
        for (int i = 0; i < DeathEffect.Count; i++)
        {
            if (!DeathEffect[i].activeInHierarchy)
            {
                return DeathEffect[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(deathEffect);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        DeathEffect.Add(obj);
        return obj;
    }

    //StarPrefab
    public GameObject GetStarPrefab()
    {
        for (int i = 0; i < StarPrefab.Count; i++)
        {
            if (!StarPrefab[i].activeInHierarchy)
            {
                return StarPrefab[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(starPrefab);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        StarPrefab.Add(obj);
        return obj;
    }

    //Platform1
    public GameObject GetPlatfrom1()
    {
        for (int i = 0; i < SpawnPlatform1.Count; i++)
        {
            if (!SpawnPlatform1[i].activeInHierarchy)
            {
                return SpawnPlatform1[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform1);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform1.Add(obj);
        return obj;
    }

    //Platform2
    public GameObject GetPlatfrom2()
    {
        for (int i = 0; i < SpawnPlatform2.Count; i++)
        {
            if (!SpawnPlatform2[i].activeInHierarchy)
            {
                return SpawnPlatform2[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform2);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform2.Add(obj);
        return obj;
    }

    //Platform3
    public GameObject GetPlatfrom3()
    {
        for (int i = 0; i < SpawnPlatform3.Count; i++)
        {
            if (!SpawnPlatform3[i].activeInHierarchy)
            {
                return SpawnPlatform3[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform3);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform3.Add(obj);
        return obj;
    }

    //Platform4
    public GameObject GetPlatfrom4()
    {
        for (int i = 0; i < SpawnPlatform4.Count; i++)
        {
            if (!SpawnPlatform4[i].activeInHierarchy)
            {
                return SpawnPlatform4[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform4);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform4.Add(obj);
        return obj;
    }

    //Platform5
    public GameObject GetPlatfrom5()
    {
        for (int i = 0; i < SpawnPlatform5.Count; i++)
        {
            if (!SpawnPlatform5[i].activeInHierarchy)
            {
                return SpawnPlatform5[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform5);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform5.Add(obj);
        return obj;
    }

    //Platform6
    public GameObject GetPlatfrom6()
    {
        for (int i = 0; i < SpawnPlatform6.Count; i++)
        {
            if (!SpawnPlatform6[i].activeInHierarchy)
            {
                return SpawnPlatform6[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform6);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform6.Add(obj);
        return obj;
    }

    //Platform7
    public GameObject GetPlatfrom7()
    {
        for (int i = 0; i < SpawnPlatform7.Count; i++)
        {
            if (!SpawnPlatform7[i].activeInHierarchy)
            {
                return SpawnPlatform7[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform7);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform7.Add(obj);
        return obj;
    }

    //Platform8
    public GameObject GetPlatfrom8()
    {
        for (int i = 0; i < SpawnPlatform8.Count; i++)
        {
            if (!SpawnPlatform8[i].activeInHierarchy)
            {
                return SpawnPlatform8[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform8);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform8.Add(obj);
        return obj;
    }

    //Platform9
    public GameObject GetPlatfrom9()
    {
        for (int i = 0; i < SpawnPlatform9.Count; i++)
        {
            if (!SpawnPlatform9[i].activeInHierarchy)
            {
                return SpawnPlatform9[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform9);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform9.Add(obj);
        return obj;
    }

    //Platform10
    public GameObject GetPlatfrom10()
    {
        for (int i = 0; i < SpawnPlatform10.Count; i++)
        {
            if (!SpawnPlatform10[i].activeInHierarchy)
            {
                return SpawnPlatform10[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform10);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform10.Add(obj);
        return obj;
    }

}