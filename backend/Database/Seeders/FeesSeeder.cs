using Backend.Entities;
using Backend.Enums;

namespace Backend.Database.Seeders;

public static class FeesSeeder
{
    public static void Seed(AppDbContext context)
    {
        var fees = new List<Fee>
        {
            new() { DocumentType = DocumentType.TOR, Amount = 100 },
            new() { DocumentType = DocumentType.Diploma, Amount = 200 },
            new() { DocumentType = DocumentType.HonorableDismissal, Amount = 50 },
            new() { DocumentType = DocumentType.CertificateOfGraduation, Amount = 150 },
        };

        foreach (var fee in fees)
        {
            var existingFee = context.Fees.FirstOrDefault(f => f.DocumentType == fee.DocumentType);
            if (existingFee == null)
            {
                context.Fees.Add(fee);
            }
            else
            {
                existingFee.Amount = fee.Amount;
            }
        }
        context.SaveChanges();
    }
}
