using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class striker : MonoBehaviour
{
    Rigidbody2D Rigidbody;
    Transform Selftrans;
    Vector2 startPos, direction;
    Vector3 mousePos, mousePos2;
    public bool hasStriker, positionSet, Barcollbool = true;
    Transform arrowtransform;
    CircleCollider2D circleCollider;

    public Slider myslider;
    public LineRenderer LineRenderer;
    public GameObject arrowdir, circle;
    public bool circlebool;
   

    public static striker instance;
    public CoinCollect[] holes;

    public Radial_Timer RT;
   
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Selftrans = transform;
        startPos = transform.position;
        arrowtransform = arrowdir.transform;
        circleCollider = GetComponent<CircleCollider2D>();

    }


    void Update()
    {

        LineRenderer.enabled = false;
        arrowdir.SetActive(false);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 inverseMousePos = new Vector3(Screen.width - Input.mousePosition.x, Screen.height - Input.mousePosition.y, Input.mousePosition.z);
        mousePos2 = Camera.main.ScreenToWorldPoint(inverseMousePos);
        mousePos2.y = mousePos2.y - 3;
        linelimit();
        if (Selftrans.position.x != 0)
        {
            mousePos2.x = mousePos2.x + (Selftrans.position.x * 2);
        }

        if (!hasStriker && !positionSet)
        {
            circleCollider.isTrigger = true;
            Selftrans.position = new Vector2(myslider.value, startPos.y);
        }
        if (Input.GetMouseButtonUp(0) && Rigidbody.velocity.magnitude == 0 && positionSet)
        {
            ShootStriker();

        }
        if (circlebool == false)
        {
            circle.SetActive(true);
        }




        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0) && Barcollbool)
            {
                if (!positionSet)
                {
                    positionSet = true;
                    circleCollider.isTrigger = false;
                }
            }
        }
        if (positionSet && Rigidbody.velocity.magnitude == 0)
        {

            circlebool = true;
            circle.SetActive(false);
            arrowdir.SetActive(true);
            LineRenderer.enabled = true;
            LineRenderer.SetPosition(0, Selftrans.position);
            LineRenderer.SetPosition(1, mousePos2);
            float angle = AngleBetweenTwoPoints(arrowtransform.position, mousePos2);
            arrowtransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        }
        if (Rigidbody.velocity.magnitude < 0.1f && Rigidbody.velocity.magnitude != 0)
        {
            strikerReset();
            Manager.instance.count++;
         
        }


    }
    public void ShootStriker()
    {

        float x = 0;
        if (positionSet && Rigidbody.velocity.magnitude == 0)
        {
            x = Vector2.Distance(transform.position, mousePos);
        }
        direction = (Vector2)(mousePos2 - transform.position);
        direction.Normalize();
        Rigidbody.AddForce(direction * x * 200);
        hasStriker = true;

       
       
    }
    public void strikerReset()
    {
        if (RT != null)
        {
            RT.timer = 0;
            RT.slider.value = 0;
        }

        Rigidbody.velocity = Vector2.zero;
        Selftrans.position = Vector2.zero;
        hasStriker = false;
        positionSet = false;
        LineRenderer.enabled = true;
        circlebool = false;
        circle.SetActive(true);
        foreach (CoinCollect hole in holes)
        {
            if (hole.checkbool == true)
            {
                hole.GetComponent<CoinCollect>().Backupcoinbool = true;
            }

        }
    }

    public void linelimit()
    {
        if (mousePos2.x > 4.18f)
        {
            mousePos2.x = 4.18f;
        }
        if (mousePos2.x < -3.904)
        {
            mousePos2.x = -3.904f;
        }
        if (mousePos2.y > 3.94)
        {
            mousePos2.y = 3.94f;
        }
        if (mousePos2.y < -4.17)
        {
            mousePos2.y = -4.17f;
        }

    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            Barcollbool = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            Barcollbool = true;

        }

    }
   
}

