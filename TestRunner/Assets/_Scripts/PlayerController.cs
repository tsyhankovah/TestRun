using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]

    private CharacterController characterController;
    private Animator animator;
    private Vector3 direction;
    
    [SerializeField] private float speed = 12;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravity = -20;
    private int lineCurrent = 1;
    private float lineDistance = 3f;

    [Header("UI")]

    [SerializeField] private int score;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text RecordText;
    [SerializeField] private Text ScoreTextFault;
    [SerializeField] private Text RecordTextFault;
    [SerializeField] private GameObject FaultPanel;
    [SerializeField] private GameObject GamePanel;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        direction = Vector3.forward;
        GamePanel.SetActive(true);
        RecordText.text = "Record: " + PlayerPrefs.GetInt("score").ToString();
    }

    void FixedUpdate()
    {
        ForwardMovement();
    }

    private void Update()
    {
        SwipeMovement();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            Fault();
        }

        if (other.gameObject.tag == "Coin")
        {
            ScoreAdd();
        }
    }

    private void ForwardMovement()
    {
        direction.z = speed;
        direction.y += gravity * Time.fixedDeltaTime;

        characterController.Move(direction * Time.fixedDeltaTime);
    }

    private void SwipeMovement()
    {
        if (Input.GetKeyDown("right") && lineCurrent < 2)
        {
            lineCurrent++;
        }
        else if (Input.GetKeyDown("left") && lineCurrent > 0)
        {
            lineCurrent--;
        }
        else if (Input.GetKeyDown("space") && characterController.isGrounded)
        {
            animator.SetTrigger("Jump");
            direction.y = jumpForce;
        }

        if (characterController.isGrounded)
            animator.SetBool("Run", true);
        else
            animator.SetBool("Run", false);

        Vector3 target = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lineCurrent == 0)
        {
            target += Vector3.left * lineDistance;
        }
        else if (lineCurrent == 2)
        {
            target += Vector3.right * lineDistance;
        }

        if (transform.position == target)
            return;

        Vector3 diff = target - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            characterController.Move(moveDir);
        else
            characterController.Move(diff);

    }

    private void ScoreAdd()
    {
        score++;
        ScoreText.text = score.ToString();

        if (score > PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", score);
            RecordText.text = "Record: " + PlayerPrefs.GetInt("score").ToString();
        }
    }

    private void Fault()
    {
        FaultPanel.SetActive(true);
        ScoreTextFault.text = "SCORE: " + score.ToString();
        RecordTextFault.text = "RECORD: " + PlayerPrefs.GetInt("score").ToString();
        GamePanel.SetActive(false);
        Time.timeScale = 0;
        score = 0;
    }



}
