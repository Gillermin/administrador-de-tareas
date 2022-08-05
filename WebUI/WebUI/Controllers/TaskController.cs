using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers
{
    
    public class TaskController : Controller
    {
        private ApiServices _apiServices = new ApiServices();
        private string _errorMessage;
        private readonly ILogger<TaskController> _logger;
        
        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Collaborator = FillCollaborators(true);
            ViewBag.Priorities = FillPriorities(true);
            ViewBag.Status = FillStatus(true);
            
            List<string> result = _apiServices.GetAllTasks();

            if (result[0] == "1")
            {
                var tasksResults = JsonConvert.DeserializeObject<IEnumerable<Tasks>>(result[1]);
                ViewBag.TasksResults = tasksResults;
                return View();
            }
            else {
                ViewBag.ErrorMessage = result[1];
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(Tasks tasks)
        {
            //Llena objetos, para crear los select de la vista | (allOption = true) agrega la opcion "TODOS"
            ViewBag.Collaborator = FillCollaborators(true);
            ViewBag.Priorities = FillPriorities(true);
            ViewBag.Status = FillStatus(true);
            
            //Se quitan del ModelState porque se estan en el modelo, pero para la busqueda no son necesarios
            this.ModelState.Remove("IdCollaborator");
            this.ModelState.Remove("Description");
            //Se valida que la fecha final sea mayor a la inicial, sino agregamos la validacion.
            if (tasks.ToDate < tasks.FromDate) {
                this.ModelState.AddModelError("ToDate","Debe ser mayor o igual a la fecha inicial");
            }
            //Cambiar el mensaje de error si la fecha es vacia
            if (tasks.ToDate.ToString() == "1/1/0001 12:00:00 am") {
                this.ModelState.Remove("ToDate");
                this.ModelState.AddModelError("ToDate", "Fecha requerida!");
            }
            if (tasks.FromDate.ToString() == "1/1/0001 12:00:00 am")
            {
                this.ModelState.Remove("FromDate");
                this.ModelState.AddModelError("FromDate", "Fecha requerida!");
            }
            //Verificar si se cumplieron las validaciones y no hay errores
            if (this.ModelState.IsValid)
            {
                List<string> result = _apiServices.GetTasksFiltred(tasks);

                if (result[0] == "1")
                {
                    var tasksResults = JsonConvert.DeserializeObject<IEnumerable<Tasks>>(result[1]);
                    ViewBag.TasksResults = tasksResults;
                    return View(tasks);
                }
                else
                {
                    ViewBag.ErrorMessage = result[1];
                    return View(tasks);
                }
            }
            else
            {
                List<string> result = _apiServices.GetAllTasks();
                var tasksResults = JsonConvert.DeserializeObject<IEnumerable<Tasks>>(result[1]);
                ViewBag.TasksResults = tasksResults;
                return View();
            }
        }

        public IActionResult Insert()
        {
            ViewBag.Collaborator = FillCollaborators(false,true);
            ViewBag.Priorities = FillPriorities();
            ViewBag.Status = FillStatus();
            ViewBag.ErrorMessage = _errorMessage;
            return View();
        }
        [HttpPost]
        public IActionResult Insert(Tasks insert)
        {
            //Remover del modelState IdCollaborador para no tomar el cuenta la validadacion de requerido si su estado es "PENDIENTE"
            if (insert.Status == "PENDIENTE") {
                this.ModelState.Remove("IdCollaborator");
            }
            ViewBag.Collaborator = FillCollaborators(false, true);
            ViewBag.Priorities = FillPriorities();
            ViewBag.Status = FillStatus();
            //Se valida que la fecha final sea mayor a la inicial, sino agregamos la validacion.
            if (insert.ToDate < insert.FromDate)
            {
                this.ModelState.AddModelError("ToDate", "Debe ser mayor o igual a la fecha inicial");
            }
            //Cambiar el mensaje de error si la fecha es vacia
            if (insert.ToDate.ToString() == "1/1/0001 12:00:00 am")
            {
                this.ModelState.Remove("ToDate");
                this.ModelState.AddModelError("ToDate", "Fecha requerida!");
            }
            if (insert.FromDate.ToString() == "1/1/0001 12:00:00 am")
            {
                this.ModelState.Remove("FromDate");
                this.ModelState.AddModelError("FromDate", "Fecha requerida!");
            }

            if (this.ModelState.IsValid)
            {
                List<string> result = _apiServices.InsertTask(insert);

                if (result[0] == "1")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = result[1];
                    return View(insert);
                }
            }
            else
            {
                return View(insert);
            }
            
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Collaborator = FillCollaborators(false, true);
            ViewBag.Priorities = FillPriorities();
            ViewBag.Status = FillStatus();

            List<string> result = _apiServices.GetTask(id);

            if (result[0] == "1")
            {
                var tasksResult = JsonConvert.DeserializeObject<Tasks>(result[1]);
                return View(tasksResult);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(Tasks edit)
        {
            //Remover del modelState IdCollaborador para no tomar el cuenta la validadacion de requerido si su estado es "PENDIENTE"
            if (edit.Status == "PENDIENTE")
            {
                this.ModelState.Remove("IdCollaborator");
            }
            ViewBag.Collaborator = FillCollaborators(false, true);
            ViewBag.Priorities = FillPriorities();
            ViewBag.Status = FillStatus();
            //Se valida que la fecha final sea mayor a la inicial, sino agregamos la validacion.
            if (edit.ToDate < edit.FromDate)
            {
                this.ModelState.AddModelError("ToDate", "Debe ser mayor o igual a la fecha inicial");
            }
            //Cambiar el mensaje de error si la fecha es vacia
            if (edit.ToDate.ToString() == "1/1/0001 12:00:00 am")
            {
                this.ModelState.Remove("ToDate");
                this.ModelState.AddModelError("ToDate", "Fecha requerida!");
            }
            if (edit.FromDate.ToString() == "1/1/0001 12:00:00 am")
            {
                this.ModelState.Remove("FromDate");
                this.ModelState.AddModelError("FromDate", "Fecha requerida!");
            }

            if (this.ModelState.IsValid)
            {
                List<string> result = _apiServices.EditTask(edit);

                if (result[0] == "1")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = result[1];
                    return View(edit);
                }
            }
            else
            {
                return View(edit);
            }
        }
        public IActionResult Details(int id)
        {
            ViewBag.Collaborator = FillCollaborators(false, true);
            ViewBag.Priorities = FillPriorities();
            ViewBag.Status = FillStatus();

            List<string> result = _apiServices.GetTask(id);

            if (result[0] == "1")
            {
                var tasksResult = JsonConvert.DeserializeObject<Tasks>(result[1]);
                ViewBag.TasksResult = tasksResult;
                return View(tasksResult);
            }
            else
            {
                ViewBag.ErrorMessage = result[1];
                return View();
            }
        }
   
        public IActionResult Delete(int id)
        {
            
            ViewBag.IdTask = id;
            List<string> result = _apiServices.DeleteTask(id);

            if (result[0] == "1")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = result[1];
                return View();
            }
        }

        //Llena la clase Priority, para pasarle los datos del select a la vista
        public List<Priority> FillPriorities(bool allOption=false ) 
        {
            List<Priority> priorityList = new List<Priority>();
            Priority priority = new Priority();
            if (allOption)
            {
                priority.IdPriority = 0;
                priority.PriorityData = "TODOS";
                priorityList.Add(priority);
            }
            priority = new Priority();
            priority.IdPriority = 1;
            priority.PriorityData = "BAJA";
            priorityList.Add(priority);
            priority = new Priority();
            priority.IdPriority = 2;
            priority.PriorityData = "MEDIA";
            priorityList.Add(priority);
            priority = new Priority();
            priority.IdPriority = 3;
            priority.PriorityData = "ALTA";
            priorityList.Add(priority);
            return priorityList;
        }

        //Llena la clase Status, para pasarle los datos del select a la vista
        public List<Status> FillStatus(bool allOption = false)
        {
            List<Status> statusList = new List<Status>();
            Status status = new Status();
            if (allOption)
            {
                status.IdStatus = 0;
                status.StatusData = "TODOS";
                statusList.Add(status);
            }
            status = new Status();
            status.IdStatus = 1;
            status.StatusData = "PENDIENTE";
            statusList.Add(status);
            status = new Status();
            status.IdStatus = 2;
            status.StatusData = "EN PROCESO";
            statusList.Add(status);
            status = new Status();
            status.IdStatus = 3;
            status.StatusData = "FINALIZADA";
            statusList.Add(status);
            return statusList;
        }

        //Llena la clase Collaborator, para pasarle los datos del select a la vista (los obtiene mediante el api)
        public List<Collaborator> FillCollaborators(bool allOption = false, bool unSelectOption = false)
        {
            List<string> result = _apiServices.getCollaborators();
            List<Collaborator> collaborators = new List<Collaborator>();
            
            if (result[0] == "1")
            { 
                var collaboratorResults = JsonConvert.DeserializeObject<IEnumerable<Collaborator>>(result[1]);
                Collaborator collaborator = new Collaborator();

                if (allOption || unSelectOption)
                {
                    collaborator.IdCollaborator = 0;
                    collaborator.CollaboratorName = unSelectOption? "Seleccione un colaborador":"TODOS";
                    collaborators.Add(collaborator);
                }
                foreach (var item in collaboratorResults) {
                    collaborators.Add(item);
                }
          
                return collaborators;
            }
            else
            {
                _errorMessage = result[1];
                return collaborators;
            }
        }

        

    }
}