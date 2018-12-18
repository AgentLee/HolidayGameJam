using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ProjectSanta.Controllers;

namespace ProjectSanta.Views
{
    internal class InitializeView : MonoBehaviour
    {
        public Transform player;
        public Transform house;
        public Transform sack, list;

        public MenuController menuController;
        public Transform endGame;

        InitializeController initializeController;

        internal bool pause;
        bool RunGame;
        float timer;
        Text timerText;

        // Start is called before the first frame update
        void Start()
        {
            initializeController = new InitializeController(player, house, list, sack);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            timerText = GameObject.Find("Timer").GetComponent<Text>();
            timer = 179;

            RunGame = true;

            crosshair = Resources.Load<Texture2D>("UI/Retical");
            //crosshair.Resize(crosshair.width * 20, crosshair.height * 20);
            //crosshair.Apply();

            endGame.Find("Button").GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene("Menu"); });
            endGame.gameObject.SetActive(false);

            References.initializeView = this;
        }

        //https://answers.unity.com/questions/201103/c-crosshair.html
        Texture2D crosshair;
        private void OnGUI()
        {
            float xMin = (Screen.width / 2f) - (crosshair.width / 2);
            float yMin = (Screen.height / 2f) - (crosshair.height / 2);
            //GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width, crosshair.height), crosshair);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                pause = !pause;
                ShowMenu();
            }

            UpdateTimer();
        }

        internal void ShowMenu()
        {
            menuController.gameObject.SetActive(pause);

            if (pause)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        //https://answers.unity.com/questions/514378/timer-in-milliseconds-to-mmssms.html
        void UpdateTimer()
        {
            int min = 0;
            int sec = 0;

            if(!pause)
            {
                timer -= Time.deltaTime;
            }
            if(timer >= 0f)
            {
                min = Mathf.FloorToInt(timer / 60);
                sec = Mathf.CeilToInt(timer % 60);

                string secs = (sec < 10) ? "0" : "";
                secs += sec.ToString("#");

                timerText.text = min.ToString() + ":" + secs;
            }
            else
            {
                RunGame = false;
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (RunGame && !pause)
                initializeController.Update();
            else if(timer <= 0f && !RunGame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                timerText.text = "00:00";
                endGame.Find("PlayerScore").GetComponent<Text>().text = References.playerController.Score.ToString();
                endGame.gameObject.SetActive(true);
            }
        }

        public void StartCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
