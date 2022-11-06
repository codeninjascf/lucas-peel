using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float bulletSpeed=20f;
    public GameObject crosshair;
    public GameObject bulletStart;
    public GameObject bulletPrefab;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=false;
        _camera=GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos=_camera.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position=new Vector3(mousePos.x, mousePos.y,0);
        if(Input.GetButtonDown("Fire1")&&!GameManager.GameOver)
        {
            Vector3 toCrosshair=crosshair.transform.position-bulletStart.transform.position;
            float rotation=Mathf.Atan2(toCrosshair.y,toCrosshair.x);
            bulletStart.transform.rotation=Quaternion.Euler(0,0,rotation*Mathf.Rad2Deg);
            GameObject projectile=Instantiate(bulletPrefab,bulletStart.transform.position,bulletStart.transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity=bulletSpeed*toCrosshair.normalized;
        }
    }
}
