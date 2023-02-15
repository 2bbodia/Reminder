namespace Application.Common.Interfaces;
public interface IRecurringMessageService
{
    Task CreateRecurringMessage();
    Task GetAllByUserId(long id);
    Task GetById(string id);
}