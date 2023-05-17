using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Striker : MonoBehaviour
{
    public static Striker instance;
    [SerializeField] Transform computerSide;
    [SerializeField] Slider slider;
    [SerializeField] float maxForce = 200f;
    [SerializeField] LineRenderer helpingArrow;
    
    Rigidbody2D rb;
    Vector3 mousePos;
    Vector3 modMousePos;
    Vector3 direction;
    Vector3 startPos;

    public bool strikeAgain = false;

    bool shooting = false;
    bool playerSide = true;
    bool positionSet = false;
    bool hasStriked = false;
    public bool prevStrikePlayer = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        modMousePos = new Vector3(-mousePos.x, -mousePos.y, mousePos.z);

        //modMousePos.y = Mathf.Clamp(modMousePos.y, -3.834f, 0.448f);
        //modMousePos.x = Mathf.Clamp(modMousePos.x, -3.755f, 3.6864f);
        if (modMousePos.y > 3.4f)
        {
            modMousePos.y = 3.4f;
        }
        if (modMousePos.y < -3.834f)
        {
            modMousePos.y = -3.834f;
        }
        if (modMousePos.x < -3.39f)
        {
            modMousePos.x = -3.39f;
        }
        if (modMousePos.x > 3.4)
        {
            modMousePos.x = 3.4f;
        }

        if(rb.velocity.magnitude <= 0.05f && rb.velocity.magnitude != 0.0f && !shooting )
        {
            if (strikeAgain)
            {
                if (playerSide)
                {
                    SwapToComputer();
                }
                else
                {
                    ResetStriker();
                }
                return;
            }
            if (playerSide)
                ResetStriker();
            else
                SwapToComputer();
        }

        if(!hasStriked && !positionSet)
        {
            SetStrikerPosition();
        }

        if (Input.GetMouseButtonUp(0) && rb.velocity.magnitude <= 0.1f && positionSet)
        {
            ShootStriker();
        }

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (!positionSet)
                {
                    positionSet = true;
                }
            }
        }

        if (positionSet && rb.velocity.magnitude == 0)
        {
            helpingArrow.SetPosition(1, new Vector3(modMousePos.x , modMousePos.y, 0));
        }
    }

    public void SetStrikerPosition()
    {
        Vector3 pos = transform.position; 
        pos.x = slider.value;
        pos.y = startPos.y;
        transform.position= pos;
    }

    private void ShootStriker()
    {
        helpingArrow.enabled = false;

        float x = 0;
        if (rb.velocity.magnitude == 0f)
        {
            x = Vector2.Distance(transform.position, mousePos);
            x = (x / 2.5f);
            x = Mathf.Clamp01(x);
        }

        direction = modMousePos;
        rb.AddForce(direction * x * maxForce);
        hasStriked = true;
        prevStrikePlayer = true;
        playerSide = false;
    }
    
    private void ShootStriker(Vector2 direction, float force)
    {
        helpingArrow.enabled = false;
        rb.AddForce(direction * force * maxForce);
        hasStriked = true;
        prevStrikePlayer = false;
        playerSide = true;
    }

    private void SwapToComputer()
    {
        strikeAgain = false;
        playerSide = false;
        shooting = true;
        rb.velocity = Vector3.zero;
        transform.position = computerSide.transform.position;
        Vector3 pos = transform.position;
        StartCoroutine(ComputerShoot(pos));
    }

    IEnumerator ComputerShoot(Vector3 pos)
    {
        Vector2 direction = Vector2.zero;
        float force = 0f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5.0f);
        foreach(Collider2D collider in colliders)
        {
            if (collider.tag == "Black")
            {
                pos.x = -collider.transform.position.x;
                pos.x = Mathf.Clamp(pos.x, -2.032f, 1.968f);
                transform.position = pos;
                direction = (collider.transform.position - transform.position).normalized;
                force = Vector2.Distance(transform.position, collider.transform.position);
                break;
            }
        }
        force = Mathf.Clamp(force, -2, 2);
        yield return new WaitForSeconds(1f);
        ShootStriker(direction, force);
        shooting = false;
        playerSide = true;
    }

    private void ResetStriker()
    {
        strikeAgain = false;
        playerSide = true;
        helpingArrow.enabled = true;
        rb.velocity = Vector3.zero;
        SetStrikerPosition();
        hasStriked = false;
        positionSet = false;
    }
}
