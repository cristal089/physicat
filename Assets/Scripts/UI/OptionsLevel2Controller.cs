using UnityEngine;
using UnityEngine.SceneManagement; 

namespace UI
{
    public class OptionsLevel2Controller : MonoBehaviour
    {
        public void WrongOption()
        {
            TimerController timer = FindFirstObjectByType<TimerController>();
            if (timer != null)
                timer.SubtractTime(2f);
            
             Time.timeScale = 1f;
        }

        public void RightOption()
        {
            Time.timeScale = 1f;
        }
    }
}