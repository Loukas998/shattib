﻿namespace Template.Application.Products.Dtos;

public class MiniProductDto
{
    public int Id { get; set; }
	public int CategoryId { get; set; }
	public string Name { get; set; } = default!;
    public float Price { get; set; }
    public string MainImagePath { get; set; } = default!;
    public string SubCategoryName { get; set; } = default!;
    public string WarehouseCode { get; set; } = default!;
}