using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DX
{
    public class Menu : MonoBehaviour
    {
        public GameObject Controles;
        public GameObject Creditos;

        private void Awake()
        {
            UnloadControls();
            UnloadCreditos();
        }
        public void LoadPlay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public void LoadControls()
        {
            Controles.SetActive(true);
        }
        public void UnloadControls()
        {
            Controles.SetActive(false);
        }
        public void LoadCreditos()
        {
            Creditos.SetActive(true);
        }
        public void UnloadCreditos()
        {
            Creditos.SetActive(false);
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}
