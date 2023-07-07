using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScrollingBackgroundController : MonoBehaviour
    {
        public Text DebugText;

        //prrivate float ScrollSpeed = -0.5f;
        private Vector2 _savedOffset;
        private Renderer _renderer;
        private World _world;
        private float _ratio;

        private void Start()
        {
            
        }

        private void FixedUpdate()
        {
            var worldSpeed = -_world.BaseSpeed.x * _ratio;
            var worldTime = worldSpeed * Time.fixedTime;

            var repeat = Mathf.Repeat(worldTime, 1);
            var offset = new Vector2(repeat, _savedOffset.y);

            DebugText.text = $"World Speed:{worldSpeed.ToString()}\n" +
                             $"World Time:{worldTime.ToString()}\n" +
                             $"Repeat:{repeat.ToString()}\n";

            _renderer.material.mainTextureOffset = offset;
        }

        

        private void OnDisable()
        {
            _renderer.material.mainTextureOffset = _savedOffset;
        }

        public void Initialize(World world, float ratio)
        {
            _ratio = ratio;

            _world = world;
            _renderer = GetComponent<Renderer>();
            _savedOffset = _renderer.material.mainTextureOffset;
        }
    }
}
