using MKDir;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Player player;
    public Player Player => player;
}
