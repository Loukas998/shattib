using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.CriteriaBills.Dtos;
using Template.Application.Users;
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
        var userId = userContext.GetCurrentUser().Id;
        if (bill.Criteria.UserId != userId)
        {
            throw new UnauthorizedAccessException();
        }
        logger.LogInformation("Add receipt to bill with Id: {CriteriaBillId}", request.Id);
        var imagePath = await fileService.SaveFileAsync(request.Receipt, "Images/Criteria/Receipt", [".jpg", ".png"]);
        var updatedBill = await criteriaBillsRepository.CreateReceiptAsync(request.Id, imagePath);
        return mapper.Map<CriteriaBillDto>(updatedBill);
    }
}