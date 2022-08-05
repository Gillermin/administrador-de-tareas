using Newtonsoft.Json;
using System.Net;
using WebUI.Models;

namespace WebUI.Services
{
    public class ApiServices : IApiServices
    {
        private static string _baseUrl;

        public ApiServices() {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //Obtenemos la baseUrl de la configuración
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public List<string> GetTasksFiltred(Tasks tasks) {
            //Asignamos el url el Api que devuelve las Tareas filtradas
            string url = $"{_baseUrl}/api/Task/Filter";

            List<string> result = new List<string>();
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //Obtiene el Json que requiere el API para filtrar los datos
                    string json = createJsonFilter(tasks);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result.Add("1");
                    result.Add(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                result.Add("0");
                result.Add(e.Message);
            }

            return result;
        }
        //Llena la clase Filter, y la serializa en un objeto Json para ser usado en el body del metodo POST
        public string createJsonFilter(Tasks tasks)
        {
            Filter filter = new Filter();
            filter.IdCollaborator = tasks.IdCollaborator;
            filter.Status = tasks.Status;
            filter.Priority = tasks.Priority;
            filter.FromDate = tasks.FromDate.ToString("yyyy-MM-dd");
            filter.ToDate = tasks.ToDate.ToString("yyyy-MM-dd");
            return JsonConvert.SerializeObject(filter);
        }
        public List<string> GetTask(int id)
        {
            //Asignamos el url el Api que devuelve la Tarea correspondiente al id
            string url = $"{_baseUrl}/api/Task/{id}";

            List<string> result = new List<string>();
            try
            {

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result.Add("1");
                    result.Add(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                result.Add("0");
                result.Add(e.Message);
            }

            return result;
        }

        public List<string> GetAllTasks()
        {
            //Asignamos el url el Api que devuelve todos las Tareas
            string url = $"{_baseUrl}/api/Task";

            List<string> result = new List<string>();
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result.Add("1");
                    result.Add(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                result.Add("0");
                result.Add(e.Message);
            }

            return result;
        }


        public List<string> getCollaborators()
        {   
            //Asignamos el url el Api que devuelve los colaboradores
            string url = $"{_baseUrl}/api/Task/GetCollaborators";

            List<string> result = new List<string>();
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "GET";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result.Add("1");
                    result.Add(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                result.Add("0");
                result.Add(e.Message);
            }

            return result;
        }

        public List<string> InsertTask(Tasks tasks)
        {
            //Asignamos el url el Api que inserta Tareas
            string url = $"{_baseUrl}/api/Task";

            List<string> result = new List<string>();
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //Obtiene el Json que requiere el API para insertar los datos
                    string json = createJsonInsert(tasks);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result.Add("1");
                    result.Add(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                result.Add("0");
                result.Add(e.Message);
            }

            return result;
        }
        //Llena la clase TasksNew, y la serializa en un objeto Json para ser usado en el body del metodo POST
        public string createJsonInsert(Tasks tasks)
        {
            TaskNew taskNew = new TaskNew();
            taskNew.IdTask = tasks.IdTask;
            taskNew.IdCollaborator = tasks.IdCollaborator;
            taskNew.Description = tasks.Description;
            taskNew.Status = tasks.Status;
            taskNew.Priority = tasks.Priority;
            taskNew.FromDate = tasks.FromDate.ToString("yyyy-MM-dd");
            taskNew.ToDate = tasks.ToDate.ToString("yyyy-MM-dd");
            taskNew.Notes = tasks.Notes;
            return JsonConvert.SerializeObject(taskNew);
        }
        public List<string> EditTask(Tasks tasks)
        {
            //Asignamos el url el Api que edita Tareas
            string url = $"{_baseUrl}/api/Task";

            List<string> result = new List<string>();
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //Obtiene el Json que requiere el API para insertar los datos
                    string json = createJsonInsert(tasks);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result.Add("1");
                    result.Add(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                result.Add("0");
                result.Add(e.Message);
            }

            return result;
        }
        
        public List<string> DeleteTask(int id)
        {
            //Asignamos el url el Api que edita Tareas
            string url = $"{_baseUrl}/api/Task?id={id}";

            List<string> result = new List<string>();
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "DELETE";

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result.Add("1");
                    result.Add(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                result.Add("0");
                result.Add(e.Message);
            }

            return result;
        }
    }
}
