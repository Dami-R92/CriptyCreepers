using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float h;
    float v;
    Vector3 moveDirection;
    public float speed = 3;
    [SerializeField] Transform aim;
    [SerializeField] Camera camera;

    Vector2 facingDirection;

    [SerializeField] Transform bulletprefab;

    bool gunLoaded = true;

    [SerializeField] float fireRate = 1;

    [SerializeField] int playerHealt = 10;
    bool powerShotEnabled;
    [SerializeField] bool invulnerable;
    [SerializeField] float invulnerableTime = 3;

    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;

    public int Healt {
        get => playerHealt;
        set {
            playerHealt = value;
            UIManager.Instance.UpdateUIHealth(playerHealt);
        }
    }

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
        PlayerAnimationController();

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

            Transform bulletClone = Instantiate(bulletprefab, transform.position, targetRotation);
            if (powerShotEnabled) {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }

            StartCoroutine(ReloadTime());
        }

    }

    void PlayerAnimationController() {
        anim.SetFloat("Speed",moveDirection.magnitude);

        if(aim.position.x > transform.position.x) {
            spriteRenderer.flipX = false;

        }else if (aim.position.x< transform.position.x) {
            spriteRenderer.flipX = true;

        }

    }

    IEnumerator ReloadTime(){
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }


    public void TakeDamage()
    {
        if(invulnerable) return;
        Healt--;
        invulnerable = true;
        StartCoroutine(MakeVulnerableAgain());
        
    }
    IEnumerator MakeVulnerableAgain() {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    void PlayerStatus() {
        if(playerHealt <=0) {
            GameManager.Instance.gameOver = true;
            UIManager.Instance.ShowGameOverScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("PowerUp")) {
            switch(collision.GetComponent<PowerUp>().powerUpType){
                case PowerUp.PowerUpType.FireRateIncrease:
                fireRate++;
                break;
                case PowerUp.PowerUpType.PowerShot:
                powerShotEnabled = true;
                break;

            }
            Destroy(collision.gameObject,0.1f);
        }
        
    }

}
