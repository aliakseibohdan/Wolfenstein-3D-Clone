using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Transform cam;

    private CharacterController controller;
    private Animator animator;

    private AudioSource source;
    [SerializeField] private AudioClip shootClip;

    private RaycastHit hit;

    private void Start()
    {
        cam = Camera.main.transform;

        source = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        animator = GameObject.FindGameObjectWithTag("Gun").GetComponent<Animator>();
    }

    private void Update()
    {
        bool isAnimationPlaying = animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot");
        if (Input.GetMouseButtonDown(0) && !isAnimationPlaying)
        {
            animator.SetTrigger("Shoot");
            source.PlayOneShot(shootClip);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100.0f))
            {
                if (hit.collider.CompareTag("Enemy"))
                    hit.collider.GetComponent<Enemy>().Kill();
            }
        }

        Vector3 moveDirection = transform.right * Input.GetAxisRaw("Horizontal") 
                                + transform.forward * Input.GetAxisRaw("Vertical");

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void Kill()
    {
        SceneManager.LoadScene(0);
    }
}