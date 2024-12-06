using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.Products;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Seeders;

internal class CategoriesSeeder(TemplateDbContext dbContext) : ICategoriesSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
        if (!dbContext.Categories.Any())
        {
            var categories = GetCategories();
            dbContext.Categories.AddRange(categories);
            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.Subcategories.Any())
        {
            var subCategories = GetSubCategories();
            dbContext.Subcategories.AddRange(subCategories);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task SetCategoriesImages()
    {
        for(int i = 1; i <= 16; i++)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == i);
            category!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/categories/category-{i}.jpg";
        }
        await dbContext.SaveChangesAsync();
    }

	public async Task SetSubCategoriesImages()
	{
        int i = 1;
        int j = 1;
		for(i = 1; i <= 5; i++)
        {
            var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
            subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/rkham/rkham0{j}.jpg";
            j++;
        }
        j = 1;

		for (i = 6; i <= 9; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/porsalen/porsalen0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 10; i <= 13; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/siramik/siramik0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 14; i <= 16; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/parkeh/parkeh0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 17; i <= 20; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/nwafez/nwafez0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 21; i <= 21; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/decors/decors01.jpg";
			j++;
		}
		j = 1;

		for (i = 22; i <= 24; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/doors/doors0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 25; i <= 28; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/panels/panels0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 29; i <= 30; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/gypsum/gypsum0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 31; i <= 33; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/stone/stone0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 34; i <= 37; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/paints/paints0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 38; i <= 40; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/insulation/insulation0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 41; i <= 43; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/edoors/edoors0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 44; i <= 60; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/switches/switches0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 61; i <= 72; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/bathroom/bathroom0{j}.jpg";
			j++;
		}
		j = 1;

		for (i = 73; i <= 76; i++)
		{
			var subcategory = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == i);
			subcategory!.ImagePath = $"https://shattibsadev.blob.core.windows.net/static-images/ac/ac0{j}.jpg";
            j++;
		}
		j = 1;

		await dbContext.SaveChangesAsync();
	}

    public async Task FixSubCateogry()
    {
        var subcat = await dbContext.Subcategories.FirstOrDefaultAsync(sb => sb.Id == 21);
        subcat.Name = "الأوراك";
        await dbContext.SaveChangesAsync();

    }

    public async Task AddCategories()
    {
        List<Category> categories =
        [
           new()
           {
                Name = "جرانيت"
           },

		   new()
		   {
				Name = "مقابض"
		   },
		   new()
		   {
				Name = "إنارة داخلية"
		   },
           new()
		   {
				Name = "إنارة خارجية"
		   },
	    ];
        dbContext.Categories.AddRange(categories);
        await dbContext.SaveChangesAsync();
	}

	public async Task AddSubCategories()
	{
		List<SubCategory> subcategories =
		[
		   new()
		   {
				Name = "جرانيت الأرضيات الداخلية",
                CategoryId = 17
		   },
		   new()
		   {
				Name = "جرانيت الأرضيات الخارجية",
				CategoryId = 17
		   },
		   new()
		   {
				Name = "جرانيت الجدران",
				CategoryId = 17
		   },
		   new()
		   {
				Name = "جرانيت الحمامات والمطابخ",
				CategoryId = 17
		   },

		   new()
		   {
				Name = "المقابض الذكية",
				CategoryId = 18
		   },
		   new()
		   {
				Name = "مسكات الأبواب الداخلية",
				CategoryId = 18
		   },
		   new()
		   {
				Name = "مسكات الأبواب الخارجية",
				CategoryId = 18
		   },
		   new()
		   {
				Name = "الكيلونات",
				CategoryId = 18

		   },
		   new()
		   {
				Name = "السلندرات",
				CategoryId = 18
		   },
		   new()
		   {
				Name = "الردادات",
				CategoryId = 18
		   },

		   new()
		   {
				Name = "داون لايت",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "الإنارة المخفية وأشرطة اليد",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "إطارات فريمات ضد التوهج وغاطسة",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "إنارة المسار المغناطيسي",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "الإنارة الخطية",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "قنوات الألومنيوم الليد بروفايل",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "إنارة المسار تراك لايت",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "لمبات اللطش والبانل",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "لمبات موجهة لمبات الفريمات",
				CategoryId = 19
		   },
		   new()
		   {
				Name = "لمبات ليد كروية وثريات",
				CategoryId = 19
		   },

		   new()
		   {
				Name = "إضاءة السور الخارجية",
				CategoryId = 20
		   },
		   new()
		   {
				Name = "الكشافات الخارجية",
				CategoryId = 20
		   },
		   new()
		   {
				Name = "الإضاءات الجدارية والمعلقات",
				CategoryId = 20
		   },
		];
		dbContext.Subcategories.AddRange(subcategories);
		await dbContext.SaveChangesAsync();
	}


	private IEnumerable<SubCategory> GetSubCategories()
    {
        List<SubCategory> subCategories =
        [
            new()
            {
                Name = "رخام الأرضيات الداخلية والخارجية",
                CategoryId = 1
            },
            new()
            {
                Name = "رخام المطابخ والحمامات",
                CategoryId = 1
            },
            new()
            {
                Name = "رخام الجدران",
                CategoryId = 1
            },
            new()
            {
                Name = "رخام الواجهات",
                CategoryId = 1
            },
            new()
            {
                Name = "بديل الرخام",
                CategoryId = 1
            },

            new()
            {
                Name = "بورسلان الأرضيات الداخلية",
                CategoryId = 2
            },
            new()
            {
                Name = "بورسلان الحمامات والمطابخ",
                CategoryId = 2
            },
            new()
            {
                Name = "بورسلان الواجهات",
                CategoryId = 2
            },
            new()
            {
                Name = "بورسلان الأرضيات الخارجية",
                CategoryId = 2
            },

            new()
            {
                Name = "سيراميك الأرضيات الداخلية",
                CategoryId = 3
            },
            new()
            {
                Name = "سيراميك الجدران",
                CategoryId = 3
            },
            new()
            {
                Name = "سيراميك الحمامات والمطابخ",
                CategoryId = 3
            },
            new()
            {
                Name = "سيراميك الأرضيات الخارجية",
                CategoryId = 3
            },

            new()
            {
                Name = "باركيه الأرضيات الداخلية",
                CategoryId = 4
            },
            new()
            {
                Name = "باركيه مقاوم للرطوبة",
                CategoryId = 4
            },
            new()
            {
                Name = "باركيه للأماكن التجارية",
                CategoryId = 4
            },

            new()
            {
                Name = "نوافذ سحاب",
                CategoryId = 5
            },
            new()
            {
                Name = "نوافذ مفصلية",
                CategoryId = 5
            },
            new()
            {
                Name = "نوافذ قلاب",
                CategoryId = 5
            },
            new()
            {
                Name = "الشتر",
                CategoryId = 5
            },

            new()
            {
                Name = "القفاري",
                CategoryId = 6
            },

            new()
            {
                Name = "الأبواب الداخلية",
                CategoryId = 7
            },
            new()
            {
                Name = "الأبواب الخارجية",
                CategoryId = 7
            },
            new()
            {
                Name = "أبواب الحمامات والمطابخ",
                CategoryId = 7
            },

            new()
            {
                Name = "الصفائح الخشبية",
                CategoryId = 8
            },
            new()
            {
                Name = "الصفائح الرخامية",
                CategoryId = 8
            },
            new()
            {
                Name = "الصفائح الطينية",
                CategoryId = 8
            },
            new()
            {
                Name = "الصفائح الحجرية الطبيعية",
                CategoryId = 8
            },

            new()
            {
                Name = "جبس مقاوم للرطوبة",
                CategoryId = 9
            },
            new()
            {
                Name = "جبس للأسقف",
                CategoryId = 9
            },

            new()
            {
                Name = "حجر للأرضيات",
                CategoryId = 10
            },
            new()
            {
                Name = "حجر للجدران",
                CategoryId = 10
            },
            new()
            {
                Name = "حجر للواجهات الخارجية",
                CategoryId = 10
            },

            new()
            {
                Name = "دهانات الجدران الداخلية",
                CategoryId = 11
            },
            new()
            {
                Name = "دهانات الجدران الخارجية",
                CategoryId = 11
            },
            new()
            {
                Name = "الأساسات والمعاجين والمخففات",
                CategoryId = 11
            },
            new()
            {
                Name = "دهانات الأرضيات",
                CategoryId = 11
            },

            new()
            {
                Name = "عوازل حرارية",
                CategoryId = 12
            },
            new()
            {
                Name = "عوازل صوتية",
                CategoryId = 12
            },
            new()
            {
                Name = "عوازل مائية",
                CategoryId = 12
            },

            new()
            {
                Name = "بوابات فتح أمامي",
                CategoryId = 13
            },
            new()
            {
                Name = "بوابات متأرجحة",
                CategoryId = 13
            },
            new()
            {
                Name = "بوابات رفع عمودي",
                CategoryId = 13
            },

			new()
			{
				Name = "مفتاح تسكيره",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح أحادي",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح ثنائي",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح ثلاثي",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح رباعي",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح مكيف",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح سخان",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح ستارة",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح دايمر",
				CategoryId = 14
			},
			new()
			{
				Name = "مفتاح جرس",
				CategoryId = 14
			},
			new()
			{
				Name = "فيش ثلاثي",
				CategoryId = 14
			},
			new()
			{
				Name = "USB, USBc فيش",
				CategoryId = 14
			},
			new()
			{
				Name = "مجرى أفياش",
				CategoryId = 14
			},
			new()
			{
				Name = "أفياش للمجرى",
				CategoryId = 14
			},
			new()
			{
				Name = "فيش تليفون",
				CategoryId = 14
			},
			new()
			{
				Name = "فيش دش",
				CategoryId = 14
			},
			new()
			{
				Name = "فيش إيثرنت",
				CategoryId = 14
			},

			new()
			{
				Name = "الخلاطات",
				CategoryId = 15
			},
			new()
			{
				Name = "كراسي الحمامات والسيفونات",
				CategoryId = 15
			},
			new()
			{
				Name = "السخانات",
				CategoryId = 15
			},
			new()
			{
				Name = "أحواض الاستحمام",
				CategoryId = 15
			},
			new()
			{
				Name = "أنظمة الدش",
				CategoryId = 15
			},
			new()
			{
				Name = "مخارج مياه للدش",
				CategoryId = 15
			},
			new()
			{
				Name = "الشطافات",
				CategoryId = 15
			},
			new()
			{
				Name = "لوازم السباكة",
				CategoryId = 15
			},
			new()
			{
				Name = "صفايات",
				CategoryId = 15
			},
			new()
			{
				Name = "مغاسل الحمام",
				CategoryId = 15
			},
			new()
			{
				Name = "خزانات المياه",
				CategoryId = 15
			},
			new()
			{
				Name = "ملحقات الحمام",
				CategoryId = 15
			},

			new()
            {
                Name = "مكيفات سبليت",
                CategoryId = 16
            },
            new()
            {
                Name = "مكيفات النافذة",
                CategoryId = 16
            },
            new()
            {
                Name = "مكيفات مركزية",
                CategoryId = 16
            },
            new()
            {
                Name = "مكيفات الكاسيت",
                CategoryId = 16
            },  
        ];
        return subCategories;
    }

    private IEnumerable<Category> GetCategories()
    {
        List<Category> categories =
        [
            new()
            {
                Name = "الرخام"
            },
            new()
            {
                Name = "البورسلان"
            },
            new()
            {
                Name = "السيراميك"
            },
            new()
            {
                Name = "الباركيه"
            },
            new()
            {
                Name = "النوافذ"
            },
            new()
            {
                Name = "الديكورات"
            },
            new()
            {
                Name = "الأبواب"
            },
            new()
            {
                Name = "الصفائح الحجرية"
            },
            new()
            {
                Name = "الجبس"
            },
            new()
            {
                Name = "الحجر"
            },
            new()
            {
                Name = "الدهانات"
            },
            new()
            {
                Name = "العوازل"
            },
            new()
            {
                Name = "البوابات الإلكترونية"
            },
            new()
            {
                Name = "مفاتيح وأفياش"
            },
            new()
            {
                Name = "مواد صحية وخزانات"
            },
            new()
            {
                Name = "التكييف"
            },
        ];
        return categories;
    }

	
}