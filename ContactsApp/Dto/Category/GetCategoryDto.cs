namespace ContactsApp.Dto.Category;

public class GetCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? SubCategoryName { get; set; }

    public static GetCategoryDto Mapper(Models.Category category)
    {
        return new GetCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            SubCategoryName = category.SubCategoryName
        };
    }
}