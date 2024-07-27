using MediatR;

namespace BankIntegration.Service.CQRSService.PoepleTransaction.Command;

public class DeletePeopleCommand : IRequest<bool>
{
    public int PeopleId;

    public DeletePeopleCommand(int id)
    {
        PeopleId = id;
    }
}