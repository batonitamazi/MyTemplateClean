    namespace MyTemplateClean.Infrastructure.Data.Configurations;

    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(k => k.Id)
                .HasConversion(
                    todoId => todoId.Value,
                    dbId => TodoId.Of(dbId)
                );
            
            builder.Property(k => k.Title)
                .HasConversion(
                    todoTitle => todoTitle.Value,
                    title => TodoTitle.Of(title)
                );

            
            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(256);
        }
    }