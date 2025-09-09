using UnityEngine;

public class UIInstaller : MonoBehaviour
{
    [SerializeField] private WinPanel winPanel;

    private void Awake()
    {
        Main.SetResultPanel(winPanel);
    }
}