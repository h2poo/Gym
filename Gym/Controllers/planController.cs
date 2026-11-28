using Gym.Entities.Enums;
using Gym.Repositeries.Interfaces;
using Gym.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepository;
        private readonly ILogger<PlanController> _logger;

        public PlanController(IPlanRepository planRepository, ILogger<PlanController> logger)
        {
            _planRepository = planRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _planRepository.GetAllAsync();

            var model = plans.Select(p => new PlanListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive
            }).ToList();

            ViewData["Title"] = "Plans";
            ViewBag.TotalPlans = model.Count;
            return View(model);
        }

        // GET: /Plan/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var plan = await _planRepository.GetByIdAsync(id);
            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }

            var blockedMessage = await GetEditBlockedMessageAsync(plan);
            if (blockedMessage is not null)
            {
                TempData["ErrorMessage"] = blockedMessage;
                return RedirectToAction(nameof(Index));
            }

            var model = new PlanEditViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                Description = plan.Description
            };

            SetEditViewData(plan.Name);
            return View(model);
        }

        // POST: /Plan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanEditViewModel model)
        {
            if (id <= 0 || id != model.Id)
            {
                return BadRequest();
            }

            var plan = await _planRepository.GetByIdAsync(id);
            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }

            var blockedMessage = await GetEditBlockedMessageAsync(plan);
            if (blockedMessage is not null)
            {
                TempData["ErrorMessage"] = blockedMessage;
                return RedirectToAction(nameof(Index));
            }

            // Name is read-only: always take it from the database, never from the posted form.
            model.Name = plan.Name;

            if (!ModelState.IsValid)
            {
                SetEditViewData(plan.Name);
                return View(model);
            }

            // Only the allowed fields are updated.
            plan.Price = model.Price!.Value;
            plan.DurationDays = model.DurationDays!.Value;
            plan.Description = string.IsNullOrWhiteSpace(model.Description) ? null : model.Description.Trim();

            try
            {
                await _planRepository.UpdateAsync(plan);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to update plan {PlanId}", id);
                ModelState.AddModelError(string.Empty, "Could not save the plan. Please try again.");
                SetEditViewData(plan.Name);
                return View(model);
            }

            // PRG: redirect after a successful POST, the toast is shown on the next GET.
            TempData["SuccessMessage"] = $"Plan \"{plan.Name}\" was updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<string?> GetEditBlockedMessageAsync(Plan plan)
        {
            if (!plan.IsActive)
            {
                return "Inactive plans can't be edited.";
            }

            if (await _planRepository.HasActiveOrUpcomingMembershipsAsync(plan.Id))
            {
                return "This plan has active or upcoming memberships and can't be edited.";
            }

            return null;
        }

        private void SetEditViewData(string planName)
        {
            ViewData["Title"] = "Edit plan";
            ViewBag.PlanName = planName;
        }
    }
}
