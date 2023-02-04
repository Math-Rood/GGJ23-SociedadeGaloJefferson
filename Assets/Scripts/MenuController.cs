using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;


namespace Interface
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private int id_screen;
        [SerializeField] private Slider slider;
        [SerializeField] private GameObject loadingScreen;
        
        public void ExitGame()
        {
            Application.Quit();
        }
        
        public void LoadScene()
        {
            Debug.Log("Vai Começar a Carregar a prôxima cena");
            StartCoroutine(LoadingAsync());
            loadingScreen.SetActive(!loadingScreen.activeSelf);
            
        }

        IEnumerator LoadingAsync()
        {
            yield return new WaitForSeconds(1f);
            AsyncOperation asyncProgress = SceneManager.LoadSceneAsync(id_screen);
            float progressBar = Mathf.Clamp01(asyncProgress.progress);
            Debug.Log("Progresso do Carregamento : " + progressBar);
            
            if (progressBar < 1f)
            {
                Debug.Log("Progresso do Carregamento com if menor que 1 : " + progressBar);
                slider.value = progressBar;
                yield return new WaitForSeconds(10f);
            }
            else if (asyncProgress.isDone)
            {
                Debug.Log("Progresso do Carregamento com if maior que 1 : " + progressBar);
                slider.value = 1f;
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(3f);
        }
    
    }
}
