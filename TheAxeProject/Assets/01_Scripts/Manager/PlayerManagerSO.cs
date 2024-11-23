using UnityEngine;

[CreateAssetMenu(fileName = "PlayerManagerSO",menuName = "SO/PlayerManager")]
public class PlayerManagerSO : ScriptableObject
{
    private Player _player;

    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                Debug.Assert(_player != null, "No player found");
            }

            return _player;
        }
        set { _player = value; }
    }

    public Transform PlayerTrm => Player.transform;
}