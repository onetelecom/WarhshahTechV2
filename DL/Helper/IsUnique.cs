//using DL.DBContext;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;
//using System.IO;
//using Microsoft.EntityFrameworkCore;

//namespace DL.Helper
//{






//    public class IsUnique : ValidationAttribute
//    {




//        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//        {
//            #region connectionString
//            // load connection string from main app appsetting start 
//            var bulider = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
//                AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

//            var dconnection = bulider.Build().GetSection("ConnectionStrings").GetSection("ConnectionString").Value;

//            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
//            optionsBuilder.UseSqlServer(dconnection);
//            AppDBContext dbContext = new AppDBContext(optionsBuilder.Options);
//            // load connection string from main app appsetting end  
//            #endregion
//            var className = validationContext.ObjectType.Name.Split('_').First();
//            var propertyName = validationContext.MemberName;
//            var parameterName = string.Format("@{0}", propertyName);


//            var addressModel = validationContext.ObjectInstance;
//            if (addressModel != null)
//            {
//                var id = validationContext.ObjectInstance.GetType().GetProperties().FirstOrDefault(x => x.Name == "Id");
//                if (id != null)
//                {
//                    var idValue = id.GetValue(addressModel, null);




//                    //using (var connection = dbContext.Database.GetDbConnection())
//                    //{
//                    //     connection.Open();
//                    //    using (var command = connection.CreateCommand())
//                    //    {
//                    //        command.CommandText = string.Format("SELECT * FROM {0} WHERE {1}='{2}' And Id <>'{3}'", className, propertyName, value, idValue);
//                    //            var result =   command.ExecuteReader();

//                    //        var commandText = "INSERT Categories (CategoryName) VALUES (@CategoryName)";
//                    //        var name = new SqlParameter("@CategoryName", "Test");
//                    //        connection.Database.ExecuteSqlCommand(commandText, name);


//                    //    }
//                    //}



//                    //using (var dbContext1 = dbContext.Database.GetDbConnection())
//                    //{
//                    //    var commandText = "INSERT Categories (CategoryName) VALUES (@CategoryName)";
//                    //    var name = new SqlParameter("@CategoryName", "Test");
//                    //    // dbContext1.ExecuteSqlCommand(commandText, name);
//                    //}


//                    if (idValue != null && Convert.ToInt32(idValue) > 0)







//                    {



//                        var result = dbContext.ApplicationPage.FromSqlRaw(
//               string.Format("SELECT * FROM {0} WHERE {1}='{2}' And Id <>'{3}'", className, propertyName, value, idValue)
//               // ,new System.Data.SqlClient.SqlParameter(parlameterName, value)
//               );
//                        if (result.Any())
//                        {
//                            return new ValidationResult(string.Format("The '{0}' already exist", propertyName),
//                                        new List<string>() { propertyName });
//                        }
//                    }



//                    else
//                    {


//                        var result = dbContext.PersonData.FromSqlRaw(
//                   string.Format("SELECT * FROM {0} WHERE {1}='{2}'", className, propertyName, value)
//                   // ,new System.Data.SqlClient.SqlParameter(parlameterName, value)
//                   );
//                        if (result.Any())
//                        {
//                            return new ValidationResult(string.Format("The '{0}' already exist", propertyName),
//                                        new List<string>() { propertyName });
//                        }

//                    }



//                }







//            }




//            return null;
//        }
//    }
//}
