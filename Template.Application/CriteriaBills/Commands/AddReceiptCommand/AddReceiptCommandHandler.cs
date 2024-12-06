using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.CriteriaBills.Dtos;
using Template.Application.Users;
using Template.Domain.Entities.Criterias;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.CriteriaBills.Commands.AddReceiptCommand;

public class AddReceiptCommandHandler(
    ILogger<AddReceiptCommand> logger,
    IMapper mapper,
    ICriteriaBillsRepository criteriaBillsRepository,
    IFileService fileService, IUserContext userContext) : IRequestHandler<AddReceiptCommand, CriteriaBillDto>
{
    public async Task<CriteriaBillDto> Handle(AddReceiptCommand request, CancellationToken cancellationToken)
    {
        var bill = await criteriaBillsRepository.GetBillByIdAsync(request.Id);

        if(bill == null)
        {
            throw new NotFoundException(nameof(CriteriaBill), request.Id.ToString());
        }

		var currentUser = userContext.GetCurrentUser();
		if (currentUser == null) throw new UnauthorizedException("You are unauthorized.. login again (no userId)");
		var userId = currentUser.Id;

		if (bill.Criteria.UserId != userId)
        {
            throw new UnauthorizedException("The resource isn't yours");
        }
        logger.LogInformation("Add receipt to bill with Id: {CriteriaBillId}", request.Id);
        var imagePath = await fileService.SaveFileAsync(request.Receipt, "Images/Criteria/Receipt", [".jpg", ".png", ".pdf"]);
        var updatedBill = await criteriaBillsRepository.CreateReceiptAsync(request.Id, imagePath);
        return mapper.Map<CriteriaBillDto>(updatedBill);
    }
}