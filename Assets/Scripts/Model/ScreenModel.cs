namespace Assets.Scripts.Model
{
    public class ScreenModel
    {
        public float ScreenTop = 5f;
        public float ScreenBottom = -5f;
        public float FloorY = -4.5f;
        public float CellingY = 2.5f; //4.5f;
        public float ScreenLeft = -9f;
        public float ScreenRight = 9f;
        public float ScreenWidth;
        public float ScreenMidpoint;
        public float ScreenDeathX;
        public float ScreenDeathY;

        public ScreenModel()
        {
            ScreenWidth = ScreenRight - ScreenLeft;
            ScreenMidpoint = ScreenLeft + ScreenWidth / 2;
            ScreenDeathX = ScreenLeft - 1f;
            ScreenDeathY = ScreenBottom - 5f;
        }
    }
}