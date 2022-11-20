namespace Game
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Main : MonoSingleton<Main>
    {

        public static int PlayerID;

        public AudioSource CoinSFX;
        public AudioSource SheepSFX;
        public AudioSource BuildSFX;
        public AudioSource DestroySFX;

        protected override void OnAwake()
        {
            DontDestroyOnLoad(this);

            SceneManager.LoadScene(1);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(2);
        }

        public void BreakGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
