using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MockSem3FishCatch.models;

namespace MockSem3FishCatch
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string db = "Server=tcp:fishy.database.windows.net,1433;Initial Catalog=fish;Persist Security Info=False;User ID=hjeppesen;Password=fx%4vnr2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static IList<Catch> catchList = new List<Catch>();

        /// <summary>
        /// Adding static values to our list
        /// </summary>
        static Service1()
        {
            catchList.Add(new Catch
            {
                id = 1,
                fisherName = "Bent",
                fishType = "Fladfisk",
                place = "Roskilde",
                week = 10,
                weight = 7
            });

            catchList.Add(new Catch
            {
                id = 2,
                fisherName = "Verner",
                fishType = "Sild",
                place = "Esbjerg",
                week = 12,
                weight = 3
            });

            catchList.Add(new Catch
            {
                id = 3,
                fisherName = "Grethe",
                fishType = "Tun",
                place = "Grenaa",
                week = 15,
                weight = 1.2
            });
        }

        /// <summary>
        /// A method for adding new cathces to the table
        /// </summary>
        /// <param name="newCatch"></param>
        /// <returns></returns>
        public Catch AddCatch(Catch newCatch)
        {
            catchList.Add(new Catch
            {
                id = catchList.Count + 1,
                fisherName = newCatch.fisherName,
                fishType = newCatch.fishType,
                place = newCatch.place,
                week = newCatch.week,
                weight = newCatch.weight
            });

            return catchList.Last();
        }

        /// <summary>
        /// This will delete a certain catch object from our list
        /// </summary>
        /// <param name="id">The ID to remove</param>
        /// <returns>True/False</returns>
        public bool DeleteCatch(string id)
        {
            int Id = int.Parse(id);
            Catch result = catchList.FirstOrDefault(x => x.id == Id);
            if (result == null)
                return false;
            if (catchList.Remove(result))
                return true;
            return false;
        }

        /// <summary>
        /// This will return all our records
        /// </summary>
        /// <returns></returns>
        public IList<Catch> GetCatches()
        {
            return catchList;
        }

        /// <summary>
        /// This will return a specific catch, according to the provided ID
        /// </summary>
        /// <param name="id">The Catch ID to return</param>
        /// <returns>Catch values</returns>
        public Catch GetOneCatch(string id)
        {
            int Id = int.Parse(id);
            Catch result = catchList.FirstOrDefault(x => x.id == Id);
            if (result == null)
                return null;
            return result;
        }

        /// <summary>
        /// This will update the values of a certain catch
        /// </summary>
        /// <param name="myCatch"></param>
        /// <returns></returns>
        public Catch UpdateCatch(Catch myCatch)
        {
            Catch result = catchList.FirstOrDefault(x => x.id == myCatch.id);
            if (result == null)
                return null;

            result.id = result.id;
            if (myCatch.fisherName != null)
                result.fisherName = myCatch.fisherName;
            if (myCatch.fishType != null)
                result.fishType = myCatch.fishType;
            if (myCatch.place != null)
                result.place = myCatch.place;
            if (myCatch.week != 0)
                result.week = myCatch.week;
            if (myCatch.fisherName != null)
                result.weight = myCatch.weight;

            return result;
        }

        /// <summary>
        /// This will fetch all the catches from the db 
        /// </summary>
        /// <returns>List of objects (Json)</returns>
        public IList<Catch> GetCatchesDb()
        {
            const string sql = "SELECT * FROM catches ORDER BY weekNo DESC";
            using (SqlConnection conn = new SqlConnection(db))
            using (SqlCommand query = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader result = query.ExecuteReader())
                {
                    IList<Catch> catches = new List<Catch>();
                    while (result.Read())
                    {

                        int id = result.GetInt32(0);
                        string fisherName = result.GetString(1);
                        string fishType = result.GetString(2);
                        int fishWeight = result.GetInt32(3);
                        string place = result.GetString(4);
                        int weekNo = result.GetInt32(5);

                        catches.Add(new Catch
                        {
                            id = id,
                            fisherName = fisherName,
                            fishType = fishType,
                            weight = fishWeight,
                            place = place,
                            week = weekNo
                        });
                    }

                    conn.Close();
                    return catches;
                }
            }
        }

        /// <summary>
        /// This will fetch all the catches within a certain week
        /// </summary>
        /// <param name="week">The week-number to look for</param>
        /// <returns>Catch object in that period (Json)</returns>
        public IList<Catch> GetWeekCatchDb(string week)
        {
            int weekNo = int.Parse(week);
            const string sql = "SELECT * FROM catches WHERE weekNo=@week ORDER BY weekNo DESC";
            using (SqlConnection conn = new SqlConnection(db))
            using (SqlCommand query = new SqlCommand(sql, conn))
            {
                query.Parameters.AddWithValue("@week", weekNo);
                conn.Open();
                using (SqlDataReader result = query.ExecuteReader())
                {
                    IList<Catch> catches = new List<Catch>();
                    while (result.Read())
                    {

                        int id = result.GetInt32(0);
                        string fisherName = result.GetString(1);
                        string fishType = result.GetString(2);
                        int fishWeight = result.GetInt32(3);
                        string place = result.GetString(4);

                        catches.Add(new Catch
                        {
                            id = id,
                            fisherName = fisherName,
                            fishType = fishType,
                            weight = fishWeight,
                            place = place,
                            week = weekNo
                        });
                    }

                    conn.Close();
                    return catches;
                }
            }
        }

        /// <summary>
        /// Add a new catch to the database
        /// </summary>
        /// <param name="newCatch">The object/values to add [fishername, fishType, fishweight, place, weekNo]</param>
        /// <returns>The amount of rows affected (should be 1)</returns>
        public int AddCatchDB(Catch newCatch)
        {
            const string sql = "INSERT INTO catches (fisherName, fishType, fishWeight, place, weekNo) VALUES (@name, @type, @weight, @place, @week)";
            using (SqlConnection conn = new SqlConnection(db))
            using (SqlCommand query = new SqlCommand(sql, conn))
            {
                conn.Open();
                int rowsAffected = query.ExecuteNonQuery();
                conn.Close();
                return rowsAffected;
            }
        }
    }
}
