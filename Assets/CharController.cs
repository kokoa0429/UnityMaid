using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject gObj;
    private Animator modelAnimation;

    public float speed;

    private int wsH,wsW;
    private float target;

    private float MLeft;
    private float MRight;

    private Vector3 touchWorldPosition;
    private int cooltime;

    void Start()
    {
        gObj = GetComponent<GameObject>();
        modelAnimation = GetComponent<Animator>();
        wsH = Screen.height;
        wsW = Screen.width;
        this.target = transform.position.x;
        Camera gameCamera = Camera.main;
        Vector3 zeroPosition = gameCamera.ScreenToWorldPoint(Vector3.zero);
        MLeft = zeroPosition.x;
        Vector3 maxPosition = gameCamera.ScreenToWorldPoint(new Vector3(wsW,0f,0f));
        MRight = maxPosition.x;
        cooltime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 touchScreenPosition = Input.mousePosition;
        touchScreenPosition.z = 5.0f;

        Camera gameCamera = Camera.main;
        touchWorldPosition = gameCamera.ScreenToWorldPoint(touchScreenPosition);

        if (Mathf.Abs(touchWorldPosition.x - transform.position.x) < 0.1f && touchScreenPosition.y / wsH < 0.333f && cooltime == 0)
        {

            Debug.Log((int)(Random.value * 2) % 2);
            if ((int)(Random.value * 2) % 2 == 0)
            {
                float canMove = Mathf.Abs(MRight - transform.position.x);
                target = transform.position.x + canMove * Random.Range(0.3f,1f);
            }
            else
            {
                float canMove = Mathf.Abs(MLeft - transform.position.x);
                target = transform.position.x - canMove * Random.Range(0.3f,1f);
            }

            cooltime = 10;
        }
        if (cooltime > 0 ) cooltime--;

        if (Mathf.Abs(transform.position.x - target) <= speed)
        {
            transform.position = new Vector3(target, transform.position.y, transform.position.z);
            modelAnimation.SetBool("isMove", false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            if(target - transform.position.x > 0)
            { // move right
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            { // move left
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }

            modelAnimation.SetBool("isMove", true);
        }
    }

    void move(int target)
    {
        this.target = target;
    }
}
