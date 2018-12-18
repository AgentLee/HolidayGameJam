using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProjectSanta.Controllers
{

    public class MenuController : MonoBehaviour
    {
        public Button start, controls, exit, back;
        public Transform menuPanel, controlsPanel;
        bool showMenu;

        // Start is called before the first frame update
        void Start()
        {
            start.onClick.AddListener(delegate { StartGame(); });
            controls.onClick.AddListener(delegate { ShowControls(); });
            back.onClick.AddListener(delegate { ShowControls(); });

//#if !UNITY_WEBGL
//            exit.onClick.AddListener(delegate { ExitGame(); });
//#else
            if (SceneManager.GetActiveScene().name == "Menu")
                exit.gameObject.SetActive(false);
            else
                exit.onClick.AddListener(delegate { ExitGame(); });
//#endif
            showMenu = true;
        }

        void StartGame()
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                SceneManager.LoadScene("Main");
            }
            else
            {
                References.initializeView.pause = false;
                References.initializeView.ShowMenu();
            }
        }

        void ShowControls()
        {
            showMenu = !showMenu;

            menuPanel.gameObject.SetActive(showMenu);
            controlsPanel.gameObject.SetActive(!showMenu);
        }

        void ExitGame()
        {
            if (SceneManager.GetActiveScene().ToString() == "Menu")
            {
                //SceneManager.LoadScene("Update 3");
                Debug.Log("EXIT GAME");
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
