using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public UnityAction OnJoinSpyTeamClicked;
    public UnityAction OnJoinMercTeamClicked;

    [SerializeField]
    private Button JoinSpyTeamButton;
    [SerializeField]
    private Button JoinMercTeamButton;

    private void OnEnable()
    {
        JoinSpyTeamButton.onClick.AddListener(OnJoinSpyTeamClicked);
        JoinMercTeamButton.onClick.AddListener(OnJoinMercTeamClicked);
    }

    private void OnDisable()
    {
        JoinSpyTeamButton.onClick.RemoveListener(OnJoinSpyTeamClicked);
        JoinMercTeamButton.onClick.RemoveListener(OnJoinMercTeamClicked);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
