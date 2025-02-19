using UnityEngine;
using UnityEngine.UI;

// 게임이 끝난 경우에도 이 방식으로 호출
namespace Scene.Fishing
{
    public class UIManager : MonoBehaviour
    {
        
        public Button StartButton;
        public Button ExitButton;

        private void Start()
        {
            if(!StartButton && !ExitButton) { Debug.LogError("cannot found start or exit button"); return; }
            
            ExitButton.onClick.AddListener(GameManager.Instance.ExitGame);
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
