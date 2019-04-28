using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody m_playerRig;
    private Vector2 m_Input;
    [SerializeField]
    private bool m_isFloat = false;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private bool m_isjump = false;
    private bool m_PreviouslyGrounded;
    private bool m_IsGrounded;
    private CapsuleCollider m_Capsule;
    private bool m_Jumping;
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private float airMoveSpeed=0.1f;
    [SerializeField]
    private float FloatVelocity = 4f;
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private bool D4c = false;
    [SerializeField]
    private GameObject OutlineObject;
    [SerializeField]
    private bool worlf = false;
    private float temptime;

    [SerializeField]
    private GameObject blackGround;

    private void Start()
    {
        m_playerRig = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (Time.time - temptime == 5f)
        {
            gameManager.SetIntervalTime();
        }
        if (Input.GetKeyDown(KeyCode.K)&&worlf)
        {
            temptime = Time.time;
            gameManager.SetIntervalTime();
        }
        if (Input.GetKeyDown(KeyCode.J)&&D4c){
            temptime = Time.time;
            gameManager.SetIntervalTime();
            if (OutlineObject != null)
            {
                StartCoroutine(Outline());
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_isFloat = !m_isFloat;
            gameManager.SetIntervalTime();
            if (m_isFloat) blackGround.SetActive(true);
            else blackGround.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !m_isjump)
        {
            m_isjump = true;
        }
    }

    private IEnumerator Outline()
    {
        for (int i = 0; i < OutlineObject.transform.childCount; i++)
        {
            if (OutlineObject.transform.GetChild(i).GetComponent<cakeslice.Outline>() != null)
            {
                OutlineObject.transform.GetChild(i).GetComponent<cakeslice.Outline>().color = 1;
            }
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < OutlineObject.transform.childCount; i++)
        {
            if (OutlineObject.transform.GetChild(i).gameObject.GetComponent<cakeslice.Outline>() != null)
            {
                Debug.Log(1);
                OutlineObject.transform.GetChild(i).GetComponent<cakeslice.Outline>().color = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        float speed;
        GetInput(out speed);
        Vector3 moveDir = m_isFloat?(transform.up*m_Input.y): transform.right * m_Input.x;
        if (moveDir.sqrMagnitude > 1f)
        {
            moveDir.Normalize();
        }
        if (m_isFloat)
        {
            m_playerRig.velocity = Vector3.zero;
            speed = 5f;
            m_playerRig.velocity = transform.right * FloatVelocity;
            m_playerRig.AddForce(moveDir * speed, ForceMode.Impulse);
            return;
        }
        if (moveDir == Vector3.zero&&m_IsGrounded)
        {
            m_playerRig.velocity = Vector3.zero;
        }
        if (!m_IsGrounded)
        {
            moveDir.y = -2f;
        }
        m_playerRig.AddForce(moveDir * speed,ForceMode.Impulse);
        if (m_IsGrounded)
        {
            m_playerRig.drag = 10f;
            if (m_isjump)
            {
                m_playerRig.drag = 0f;
                m_playerRig.velocity = Vector3.zero;
                m_playerRig.AddForce(transform.up*JumpForce, ForceMode.Impulse);
                m_Jumping = true;
            }
        }
        else
        {
            m_playerRig.drag = 0f;
        }
        m_isjump = false;
    }

    void GroundCheck()
    {
        m_PreviouslyGrounded = m_IsGrounded;
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, m_Capsule.radius, Vector3.down, out hit,
                                   ((m_Capsule.height / 2f) - m_Capsule.radius), Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
        }
        if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
        {
            m_Jumping = false;
        }
    }

    void GetInput(out float speed)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        m_Input = new Vector2(horizontal, Vertical);
        if (Input.GetKey(KeyCode.Space))
        {
            m_isjump = true;
        }
        if (!m_isFloat)
        {
            m_playerRig.useGravity = true;
        }
        else
        {
            m_playerRig.useGravity = false;
        }
        speed = m_IsGrounded ? Speed : airMoveSpeed;
    }
}
