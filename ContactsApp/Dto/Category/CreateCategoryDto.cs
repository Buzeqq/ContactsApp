namespace ContactsApp.Dto.Category;

public class CreateCategoryDto
{
    public string Subcategory { get; set; }

    public static Models.Category Mapper(CreateCategoryDto category)
    {
        return new Models.Category
        {
            Name = "other",
            SubCategoryName = category.Subcategory
        };
    }
}