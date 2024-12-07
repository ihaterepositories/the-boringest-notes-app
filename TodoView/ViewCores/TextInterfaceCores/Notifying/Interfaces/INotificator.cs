namespace TodoView.ViewCores.TextInterfaceCores.Notifying.Interfaces;

public interface INotificator
{
    public void Notify(string message);
    public void NotifyWarning(string message);
    public void NotifyError(string message);
}