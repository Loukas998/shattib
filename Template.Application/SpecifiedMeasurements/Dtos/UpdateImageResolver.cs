using AutoMapper;
using Template.Application.SpecifiedMeasurements.Commands.CreateSpecifiedMeasurements;
using Template.Application.SpecifiedMeasurements.Commands.UpdateSpecifiedMeasurements;
using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;

namespace Template.Application.SpecifiedMeasurements.Dtos
{
	public class UpdateImageResolver(IFileService fileService) : IValueResolver<UpdateSpecifiedMeasurementsCommand, SpecifiedMeasurement, string>
	{
		public string Resolve(UpdateSpecifiedMeasurementsCommand source, SpecifiedMeasurement destination, string destMember, ResolutionContext context)
		{
			return source.Image == null ?
					null :
					fileService.SaveFile(source.Image!, "Images/SpecifiedMeasurement/", [".jpg", ".png"]);
		}
	}
}
