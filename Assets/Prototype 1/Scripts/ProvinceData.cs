using UnityEngine;

[CreateAssetMenu(fileName = "ProvinceData", menuName = "KacamataBudaya/ProvinceData")]
public class ProvinceData : ScriptableObject
{
    public string provinceName;
    public Vector2 mapCoordinates; // Lokasi pada peta
    public string description;     // Informasi budaya
    public Sprite provinceSprite;  // Gambar provinsi
}
