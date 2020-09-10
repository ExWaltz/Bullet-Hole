using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public GameObject player;
    public SpriteRenderer soulMR;
    public Animator playerState;
    private float speed = 5f, normSpeed;
    private Vector3 dirCal;
    Vector3 Bounds = new Vector2(3.5f, 4f);
    Rigidbody2D rb;
    private void Awake()
    {
        normSpeed = speed;
        instance = this;
        player = this.gameObject;
        soulMR.enabled = false;
    }
    void Start()
    {
        speed = normSpeed;
        rb = GetComponent<Rigidbody2D>();
        
        soulMR.enabled = false;
    }
    void Update()
    {
        dirCal.x = ControlInput.GetAxisRaw("UserHorizontal");
        dirCal.y = ControlInput.GetAxisRaw("UserVeritical");
        ifPower();
        SlowDown();
    }
    private void FixedUpdate()
    {
        if (rb == null) GameMaster.ErrorList("Null_Cp");
        rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position + dirCal, speed * Time.fixedDeltaTime));
        AniState();


    }
    private void LateUpdate()
    {
        Vector2 boundPlayer = new Vector2(Mathf.Clamp(transform.position.x, -Bounds.x, Bounds.x), Mathf.Clamp(transform.position.y, -Bounds.y, Bounds.y));
        transform.position = new Vector3(boundPlayer.x, boundPlayer.y, transform.position.z);
    }
    public void Die()
    {
        
        //error check
        if (player == null) GameMaster.ErrorList("Null_Gm");
        SoundManager.PlayerSound("Death");
        player.SetActive(false);

        GameMaster.instance.isDeath = true;
        FireBullets.instance.isRes = true;
    }
    void ifPower()
    {
        if (Input.GetKeyDown("z") || Input.GetKeyDown("k"))
        {
            PowerPool.instance.ActivatePower();
        }
    }
    void AniState()
    {
        Vector2 xDir = dirCal;
        if (xDir != Vector2.zero)
        {
            playerState.SetInteger("State", 1);
        }
        else
        {
            playerState.SetInteger("State", 0);
        }
    }
    private void SlowDown()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed /= 2;
            if (soulMR == null) GameMaster.ErrorList("Null_Cp");
            soulMR.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = normSpeed;
            if (soulMR == null) GameMaster.ErrorList("Null_Cp");
            soulMR.enabled = false;
        }
    }


}
