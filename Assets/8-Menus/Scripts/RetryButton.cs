
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class RetryButton : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField] string _sceneName;
    
    void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wasUpdatedThisFrame )
            {
                Restart();
            }
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.wasUpdatedThisFrame )
            {
                Restart();
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
