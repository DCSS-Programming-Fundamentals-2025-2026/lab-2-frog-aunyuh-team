using lab_1_frog_aunyuh_team.back.Domain.User;

public static class Session
{
    public static User CurrentUser { get; set; } = new User();
    public static ProductCatalog Catalog { get; set; } = new ProductCatalog();
    public static DateTime LastRecalculation { get; set; } = DateTime.Now;
}