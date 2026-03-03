namespace Phonebook.Presentation
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}
