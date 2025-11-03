using System.Collections.Generic;

public class CommandInvoker
{
    private Stack<ICommand> history = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        history.Push(command);
    }

    public void Undo()
    {
        if (history.Count == 0) return;
        ICommand cmd = history.Pop();
        cmd.Undo();
    }

    public void Clear() => history.Clear();
}