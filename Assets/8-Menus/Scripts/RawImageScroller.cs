using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(RawImage))]
    //Class - Scroll the background texture with given speed
    public class RawImageScroller : MonoBehaviour
    {
        [SerializeField] RawImage _image = null;

        [SerializeField] bool _scrollHorizontally = true;
        [SerializeField] [Range(-1f, 1f)] float _horizontalSpeed = 0.2f;

        [SerializeField] bool _scrollVertically = true;
        [SerializeField] [Range(-1f, 1f)] float _verticalSpeed = -0.2f;

        void Update()
        {
            var horizontalOffset = _image.uvRect.x;
            var verticalOffset = _image.uvRect.y;

            horizontalOffset += _scrollHorizontally ? _horizontalSpeed * Time.deltaTime: 0;
            verticalOffset += _scrollVertically ? _verticalSpeed * Time.deltaTime : 0;

            horizontalOffset %= 1;
            verticalOffset %= 1;
            
            //Updates the offset value of the texture
            _image.uvRect = new Rect(horizontalOffset,verticalOffset ,
                                    _image.uvRect.width, _image.uvRect.height);
        }
        
    }
}
