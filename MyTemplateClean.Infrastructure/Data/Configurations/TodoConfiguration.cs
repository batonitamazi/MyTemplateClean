namespace MyTemplateClean.Infrastructure.Data.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(256);
    }
}