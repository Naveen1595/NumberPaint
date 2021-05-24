using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Paint : MonoBehaviour
{
    [SerializeField] Button tryAgain;
    [SerializeField] Button Exit;
    [SerializeField] int width = 10;
    [SerializeField] int height = 5;
    float cellSize = 3f;
    [SerializeField] Vector3 originPosition = new Vector3(-16, -6);
    [SerializeField] Vector3 statusPosition = new Vector3(-7, -9.5f);
    [SerializeField] Color color = Color.red;
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject congratulation;
    Pattern pattern;
    Pattern numberStatus;
    int currentNumber;
    int updateStatus;
    int[] countNumber;

    
    private void Start()
    {
        congratulation.SetActive(false);
        pattern = new Pattern(width, height, cellSize, originPosition,sprite,color);
        countNumber = pattern.GetCountNumber();
        numberStatus = new Pattern(8, 2, cellSize/2, statusPosition, countNumber);
	    tryAgain.onClick.AddListener(TryAgainFun);
	    Exit.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            currentNumber = pattern.GetCurrentNumber();
            
            if(pattern.GetValue(UtilsClass.GetMouseWorldPosition()) == currentNumber )
            {
                
                
                if(pattern.GetFlagValue(UtilsClass.GetMouseWorldPosition()) == 0)
                {
                    pattern.SetValue(UtilsClass.GetMouseWorldPosition(), sprite);

                    updateStatus = countNumber[currentNumber];
                    
                    numberStatus.SetValueStatusGrid(currentNumber, updateStatus);
                }
            }
            
        }

        if(countNumber[8] == 0)
        {
            congratulation.SetActive(true);
        }

        
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void TryAgainFun()
    {
        SceneManager.LoadScene(0);
    }
}
