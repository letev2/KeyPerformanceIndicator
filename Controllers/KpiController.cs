using KpiNew.Dto;
using KpiNew.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KpiNew.Controllers
{
    public class KpiController : Controller
    {
        private readonly IKpiService _kpiService;
        public KpiController(IKpiService kpiService)
        {
            _kpiService = kpiService;
        }

        public async Task<IActionResult> Index()
        {
          var kpis = await _kpiService.GetAllKpi();
            return View(kpis.Data);
        }
        
      
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateKpiRequestModel model)
        {
             await _kpiService.AddKpi(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var kpi = await _kpiService.GetKpiById(id);
            if (kpi.Data == null)
            {
                return ViewBag.Message("Kpi not found");
            }
            return View(kpi.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var kpi = await _kpiService.GetKpiById(id);
            if (kpi == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateKpiRequestModel model)
        {
            await _kpiService.UpdateKpi(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {

            var kpi = await _kpiService.GetKpiById(id);
            if (kpi == null)
            {
                return NotFound();
            }
            return View(kpi.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _kpiService.DeleteKpi(id);
            return RedirectToAction("Index", "Kpi");
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeKpiList(int id)
        {
           var kpi =  await _kpiService.GetEmployeeKpiList(id);
            return View(kpi.Data);
        }
    }
}
