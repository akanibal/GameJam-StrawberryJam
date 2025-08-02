using UnityEngine;

public class BrokenIcon : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRend;

    public void setSprite(int index)
    {
        spriteRend.sprite = sprites[index];
    }
}
