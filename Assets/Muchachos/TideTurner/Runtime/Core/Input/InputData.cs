namespace Muchachos.TideTurner.Runtime.Core.Input
{
    public class InputData
    {
        public float HorizontalInput;
        public float VerticalInput;
        public bool IsPause;

        public void Reset()
        {
            HorizontalInput = 0;
            VerticalInput = 0;
            IsPause = false;
        }
    }
}
