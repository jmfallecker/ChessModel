namespace StandardChess.Model.MovementModel
{
    /// <summary>
    /// Should only be used to Capture another piece
    /// </summary>
    public class Capture : Move
    {
        public Capture()
        {
            IsCapture = true;
        }
    }
}
