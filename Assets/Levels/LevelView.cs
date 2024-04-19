using Picture;
using UnityEngine;

namespace Levels
{
    public class LevelView: MonoBehaviour
    {
        public PictureView[] PictureViews => _pictureViews;
        [SerializeField] private PictureView[] _pictureViews;
    }
}