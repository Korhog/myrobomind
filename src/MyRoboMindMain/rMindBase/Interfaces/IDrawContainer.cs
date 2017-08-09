namespace rMind.Draw
{
    public interface IDrawContainer : IDrawElement
    {
        void SetPosition(double x, double y);
        void Translate(Types.Vector2 vector);
    }
}
