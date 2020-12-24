using TMPro;
using UnityEngine;

[RequireComponent(typeof(Block))]
public class BlockView : MonoBehaviour
{
    [SerializeField] private TMP_Text view;
    
    private Block blockChecker;

    private void Awake()
    {
        blockChecker = GetComponent<Block>();
    }
    private void OnEnable()
    {
        blockChecker.FillingUpdated += OnFillingUpdated;
    } 
    private void OnDisable()
    {
        blockChecker.FillingUpdated -= OnFillingUpdated;
    }
    private void OnFillingUpdated(int leftToFill)
    {
        view.text = leftToFill.ToString();
    }
}

