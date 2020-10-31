namespace Framework.Application
{
    public interface ICommandBus
    {
        void Dispatch<T>(T command);
    }
}