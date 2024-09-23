using MediatR;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Command;

public record DeletePeopleCommand : IRequest<bool>
{
    public int PeopleId;

    public DeletePeopleCommand(int id)
    {
        PeopleId = id;
    }
}