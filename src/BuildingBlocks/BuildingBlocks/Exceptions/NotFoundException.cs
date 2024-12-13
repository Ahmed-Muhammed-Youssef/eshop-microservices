namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string name, string key): base($"Entity \"{name}\" ({key}) was not found.") { }
    }
}
