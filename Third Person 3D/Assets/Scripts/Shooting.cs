using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{ 
    public GameObject Bullet;
    public Transform SpawnPosition;
    public float BulletForce;
    public Text KillCountUi;
    public int KillCount;

    public Text Bullets;
    public int BulletCount;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))

            {
            Debug.Log("Shoot");

            GameObject BulletClone = Instantiate(Bullet, SpawnPosition.position, SpawnPosition.rotation);

            Rigidbody rb = BulletClone.GetComponent<Rigidbody>();

            rb.AddRelativeForce( Vector3.forward * BulletForce, ForceMode.Impulse);


            BulletCount--;
            Bullets.text = BulletCount.ToString();

            if(BulletCount == 0)
            {
                SceneManager.LoadScene("GameOver");
            }

        }
    }

    public void CountKill()
    {
        KillCount++;
        KillCountUi.text = KillCount.ToString();
    }


}
