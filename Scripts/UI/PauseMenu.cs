using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DX
{
    public class PauseMenu : MonoBehaviour
    {
        Input_Handler Input_Handler;
        Player_Controls inputActions;
        public static bool GameIsPaused = false;
        public bool Esc_Input = false;

        public GameObject MenuPausa;

        private void Start()
        {
            
            Input_Handler = GetComponent<Input_Handler>();
            MenuPausa.SetActive(false);

        }
        void Update()
        {
            Input_Handler.HandlePause();
            if (Input_Handler.Esc_Input)
            {
                Debug.Log("input correcto");
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        private void LateUpdate()
        {
            Input_Handler.Esc_Input = false;
        }


        public void Resume()
        {
            MenuPausa.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            MenuPausa.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void Salir()
        {
            Application.Quit();
        }
    }
}
