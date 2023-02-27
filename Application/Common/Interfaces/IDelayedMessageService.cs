using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IDelayedMessageService
{
    Task<string> CreateDelayedMessageAsync(string text, long receiverId, DateTime dateToSend);
    Task<IReadOnlyList<DelayedMessageDto>> GetAllByUserIdAsync(long userId);
    Task<bool> CancelDelayedMessageAsync(string jobId);
}