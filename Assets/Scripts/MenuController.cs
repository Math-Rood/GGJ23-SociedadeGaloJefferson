using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interface
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        
        public void ExitGame()
        {
            Application.Quit();
        }
        
        public void LoadScene()
        {
            SceneManager.LoadScene(sceneName);
        }
    
    }
}
