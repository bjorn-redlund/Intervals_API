using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.ObjectModel;





namespace Intervals_API
{

    // Person 
    public class RootPerson
    {
        public Person[] person { get; set; }
    }
    public class Person
    {
        public string id { get; set; }
        public string localid { get; set; }
        public string clientid { get; set; }
        public string title { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string primaryaccount { get; set; }
        public string notes { get; set; }
        public string allprojects { get; set; }
        public string active { get; set; }
        public string _private { get; set; }
        public string tips { get; set; }
        public string username { get; set; }
        public string groupid { get; set; }
        public string group { get; set; }
        public string client { get; set; }
        public string numlogins { get; set; }
        public string lastlogin { get; set; }
        public string timezone { get; set; }
        public string timezone_offset { get; set; }
        public string restricttasks { get; set; }
        public string clientlocalid { get; set; }
        public string calendarorientation { get; set; }
        public string editordisabled { get; set; }
    }

    // Task
    public class RootTask
    {
        public Task[] task { get; set; }
    }
    public class Task
    {
        public string id { get; set; }
        public string localid { get; set; }
        public string queueid { get; set; }
        public string assigneeid { get; set; }
        public string statusid { get; set; }
        public string projectid { get; set; }
        public string moduleid { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string dateopen { get; set; }
        public string datedue { get; set; }
        public string dateclosed { get; set; }
        public string estimate { get; set; }
        public string ownerid { get; set; }
        public string priorityid { get; set; }
        public string datemodified { get; set; }
        public string color { get; set; }
        public string priority { get; set; }
        public string severity { get; set; }
        public string status { get; set; }
        public string status_order { get; set; }
        public string project { get; set; }
        public string clientid { get; set; }
        public string clientlocalid { get; set; }
        public string client { get; set; }
        public string milestoneid { get; set; }
        public string milestone { get; set; }
        public string module { get; set; }
        public string projectlocalid { get; set; }
        public string assignees { get; set; }
        public string followers { get; set; }
        public string followerid { get; set; }
        public string owners { get; set; }
        public string billable { get; set; }
        public string unbillable { get; set; }
        public string actual { get; set; }
        public string projectlabel { get; set; }
        public string projectlabel_order { get; set; }
        public string projectlabelid { get; set; }
    }

    // Client
    public class RootClient
    {
        public Client[] client { get; set; }
    }
    public class Client
    {
        public string id { get; set; }
        public string name { get; set; }
        public string active { get; set; }
        public string localidunpadded { get; set; }
        public string localid { get; set; }
    }

    // Module
    public class RootModule
    {
        public Module[] module { get; set; }
    }
    public class Module
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string active { get; set; }
    }

    // Priority
    public class RootPriority
    {
        public Priority[] taskPriority { get; set; }
    }
    public class Priority
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string priority { get; set; }
        public string isdefault { get; set; }
        public string active { get; set; }
    }

    // Status 
    public class RootStatus
    {
        //public int personid { get; set; }
        //public string status { get; set; }
        //public int code { get; set; }
        //public int listcount { get; set; }
        public Status[] taskStatus { get; set; }
    }
    public class Status
    {
        public string id { get; set; }
        public string name { get; set; }
        public string priority { get; set; }
        public string frozen { get; set; }
        public string active { get; set; }
    }

    // Project
    public class RootProject
    {
        public Project[] project { get; set; }
    }
    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string datestart { get; set; }
        public string dateend { get; set; }
        public string clientid { get; set; }
        public string client { get; set; }
        public string clientlocalid { get; set; }
        public string labelid { get; set; }
        public string label { get; set; }
        public string localid { get; set; }
        public string manager { get; set; }
        public string managerid { get; set; }        
    }



    class Program
    {
        static void Main(string[] module)
        {

            switch (module[0])
            {
                case "Person":
                    Interval_Person();
                    break;
                case "Task":
                    Interval_Task();
                    break;
                case "Client":
                    Interval_Client();
                    break;
                case "Module":
                    Interval_Module();
                    break;
                case "Priority":
                    Interval_Priority();
                    break;
                case "Status":
                    Interval_Status();
                    break;
                case "Project":
                    Interval_Project();
                    break;
                default:
                    break;
            }

            //Interval_Person();
            //Interval_Task();
            //Interval_Client();
            //Interval_Module();
            //Interval_Priority();
            //Interval_Status();
            //Interval_Project();
        }
        
        static void Interval_Person()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string logFolder = @"D:\VS\Intervals_API\Logs";
            string txtFile = @"D:\VS\Intervals_API\Txt\txt.json";
            string userAccessKey = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("5so7gyreizi:x"));
            string APIAddress = "https://api.myintervals.com/person/?clientid=325541&limit=1000";
            string sqlServer = "MDCFAK01";
            string sqlDatabase = "Intervals_Stats";
            int insertSQL = 1;
            int writeFile = 0;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var json = "";

                using (WebClient wc = new WebClient())
                {
                    // Basic Auth
                    wc.Headers.Add("Accept", "application/json");
                    wc.Headers.Add("Content-Type", "application/json;charset=utf-8");
                    wc.Headers.Add(HttpRequestHeader.Authorization, "Basic " + userAccessKey);
                    json = wc.DownloadString(APIAddress);


                    // Json to txt file                   
                    if (writeFile == 1)
                    {
                        File.WriteAllText(txtFile, json);
                    }

                    // Json to class object
                    var jsonNullHandling = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    RootPerson rootPerson = JsonConvert.DeserializeObject<RootPerson>(json, jsonNullHandling);


                    // Class object loop   
                    foreach (Person person in rootPerson.person)
                    {
                        if (insertSQL == 0)
                        {
                            // TEST >>
                            Console.WriteLine("id:" + person.id);
                            Console.WriteLine("firstname:" + person.firstname);
                            Console.WriteLine("lastname:" + person.lastname);
                            Console.WriteLine("title:" + person.title);
                            Console.WriteLine("active:" + person.active);

                            //Debug.WriteLine("id:" + person.id);
                            //Debug.WriteLine("localid:" + person.localid);
                            //Debug.WriteLine("clientid:" + person.title);
                            //Debug.WriteLine("title:" + person.title);
                            //Debug.WriteLine("firstname:" + person.firstname);
                            //Debug.WriteLine("lastname:" + person.lastname);
                            //Debug.WriteLine("primaryaccount:" + person.primaryaccount);
                            //Debug.WriteLine("notes:" + person.notes);
                            //Debug.WriteLine("allprojects:" + person.allprojects);
                            //Debug.WriteLine("active:" + person.active);
                            //Debug.WriteLine("_private:" + person._private);
                            //Debug.WriteLine("tips:" + person.tips);
                            //Debug.WriteLine("username:" + person.username);
                            //Debug.WriteLine("groupid:" + person.groupid);
                            //Debug.WriteLine("group:" + person.group);
                            //Debug.WriteLine("client:" + person.client);
                            //Debug.WriteLine("numlogins:" + person.numlogins);
                            //Debug.WriteLine("lastlogin:" + person.timezone);
                            //Debug.WriteLine("timezone_offset:" + person.timezone_offset);
                            //Debug.WriteLine("restricttasks:" + person.restricttasks);
                            //Debug.WriteLine("clientlocalid:" + person.clientlocalid);
                            //Debug.WriteLine("calendarorientation:" + person.calendarorientation);
                            //Debug.WriteLine("editordisabled:" + person.editordisabled);
                            // TEST <<
                        }

                        if (insertSQL == 1)
                        {
                            string connectionString = @"Data Source = " + sqlServer + "; Initial Catalog = " + sqlDatabase + "; Integrated Security = True; ";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                // Open connection
                                connection.Open();

                                // Create the insert statement
                                string insertStatement =
                                   "INSERT INTO [int].[Person](" +
                                        "[id],[localid],[clientid],[title],[firstname],[lastname]," +
                                        "[primaryaccount],[notes],[allprojects],[active],[private]," +
                                        "[tips],[username],[groupid],[group],[client]," +
                                        "[numlogins],[lastlogin],[timezone],[timezone_offset],[restricttasks]," +
                                        "[clientlocalid],[calendarorientation],[editordisabled]) " +
                                    "VALUES('" +
                                        person.id + "', '" + person.localid + "', '" + person.clientid + "', '" + person.title + "', '" + person.firstname + "','" + person.lastname + "','" +
                                        person.primaryaccount + "', substring('" + person.notes.Replace("'", "''") + "',1,999), '" + person.allprojects + "', '" + person.active + "', '" + "''" + "', '" +
                                        person.tips + "', '" + person.username + "', '" + person.groupid + "', '" + person.group + "', '" + person.client + "', '" +
                                        person.numlogins + "', cast(substring('" + person.lastlogin + "',1,10) as datetime),'" +
                                        person.timezone + "', '" + person.timezone_offset + "', '" + person.restricttasks + "', '" +
                                        person.clientlocalid + "', '" + person.calendarorientation + "', '" + person.editordisabled +
                                        "')";

                                // Create a SqlCommand object with the insert statement and the SqlConnection object
                                SqlCommand command = new SqlCommand(insertStatement, connection);

                                // Execute the insert command
                                int rowsAffected = command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }


            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(logFolder + "\\" + "ErrorLog " + currentDateTime + ".log"))
                {
                    sw.WriteLine(exception.ToString());
                }

            }

        }

        static void Interval_Task()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string logFolder = @"D:\VS\Intervals_API\Logs";
            string txtFile = @"D:\VS\Intervals_API\Txt\txt.json";
            string userAccessKey = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("5so7gyreizi:x"));
            string DateLimit = DateTime.Today.Date.AddDays(-30).ToString("yyyy/MM/dd");
            string APIAddress = "https://api.myintervals.com/task/?limit=10000&datemodifiedbegin=" + DateLimit;

            // Initial Load
            //string APIAddress = "https://api.myintervals.com/task/?limit=100000";


            string sqlServer = "MDCFAK01";
            string sqlDatabase = "Intervals_Stats";
            int insertSQL = 1;
            int writeFile = 0;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var json = "";

                using (WebClient wc = new WebClient())
                {
                    // Basic Auth
                    wc.Headers.Add("Accept", "application/json");
                    wc.Headers.Add(HttpRequestHeader.Authorization, "Basic " + userAccessKey);

                    // Request string                    
                    json = wc.DownloadString(APIAddress);


                    // Json to txt file                   
                    if (writeFile == 1)
                    {
                        File.WriteAllText(txtFile, json);
                    }

                    // Json to class object
                    var jsonNullHandling = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    RootTask rootTask = JsonConvert.DeserializeObject<RootTask>(json, jsonNullHandling);


                    // Class object loop   
                    foreach (Task task in rootTask.task)
                    {
                        if (insertSQL == 0)
                        {
                            // TEST >>
                            Console.WriteLine("id:" + task.id);
                            Console.WriteLine("localid:" + task.localid);
                            Console.WriteLine("title:" + task.title);
                            Console.WriteLine("project:" + task.project);
                            Console.WriteLine("client:" + task.client);
                            Console.WriteLine("status:" + task.status);
                            // TEST <<
                        }

                        if (insertSQL == 1)
                        {
                            string connectionString = @"Data Source = " + sqlServer + "; Initial Catalog = " + sqlDatabase + "; Integrated Security = True; ";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                // Open connection
                                connection.Open();

                                // Create the insert statement
                                string insertStatement =
                                    "INSERT INTO [int].[Task_Daily](" +
                                        "[id],[localid],[queueid],[assigneeid],[statusid],[projectid],[moduleid]," +
                                        "[title],[summary],[dateopen],[datedue],[dateclosed],[estimate],[ownerid],[priorityid]," +
                                        "[datemodified],[color],[priority],[severity],[status],[status_order],[project]," +
                                        "[clientid],[clientlocalid],[client],[milestoneid],[milestone],[module],[projectlocalid]," +
                                        "[assignees],[followers],[followerid],[owners],[billable],[unbillable],[actual]," +
                                        "[projectlabel],[projectlabel_order],[projectlabelid])" +
                                    "VALUES('" +
                                        task.id + "', '" + task.localid + "', '" + task.queueid + "', '" + task.assigneeid + "', '" + task.statusid + "', '" + task.projectid + "', '" + task.moduleid + "', '" +
                                        task.title.Replace("'", "''") + "', substring('" + task.summary.Replace("'", "''") + "',1,999), '" + task.dateopen + "', '" + task.datedue + "', '" + task.dateclosed + "', '" + task.estimate + "', '" + task.ownerid + "', '" + task.priorityid + "', '" +
                                        task.datemodified + "', '" + task.color + "', '" + task.priority + "', '" + task.severity + "', '" + task.status + "', '" + task.status_order + "', '" + task.project + "', '" +
                                        task.clientid + "', '" + task.clientlocalid + "', '" + task.client + "', '" + task.milestoneid + "', '" + task.milestone + "', '" + task.module + "', '" + task.projectlocalid + "', '" +
                                        task.assignees + "', '" + task.followers + "', '" + task.followerid + "', '" + task.owners + "', '" + task.billable + "', '" + task.unbillable + "', '" + task.actual + "', '" +
                                        task.projectlabel + "', '" + task.projectlabel_order + "', '" + task.projectlabelid +
                                        "')";


                                // Create a SqlCommand object with the insert statement and the SqlConnection object
                                SqlCommand command = new SqlCommand(insertStatement, connection);

                                // Execute the insert command
                                int rowsAffected = command.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }


            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(logFolder + "\\" + "ErrorLog " + currentDateTime + ".log"))
                {
                    sw.WriteLine(exception.ToString());
                }

            }
        }

        static void Interval_Client()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string logFolder = @"D:\VS\Intervals_API\Logs";
            string txtFile = @"D:\VS\Intervals_API\Txt\txt.json";
            string userAccessKey = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("5so7gyreizi:x"));
            string APIAddress = "https://api.myintervals.com/client/?limit=1000";
            string sqlServer = "MDCFAK01";
            string sqlDatabase = "Intervals_Stats";
            int insertSQL = 1;
            int writeFile = 0;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var json = "";

                using (WebClient wc = new WebClient())
                {
                    // Basic Auth
                    wc.Headers.Add("Accept", "application/json");
                    wc.Headers.Add(HttpRequestHeader.Authorization, "Basic " + userAccessKey);
                    json = wc.DownloadString(APIAddress);


                    // Json to txt file                   
                    if (writeFile == 1)
                    {
                        File.WriteAllText(txtFile, json);
                    }

                    // Json to class object
                    var jsonNullHandling = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    RootClient rootClient = JsonConvert.DeserializeObject<RootClient>(json, jsonNullHandling);


                    // Class object loop   
                    foreach (Client client in rootClient.client)
                    {
                        if (insertSQL == 0)
                        {
                            // TEST >>
                            Console.WriteLine("id:" + client.id);
                            Console.WriteLine("name:" + client.name);
                            // TEST <<
                        }

                        if (insertSQL == 1)
                        {
                            string connectionString = @"Data Source = " + sqlServer + "; Initial Catalog = " + sqlDatabase + "; Integrated Security = True; ";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                // Open connection
                                connection.Open();

                                // Create the insert statement
                                string insertStatement =
                                    "INSERT INTO [int].[Client](" +
                                        "[id],[name],[active],[localidunpadded],[localid]" +
                                        ")" +
                                    "VALUES('" +
                                        client.id + "', '" + client.name.Replace("'", "''") + "', '" + client.active + "', '" + client.localidunpadded + "', '" + client.localid +
                                        "')";


                                // Create a SqlCommand object with the insert statement and the SqlConnection object
                                SqlCommand command = new SqlCommand(insertStatement, connection);

                                // Execute the insert command
                                int rowsAffected = command.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }


            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(logFolder + "\\" + "ErrorLog " + currentDateTime + ".log"))
                {
                    sw.WriteLine(exception.ToString());
                }

            }

        }

        static void Interval_Module()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string logFolder = @"D:\VS\Intervals_API\Logs";
            string txtFile = @"D:\VS\Intervals_API\Txt\txt.json";
            string userAccessKey = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("2dudc9px45t:x"));
            string APIAddress = "https://api.myintervals.com/module/?limit=100";
            string sqlServer = "MDCFAK01";
            string sqlDatabase = "Intervals_Stats";
            int insertSQL = 1;
            int writeFile = 0;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var json = "";

                using (WebClient wc = new WebClient())
                {
                    // Basic Auth
                    wc.Headers.Add("Accept", "application/json");
                    wc.Headers.Add(HttpRequestHeader.Authorization, "Basic " + userAccessKey);
                    json = wc.DownloadString(APIAddress);


                    // Json to txt file                   
                    if (writeFile == 1)
                    {
                        File.WriteAllText(txtFile, json);
                    }

                    // Json to class object
                    var jsonNullHandling = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    RootModule rootModule = JsonConvert.DeserializeObject<RootModule>(json, jsonNullHandling);


                    // Class object loop   
                    foreach (Module module in rootModule.module)
                    {
                        if (insertSQL == 0)
                        {
                            // TEST >>
                            Console.WriteLine("id:" + module.id);
                            Console.WriteLine("name:" + module.name);
                            Console.WriteLine("description:" + module.description);
                            // TEST <<
                        }

                        if (insertSQL == 1)
                        {
                            string connectionString = @"Data Source = " + sqlServer + "; Initial Catalog = " + sqlDatabase + "; Integrated Security = True; ";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                // Open connection
                                connection.Open();

                                // Create the insert statement
                                string insertStatement =
                                    "INSERT INTO [int].[Module](" +
                                        "[id],[name],[description],[active]" +
                                        ")" +
                                    "VALUES('" +
                                        module.id + "', '" + module.name.Replace("'", "''") + "', '" + module.description + "', '" + module.active +
                                        "')";


                                // Create a SqlCommand object with the insert statement and the SqlConnection object
                                SqlCommand command = new SqlCommand(insertStatement, connection);

                                // Execute the insert command
                                int rowsAffected = command.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }

            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(logFolder + "\\" + "ErrorLog " + currentDateTime + ".log"))
                {
                    sw.WriteLine(exception.ToString());
                }

            }

        }

        static void Interval_Priority()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string logFolder = @"D:\VS\Intervals_API\Logs";
            string txtFile = @"D:\VS\Intervals_API\Txt\txt.json";
            string userAccessKey = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("2dudc9px45t:x"));
            string APIAddress = "https://api.myintervals.com/taskpriority/";
            string sqlServer = "MDCFAK01";
            string sqlDatabase = "Intervals_Stats";
            int insertSQL = 1;
            int writeFile = 0;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var json = "";

                using (WebClient wc = new WebClient())
                {
                    // Basic Auth
                    wc.Headers.Add("Accept", "application/json");
                    wc.Headers.Add(HttpRequestHeader.Authorization, "Basic " + userAccessKey);
                    json = wc.DownloadString(APIAddress);


                    // Json to txt file                   
                    if (writeFile == 1)
                    {
                        File.WriteAllText(txtFile, json);
                    }

                    // Json to class object
                    var jsonNullHandling = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    RootPriority rootPriority = JsonConvert.DeserializeObject<RootPriority>(json, jsonNullHandling);


                    // Class object loop   
                    foreach (Priority priority in rootPriority.taskPriority)
                    {
                        if (insertSQL == 0)
                        {
                            // TEST >>
                            Console.WriteLine("id:" + priority.id);
                            Console.WriteLine("name:" + priority.name);
                            Console.WriteLine("priority" + priority.priority);
                            // TEST <<
                        }

                        if (insertSQL == 1)
                        {
                            string connectionString = @"Data Source = " + sqlServer + "; Initial Catalog = " + sqlDatabase + "; Integrated Security = True; ";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                // Open connection
                                connection.Open();

                                // Create the insert statement
                                string insertStatement =
                                    "INSERT INTO [int].[Priority](" +
                                        "[id],[name],[color],[priority],[isdefault],[active]" +
                                        ")" +
                                    "VALUES('" +
                                        priority.id + "', '" + priority.name.Replace("'", "''") + "', '" + priority.color + "', '" + priority.priority + "', '" + priority.isdefault + "', '" + priority.active +
                                        "')";


                                // Create a SqlCommand object with the insert statement and the SqlConnection object
                                SqlCommand command = new SqlCommand(insertStatement, connection);

                                // Execute the insert command
                                int rowsAffected = command.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }


            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(logFolder + "\\" + "ErrorLog " + currentDateTime + ".log"))
                {
                    sw.WriteLine(exception.ToString());
                }

            }

        }

        static void Interval_Status()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string logFolder = @"D:\VS\Intervals_API\Logs";
            string txtFile = @"D:\VS\Intervals_API\Txt\txt.json";
            string userAccessKey = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("2dudc9px45t:x"));
            string APIAddress = "https://api.myintervals.com/taskstatus/?limit=100";
            string sqlServer = "MDCFAK01";
            string sqlDatabase = "Intervals_Stats";
            int insertSQL = 1;
            int writeFile = 0;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var json = "";

                using (WebClient wc = new WebClient())
                {
                    // Basic Auth
                    wc.Headers.Add("Accept", "application/json");
                    wc.Headers.Add(HttpRequestHeader.Authorization, "Basic " + userAccessKey);
                    json = wc.DownloadString(APIAddress);


                    // Json to txt file                   
                    if (writeFile == 1)
                    {
                        File.WriteAllText(txtFile, json);
                    }

                    // Json to class object
                    var jsonNullHandling = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    RootStatus rootStatus = JsonConvert.DeserializeObject<RootStatus>(json, jsonNullHandling);


                    // Class object loop   
                    foreach (Status status in rootStatus.taskStatus)
                    {
                        if (insertSQL == 0)
                        {
                            // TEST >>
                            Console.WriteLine("id:" + status.id);
                            Console.WriteLine("name:" + status.name);
                            // TEST <<
                        }

                        if (insertSQL == 1)
                        {
                            string connectionString = @"Data Source = " + sqlServer + "; Initial Catalog = " + sqlDatabase + "; Integrated Security = True; ";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                // Open connection
                                connection.Open();

                                // Create the insert statement
                                string insertStatement =
                                    "INSERT INTO [int].[Status](" +
                                        "[id],[name],[Priority],[frozen],[active]" +
                                        ")" +
                                    "VALUES('" +
                                        status.id + "', '" + status.name.Replace("'", "''") + "', '" + status.priority + "', '" + status.frozen + "', '" + status.active +
                                        "')";


                                // Create a SqlCommand object with the insert statement and the SqlConnection object
                                SqlCommand command = new SqlCommand(insertStatement, connection);

                                // Execute the insert command
                                int rowsAffected = command.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }

            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(logFolder + "\\" + "ErrorLog " + currentDateTime + ".log"))
                {
                    sw.WriteLine(exception.ToString());
                }

            }

        }

        static void Interval_Project()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string logFolder = @"D:\VS\Intervals_API\Logs";
            string txtFile = @"D:\VS\Intervals_API\Txt\txt.json";
            string userAccessKey = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("2dudc9px45t:x"));
            string APIAddress = "https://api.myintervals.com/project/?limit=70000";
            string sqlServer = "MDCFAK01";
            string sqlDatabase = "Intervals_Stats";
            int insertSQL = 1;
            int writeFile = 0;

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var json = "";

                using (WebClient wc = new WebClient())
                {
                    // Basic Auth
                    wc.Headers.Add("Accept", "application/json");
                    wc.Headers.Add(HttpRequestHeader.Authorization, "Basic " + userAccessKey);
                    json = wc.DownloadString(APIAddress);


                    // Json to txt file                   
                    if (writeFile == 1)
                    {
                        File.WriteAllText(txtFile, json);
                    }

                    // Json to class object
                    var jsonNullHandling = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    RootProject rootProject = JsonConvert.DeserializeObject<RootProject>(json, jsonNullHandling);


                    // Class object loop   
                    foreach (Project project in rootProject.project)
                    {
                        if (insertSQL == 0)
                        {
                            // TEST >>
                            Console.WriteLine("id:" + project.id);
                            Console.WriteLine("name:" + project.name);
                            // TEST <<
                        }

                        if (insertSQL == 1)
                        {
                            string connectionString = @"Data Source = " + sqlServer + "; Initial Catalog = " + sqlDatabase + "; Integrated Security = True; ";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                // Open connection
                                connection.Open();

                                // Create the insert statement
                                string insertStatement =
                                     "INSERT INTO [int].[Project](" +
                                        "[id],[name],[datestart],[dateend]," +
                                        "[clientid],[client],[clientlocalid],[labelid]," +
                                        "[label],[localid],[manager],[managerid]" +
                                        ")" +
                                    "VALUES('" +
                                        project.id + "', '" + project.name.Replace("'", "''") + "', '" + project.datestart + "', '" + project.dateend + "', '" +
                                        project.clientid + "', '" + project.client + "', '" + project.clientlocalid + "', '" + project.labelid + "', '" +
                                        project.label + "', '" + project.localid + "', '" + project.manager + "', '" + project.managerid + "')";


                                // Create a SqlCommand object with the insert statement and the SqlConnection object
                                SqlCommand command = new SqlCommand(insertStatement, connection);

                                // Execute the insert command
                                int rowsAffected = command.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }

            catch (Exception exception)
            {
                using (StreamWriter sw = File.CreateText(logFolder + "\\" + "ErrorLog " + currentDateTime + ".log"))
                {
                    sw.WriteLine(exception.ToString());
                }

            }
        }
    }

}
