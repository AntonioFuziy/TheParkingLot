using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;
    private float speed = 4f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    private Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;

    public bool isSprinting = false;
    public bool isMoving = false;
    private float sprintingMultiplier = 1.5f;

    private float baseStepSpeed = 0.5f;
    private float sprintStepMultiplier = 0.6f;
    private AudioSource footStepAudioSource;
    public AudioClip[] footStepClips;
    private float footStepTimer = 0;
    private float GetCurrentOffset => isSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;
    
    private float walkBobSpeed = 14f;
    private float walkBobAmount = 0.05f;
    private float sprintBobSpeed = 18f;
    private float sprintBobAmount = 0.1f;
    private float defaultYPos = 0f;
    private float headBobTimer = 0f;

    public GameObject playerCamera;

    private Vector3 interactRayPoint = new Vector3 (0.5f, 0.5f, 0f);
    private float interactionDistance = 2f;
    public LayerMask interactionLayer;
    private Interactable currentInteractable;

    public Image crossHair;

    public bool isDead;

    public bool noMovement;
    void Awake(){
        footStepAudioSource = this.GetComponent<AudioSource>();
        defaultYPos = playerCamera.transform.localPosition.y;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(isGrounded && !noMovement){
            HandleFootSteps();
            HandleHeadBob();
        }

        HandleInteractCheck();
        HandleInteractInput();

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        if(Mathf.Abs(x) != 0 || Mathf.Abs(z) != 0){
            isMoving = true;
        }
        else{
            isMoving = false;
        }

        if(Input.GetKey(KeyCode.LeftShift)){
            isSprinting = true;
        }
        else{
            isSprinting = false;
        }

        if(isSprinting){
            move *= sprintingMultiplier;
        }

        if(noMovement){

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        else{
            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void HandleFootSteps(){
        if(!isMoving || !isGrounded) return;

        footStepTimer -= Time.deltaTime;

        if(footStepTimer <= 0){
            footStepAudioSource.PlayOneShot(footStepClips[Random.Range(0, footStepClips.Length - 1)]);
            footStepTimer = GetCurrentOffset;
        }
    }

    private void HandleHeadBob(){
        if(!isMoving || !isGrounded) return;

        headBobTimer += Time.deltaTime * (isSprinting ? sprintBobSpeed : walkBobSpeed);
        playerCamera.transform.localPosition = new Vector3 (
            playerCamera.transform.localPosition.x,
            defaultYPos + Mathf.Sin(headBobTimer) * (isSprinting ? sprintBobAmount : walkBobAmount),
            playerCamera.transform.localPosition.z
        );
        
    }

    private void HandleInteractCheck(){

        if(currentInteractable != null){
            crossHair.color = Color.red;
        }
        else{
            crossHair.color = Color.white;
        }

        if(Physics.Raycast(playerCamera.GetComponent<Camera>().ViewportPointToRay(interactRayPoint), out RaycastHit hit, interactionDistance)){
            if(hit.collider.gameObject.layer == 7 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID())){
                hit.collider.TryGetComponent(out currentInteractable);

                if(currentInteractable){
                    currentInteractable.OnFocus();
                }
            }
        }
        else if(currentInteractable){
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractInput(){
        if(Input.GetKeyDown(KeyCode.Mouse0) && currentInteractable != null && Physics.Raycast(playerCamera.GetComponent<Camera>().ViewportPointToRay(interactRayPoint), 
        out RaycastHit hit , interactionDistance , interactionLayer)){
            currentInteractable.OnInteract();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Death"){
            isDead = true;
        }
    }
}
