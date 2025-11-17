//using UnityEngine;
//using UnityEngine.UI;

//public class DigButton : MonoBehaviour
//{
//    [SerializeField] private MapManager mapManager;
//    [SerializeField] private Image buttonImage;

//    [Header("Colors")]
//    public Color normalColor = Color.white;
//    public Color activeColor = new Color(0.8f, 1f, 0.8f); // xanh nhạt

//    private void Start()
//    {
//        buttonImage.color = normalColor;
//    }

//    public void OnDigButtonPressed()
//    {
//        mapManager.ActivateDigMode();

//        // Đổi màu theo trạng thái digMode
//        if (mapManager.digMode)
//            buttonImage.color = activeColor;
//        else
//            buttonImage.color = normalColor;
//    }
//}
