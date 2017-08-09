namespace rMindData.Model.Entities
{
    // Генератор кода
    public enum GenerationResultCode
    {
        Success,
        Error
    }

    public struct GenerationResult
    {
        public string SourceCode;
        public GenerationResultCode Result;
    }

    public interface ICodeGeneratable
    {
        GenerationResult GetSourceCode(int level);
    }
}
