using UnityEngine;

public class GameManagerJSON : MonoBehaviour
{
    public SaveDataJSON SaveDataJSON;
    public PurchasedObject PurchasedObjectsJSON;

    private void Start()
    {
        SaveDataJSON = GetComponent<SaveDataJSON>();
        LoadDataGame();
    }

    [ContextMenu("Save Data")]
    public void SaveDataGame()
    {
        SaveDataJSON = GetComponent<SaveDataJSON>();

        SaveDataJSON.SaveDataObjects(PurchasedObjectsJSON);
    }

    [ContextMenu("Load Data")]
    public void LoadDataGame()
    {
        SaveDataJSON = GetComponent<SaveDataJSON>();

        PurchasedObjectsJSON = SaveDataJSON.LoadDataObjects<PurchasedObject>();

        if (PurchasedObjectsJSON == null)
            PurchasedObjectsJSON = new PurchasedObject();
    }
}
