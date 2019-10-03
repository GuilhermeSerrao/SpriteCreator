using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    public Texture[,] grid;
    public Transform start;

    public GameObject panel;
    public RawImage defaultImage;

    private string path;
    private string[] files;

    public int columns;
    public int rows;

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

        print(files.Length);

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (fileCounter < files.Length)
                {

                    WWW www = new WWW("file:///" + files[fileCounter]);
                    tempTexture = www.texture;

                    grid[i, j] = tempTexture;
                    

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
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var image = Instantiate(defaultImage, transform.position, transform.rotation);
                image.transform.parent = panel.transform;
                
                image.texture = grid[i,j];
                //.transform.position = new Vector2(start.position.x * (i * image.texture.width), start.position.y * (j * image.texture.height));
            }
        }
    }

}

