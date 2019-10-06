using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    public string name;

    public float factor;

    public Texture[,] grid;
    public Transform start;

    public GameObject panel;
    public GameObject button;
    public RawImage defaultImage;

    private string path;
    private string[] files;

    public int rows;
    public int columns;

    private int fileCounter = 0;


    public Texture tempTexture;


    public void Start()
    {
        
        grid = new Texture[columns, rows];
    }

    public void ChooseFolder()
    {
        path = EditorUtility.OpenFolderPanel("SelectFolder", "", "*.png");

        files = Directory.GetFiles(path);

        button.SetActive(false);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (fileCounter < files.Length)
                {

                    WWW www = new WWW("file:///" + files[fileCounter]);
                    tempTexture = www.texture;

                    grid[j, i] = tempTexture;
                    

                    print("file" + fileCounter);
                
                    fileCounter++;
                }                
            }
        }

        if (fileCounter < files.Length)
        {
            print("Files missing");
        }

        SpawnGrid();
        
    }

    public void SpawnGrid()
    {
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var image = Instantiate(defaultImage, transform.position, transform.rotation);                 
                
                image.transform.parent = panel.transform;                
                image.texture = grid[j,i];
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(grid[j, i].width / factor, grid[j, i].height / factor);
                print("Width: " + image.texture.width + "\n Height: " + image.texture.height);
                image.transform.position = new Vector2(start.position.x + (j * image.texture.width / factor), start.position.y - (i * image.texture.height / factor));
            }
        }

        print(Mathf.RoundToInt((grid[0, 0].width * rows) / factor) + " " + Mathf.RoundToInt((grid[0, 0].height * columns) / factor));

        FindObjectOfType<ScreenShotHandler>().TakeScreenshot(Screen.width, Screen.height, name);
        
    }

    

}

