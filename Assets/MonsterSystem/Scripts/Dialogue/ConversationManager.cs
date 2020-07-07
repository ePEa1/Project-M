using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SetNames
{
    estelle,
    serena,
    space,
    time
}
public class ConversationManager : MonoBehaviour
{
    public Color32 blackColor;
    public Color originalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);


    public float XValue = 40f;
    public float YValue = 30f;


    [SerializeField] Animator[] animators;
    [SerializeField] Image[] charactors;
    [SerializeField] Sprite[] NameBoxSprite;
    public bool isMoving = false;

    public void SetChar(string name, Image box)
    {
        
        switch(name)
        {
            case "estelle":
                    PaintImg(charactors[1], blackColor);
                box.sprite = NameBoxSprite[0];
                break;

            case "serena":
                PaintImg(charactors[0], blackColor);
                box.sprite = NameBoxSprite[1];
                break;

            case "space":
                PaintImg(charactors[1], blackColor);
                box.sprite = NameBoxSprite[2];
                break;

            case "time":
                PaintImg(charactors[0], blackColor);
                box.sprite = NameBoxSprite[3];
                break;
        }
    }
    public bool SetPosition()
    {
        return true;
    }
    private void PaintImg(Image img, Color32 color)
    {
        img.color = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
