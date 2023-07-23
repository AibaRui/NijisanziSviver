using UnityEngine;

[CreateAssetMenu(fileName = "Audio")]
public class AudioDataOnUseInGameAllScritable : ScriptableObject
{
    [Header("レベルアップした時の音")]
    [SerializeField] private AudioClip _levelUp;


}
