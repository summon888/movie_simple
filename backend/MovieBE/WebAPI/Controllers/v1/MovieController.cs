using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Identity.Authorization;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class MovieController : ApiController
    {
        private readonly IMovieAppService _movieAppService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MovieController(
            UserManager<ApplicationUser> userManager,
            IMovieAppService movieAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
            : base(notifications, mediator)
        {
            _userManager = userManager;
            _movieAppService = movieAppService;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("movie-management/pagination_test")]
        //public IActionResult PaginationTest(int skip, int take)
        //{
        //    return Response(_movieAppService.GetAll(skip, take));
        //}

        [HttpPost]
        [Authorize(Policy = "CanWriteCustomerData", Roles = Roles.Admin)]
        [Route("movie-management")]
        public IActionResult Post([FromBody] MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(movieViewModel);
            }

            _movieAppService.Add(movieViewModel);

            return Response(movieViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteCustomerData", Roles = Roles.Admin)]
        [Route("movie-management/like")]
        public async Task<IActionResult> Post([FromBody] MovieLikeViewModel movieLikeViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(movieLikeViewModel);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            movieLikeViewModel.CustomerId = user.CustomerId;
            _movieAppService.Like(movieLikeViewModel);

            return Response(movieLikeViewModel);
        }

        [HttpGet]
        [Route("movie-management/pagination")]
        public async Task<IActionResult> Pagination(int skip, int take)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                NotifyError("Movie", "User does not exist");
                return Response();
            }

            return Response(_movieAppService.GetAll(user.CustomerId, skip, take));
        }
    }
}
