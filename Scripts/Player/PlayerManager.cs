using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX
{
    public class PlayerManager : MonoBehaviour
    {
        Input_Handler input_Handler;
        Animator anim;
        CameraHandler cameraHandler;
        Player_Locomotion player_Locomotion;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }
        void Start()
        {
            input_Handler = GetComponent<Input_Handler>();
            anim = GetComponentInChildren<Animator>();
            player_Locomotion = GetComponent<Player_Locomotion>();
        }


        void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = anim.GetBool("isInteracting");
            anim.SetBool("isInAir", isInAir);

            input_Handler.TickInput(delta);
            player_Locomotion.HandleMovement(delta);
            player_Locomotion.HandleRollingAndSprinting(delta);
            player_Locomotion.HandleFalling(delta, player_Locomotion.moveDirection);
            player_Locomotion.HandleJumping();
        }
        

        private void FixedUpdate()
        {
            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, input_Handler.mouseX, input_Handler.mouseY);
            }
        }

        private void LateUpdate()
        {
            input_Handler.rollFlag = false;
            input_Handler.sprintFlag = false;
            input_Handler.rb_Input = false;
            input_Handler.rt_Input = false;
            input_Handler.d_Pad_Up = false;
            input_Handler.d_Pad_Down = false;
            input_Handler.d_Pad_Right = false;
            input_Handler.d_Pad_Left = false;
            input_Handler.jump_Input = false;
            

            if (isInAir)
            {
                player_Locomotion.inAirTimer = player_Locomotion.inAirTimer + Time.deltaTime;
            }
        }
    }
}
