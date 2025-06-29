using UnityEngine;

public class EndGameLever : Interactable {
    [SerializeField] private GameObject _endGame;

    public override void Interaction() {
        _endGame.SetActive(true);
    }
}
