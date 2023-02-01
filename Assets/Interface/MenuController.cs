using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interface
{
    public class MenuController : MonoBehaviour
    {
        public string sceneName; //Pega o nome da cena a ser carregada em LoadScene()
        
        public void ExitGame() //Fecha o jogo
        {
            Application.Quit();
        }
        
        public void LoadScene() //Carrega uma cena
        {
            SceneManager.LoadScene(sceneName);
        }
    
    }
}
