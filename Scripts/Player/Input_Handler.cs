using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DX {
    public class Input_Handler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool jump_Input;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool rollFlag;
        public bool sprintFlag;
        public float rollInputTimer;
        public bool Esc_Input=false;


        Player_Controls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
       

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        private void OnEnable()
        {
            if (inputActions==null)
            {
                inputActions = new Player_Controls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotInput();
            HandleJumpInput();
            
        }
        public void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;

        }

        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            if (b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                if (rollInputTimer>0 && rollInputTimer<0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            inputActions.PlayerActions.RT.performed += i => rt_Input = true;
            //mano derecha
            if (rb_Input)
            {
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
            //mano izquierda
            if (rt_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.leftWeapon);
            }
        }

        private void HandleQuickSlotInput()
        {
            inputActions.InventoryActions.DPadRight.performed += i => d_Pad_Right = true;
            inputActions.InventoryActions.DPadLeft.performed += i => d_Pad_Left = true;
            if (d_Pad_Right)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (d_Pad_Left)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }

        private void HandleJumpInput()
        {
            inputActions.PlayerActions.Jump.performed += inputActions => jump_Input = true;
            
        }
        public void HandlePause()
        {
            inputActions.UI.Esc.performed += i => Esc_Input = true;
        }
    }
}
