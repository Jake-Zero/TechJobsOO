using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            Job thisJob = jobData.Find(id);
            
            return View(thisJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                int EmployerID = newJobViewModel.EmployerID;
                int LocationID = newJobViewModel.LocationID;
                int CoreCompetencyID = newJobViewModel.CoreCompencyID;
                int PositionTypeID = newJobViewModel.PositionTypeID;

                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(EmployerID),
                    Location = jobData.Locations.Find(LocationID),
                    CoreCompetency = jobData.CoreCompetencies.Find(CoreCompetencyID),
                    PositionType = jobData.PositionTypes.Find(PositionTypeID)
                };
                jobData.Jobs.Add(newJob);
                return Redirect("/Job?id=" + newJob.ID);
            }
            return View(newJobViewModel);
        }
    }
}
