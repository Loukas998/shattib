﻿using AutoMapper;
using Template.Application.Products.Commands.CreateProductCommand;
using Template.Domain.Entities.Products;
using Template.Domain.Repositories;

namespace Template.Application.Products.Dtos;

public class ImagesResolver(IFileService fileService)
    : IValueResolver<CreateProductCommand, Product, List<ProductImages>>
{
    public List<ProductImages> Resolve(CreateProductCommand source, Product destination, List<ProductImages> destMember,
        ResolutionContext context)
    {
        List<ProductImages> productImages = [];

        var paths = fileService.SaveFiles(source.Images, "Images/Products", [".jpg", ".png", ".jpeg", ".webg", ".JPG", ".PNG", ".jfif", ".pdf"]);
        if (paths != null) productImages.AddRange(paths.Select(path => new ProductImages { ImagePath = path }));
        return productImages;
    }
}