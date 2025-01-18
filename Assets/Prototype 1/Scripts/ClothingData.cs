using UnityEngine;

[CreateAssetMenu(fileName = "ClothingData", menuName = "KacamataBudaya/ClothingData")]
public class ClothingData : ScriptableObject
{
    public string clothingName;
    public string regionOrigin;
    public string description;
    public Sprite clothingSprite;
    public Gender gender; // Enum: Male/Female
}

public enum Gender
{
    Male,
    Female
}
