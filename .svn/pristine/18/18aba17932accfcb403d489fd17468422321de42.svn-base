using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gmou.DomainModelEntities;
using System.Data.SqlClient;
using System.Configuration.Provider;
using System.Text.RegularExpressions;
using System.Web;

namespace Gmou.Repository
{
    public class EmployeeRepository
    {
        public static IEnumerable<LoginUser> GetLogin(string username, string password)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetEmployeeLogin", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new LoginUser(
                           dr["username"].ToString(),

                           (dr["password"].ToString()),
                           (dr["role"].ToString()),
                        (Convert.ToInt32(dr["department_id"])),
                        (Convert.ToInt32(dr["serial_id"])));
                    }
                    conn.Close();

                }
            }

        }
        public static IEnumerable<Department> GetDepartments()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllDepartments", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new Department(
                         Convert.ToInt32(dr["id"]),

                           (dr["department_name"].ToString()));

                    }
                    conn.Close();

                }
            }

        }
        public static IEnumerable<EmployeeFamilyDetails> GetAllEmployee()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllEmployee", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new EmployeeFamilyDetails((
                          Convert.ToInt32(dr["empid"])),
                         (Convert.ToInt32(dr["employee_id"])),
                          (dr["first_name"].ToString()),
                            (dr["middle_name"].ToString()),
                            (dr["last_name"].ToString()),
                            (dr["cast"].ToString()),
                            (dr["father_husband_name"].ToString()),
                            (dr["village"].ToString()),
                            (dr["postoffice"].ToString()),
                            (dr["patti"].ToString()),
                            (dr["thana"].ToString()),
                            (dr["tehsil"].ToString()),
                            (dr["district"].ToString()),
                            (dr["state"].ToString()),
                            (Convert.ToDateTime(dr["date_of_birth"])),
                           (Convert.ToDateTime(dr["date_of_joining"])),
                            (Convert.ToDouble(dr["contact_number"])),
                            (dr["qaulification"].ToString()),
                            (dr["local_address"].ToString()),
                            (dr["identification_mark_1"].ToString()),
                            (dr["identification_mark_2"].ToString()),
                            (dr["UAN"].ToString()),
                            (dr["PAN"].ToString()),
                            (dr["wife_name"].ToString()),
                            (dr["next_kin"].ToString()),
                            (Convert.ToDateTime(dr["wife_dob"].ToString())),
                            (dr["nominee_name"].ToString()),
                            (dr["nominee_identification1"].ToString()),
                            (dr["nominee_identification2"].ToString()), (dr["relationship"].ToString()),
                            (dr["member_name"].ToString()),
                            (dr["Office_Name"].ToString()),
                            (dr["department_name"].ToString())

                           );



                    }
                    conn.Close();

                }
            }

        }

        public static IEnumerable<Employee> GetEmployeeDetail(int id)
        {

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetEmployeeDetail", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new Employee(
                         Convert.ToInt32(dr["id"]), Convert.ToInt32(dr["emp_id"]),
                         dr["first_name"].ToString(), dr["middle_name"].ToString(),
                         dr["last_name"].ToString(), Convert.ToDateTime(dr["date_of_birth"]),
                         dr["contact_number"].ToString(), dr["emergency_number"].ToString(),
                        dr["city"].ToString(), dr["address"].ToString(),
                        dr["qualification"].ToString(), Convert.ToDateTime(dr["date_of_joining"]),
                        dr["emp_type"].ToString(), Convert.ToBoolean(dr["marital_status"]),
                        dr["department_name"].ToString(), dr["working_City"].ToString());
                    }
                    conn.Close();

                }
            }

        }
        public static IEnumerable<Office> GetOffice()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllOffices", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new Office(
                         Convert.ToInt32(dr["id"]),

                           (dr["Office_Name"].ToString()));

                    }
                    conn.Close();

                }
            }

        }

        public static int SaveEmployeeDetails(EmployeeDetails employeeDetails, bool IsUpdate, int EmployeeId = 0)
        {
            int employeeID;
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_SaveEmployeeDetails", conn))
                {
                    //string dt = DateTime.Parse(txtDate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("firstname", employeeDetails.FirstName);
                    cmd.Parameters.AddWithValue("middlename", employeeDetails.MiddleName);
                    cmd.Parameters.AddWithValue("lastname", employeeDetails.LastName);
                    cmd.Parameters.AddWithValue("caste", employeeDetails.Caste);
                    cmd.Parameters.AddWithValue("fathername", employeeDetails.FatherName);
                    cmd.Parameters.AddWithValue("village", employeeDetails.Village);
                    cmd.Parameters.AddWithValue("postoffice", employeeDetails.PostOffice);
                    cmd.Parameters.AddWithValue("patti", employeeDetails.Patti);
                    cmd.Parameters.AddWithValue("thana", employeeDetails.Thana);
                    cmd.Parameters.AddWithValue("tehsil", employeeDetails.Tehsil);
                    cmd.Parameters.AddWithValue("district", employeeDetails.District);
                    cmd.Parameters.AddWithValue("state", employeeDetails.State);
                    cmd.Parameters.AddWithValue("birthdate", employeeDetails.BirthDate);
                    cmd.Parameters.AddWithValue("joiningdate", employeeDetails.JoiningDate);
                    cmd.Parameters.AddWithValue("contactnumber", employeeDetails.Contact);
                    cmd.Parameters.AddWithValue("qualification", employeeDetails.Qaulification);
                    cmd.Parameters.AddWithValue("localaddress", employeeDetails.LocalAddress);
                    cmd.Parameters.AddWithValue("identification1", employeeDetails.IdentificationMark1);
                    cmd.Parameters.AddWithValue("identification2", employeeDetails.IdentificationMark2);
                    cmd.Parameters.AddWithValue("uan", employeeDetails.UAN);
                    cmd.Parameters.AddWithValue("pan", employeeDetails.PAN);
                    cmd.Parameters.AddWithValue("workingoffice", employeeDetails.WorkingOffice);
                    cmd.Parameters.AddWithValue("workingdepartment", employeeDetails.WorkingDepartment);
                    cmd.Parameters.AddWithValue("empID", EmployeeId);
                    cmd.Parameters.AddWithValue("IsUpdate", IsUpdate);
                    cmd.Parameters.AddWithValue("image", employeeDetails.Image);
                    conn.Open();
                    if (!IsUpdate)
                    {
                        employeeID = (int)cmd.ExecuteScalar();
                    }
                    else
                    {
                        cmd.ExecuteScalar();
                        employeeID = EmployeeId;
                    }
                    conn.Close();

                }
                using (var cmd = new SqlCommand("sp_SaveEmployeeFamily", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("wife_name", employeeDetails.employeeFamily.WifeName);
                    cmd.Parameters.AddWithValue("next_kin", employeeDetails.employeeFamily.NextToKin);
                    cmd.Parameters.AddWithValue("wife_dob", employeeDetails.employeeFamily.WifeDOB);
                    cmd.Parameters.AddWithValue("nominee_name", employeeDetails.employeeFamily.NomineeName);
                    cmd.Parameters.AddWithValue("nominee_identification1", employeeDetails.employeeFamily.NomineeIdentificationMark1);
                    cmd.Parameters.AddWithValue("nominee_identification2", employeeDetails.employeeFamily.NomineeIdentificationMark2);
                    cmd.Parameters.AddWithValue("employee_id", employeeID);
                    cmd.Parameters.AddWithValue("IsUpdate", IsUpdate);

                    conn.Open();
                    int dr = cmd.ExecuteNonQuery();
                    conn.Close();

                }

                using (var cmd = new SqlCommand("sp_SaveEmployeeFamilyDetails", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("emp_id", employeeID);
                    cmd.Parameters.AddWithValue("relationship", employeeDetails.employeeFamilyDetails.RelationShip);
                    cmd.Parameters.AddWithValue("member_name", employeeDetails.employeeFamilyDetails.MemberName);
                    cmd.Parameters.AddWithValue("IsUpdate", IsUpdate);

                    conn.Open();
                    int dr = cmd.ExecuteNonQuery();
                    conn.Close();

                }
                using (var cmd = new SqlCommand("sp_InsertEmoployeeServiceRecord", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("empid", employeeID);
                    cmd.Parameters.AddWithValue("designation", employeeDetails.Designation);
                    cmd.Parameters.AddWithValue("working_office", Convert.ToInt32(employeeDetails.workingOffice));
                    cmd.Parameters.AddWithValue("appointment_date", employeeDetails.AppointmentDate);
                    cmd.Parameters.AddWithValue("payscale", employeeDetails.PayScale);
                    cmd.Parameters.AddWithValue("document", employeeDetails.Document);
                    cmd.Parameters.AddWithValue("working_department", Convert.ToInt32(employeeDetails.workingDepartment));
                    cmd.Parameters.AddWithValue("IsUpdate", IsUpdate);

                    conn.Open();
                    int dr = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
            return employeeID;
        }
        public static IEnumerable<EmployeeFamilyDetails> EditEmployeeDetails(int EmployeeID)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetEmployeeForEdit", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("EmpId", EmployeeID);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new EmployeeFamilyDetails(
                         (Convert.ToInt32(dr["empid"])),
                         (dr["first_name"].ToString()),
                           (dr["middle_name"].ToString()),
                           (dr["last_name"].ToString()),
                           (dr["cast"].ToString()),
                           (dr["father_husband_name"].ToString()),
                           (dr["village"].ToString()),
                           (dr["postoffice"].ToString()),
                           (dr["patti"].ToString()),
                           (dr["thana"].ToString()),
                           (dr["tehsil"].ToString()),
                           (dr["district"].ToString()),
                           (dr["state"].ToString()),
                           (Convert.ToDateTime(dr["date_of_birth"])),
                          (Convert.ToDateTime(dr["date_of_joining"])),
                           (Convert.ToDouble(dr["contact_number"])),
                           (dr["qaulification"].ToString()),
                           (dr["local_address"].ToString()),
                           (dr["identification_mark_1"].ToString()),
                           (dr["identification_mark_2"].ToString()),
                           (dr["UAN"].ToString()),
                           (dr["PAN"].ToString()),
                           (dr["wife_name"].ToString()),
                           (dr["next_kin"].ToString()),
                           (Convert.ToDateTime(dr["wife_dob"].ToString())),
                           (dr["nominee_name"].ToString()),
                           (dr["nominee_identification1"].ToString()),
                           (dr["nominee_identification2"].ToString()), (dr["relationship"].ToString()),
                           (dr["member_name"].ToString()),
                           (dr["id"].ToString()),
                          (dr["id"].ToString()),
                          ((byte[])(dr["image"])),

                          (dr["employee_designation"].ToString()),
                           (dr["working_office"].ToString()),
                          ((Convert.ToDateTime(dr["appointment_date"]))),
                           (dr["pay_scale"].ToString()),
                           ((byte[])(dr["service_document"])),

                             (dr["working_department"].ToString())

                          );

                    }
                    conn.Close();

                }
            }

        }

        public static String DMYToMDY(String input)
        {
            return Regex.Replace(input,
            @"\b(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{2,4})\b",
            "${month}/${day}/${year}");
        }

        public static IEnumerable<EmployeeFamilyDetails> GetEmployeeDetails(int EmployeeID)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetEmployeeByID", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("EmpID", EmployeeID);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new EmployeeFamilyDetails(
                         (dr["first_name"].ToString()),
                           (dr["middle_name"].ToString()),
                           (dr["last_name"].ToString()),
                           (dr["cast"].ToString()),
                           (dr["father_husband_name"].ToString()),
                           (dr["village"].ToString()),
                           (dr["postoffice"].ToString()),
                           (dr["patti"].ToString()),
                           (dr["thana"].ToString()),
                           (dr["tehsil"].ToString()),
                           (dr["district"].ToString()),
                           (dr["state"].ToString()),
                           (Convert.ToDateTime(dr["date_of_birth"])),
                          (Convert.ToDateTime(dr["date_of_joining"])),
                           (Convert.ToDouble(dr["contact_number"])),
                           (dr["qaulification"].ToString()),
                           (dr["local_address"].ToString()),
                           (dr["identification_mark_1"].ToString()),
                           (dr["identification_mark_2"].ToString()),
                           (dr["UAN"].ToString()),
                           (dr["PAN"].ToString()),
                           (dr["wife_name"].ToString()),
                           (dr["next_kin"].ToString()),
                           (Convert.ToDateTime(dr["wife_dob"].ToString())),
                           (dr["nominee_name"].ToString()),
                           (dr["nominee_identification1"].ToString()),
                           (dr["nominee_identification2"].ToString()), (dr["relationship"].ToString()),
                           (dr["member_name"].ToString()),
                           (dr["Office_Name"].ToString()),
                           (dr["department_name"].ToString()),
                           ((byte[])(dr["image"]))
                          );

                    }
                    conn.Close();

                }
            }

        }

        public static int SaveQuickEmployee(EmployeeRegistartion employeeresgitration,string user)
        {
            int result;
            try
            {


                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    using (var cmd = new SqlCommand("sp_insert_quickEmployee", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("first_name", employeeresgitration.first_name);
                        cmd.Parameters.AddWithValue("middle_name", employeeresgitration.middle_name);
                        cmd.Parameters.AddWithValue("last_name", employeeresgitration.last_name);
                        cmd.Parameters.AddWithValue("department_id", employeeresgitration.department_id);
                        cmd.Parameters.AddWithValue("user_name", employeeresgitration.username);
                        cmd.Parameters.AddWithValue("password", employeeresgitration.password);
                        cmd.Parameters.AddWithValue("confirm_password", employeeresgitration.confirmpassword);
                        cmd.Parameters.AddWithValue("created_by", user);
                        cmd.Parameters.AddWithValue("dob", employeeresgitration.date_of_birth);
                        conn.Open();
                                              result = cmd.ExecuteNonQuery();


                        conn.Close();

                    }

                }
                return result;
            }catch(Exception ex)
            {
                

            }
            return 0;
        }

        public static IEnumerable<EmployeeRegistrationDetails> GetAllRegisteredEmployess()
        {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("sp_GetAllRegisteredEmployees", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yield return new EmployeeRegistrationDetails(
                         (Convert.ToInt32(dr["serial_id"])),
                           (dr["first_name"].ToString()),
                            (dr["middle_name"].ToString()),
                             (dr["last_name"].ToString()),
                              (dr["department_name"].ToString()),
                               (dr["username"].ToString()),
                                (dr["password"].ToString()),
                                 (dr["created_by"].ToString()),
                                  (Convert.ToDateTime(dr["created_on"])));
                           }
                    }
                }

        }
        //sp_GetAllRegisteredEmployees
       public static bool DeleteRegisteredEmployee(int serialID)
        {
           int result;
               try
            {


                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    using (var cmd = new SqlCommand("sp_DeleteEmployees", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("id", serialID);
                        conn.Open();
                        result = cmd.ExecuteNonQuery();


                        conn.Close();
                        if(result<0)
                        {
                            return false;
                        }
                       
                    }
                }
                
               }

           catch(Exception ex)
               {

           }
               return true;
        }
    }
}
