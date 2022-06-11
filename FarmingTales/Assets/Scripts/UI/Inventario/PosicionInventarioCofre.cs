using UnityEngine;

public class PosicionInventarioCofre
{
    public string item = "";
    public int cantidad = 0;
    public Sprite sprite;
    
    public PosicionInventarioCofre()
    {
        this.item = "";
        this.cantidad = 0;
        this.sprite = null;
    }

    public PosicionInventarioCofre(string item, int cantidad, Sprite sprite)
    {
        this.item = item;
        this.cantidad = cantidad;
        this.sprite = sprite;
    }
}