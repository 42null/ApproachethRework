namespace Approacheth
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "NewUIConfig", menuName = "UI / UIConfig")]
    public class UIConfig : ScriptableObject
    {
        public GameObject uiPrefab;
        public string windowTitle;
        public Sprite windowIcon;
    }

}