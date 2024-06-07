using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Animations;
using System.Collections;
public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] private float characterSpeed = 11f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = -20f; 
    private int lane = 1; // 0 = sol, 1 = orta, 2 = sağ
    private float slidingDistance = 2.5f; // Bir şerit değişimi sırasında karakterin kat edeceği mesafe
    private float accelerationRate = 0.4f;
    private float currentSpeed;
    private bool isSliding = false;
    public float slideDuration = 1.5f;


    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = characterSpeed;
    }

    void Update()
    {
        if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
            return;

        animator.SetBool("isGameStarted", true);
        animator.SetBool("isJumping", false);


        if(currentSpeed < 20)
            currentSpeed += accelerationRate * Time.deltaTime;

        direction.z = currentSpeed;

        if (controller.isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                Jump();
                animator.SetBool("isJumping", true);
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime; 
        }

        if(SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }
        
        if (SwipeManager.swipeRight && lane < 2)
        {
            lane++;
        }
        else if (SwipeManager.swipeLeft && lane > 0)
        {
            lane--;
        }


        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lane == 0)
        {
            targetPosition += Vector3.left * slidingDistance;
        }
        else if (lane == 2)
        {
            targetPosition += Vector3.right * slidingDistance;
        }

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 50 * Time.fixedDeltaTime);

        if (transform.position != targetPosition){
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move (moveDir);
        else
            controller.Move(diff);   
        }

        controller.Move(direction * Time.deltaTime);

}
    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Obstacles"){
            PlayerManager.gameOver = true;
            FindObjectOfType<SoundManager>().PlaySound("GameOver");
        }
        if(other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            PlayerManager.AddCoins(1);
            FindObjectOfType<SoundManager>().PlaySound("PickUpCoin");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        controller.center = new Vector3(0, -0.3f, 0);
        controller.height = 0.5f;

        animator.SetBool ("isSliding", true);

        yield return new WaitForSeconds (1);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;

        animator.SetBool ("isSliding", false);

        isSliding = false;
    }
}