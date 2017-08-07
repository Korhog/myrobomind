namespace rMind.Draw
{
    public interface IDrawContainer : IDrawElement
    {
        void SetPosition(float x, float y);
        void Translate(Types.Vector2 vector);
    }
}
