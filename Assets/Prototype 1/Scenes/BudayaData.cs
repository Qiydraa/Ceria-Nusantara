using UnityEngine;

[CreateAssetMenu(fileName = "BudayaData", menuName = "Scriptable Objects/BudayaData")]
public class BudayaData : ScriptableObject
{
    public string provinsi;
    public string deskripsiBudaya;
    public string pakaianAdat;
    public string makananKhas;
    public Sprite gambarPakaian;
    public Sprite gambarMakanan;
}
