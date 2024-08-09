using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialog/DialogLoader")]
public class DialogLoader : ScriptableObject
{
    public TextAsset _dialogFile;
    public Sprite [] dialogPortraits;

    private DialogNodes dialogNodes;
    public Node[] DialogNodes { get { return dialogNodes.nodes; } }

    public void Load()
    {
        dialogNodes = JsonHelper.Deserialize<DialogNodes>(_dialogFile.ToString());
        LoadPortraits();
    }

    private void LoadPortraits()
    {
        var loadesSprites = Resources.LoadAll("Portraits", typeof(Sprite));
        dialogPortraits = new Sprite[loadesSprites.Length];

        for(int i = 0; i < loadesSprites.Length; i++)
        {
            dialogPortraits[i] = (Sprite) loadesSprites[i];
        }
    }

    public Sprite GetPortrait(string portraitName)
    {
        for(int i = 0; i < dialogPortraits.Length; i++)
        {
            if (dialogPortraits[i].name.Contains(portraitName.ToLower()))
                return dialogPortraits[i];
        }

        return null;
    }

    public string CaptalizeFirstLeter(string name)
    {
        return char.ToUpper(name[0]) + name.Substring(1);
    }
}
