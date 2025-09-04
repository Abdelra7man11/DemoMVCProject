using Demo.DataAccess.Models.EmployeesModels;
using Demo.DataAccess.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configurations
{
    public class EmployeeConfiguration : BaseEntityConfigurations<Employee> , IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Address).HasColumnType("varchar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E => E.Gender)
                .HasConversion((EmpGender) => EmpGender.ToString(),
                (gender) => (Gender)Enum.Parse(typeof(Gender), gender));
            builder.Property(E => E.EmployeeType)
                .HasConversion((EmpType) => EmpType.ToString(),
                (Emp) => (EmployeeType)Enum.Parse(typeof(EmployeeType), Emp));

            base.Configure(builder); // Implement Form BaseEntityConfigurations
        }
    }
}
