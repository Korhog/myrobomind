namespace rMind.Draw
{
    public interface IDrawElement
    {
        void Init();
        Elements.rMindBaseController GetController();
        Types.Vector2 GetOffset();
    }
}
