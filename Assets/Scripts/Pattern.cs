using UnityEngine;
using CodeMonkey.Utils;

public class Pattern
{
    int width;
    int height;
    float cellSize;
    Vector3 originPosition;
    int[,] gridArray;
    int[,] flagGridArray;
    TextMesh[,] debugTextArray;
    GameObject[,] SpriteArray;
    Color color = Color.red;
    Sprite sprite;
    int randomNumber;
    int currentNumber = 1;
    int[] countNumber = new int[14];


    //Constructor for Number Status;
    public Pattern(int width, int height, float cellSize, Vector3 originPosition , int[] countNumber)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.countNumber = countNumber;

        gridArray = new int[width, height];
        
        debugTextArray = new TextMesh[width, height];
        SpriteArray = new GameObject[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                
                if(y==0)
                {
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(countNumber[x + 1].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 10, Color.white, TextAnchor.MiddleCenter);
                }
               else
                {
                    
                    debugTextArray[x, y] = UtilsClass.CreateWorldText((x+1).ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 10, Color.white, TextAnchor.MiddleCenter);
                }
                
                
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    //Constructor for Grid
    public Pattern(int width,int height,float cellSize, Vector3 originPosition,Sprite sprite,Color color)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.sprite = sprite;
        this.color = color;

        gridArray = new int[width, height];
        flagGridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];
        SpriteArray = new GameObject[width, height];
        for(int x = 0; x < gridArray.GetLength(0);x++)
        {
            for(int y = 0; y < gridArray.GetLength(1);y++)
            {
                randomNumber = Random.Range(1, 9);
                countNumber[randomNumber] += 1;
                gridArray[x, y] = randomNumber;
                
                debugTextArray[x, y] = UtilsClass.CreateWorldText(randomNumber.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white, 100f);
                SpriteArray[x,y] = UtilsClass.CreateWorldSprite(gridArray[x, y].ToString(), sprite, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, new Vector3(20f,20f),0,Color.red);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
 
    }

    
    
    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //Get Position
    public void GetXY(Vector3 worldPosition,out int x,out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x/ cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }


    
    //Set value inside grid
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }

    }

    public void SetValue(Vector3 worldPosition, Sprite sprite)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            flagGridArray[x, y] = 1;
            debugTextArray[x, y].text = " ";
            SpriteArray[x, y] = UtilsClass.CreateWorldSprite(gridArray[x, y].ToString(), sprite, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, new Vector3(20f, 20f), 0, Color.cyan);
            countNumber[currentNumber] -= 1;
            if(countNumber[currentNumber] == 0)
            {
                currentNumber += 1;
            }
        }
    }

    public void SetValueStatusGrid(int currentNumber,int value)
    {
        debugTextArray[currentNumber-1, 0].text = value.ToString();
    }

    //Get Value of Grid
    public int GetValue(int x, int y)
    {
        if(x >=0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x,y);
    }

    public int GetCurrentNumber()
    {
        return currentNumber;
    }

    public int GetFlagValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetFlagValue(x, y);
    }

    public int GetFlagValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return flagGridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int[] GetCountNumber()
    {
        return countNumber;
    }
    

}
 