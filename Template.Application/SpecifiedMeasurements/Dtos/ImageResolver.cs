using AutoMapper;
using Template.Application.SpecifiedMeasurements.Commands.CreateSpecifiedMeasurements;
using Template.Domain.Entities.Orders;
using Template.Domain.Repositories;

namespace Template.Application.SpecifiedMeasurements.Dtos
{
	public class ImageResolver(IFileService fileService)
	: IValueResolver<CreateSpecifiedMeasurementsCommand, SpecifiedMeasurement, string>
	{
		public string Resolve(CreateSpecifiedMeasurementsCommand source, SpecifiedMeasurement destination, string destMember, ResolutionContext context)
		{
			return source.Image == null ?
					null :
					fileService.SaveFile(source.Image!, "Images/SpecifiedMeasurement/", [".jpg", ".png", ".jpeg", ".webg", ".JPG", ".PNG", ".jfif"]);
		}
	}
}
