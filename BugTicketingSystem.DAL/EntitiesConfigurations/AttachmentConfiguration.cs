using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL
{
    class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            //builder.Property(a => a.ImgLink).IsRequired();
            builder.Property(a => a.FileUrl).IsRequired();
            builder.Property(a => a.Type).IsRequired();
            builder
            .HasOne(a => a.Bugs)
            .WithMany(b => b.Attachments)
            .HasForeignKey(a => a.BugID);
        }
    }
}
