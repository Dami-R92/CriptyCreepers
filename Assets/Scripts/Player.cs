using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float h;
    float v;
    Vector3 moveDirection;
    [SerializeField] float speed = 3;
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;

    Vector2 facingDirection;

    [SerializeField] Transform bulletprefab;

    bool gunLoaded = true;

    [SerializeField] float fireRate = 1;

    [SerializeField] float playerHealt = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
        AimController();
        BulletController();
        PlayerStatus();

    }

    void PlayerController() {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        moveDirection.x = h;
        moveDirection.y = v;
        transform.position += moveDirection * Time.deltaTime * speed;
    }

    void AimController() {
        facingDirection = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;
    }

    void BulletController () {
        if (Input.GetMouseButton(0) && gunLoaded){
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x)*Mathf.Rad2Deg;

            //! Esto da la rotacion a la bala
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Instantiate(bulletprefab, transform.position, targetRotation);

            StartCoroutine(ReloadTime());
        }
    }

    IEnumerator ReloadTime(){
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }


    public void TakeDamage()
    {
        playerHealt--;
    }

    void PlayerStatus() {
        if(playerHealt <=0) {
            //TODO: Game Over
            Destroy(gameObject);
        }
    }

}
