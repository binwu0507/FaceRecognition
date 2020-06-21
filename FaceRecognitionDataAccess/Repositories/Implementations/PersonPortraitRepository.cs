using Dapper;
using FaceRecognitionDataAccess.Models;
using FaceRecognitionDataAccess.Repositories.Interfaces;
using FaceRecognitionDataAccess.UserClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionDataAccess.Repositories.Implementations
{
    public class PersonPortraitRepository : IPersonPortraitRepository
    {
        private string mConnectionStringPDMSDB;

        public PersonPortraitRepository()
        {
            //            mConnectionStringPDMSDB = Encryption.ConvertFromBase64String(UserClass.Encryption.Decrypt(ConfigurationManager.AppSettings[$"CACTUS_{clsStaticUtilities.EnvSuffix}"]));
            mConnectionStringPDMSDB = "Server=dalsql01\\Stage;Database=CactusBridge;Trusted_Connection=True;";

        }

        public List<PersonPortrait> Read()
        {
            using (SqlConnection connection = new SqlConnection(mConnectionStringPDMSDB))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PersonId", null);
                return connection.Query<PersonPortrait>("dbo.ReadPersonPortraits", parameters, commandType: CommandType.StoredProcedure).ToList();
            }

        }

        public PersonPortrait ReadById(int id)
        {
            using (SqlConnection connection = new SqlConnection(mConnectionStringPDMSDB))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PersonId", id);
                return connection.Query<PersonPortrait>("dbo.ReadPersonPortraits", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault(x => x.PersonId == id);
            }
        }

        public int Save(PersonPortrait personPortrait)
        {
            using (SqlConnection connection = new SqlConnection(mConnectionStringPDMSDB))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PersonId", personPortrait.PersonId);
                parameters.Add("@Name", personPortrait.Name);
                parameters.Add("@Portrait", personPortrait.Portrait);
                parameters.Add("@NewId", DbType.Int32, direction: ParameterDirection.Output);

                try
                {
                    connection.Execute("dbo.SavePersonPortrait", parameters, commandType: CommandType.StoredProcedure);

                    int newId = parameters.Get<int>("NewId");
                    return newId;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
