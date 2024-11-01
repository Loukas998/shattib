using Template.Domain.Entities.Products;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Seeders;

internal class CategoriesSeeder(TemplateDbContext dbContext) : ICategoriesSeeder
{
    public async Task Seed()
    {
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
                Name = "مكيفات سبليت",
                CategoryId = 14
            },
            new()
            {
                Name = "مكيفات النافذة",
                CategoryId = 14
            },
            new()
            {
                Name = "مكيفات مركزية",
                CategoryId = 14
            },
            new()
            {
                Name = "مكيفات الكاسيت",
                CategoryId = 14
            },

            new()
            {
                Name = "مفتاح تسكيره",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح أحادي",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح ثنائي",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح ثلاثي",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح رباعي",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح مكيف",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح سخان",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح ستارة",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح دايمر",
                CategoryId = 15
            },
            new()
            {
                Name = "مفتاح جرس",
                CategoryId = 15
            },
            new()
            {
                Name = "فيش ثلاثي",
                CategoryId = 15
            },
            new()
            {
                Name = "USB, USBc فيش",
                CategoryId = 15
            },
            new()
            {
                Name = "مجرى أفياش",
                CategoryId = 15
            },
            new()
            {
                Name = "أفياش للمجرى",
                CategoryId = 15
            },
            new()
            {
                Name = "فيش تليفون",
                CategoryId = 15
            },
            new()
            {
                Name = "فيش دش",
                CategoryId = 15
            },
            new()
            {
                Name = "فيش إيثرنت",
                CategoryId = 15
            },

            new()
            {
                Name = "الخلاطات",
                CategoryId = 16
            },
            new()
            {
                Name = "كراسي الحمامات والسيفونات",
                CategoryId = 16
            },
            new()
            {
                Name = "السخانات",
                CategoryId = 16
            },
            new()
            {
                Name = "أحواض الاستحمام",
                CategoryId = 16
            },
            new()
            {
                Name = "أنظمة الدش",
                CategoryId = 16
            },
            new()
            {
                Name = "مخارج مياه للدش",
                CategoryId = 16
            },
            new()
            {
                Name = "الشطافات",
                CategoryId = 16
            },
            new()
            {
                Name = "لوازم السباكة",
                CategoryId = 16
            },
            new()
            {
                Name = "صفايات",
                CategoryId = 16
            },
            new()
            {
                Name = "مغاسل الحمام",
                CategoryId = 16
            },
            new()
            {
                Name = "خزانات المياه",
                CategoryId = 16
            },
            new()
            {
                Name = "ملحقات الحمام",
                CategoryId = 16
            }
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
            new()
            {
                Name = "الإنارة"
            }
        ];
        return categories;
    }
}