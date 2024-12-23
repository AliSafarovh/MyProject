﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TraversalProject.CQRS.Commands.GuideCommands;
using TraversalProject.CQRS.Queries.GuideQueries;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuideMediatRController : Controller
    {
        private readonly IMediator _mediator;

        public GuideMediatRController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        public async Task <IActionResult> Index()
        {
            var values = await _mediator.Send(new GetAllGuideQuery());
           
            return View(values);
        }

        public async Task<IActionResult> GetGuides(int id)
        {
            var values=await _mediator.Send(new GetGuideByIDQuery(id));
            return View(values);
        }

        [HttpGet]
        public IActionResult AddGuide()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGuide(CreateGuideCommand command)
        {
            await _mediator.Send(command);
            return Redirect("/Admin/Guide/Index");

        }
    }
}
