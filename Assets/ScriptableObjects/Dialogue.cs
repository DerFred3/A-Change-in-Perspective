using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject {
    [SerializeField] public string[] Entries;
}
