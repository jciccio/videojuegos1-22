using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/Player")]
public class PlayerScriptableObject : ScriptableObject {
    public int health = 100;
    public float speed = 5f;
    public int maxHealth = 100;

    private void Awake () => Debug.Log("Awake");

    private void OnEnable() => Debug.Log("On Enable");

    private void OnDisable () => Debug.Log("On Disable");

    private void OnDestroy() => Debug.Log("On Destroy");

    private void OnValidate () => Debug.Log("On Validate");

}