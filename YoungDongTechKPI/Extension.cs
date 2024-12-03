using System.ComponentModel;
using UnityEngine;

public static class Extension
{
    public class VM
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)//값이 변경되었을 때 이벤트를 발생시키기 위한 용도 (데이터 바인딩)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static string ToTimeString(this float seconds)
    {
        int hours = (int)(seconds / 3600); // 1시간은 3600초
        int minutes = (int)((seconds % 3600) / 60); // 남은 초를 60으로 나누어 분 계산
        return $"{hours:D2}:{minutes:D2}";
    }

    static Canvas canvas;
    static Camera camera;
    public static void SetUIPos_WorldToScreenPos(this RectTransform rectTransform, Vector3 originPos)
    {
        if(canvas == null)
        {
            canvas = rectTransform.GetComponentInParent<Canvas>();
        }
        if(camera == null)
        {
            camera = Camera.main;
        }        

        Vector3 screenPosition = camera.WorldToScreenPoint(originPos);        
        bool isOutsideOfCamera = (screenPosition.z < 0);// ||
                                                        //screenPosition.x < 0 || screenPosition.x > screenSize.x ||
                                                        //screenPosition.y < 0 || screenPosition.y > screenSize.y);

        if (isOutsideOfCamera)
        {
            rectTransform.anchoredPosition = new Vector2(-3000, -3000);
        }
        else
        {            

            Vector2 position = (Vector2)screenPosition / canvas.scaleFactor;// - screenSize * 0.5f;
            rectTransform.anchoredPosition = position;
        }
    }
}